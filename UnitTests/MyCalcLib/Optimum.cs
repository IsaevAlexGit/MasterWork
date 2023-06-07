using System;
using System.Linq;
using System.Collections.Generic;

namespace MyCalcLib
{
    // Класс "Оптимальная зона"
    public class OptimalZone
    {
        // Оптимальная буферная зона
        public BufferZone optimalZone { get; set; }
        // Список сверток, которые выбрали буферную зону оптимальной
        public List<string> namesConvolutiones { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public OptimalZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="OptimalZone">Оптимальная точка</param>
        /// <param name="Convolutiones">Названия сверток, которые проголосовали за эту оптимальную точку</param>
        public OptimalZone(BufferZone OptimalZone, List<string> Convolutiones)
        {
            optimalZone = OptimalZone;
            namesConvolutiones = Convolutiones;
        }
    }

    // ICloneable позволяет копировать список объектов
    // Класс "Частный критерий оптимальности"
    public class Criterion
    {
        // Название критерия оптимальности
        public string nameOfCriterion { get; set; }
        // Направление критерия
        // false - минимизация, true - максимизация
        public bool directionOfCriterion { get; set; }
        // Весовой коэффициент критерия оптимальности
        public double weightOfCriterion { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Criterion() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="NameOfCriterion">Название критерия оптимальности</param>
        /// <param name="DirectionOfCriterion">Направление критерия</param>
        /// <param name="WeightOfCriterion">Весовой коэффициент</param>
        public Criterion(string NameOfCriterion, bool DirectionOfCriterion, double WeightOfCriterion)
        {
            nameOfCriterion = NameOfCriterion;
            directionOfCriterion = DirectionOfCriterion;
            weightOfCriterion = WeightOfCriterion;
        }

        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <returns>объект класса BufferZone</returns>
        public object Clone()
        {
            return new Criterion
            {
                nameOfCriterion = this.nameOfCriterion,
                directionOfCriterion = this.directionOfCriterion,
                weightOfCriterion = this.weightOfCriterion
            };
        }
    }

    // ICloneable позволяет копировать список объектов
    // Класс "Буферная зона"
    public class BufferZone : ICloneable
    {
        // Идентификатор буферной зоны
        public int idBufferZone { get; set; }
        // Координата Х буферной зоны
        public double x { get; set; }
        // Координата Y буферной зоны
        public double y { get; set; }
        // Радиус поиска буферной зоны
        public int lengthRadiusSearch { get; set; }
        // Массив значений частных критериев для буферной зоны
        public List<double> arrayValuesCriterionOnZone { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BufferZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="IdBufferZone">Идентификатор буферной зоны</param>
        /// <param name="X"> Координата Х</param>
        /// <param name="Y">Координата Y</param>
        /// <param name="LengthRadiusSearch">Радиус поиска буферной зоны</param>
        /// <param name="ArrayValuesCriterionOnZone">Массив значений частных критериев для буферной зоны</param>
        public BufferZone(int IdBufferZone, double X, double Y, int LengthRadiusSearch, List<double> ArrayValuesCriterionOnZone)
        {
            idBufferZone = IdBufferZone;
            x = X;
            y = Y;
            lengthRadiusSearch = LengthRadiusSearch;
            arrayValuesCriterionOnZone = ArrayValuesCriterionOnZone;
        }

        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <returns>объект класса BufferZone</returns>
        public object Clone()
        {
            return new BufferZone
            {
                idBufferZone = this.idBufferZone,
                x = this.x,
                y = this.y,
                lengthRadiusSearch = this.lengthRadiusSearch,
                arrayValuesCriterionOnZone = new List<double>(arrayValuesCriterionOnZone)
            };
        }
    }

    // Класс "Пара"
    public class Pair
    {
        // Идентификатор пары
        public int id;
        // Названия свертки
        public string convolution;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">Идентификатор пары</param>
        /// <param name="Convolution">Названия свертки</param>
        public Pair(int Id, string Convolution)
        {
            id = Id;
            convolution = Convolution;
        }
    }

    public class Optimum
    {
        // Пара, которую возвращает каждая свертка - ID оптимума и название свертки
        private List<Pair> _pairInfoOptimum = new List<Pair>();

