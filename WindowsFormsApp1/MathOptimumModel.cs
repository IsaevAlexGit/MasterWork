using System;
using System.Linq;
using System.Collections.Generic;

namespace Optimum
{
    public class MathOptimumModel
    {
        // Пара - ID зоны, название свёртки
        public class Pair
        {
            public int ID;
            public string Convolution;

            public Pair() { }
            public Pair(int id, string con)
            {
                ID = id;
                Convolution = con;
            }
        }
        // Пара, которую возвращает каждая свертка - ID оптимума и название свертки
        private List<Pair> _pairInfoOptimum = new List<Pair>();

        // ПРИНИМАЕТ КЛАСС
        // Список буферных зон, который нормализуется (для изменения)
        private List<BufferZone> _listForNormalization = new List<BufferZone>();
        // Список буферных зон с карты (без изменения)
        private List<BufferZone> _listWithStartZones = new List<BufferZone>();
        // Список критериев оптимальности
        private readonly List<Criterion> _listCriterion = new List<Criterion>();

        // ВОЗВРАЩАЕТ КЛАСС
        // Список всех уникальных оптимумов, который возвращает класс
        private List<OptimalZone> _listAllOptimalZones = new List<OptimalZone>();

        // Утопическая точка
        private OptimalZone _UtopiaPoint = new OptimalZone();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_arrayBuffers">Все буферные зоны на карте</param>
        /// <param name="criterion">Все заданные пользоветелем критерии оптимальности</param>
        public MathOptimumModel(List<BufferZone> _arrayBuffers, List<Criterion> criterion)
        {
            _listForNormalization = _arrayBuffers;
            _listCriterion = criterion;
        }

        /// <summary>
        /// Получение оптимальной точки
        /// </summary>
        /// <returns>Оптимальная точка</returns>
        public List<OptimalZone> GetOptimum()
        {
            // Сохранить значения списка до нормализации
            _CloneValuesFromListNormalizationToListStartZones();
            // Максимизация всех критериев, которые минимизируются
            _MaximizationOfCriterion();
            // Поиск минимума и максимума для каждого критерия
            _SearchMaxMinForEachCriterion();
            // Нормализация
            _Normalization();

            // Линейная свертка
            _LinearConvolution();
            // Мультипликативная свертка
            _MultiplicativeConvolution();
            // Свертка агрегирования
            _AggregationConvolution();
            // Свертка Гермейера
            _HermeierConvolution();
            // Если имеет смысл вызвать метод главного критерия
            if (_checkIsMainMethod())
                // Метод главного критерия
                _MainCriterionMethod();

            // Найти Утопию
            _createUtopiaPoint();
            // Свертка на основе идеальной точки по метрике Чебышева
            _IdealPointChebyshevMetricConvolution();
            // Свертка на основе идеальной точки по метрике Хемминга
            _IdealPointHammingMetricConvolution();
            // Свертка на основе идеальной точки по метрике Евклида
            _IdealPointEuclideanMetricConvolution();

            // Вернуть массив оптимальных зон, где каждый объект это Оптимум (Буферная зона, свертки выбравшие ее оптимальной)
            return _getOptimalZonesDisionMaking();
        }

