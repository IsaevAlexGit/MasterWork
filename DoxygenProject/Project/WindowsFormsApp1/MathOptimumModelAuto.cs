using System;
using System.Collections.Generic;

namespace Optimum
{
    //! Класс "Математика для автопоиска и нормы"
    public class MathOptimumModelAuto
    {
        //! Класс "Авто-буферная зона"
        public class AutoBufferZone
        {
            //! Буферная зона
            public BufferZone bufferZone;
            //! Параметр свертки для данной буферной зоны
            public double parameterConvolution;
            //! Суммарное место в общем рейтинге всех авто-точек
            public int summaryPlace;

            /*!
            \version 1.0
            */
            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="BufferZone">Буферная зона</param>
            /// <param name="ParameterConvolution">Параметр свертки для данной буферной зоны</param>
            /// <param name="SummaryPlace">Суммарное место в общем рейтинге всех авто-точек</param>
            /// <permission cref="">Доступ к функции: public</permission>
            public AutoBufferZone(BufferZone BufferZone, double ParameterConvolution, int SummaryPlace)
            {
                bufferZone = BufferZone;
                parameterConvolution = ParameterConvolution;
                summaryPlace = SummaryPlace;
            }
        }

        //! Массив оптимальных автоточек
        private List<AutoBufferZone> _superBestPoints = new List<AutoBufferZone>();
        //! Требуемое количество авто-оптимумов
        private readonly int _countOptimums;

        // ПРИНИМАЕТ КЛАСС
        //! Список буферных зон, который нормализуется (для изменения)
        private List<BufferZone> _listForNormalization = new List<BufferZone>();
        //! Список буферных зон с карты (без изменения)
        private List<BufferZone> _listWithStartZones = new List<BufferZone>();
        //! Список критериев оптимальности
        private readonly List<Criterion> _listCriterion = new List<Criterion>();

        // ВОЗВРАЩАЕТ КЛАСС
        //! Массив оптимальных автоточек
        private List<BufferZone> _autoBestPoints = new List<BufferZone>();