        // ПРИНИМАЕТ КЛАСС
        // Список буферных зон, который нормализуется (для изменения)
        private List<BufferZone> _listForNormalization = new List<BufferZone>();
        // Список буферных зон с карты (без изменения)
        private List<BufferZone> _listWithStartZones = new List<BufferZone>();
        // Список критериев оптимальности
        private List<Criterion> _listCriterion = new List<Criterion>();

        // ВОЗВРАЩАЕТ КЛАСС
        // Список всех уникальных оптимумов
        private List<OptimalZone> _listAllOptimalZones = new List<OptimalZone>();

        // Утопическая точка
        private OptimalZone _utopiaPoint = new OptimalZone();

        /// <summary>
        /// Получение оптимальной точки
        /// </summary>
        /// <returns></returns>
        public List<OptimalZone> getOptimum(List<BufferZone> arrayBuffers, List<Criterion> criterion)
        {
            _listForNormalization = arrayBuffers;
            _listCriterion = criterion;

            // Перенести все значения из списка буферных зон для нормализации в список стартовых буферных зон
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                BufferZone zone = (BufferZone)_listForNormalization[i].Clone();
                _listWithStartZones.Add(zone);
            }

            // Для каждой буферной зоны надо за-максимизировать все критерии
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // В каждом полигоне каждый критерий надо максимизировать
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Если критерий минимизируется надо максимизировать, то есть *(-1)
                    if (_listCriterion[j].directionOfCriterion == false)
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] = _listForNormalization[i].arrayValuesCriterionOnZone[j] * (-1);
                }
            }

            // Для нормализации надо найти максимум и минимум по каждому критерию
            // Тут хранится минимум для каждого из Х критериев - Х минимумов по каждому критерию
            List<double> _minimums = new List<double>();
            // Тут хранится максимум для каждого из Х критериев - Х максимумов по каждому критерию
            List<double> _maximums = new List<double>();

            // Проход по всем критериям
            for (int i = 0; i < _listCriterion.Count; i++)
            {
                // Самое маленькое число Double
                double max = double.MinValue;
                // Самое большое число Double
                double min = double.MaxValue;

                // Проходим по всем буферным зонам и ищем максимум и минимум по i-ому критерию
                for (int j = 0; j < _listForNormalization.Count; j++)
                {
                    if (_listForNormalization[j].arrayValuesCriterionOnZone[i] >= max)
                        max = _listForNormalization[j].arrayValuesCriterionOnZone[i];
                    if (_listForNormalization[j].arrayValuesCriterionOnZone[i] <= min)
                        min = _listForNormalization[j].arrayValuesCriterionOnZone[i];
                }
                // Сохранить минимум и максимум
                _minimums.Add(min);
                _maximums.Add(max);
            }

            // Массив разностей для каждого критерия
            List<double> differences = new List<double>();

            // Разность между максимумом и минимумом для каждого критерия
            for (int i = 0; i < _listCriterion.Count; i++)
            {
                double differenceCriterion = _maximums[i] - _minimums[i];
                // Массив разностей для каждого критерия
                differences.Add(differenceCriterion);
            }