        /// <summary>
        /// Максимизация всех параметров
        /// </summary>
        private void _MaximizationOfCriterion()
        {
            // Для каждой буферной зоны надо за-максимизировать все критерии
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // В каждой зоне по 5 критериев
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Если критерий минимизируется надо максимизировать, то есть *(-1)
                    if (_listCriterion[j].direction == false)
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] = _listForNormalization[i].arrayValuesCriterionOnZone[j] * (-1);
                }
            }
        }

        // Для нормализации надо найти максимум и минимум по каждому критерию
        // Тут хранится минимум для каждого из Х критериев - Х минимумов по каждому критерию 
        private List<double> minimums = new List<double>();
        // Тут хранится максимум для каждого из Х критериев - Х максимумов по каждому критерию
        private List<double> maximums = new List<double>();

        /// <summary>
        /// Поиск максимума и минимума для каждого критерия
        /// </summary>
        private void _SearchMaxMinForEachCriterion()
        {
            // Идем по всем 5 критериям
            for (int i = 0; i < _listCriterion.Count; i++)
            {
                // Самое маленькое число Double
                double _max = double.MinValue;
                // Самое большое число Double
                double _min = double.MaxValue;

                // Идем по всем 10 зонам и ищем мин-макс для 0 критерия
                for (int j = 0; j < _listForNormalization.Count; j++)
                {
                    if (_listForNormalization[j].arrayValuesCriterionOnZone[i] >= _max)
                        _max = _listForNormalization[j].arrayValuesCriterionOnZone[i];
                    if (_listForNormalization[j].arrayValuesCriterionOnZone[i] <= _min)
                        _min = _listForNormalization[j].arrayValuesCriterionOnZone[i];
                }
                // Сохранить минимум и максимум
                minimums.Add(_min);
                maximums.Add(_max);
            }
        }

        /// <summary>
        /// Нормализация всех критериев
        /// </summary>
        private void _Normalization()
        {
            // Массив разностей по каждому критерию
            List<double> differences = new List<double>();

            // Разность между максимумом и минимумом для каждого критерия
            for (int i = 0; i < _listCriterion.Count; i++)
            {
                // Ищем разность между макс и мин у каждого критерия
                double differenceCriterion = maximums[i] - minimums[i];
                // Массив разностей по каждому критерию
                differences.Add(differenceCriterion);
            }

            // Нормализация: (текущее - минимум) / (максимум - минимум)
            // Идем по всем зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Нормализуем каждый критерий каждой зоны - идем по каждому критериям
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Для текущих нормализованных значений округлить результат до 7 знаков после запятой
                    if (differences[j] != 0)
                    {
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] = (_listForNormalization[i].arrayValuesCriterionOnZone[j] - minimums[j]) / differences[j];
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] = Math.Round(_listForNormalization[i].arrayValuesCriterionOnZone[j], 7);
                    }
                }
            }
        }

        /// <summary>
        /// Линейная свертка
        /// </summary>
        private void _LinearConvolution()
        {
            int _idBestPointByLinearConvolution = -1;
            // Максимум для линейной свертки
            double MaxLinearConvolution = -1;
            // Идем по 10 зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariable = 0;
                // Идем по 5 критериям - перемножая у зоны каждый ее критерий на соответствующий ему вес
                for (int j = 0; j < _listCriterion.Count; j++)
                    temporaryVariable = temporaryVariable + (_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion);


                if (temporaryVariable > MaxLinearConvolution)
                {
                    MaxLinearConvolution = temporaryVariable;
                    _idBestPointByLinearConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointByLinearConvolution, "Линейная свёртка"));
        }

        /// <summary>
        /// Мультипликативная свертка
        /// </summary>
        private void _MultiplicativeConvolution()
        {
            int _idBestPointByMultiplicativeConvolution = -1;
            // Максимум для мультипликативной свертки
            double MaxMultiplicativeConvolution = -1;
            double temporaryVariable = 1;
            // Идем по 10 зонам
            for (int i = 0; i < _listWithStartZones.Count; i++)
            {
                // Флаг входа в одну из веток
                bool flag = false;
                // Сбросили прошлое вычисление
                temporaryVariable = 1;

                // Идем по 5 критериям - перемножая у зоны каждый ее критерий на соответствующий ему вес
                for (int j = 0; j < _listCriterion.Count; j++)
                {
                    // Если критерий и значение не нулевые оба
                    if (_listCriterion[j].weightOfCriterion != 0 && _listWithStartZones[i].arrayValuesCriterionOnZone[j] != 0)
                    {
                        flag = true;
                        temporaryVariable = temporaryVariable * _listWithStartZones[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion;
                    }
                }

                // Если везде были нули и temporaryVariable не изменился
                if (temporaryVariable == 1 && flag == false)
                    temporaryVariable = 0;

                if (temporaryVariable > MaxMultiplicativeConvolution)
                {
                    MaxMultiplicativeConvolution = temporaryVariable;
                    _idBestPointByMultiplicativeConvolution = _listWithStartZones[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointByMultiplicativeConvolution, "Мультипликативная свёртка"));
        }

        /// <summary>
        /// Свертка агрегирования
        /// </summary>
        private void _AggregationConvolution()
        {
            int _idBestPointAggregationConvolution = -1;
            // Коэффициент агрегирования,  0 - нельзя, p->0 - мультипликативная, p=1 - линайная
            double _aggregationCoefficient = 2;

            // Максимум для агрегирования свертки
            double MaxAggregationeConvolution = -1;
            // Идем по 10 зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariable = 0;
                // Идем по 5 критериям - перемножая у зоны каждый ее критерий на соответствующий ему вес
                for (int j = 0; j < _listCriterion.Count; j++)
                {
                    // (вес*критерий) в степени коэффициента, все это суммируем в одну переменную - сумма произведений (веса на критерий) в степени коэффициента
                    temporaryVariable = temporaryVariable + Math.Pow(_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion, _aggregationCoefficient);
                }

                double div = 1 / _aggregationCoefficient;
                // Полученую сумму произведений возводим в степени 1/p
                temporaryVariable = Math.Pow(temporaryVariable, div);

                // Ищем максимальное значений
                if (temporaryVariable > MaxAggregationeConvolution)
                {
                    MaxAggregationeConvolution = temporaryVariable;
                    _idBestPointAggregationConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointAggregationConvolution, "Свёртка агрегирования"));
        }

        /// <summary>
        /// Свертка Гермейера
        /// </summary>
        private void _HermeierConvolution()
        {
            int _idBestPointHermeierConvolution = -1;

            double MaxHermeier = -1;
            // Идем по всем зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double MinHermeier = 10000;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    double temp = _listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion;
                    // Найти минимальное произведение из всех произведений критерия на вес у одной зоны
                    if (temp <= MinHermeier)
                        MinHermeier = temp;
                }
                // Найти максимальное произведение из всех самых минимальных
                if (MinHermeier > MaxHermeier)
                {
                    MaxHermeier = MinHermeier;
                    _idBestPointHermeierConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointHermeierConvolution, "Свёртка Гермейера"));
        }

        /// <summary>
        /// Проверка использования метода главного критерия
        /// </summary>
        /// <returns></returns>
        private bool _checkIsMainMethod()
        {
            // Если однокритериальный поиск
            if (_listCriterion.Count == 1)
                return true;
            else
            {
                // Пороговое значение, разница между двумя важными приоритетатми выше этого значения, то МГК используем
                double threshold = 0.3999;

                List<double> weights = new List<double>();
                // Идем по всем критериям
                for (int i = 0; i < _listCriterion.Count; i++)
                    weights.Add(_listCriterion[i].weightOfCriterion);

                weights.Sort((x, y) => y.CompareTo(x));

                // Какая разница между самым важным критерием и критерием по важности на 2 месте
                double diff = weights[0] - weights[1];

                // Если разница больше 0.3999 значит самый важный критерий останется и мы берем только по нему наилучшую точку
                if (diff >= threshold)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Метод главного критерия
        /// </summary>
        private void _MainCriterionMethod()
        {
            int _idBestPointMainCriterionMethod = -1;

            double _maxCr = -1;
            int positionMaxCr = -1;
            // Идем по всем критериям - 0.1 0.6 0.2 0 0.1
            for (int i = 0; i < _listCriterion.Count; i++)
            {
                // Ищем самый приоритетный критерий и его позицию из всех критериев
                if (_listCriterion[i].weightOfCriterion >= _maxCr)
                {
                    _maxCr = _listCriterion[i].weightOfCriterion;
                    positionMaxCr = i;
                }
            }
            // Идем по всем значениям
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                if (_listForNormalization[i].arrayValuesCriterionOnZone[positionMaxCr] == 1)
                    _idBestPointMainCriterionMethod = _listForNormalization[i].idBufferZone;
            }

            _pairInfoOptimum.Add(new Pair(_idBestPointMainCriterionMethod, "Метод главного критерия"));
        }

        // Создать идеальную точку
        private void _createUtopiaPoint()
        {
            // Сколько критериев столько и 1
            List<double> odin = new List<double>();
            for (int i = 0; i < _listCriterion.Count; i++)
                odin.Add(1);

            BufferZone UtopiaBufferZone = new BufferZone(-1, 0, 0, _listForNormalization[0].lengthRadiusSearch, odin);

            // Утопия - идеальная точка, где частные критерии идеальны относительно каждого критерия относительно поставленных точек на карте
            _UtopiaPoint.namesConvolutiones = new List<string>();
            _UtopiaPoint.optimal = UtopiaBufferZone;
        }

        /// <summary>
        /// Свертка на основе идеальной точки по метрике Чебышева
        /// </summary>
        private void _IdealPointChebyshevMetricConvolution()
        {
            int _idBestPointIdealPointChebyshevMetricConvolution = -1;
            double MinChebyshev = 10000;

            // Идем по 10 точках
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double MaxChebyshev = -1;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Модуль разности (Утопический критерий - частный текущий критерий)
                    double diffUtopia = Math.Abs(_UtopiaPoint.optimal.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    //double diffUtopia2 = Math.Abs(1 - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    // Вес * модуль разности
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;

                    if (multi >= MaxChebyshev)
                        MaxChebyshev = multi;
                }

                if (MaxChebyshev < MinChebyshev)
                {
                    MinChebyshev = MaxChebyshev;
                    _idBestPointIdealPointChebyshevMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointIdealPointChebyshevMetricConvolution, "Свёртка на основе идеальной точки по метрике Чебышева"));
        }

        /// <summary>
        /// Свертка на основе идеальной точки по метрике Хемминг
        /// </summary>
        private void _IdealPointHammingMetricConvolution()
        {
            int _idBestPointIdealPointHammingMetricConvolution = -1;
            double MinHamming = 10000;

            // Идем по 10 точках
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Обнуление суммы для каждой точки
                double summ = 0;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Модуль разности (Утопический критерий - частный текущий критерий)
                    double diffUtopia = Math.Abs(_UtopiaPoint.optimal.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    // Вес * модуль разности
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;
                    summ = summ + multi;
                }
                if (summ < MinHamming)
                {
                    MinHamming = summ;
                    _idBestPointIdealPointHammingMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointIdealPointHammingMetricConvolution, "Свёртка на основе идеальной точки по метрике Хемминга"));
        }

        /// <summary>
        /// Свертка на основе идеальной точки по метрике Евклида
        /// </summary>
        private void _IdealPointEuclideanMetricConvolution()
        {
            int _idBestPointIdealPointEuclideanMetricConvolution = -1;
            double MinEuclidean = 10000;

            // Идем по 10 точках
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Обнуление суммы для каждой точки
                double summ = 0;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Квадрат разности (Утопический критерий - частный текущий критерий)
                    double diffUtopia = Math.Pow(_UtopiaPoint.optimal.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j], 2);
                    // Вес * квадрат разности
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;
                    summ = summ + multi;
                }
                if (summ < MinEuclidean)
                {
                    MinEuclidean = summ;
                    _idBestPointIdealPointEuclideanMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(_idBestPointIdealPointEuclideanMetricConvolution, "Свёртка на основе идеальной точки по метрике Евклида"));
        }

        // Из списка уникальных ID оптимумов надо вернуть список оптимальных точек
        private List<OptimalZone> _getOptimalZonesDisionMaking()
        {
            // Все ID всех выбранных каждой сверткой оптимумов
            List<int> ID = new List<int>();
            for (int i = 0; i < _pairInfoOptimum.Count; i++)
                ID.Add(_pairInfoOptimum[i].ID);

            // Уникальные ID оптимумов
            var UniqueID = ID.Distinct();

            // Оптимальные уникальные зоны
            _listAllOptimalZones = new List<OptimalZone>();

            // Идем по всему списку установленных точек
            for (int i = 0; i < _listWithStartZones.Count; i++)
            {
                // Идем по всему списку уникальных ID
                for (int j = 0; j < UniqueID.Count(); j++)
                {
                    if (_listWithStartZones[i].idBufferZone == UniqueID.ElementAt(j))
                    {
                        List<string> str = new List<string>();
                        _listAllOptimalZones.Add(new OptimalZone(_listWithStartZones[i], str));
                    }
                }
            }

            // Идем по всем оптимумам
            for (int i = 0; i < _listAllOptimalZones.Count; i++)
            {
                for (int j = 0; j < _pairInfoOptimum.Count; j++)
                {
                    // Если ID оптимума совпал с ID в парах
                    if (_listAllOptimalZones[i].optimal.idBufferZone == _pairInfoOptimum[j].ID)
                        _listAllOptimalZones[i].namesConvolutiones.Add(_pairInfoOptimum[j].Convolution);
                }
            }

            return _listAllOptimalZones;
        }

        /// <summary>
        /// Клонирование буферных зон
        /// </summary>
        private void _CloneValuesFromListNormalizationToListStartZones()
        {
            // Перенести все значения из списка буферных зон для нормализации в список стартовых буферных зон
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                BufferZone zone = (BufferZone)_listForNormalization[i].Clone();
                _listWithStartZones.Add(zone);
            }
        }
    }
}