        //! Утопическая точка
        private OptimalZone _utopiaPoint = new OptimalZone();
        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="arrayBuffers">Массив буферных зон</param>
        /// <param name="criterion">Список критериев</param>
        /// <param name="countOptimums">Искомое число оптимумов (1-10)</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public MathOptimumModelAuto(List<BufferZone> arrayBuffers, List<Criterion> criterion, int countOptimums)
        {
            // Создаем для каждого кандидата рейтинг
            for (int i = 0; i < arrayBuffers.Count; i++)
            {
                AutoBufferZone zone = new AutoBufferZone(arrayBuffers[i], 0, 0);
                _superBestPoints.Add(zone);
            }

            _listForNormalization = arrayBuffers;
            _listCriterion = criterion;
            _countOptimums = countOptimums;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Получение оптимальной точки
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <returns>Оптимальные точки</returns>
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
            // Если возможно вызвать метод главного критерия
            if (_CheckIsMainMethod())
                // Метод главного критерия
                _MainCriterionMethod();

            // Найти Утопию
            _CreateUtopiaPoint();
            // Свертка на основе идеальной точки по метрике Чебышева
            _IdealPointChebyshevMetricConvolution();
            // Свертка на основе идеальной точки по метрике Хемминга
            _IdealPointHammingMetricConvolution();
            // Свертка на основе идеальной точки по метрике Евклида
            _IdealPointEuclideanMetricConvolution();

            // Вернуть массив оптимальных точек
            return _FinalSort();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Максимизация всех параметров
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _MaximizationOfCriterion()
        {
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
        }

        // Для нормализации надо найти максимум и минимум по каждому критерию
        //! Тут хранится минимум для каждого из Х критериев - Х минимумов по каждому критерию 
        private List<double> _minimums = new List<double>();
        //! Тут хранится максимум для каждого из Х критериев - Х максимумов по каждому критерию
        private List<double> _maximums = new List<double>();

        /*!
        \version 1.0
        */
        /// <summary>
        /// Поиск максимума и минимума для каждого критерия
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _SearchMaxMinForEachCriterion()
        {
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
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Нормализация всех критериев
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _Normalization()
        {
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
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Линейная свертка
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _LinearConvolution()
        {
            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariable = 0;
                // Ищем показатель линейной свертки для каждой буферной зоны
                for (int j = 0; j < _listCriterion.Count; j++)
                    temporaryVariable = temporaryVariable + (_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion);

                // У каждой буферной зоны свой показатель линейной свертки
                _superBestPoints[i].parameterConvolution = temporaryVariable;
            }

            // Сортируем все буферные зоны по рейтингу линейной свертки
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Мультипликативная свертка
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _MultiplicativeConvolution()
        {
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

                // У каждой буферной зоны свой показатель мультипликативной свертки
                _superBestPoints[i].parameterConvolution = temporaryVariable;
            }

            // Сортируем все буферные зоны по рейтингу мультипликативной свертки
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        //! Коэффициент агрегирования,  0 - нельзя, p->0 - мультипликативная, p=1 - линайная
        private const double AGGREGATION_COEFFICIENT = 2;
        /*!
        \version 1.0
        */
        /// <summary>
        /// Свертка агрегирования
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _AggregationConvolution()
        {
            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double temporaryVariable = 0;
                // Проход по всем критериям
                for (int j = 0; j < _listCriterion.Count; j++)
                {
                    temporaryVariable = temporaryVariable +
                        Math.Pow(_listForNormalization[i].arrayValuesCriterionOnZone[j] * _listCriterion[j].weightOfCriterion, AGGREGATION_COEFFICIENT);
                }

                double div = 1 / AGGREGATION_COEFFICIENT;
                // Полученую сумму произведений возводим в степени 1/p
                temporaryVariable = Math.Pow(temporaryVariable, div);

                // У каждой буферной зоны свой показатель свертки агрегирования
                _superBestPoints[i].parameterConvolution = temporaryVariable;
            }

            // Сортируем все буферные зоны по рейтингу свертки агрегирования
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Свертка Гермейера
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _HermeierConvolution()
        {
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
                // У каждой буферной зоны свой показатель свертки Гермейера
                _superBestPoints[i].parameterConvolution = minHermeier;
            }

            // Сортируем все буферные зоны по рейтингу свертки Гермейера
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        //! Пороговое значение, разница между двумя важными приоритетатми выше этого значения, то метод главного критерия используется
        private const double THRESHOLD = 0.3999;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Проверка использования метода главного критерия
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
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

        /*!
         * \version 1.0
         */
        /// <summary>
        /// Метод главного критерия
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _MainCriterionMethod()
        {
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
                // У каждой буферной зоны свой показатель метода главного критерия
                _superBestPoints[i].parameterConvolution = _listForNormalization[i].arrayValuesCriterionOnZone[positionMaxCriterion];

            // Сортируем все буферные зоны по рейтингу метода главного критерия 
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Создать идеальную точку
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _CreateUtopiaPoint()
        {
            // Утопия - точка, в которой все критерии максимизируются, то есть равны 1
            List<double> arrayForUtopiaCriterion = new List<double>();
            for (int i = 0; i < _listCriterion.Count; i++)
                arrayForUtopiaCriterion.Add(1);

            BufferZone utopiaBufferZone = new BufferZone(-1, 0, 0, _listForNormalization[0].lengthRadiusSearch, arrayForUtopiaCriterion);

            // Утопия - идеальная точка, где частные критерии идеальны относительно каждого критерия относительно поставленных точек на карте
            _utopiaPoint.namesConvolutiones = new List<string>();
            _utopiaPoint.optimalZone = utopiaBufferZone;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Свертка на основе идеальной точки по метрике Чебышева
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _IdealPointChebyshevMetricConvolution()
        {
            // Проход по всем буферным зонам
            for (int i = 0; i < _listForNormalization.Count; i++)
            {
                double maxChebyshev = -1;
                // В каждой зоне идем по каждому критерию
                for (int j = 0; j < _listForNormalization[i].arrayValuesCriterionOnZone.Count; j++)
                {
                    double diffUtopia =
                        Math.Abs(_utopiaPoint.optimalZone.arrayValuesCriterionOnZone[j] - _listForNormalization[i].arrayValuesCriterionOnZone[j]);
                    double multi = diffUtopia * _listCriterion[j].weightOfCriterion;

                    if (multi >= maxChebyshev)
                        maxChebyshev = multi;
                }
                // У каждой буферной зоны свой показатель свертки Чебышева
                _superBestPoints[i].parameterConvolution = maxChebyshev;
            }

            // Сортируем все буферные зоны по рейтингу свертки Чебышева
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Свертка на основе идеальной точки по метрике Хемминг
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _IdealPointHammingMetricConvolution()
        {
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

                    // У каждой буферной зоны свой показатель свертки Хемминга
                    _superBestPoints[i].parameterConvolution = summ;
                }
            }

            // Сортируем все буферные зоны по рейтингу свертки Хемминга
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
       */
        /// <summary>
        /// Свертка на основе идеальной точки по метрике Евклида
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _IdealPointEuclideanMetricConvolution()
        {
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

                    // У каждой буферной зоны свой показатель свертки Евклида
                    _superBestPoints[i].parameterConvolution = summ;
                }
            }

            // Сортируем все буферные зоны по рейтингу свертки Евклида
            _superBestPoints.Sort((a, b) => b.parameterConvolution.CompareTo(a.parameterConvolution));

            // Добавляем занятое место в рейтинг
            for (int i = 0; i < _superBestPoints.Count; i++)
            {
                int place = i + 1;
                _superBestPoints[i].summaryPlace += place;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Сортировка всех буферных зон по рейтингу
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <returns>Список авто-оптимумов</returns>
        private List<BufferZone> _FinalSort()
        {
            // Сортируем все буферные зоны по общему рейтингу
            _superBestPoints.Sort((b, a) => b.summaryPlace.CompareTo(a.summaryPlace));

            // Добавляем столько, сколько требовалось
            for (int i = 0; i < _countOptimums; i++)
                _autoBestPoints.Add(_superBestPoints[i].bufferZone);
            return _autoBestPoints;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Клонирование буферных зон
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
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