            // Нормализация: (текущее - минимум) / (максимум - минимум)
            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Проходим по каждому критерию каждой буферной зоны и нормализуем его
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Для текущих нормализованных значений округлить результат до 7 знаков после запятой
                    if (differences[j] != 0)
                    {
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] =
                            (_listForNormalization[i].arrayValuesCriterionOnZone[j] - _minimums[j]) / differences[j];
                        _listForNormalization[i].arrayValuesCriterionOnZone[j] = Math.Round(_listForNormalization[i].arrayValuesCriterionOnZone[j], 7);
                    }
                }
            }

            int idBestPointByLinearConvolution = -1;
            // Максимум для линейной свертки
            double maxLinearConvolution = -1;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariableLinear = 0;
                // Идем по каждому критерию, перемножая у зоны каждый ее критерий на соответствующий ему весовой коэффициент
                for (int j = 0; j < _listCriterion.Count; j++)
                    temporaryVariableLinear = temporaryVariableLinear + (_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion);

                if (temporaryVariableLinear > maxLinearConvolution)
                {
                    maxLinearConvolution = temporaryVariableLinear;
                    idBestPointByLinearConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointByLinearConvolution, "Линейная свёртка"));

            int idBestPointByMultiplicativeConvolution = -1;
            // Максимум для мультипликативной свертки
            double maxMultiplicativeConvolution = -1;
            double temporaryVariable = 1;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listWithStartZones.Count; i++)
            {
                // Флаг входа в одну из веток
                bool flag = false;
                // Сбросили прошлое вычисление
                temporaryVariable = 1;

                // Идем по всем критериям, перемножая у зоны каждый ее критерий на соответствующий ему весовой коэффициент
                for (int j = 0; j < _listCriterion.Count; j++)
                {
                    // Если вес критерия и значение критерия не равны нулю
                    if (_listCriterion[j].weightOfCriterion != 0 && _listWithStartZones[i].arrayValuesCriterionOnZone[j] != 0)
                    {
                        flag = true;
                        temporaryVariable = temporaryVariable * _listWithStartZones[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion;
                    }
                }

                // Если везде были нули и temporaryVariable не изменился
                if (temporaryVariable == 1 && flag == false)
                    temporaryVariable = 0;

                if (temporaryVariable > maxMultiplicativeConvolution)
                {
                    maxMultiplicativeConvolution = temporaryVariable;
                    idBestPointByMultiplicativeConvolution = _listWithStartZones[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointByMultiplicativeConvolution, "Мультипликативная свёртка"));

            // Коэффициент агрегирования,  0 - нельзя, p->0 - мультипликативная, p=1 - линайная
            const double AGGREGATION_COEFFICIENT = 2;
            int idBestPointAggregationConvolution = -1;
            // Максимум для свертки агрегирования
            double maxAggregationeConvolution = -1;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariableAggregation = 0;
                // Проход по всем критериям
                for (int j = 0; j < _listCriterion.Count; j++)
                {
                    temporaryVariableAggregation = temporaryVariableAggregation +
                        Math.Pow(_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion, AGGREGATION_COEFFICIENT);
                }

                double div = 1 / AGGREGATION_COEFFICIENT;
                // Полученую сумму произведений возводим в степени 1/p
                temporaryVariableAggregation = Math.Pow(temporaryVariableAggregation, div);

                // Ищем максимальное значение
                if (temporaryVariableAggregation > maxAggregationeConvolution)
                {
                    maxAggregationeConvolution = temporaryVariableAggregation;
                    idBestPointAggregationConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointAggregationConvolution, "Свёртка агрегирования"));

            int idBestPointHermeierConvolution = -1;
            double maxHermeier = -1;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double minHermeier = 10000;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    double temp = _listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion;
                    // Найти минимальное произведение из всех произведений критерия на весовой коэффициент у одной зоны
                    if (temp <= minHermeier)
                        minHermeier = temp;
                }
                // Найти максимальное произведение из всех самых минимальных
                if (minHermeier > maxHermeier)
                {
                    maxHermeier = minHermeier;
                    idBestPointHermeierConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointHermeierConvolution, "Свёртка Гермейера"));

            if (_CheckIsMainMethod())
            {
                int idBestPointMainCriterionMethod = -1;
                double maxCriterion = -1;
                int positionMaxCriterion = -1;

                // Проход по всем критериям
                for (int i = 0; i < _listCriterion.Count; i++)
                {
                    // Ищем самый приоритетный критерий и его позицию из всех критериев
                    if (_listCriterion[i].weightOfCriterion >= maxCriterion)
                    {
                        maxCriterion = _listCriterion[i].weightOfCriterion;
                        positionMaxCriterion = i;
                    }
                }

                // Проход по всем буферным зонам
                for (int i = 0; i < _listForNormalization.Count; i++)
                {
                    if (_listForNormalization[i].arrayValuesCriterionOnZone[positionMaxCriterion] == 1)
                        idBestPointMainCriterionMethod = _listForNormalization[i].idBufferZone;
                }

                _pairInfoOptimum.Add(new Pair(idBestPointMainCriterionMethod, "Метод главного критерия"));
            }

            // Утопия - точка, в которой все критерии максимизируются, то есть равны 1
            List<double> arrayForUtopiaCriterion = new List<double>();
            for (int i = 0; i < _listCriterion.Count; i++)
                arrayForUtopiaCriterion.Add(1);

            BufferZone utopiaBufferZone = new BufferZone(-1, 0, 0, _listForNormalization[0].lengthRadiusSearch, arrayForUtopiaCriterion);

            // Утопия - идеальная точка, где частные критерии идеальны относительно каждого критерия относительно поставленных точек на карте
            _utopiaPoint.namesConvolutiones = new List<string>();
            _utopiaPoint.optimalZone = utopiaBufferZone;

            int idBestPointIdealPointChebyshevMetricConvolution = -1;
            double minChebyshev = 10000;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double maxChebyshev = -1;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Модуль разности (Утопический критерий - частный текущий критерий)
                    double diffUtopia =
                        Math.Abs(_utopiaPoint.optimalZone.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;

                    if (multi >= maxChebyshev)
                        maxChebyshev = multi;
                }

                if (maxChebyshev < minChebyshev)
                {
                    minChebyshev = maxChebyshev;
                    idBestPointIdealPointChebyshevMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointIdealPointChebyshevMetricConvolution, "Свёртка на основе идеальной точки по метрике Чебышева"));

            int idBestPointIdealPointHammingMetricConvolution = -1;
            double minHamming = 10000;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Обнуление суммы для каждой точки
                double summ = 0;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    double diffUtopia =
                        Math.Abs(_utopiaPoint.optimalZone.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;
                    summ = summ + multi;
                }
                if (summ < minHamming)
                {
                    minHamming = summ;
                    idBestPointIdealPointHammingMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointIdealPointHammingMetricConvolution, "Свёртка на основе идеальной точки по метрике Хемминга"));

            int idBestPointIdealPointEuclideanMetricConvolution = -1;
            double minEuclidean = 10000;

            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                // Обнуление суммы для каждой точки
                double summ = 0;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    double diffUtopia =
                        Math.Pow(_utopiaPoint.optimalZone.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j], 2);
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;
                    summ = summ + multi;
                }
                if (summ < minEuclidean)
                {
                    minEuclidean = summ;
                    idBestPointIdealPointEuclideanMetricConvolution = _listForNormalization[i].idBufferZone;
                }
            }
            _pairInfoOptimum.Add(new Pair(idBestPointIdealPointEuclideanMetricConvolution, "Свёртка на основе идеальной точки по метрике Евклида"));

            // ID всех выбранных буферных зон каждой сверткой
            List<int> listWithID = new List<int>();
            for (int i = 0; i < _pairInfoOptimum.Count; i++)
                listWithID.Add(_pairInfoOptimum[i].id);

            // Уникальные ID оптимумов
            var listWithUniqueID = listWithID.Distinct();

            // Оптимальные уникальные зоны
            _listAllOptimalZones = new List<OptimalZone>();

            // Проход по всем буферным зонам
            for (int i = 0; i < _listWithStartZones.Count; i++)
            {
                // Проход по всем уникальным ID
                for (int j = 0; j < listWithUniqueID.Count(); j++)
                {
                    if (_listWithStartZones[i].idBufferZone == listWithUniqueID.ElementAt(j))
                    {
                        List<string> listString = new List<string>();
                        _listAllOptimalZones.Add(new OptimalZone(_listWithStartZones[i], listString));
                    }
                }
            }

            // Проход по всем оптимальным точкам
            for (int i = 0; i < _listAllOptimalZones.Count; i++)
            {
                // Проход по всем парам оптимальных зон
                for (int j = 0; j < _pairInfoOptimum.Count; j++)
                {
                    // Если ID оптимума совпал с ID в паре
                    if (_listAllOptimalZones[i].optimalZone.idBufferZone == _pairInfoOptimum[j].id)
                        _listAllOptimalZones[i].namesConvolutiones.Add(_pairInfoOptimum[j].convolution);
                }
            }

            return _listAllOptimalZones;
        }

        // Пороговое значение, разница между двумя важными приоритетатми выше этого значения, то метод главного критерия используется
        private const double THRESHOLD = 0.3999;
        /// <summary>
        /// Проверка использования метода главного критерия
        /// </summary>
        /// <returns>Флаг используется метод или нет</returns>
        private bool _CheckIsMainMethod()
        {
            // Если однокритериальный поиск
            if (_listCriterion.Count == 1)
                return true;
            else
            {
                List<double> weights = new List<double>();
                // Идем по всем критериям
                for (int i = 0; i < _listCriterion.Count; i++)
                    weights.Add(_listCriterion[i].weightOfCriterion);

                // Сортируем все веса по убыванию
                weights.Sort((x, y) => y.CompareTo(x));

                // Разница между самым важным критерием и критерием по важности на 2 месте
                double diff = weights[0] - weights[1];

                // Если разница больше 0.3999 значит самый важный критерий останется, и мы берем только по нему наилучшую точку
                if (diff >= THRESHOLD)
                    return true;
                else
                    return false;
            }
        }
    }
}