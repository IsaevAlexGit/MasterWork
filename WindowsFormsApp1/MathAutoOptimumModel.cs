using System;
using System.Collections.Generic;

namespace Optimum
{
    public class AutoBufferZone
    {
        public BufferZone bufferZone;
        public double parameterConvolution;
        public int summaryPlace;

        public AutoBufferZone(BufferZone zone, double param, int summ)
        {
            bufferZone = zone;
            parameterConvolution = param;
            summaryPlace = summ;
        }
    }

    public class MathAutoOptimumModel
    {
        // Массив оптимальных автоточек
        private List<AutoBufferZone> _SuperBestPoints = new List<AutoBufferZone>();
        // Требуемое количество авто-оптимумов
        private readonly int _countOptimums;

        // ПРИНИМАЕТ КЛАСС
        // Список буферных зон, который нормализуется (для изменения)
        private List<BufferZone> _listForNormalization = new List<BufferZone>();
        // Список буферных зон с карты (без изменения)
        private List<BufferZone> _listWithStartZones = new List<BufferZone>();
        // Список критериев оптимальности
        private readonly List<Criterion> _listCriterion = new List<Criterion>();

        // ВОЗВРАЩАЕТ КЛАСС
        // Массив оптимальных автоточек
        private List<BufferZone> _autoBestPoints = new List<BufferZone>();

        // Утопическая точка
        private OptimalZone _UtopiaPoint = new OptimalZone();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="arrayBuffers">Массив буферных зон</param>
        /// <param name="criterion">Список критериев</param>
        /// <param name="countOptimums">Искомое число оптимумов (1-10)</param>
        public MathAutoOptimumModel(List<BufferZone> arrayBuffers, List<Criterion> criterion, int countOptimums)
        {
            for (int i = 0; i < arrayBuffers.Count; i++)
            {
                AutoBufferZone spz = new AutoBufferZone(arrayBuffers[i], 0, 0);
                _SuperBestPoints.Add(spz);
            }

            _listForNormalization = arrayBuffers;
            _listCriterion = criterion;
            _countOptimums = countOptimums;
        }

        /// <summary>
        /// Получение оптимальной точки
        /// </summary>
        /// <returns>Оптимальная точка</returns>
        public List<BufferZone> GetArrayWithOptimums()
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

            // Вернуть лучшую точку
            return _FinalSort();
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
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariable = 0;
                // Ищем показатель Л. свертки для каждой точки
                for (int j = 0; j < _listCriterion.Count; j++)
                    temporaryVariable = temporaryVariable + (_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion);

                // У каждой точки свой показатель Л. свертки
                _SuperBestPoints[i].parameterConvolution = temporaryVariable;
            }

            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        /// <summary>
        /// Мультипликативная свертка
        /// </summary>
        private void _MultiplicativeConvolution()
        {
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

                _SuperBestPoints[i].parameterConvolution = temporaryVariable;
            }
            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }


        /// <summary>
        /// Свертка агрегирования
        /// </summary>
        private void _AggregationConvolution()
        {
            // Коэффициент агрегирования,  0 - нельзя, p->0 - мультипликативная, p=1 - линайная
            double _aggregationCoefficient = 2;

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

                _SuperBestPoints[i].parameterConvolution = temporaryVariable;
            }
            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        /// <summary>
        /// Свертка Гермейера
        /// </summary>
        private void _HermeierConvolution()
        {
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
                _SuperBestPoints[i].parameterConvolution = MinHermeier;
            }

            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
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
                _SuperBestPoints[i].parameterConvolution = _listForNormalization[i].arrayValuesCriterionOnZone[positionMaxCr];

            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        // Создать идеальную точку
        private void _createUtopiaPoint()
        {
            // Сколько критериев столько и единиц
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
            // Идем по 10 точках
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double MaxChebyshev = -1;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    // Модуль разности (Утопический критерий - частный текущий критерий)
                    double diffUtopia = Math.Abs(_UtopiaPoint.optimal.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    // Вес * модуль разности
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;

                    if (multi >= MaxChebyshev)
                        MaxChebyshev = multi;
                }
                _SuperBestPoints[i].parameterConvolution = MaxChebyshev;
            }
            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        /// <summary>
        /// Свертка на основе идеальной точки по метрике Хемминг
        /// </summary>
        private void _IdealPointHammingMetricConvolution()
        {
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
                    _SuperBestPoints[i].parameterConvolution = summ;
                }
            }
            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        /// <summary>
        /// Свертка на основе идеальной точки по метрике Евклида
        /// </summary>
        private void _IdealPointEuclideanMetricConvolution()
        {
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
                    _SuperBestPoints[i].parameterConvolution = summ;
                }
            }
            _SuperBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // добавляем к месту занятое место
            for (int i = 0; i < _SuperBestPoints.Count; i++)
            {
                int place = i + 1;
                _SuperBestPoints[i].summaryPlace += place;
            }
        }

        /// <summary>
        /// Финальный массив лучших
        /// </summary>
        private List<BufferZone> _FinalSort()
        {
            _SuperBestPoints.Sort((b, a) => b.summaryPlace.CompareTo(a.summaryPlace));

            //FileStream fileStream = new FileStream(Application.StartupPath + @"\Data\in.txt", FileMode.Create, FileAccess.Write);
            //StreamWriter streamWriter = new StreamWriter(fileStream);
            //for (int t = 0; t < _SuperBestPoints.Count; t++)
            //    streamWriter.WriteLine(_SuperBestPoints[t].summaryPlace);
            //streamWriter.Close();

            for (int i = 0; i < _countOptimums; i++)
                _autoBestPoints.Add(_SuperBestPoints[i].bufferZone);

            return _autoBestPoints;

            //FileStream fileStream = new FileStream(Application.StartupPath + @"\Data\in.txt", FileMode.Create, FileAccess.Write);
            //StreamWriter streamWriter = new StreamWriter(fileStream);
            //for (int t = 0; t < _autoBestPoints.Count; t++)
            //    streamWriter.WriteLine(_autoBestPoints[t].idBufferZone);
            //streamWriter.Close();
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