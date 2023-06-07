using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Optimum
{
    public class MapModel
    {
        // Основной и дополнительный цвет интерфейса
        public Color mainColor;
        public Color secondaryColor;

        // Точка центрирования карты
        public Location centerMap = new Location();

        // Название объекта социальной инфраструктуры
        public string nameObjectFacility;
        // Загруженный пользователем значок для отображения объектов социальной инфраструктуры
        public string pathToIconObjectFacility;
        public Image iconFacility;

        // Список критериев оптимальности
        public List<Criterion> listUserCriterion = new List<Criterion>();

        // Флаги изменения файлов в настройках
        public bool flagChangePolygon = false;
        public bool flagChangeTerritory = false;
        public bool flagChangeCriterion = false;
        public bool flagLoadFileNorma = false;

        // Массив значков для точечной раскраски
        public List<Image> arrayIcons = new List<Image>();

        // Состояние нижней панели "Open"/"Close"
        public string stateOfBottomPanel = "";

        // Список оптимальных точек
        public List<OptimalZone> optimalZones = new List<OptimalZone>();

        // Библиотечный слой со всеми объектами социальной инфраструктуры
        private readonly GMapOverlay _overlayFacility = new GMapOverlay("Объекты социальной инфраструктуры");
        // Слой со всеми объектами социальной инфраструктуры
        public SublayerFacility sublayerFacility = new SublayerFacility("", "");
        // Список объектов социальной инфраструктуры из файла
        public List<Facility> listUserFacilities;

        // Библиотечный слой с граничными точками зоны анализа
        private readonly GMapOverlay _overlayWithBorderPointsTerritoryForUser = new GMapOverlay("Граничные точки зоны анализа");
        // Слой с граничными зоны анализа
        public SublayerLocation sublayerBorderPointsTerritory = new SublayerLocation("", "");
        // Список граничных точек зоны анализа
        public List<Location> listUserPointsBorderTerritory;

        // Библиотечный слой с полигонами
        private readonly GMapOverlay _overlayPolygon = new GMapOverlay("");
        // Слой со всеми полигонами
        public SublayerPolygon sublayerPolygonSquare = new SublayerPolygon("", "");
        // Библиотечный слой с полигонами для точечной раскраски
        private readonly GMapOverlay _overlayPolygonIcon = new GMapOverlay("");
        // Слой со всеми полигонами для точечной раскраски
        public SublayerPolygon sublayerPolygonrIcon = new SublayerPolygon("", "");
        // Библиотечный слой для проверки вхождения точки в полигоны
        private readonly GMapOverlay _overlayPolygonCheck = new GMapOverlay("");
        // Слой для проверки вхождения точки в полигоны
        public SublayerPolygon sublayerPolygonCheck = new SublayerPolygon("", "");
        // Список полигонов из файла
        public List<Polygon> listUserPolygons;
        // Временный список для граничных точек полигонов
        public List<PointLatLng> tempListForPolygons;
        // Количество оттенков для раскраски полигонов по критериям
        public int numberOfShadesCriterion = 8;
        // Минимум и максимум для точечной и площадной раскраски
        public double maxCriterionPolygonColoring, minCriterionPolygonColoring;
        public double maxCriterionIconColoring, minCriterionIconColoring;
        // Путь к файлу с полигонами
        public string pathToFilePolygonUser;

        // Библиотечный слой с маркерами пользователя
        private readonly GMapOverlay _overlayUserPoints = new GMapOverlay("Точки-кандидаты пользователя");
        // Слой с маркерами пользователя
        public SublayerLocation sublayerUserPoints = new SublayerLocation("", @"Icon/iconUserFacility.png");
        // Список точек-кандидатов пользователя
        public List<Location> listPointsUser = new List<Location>();
        // Список буферных зон пользователя
        public List<BufferZone> listPointsBufferZone = new List<BufferZone>();
        // Радиус буферных зон по умолчанию
        public int radiusBufferZone = 500;
        // Ограничение на установку пользовательских маркеров на карте
        public readonly int LIMIT_USER_POINTS_ON_MAP = 30;
        // Ограничение на расстояние между пользовательскими маркерами
        public readonly int DISTANCE_BETWEEN_USER_POINTS = 25;

        // Библиотечный слой с авто-оптимумами
        private readonly GMapOverlay _overlayAutoPoints = new GMapOverlay("Авто-поиск");
        // Слой с авто-оптимумами
        public SublayerLocation sublayerAutoPoints = new SublayerLocation("Авто-оптимумы", @"Icon/iconBestOptimum.png");
        // Список авто-точек
        public List<Location> listAutoPoints = new List<Location>();
        // Список авто-буферных зон
        public List<BufferZone> listAutoPointsBufferZone = new List<BufferZone>();

        // Библиотечный слой для нормы на душу населения
        private readonly GMapOverlay _overlayNorma = new GMapOverlay("Норма");
        // Слой для нормы на душу населения
        public SublayerLocation sublayerNormaFacility = new SublayerLocation("Норма", @"Icon/iconBestOptimum.png");
        // Список авто-точек для нормы на душу населения
        public List<Location> listNorma = new List<Location>();
        // Список буферных зон для нормы на душу населения
        public List<BufferZone> listNormaPointsBufferZone = new List<BufferZone>();
        // Радиус буферных зон по умолчанию для нормы на душу населения
        public int radiusNormaBufferZone = 500;
        // Норма объекта социальной инфраструктуры и населения
        public int implementedRateOfFacility = 1;
        public int implementedRateOfPopulation = 1000;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MapModel() { }

        /// <summary>
        /// Инициализация слоев и списков данными
        /// </summary>
        public void InitializationSublayersAndLists()
        {
            try
            {
                // Заполнить массив значков для точечной раскраски
                for (int i = 1; i <= 10; i++)
                    arrayIcons.Add((Bitmap)Image.FromFile(@"Icon/IconsColoring/" + i.ToString() + ".png"));

                // Преобразовать путь к файлу в Image
                Bitmap bitmapUserIconFacility = (Bitmap)Image.FromFile(pathToIconObjectFacility);
                iconFacility = bitmapUserIconFacility;

                // Задать название слоя как название объекта социальной инфраструктуры
                sublayerFacility.nameOfOverlay = nameObjectFacility;
                // Задать загруженный значок слою
                sublayerFacility.iconOfOverlay = pathToIconObjectFacility;
                // Слой для объектов социальной инфраструктуры
                sublayerFacility.listWithFacility = listUserFacilities;
                sublayerFacility.overlay = _overlayFacility;
                sublayerFacility.overlay.IsVisibile = false;

                // Слой для граничных точек зоны анализа
                sublayerBorderPointsTerritory.listWithLocation = listUserPointsBorderTerritory;
                sublayerBorderPointsTerritory.overlay = _overlayWithBorderPointsTerritoryForUser;
                sublayerBorderPointsTerritory.overlay.IsVisibile = false;

                // Слой для полигонов - площадная раскраска
                sublayerPolygonSquare.listWithPolygons = listUserPolygons;
                sublayerPolygonSquare.overlay = _overlayPolygon;
                sublayerPolygonSquare.overlay.IsVisibile = false;
                // Слой для полигонов - точечная раскраска
                sublayerPolygonrIcon.listWithPolygons = sublayerPolygonSquare.listWithPolygons;
                sublayerPolygonrIcon.overlay = _overlayPolygonIcon;
                sublayerPolygonrIcon.overlay.IsVisibile = false;
                // Слой для полигонов - принадлежит ли точка пользователя поставленная на карте одному из полигонов
                sublayerPolygonCheck.listWithPolygons = sublayerPolygonSquare.listWithPolygons;
                sublayerPolygonCheck.overlay = _overlayPolygonCheck;
                sublayerPolygonCheck.overlay.IsVisibile = false;

                // Слой для пользовательских кандидатов
                sublayerUserPoints.listWithLocation = listPointsUser;
                sublayerUserPoints.overlay = _overlayUserPoints;
                sublayerUserPoints.overlay.IsVisibile = false;

                // Слой для поиска авто-оптимумов
                sublayerAutoPoints.listWithLocation = listAutoPoints;
                sublayerAutoPoints.overlay = _overlayAutoPoints;
                sublayerAutoPoints.overlay.IsVisibile = false;

                // Слой для поиска нормы на душу населения
                sublayerNormaFacility.listWithLocation = listNorma;
                sublayerNormaFacility.overlay = _overlayNorma;
                sublayerNormaFacility.overlay.IsVisibile = false;
            }
            catch
            {
                // Аварийный выход
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Создание таблицы из полигонов
        /// </summary>
        /// <returns>Таблица, заполненная данными из списка полигонов</returns>
        public DataTable CreateTableFromPolygons()
        {
            // Создание таблицы
            DataTable dateTablePolygons = new DataTable();
            // Добавление столбцов для таблицы
            dateTablePolygons.Columns.Add("ID", typeof(int));
            dateTablePolygons.Columns.Add("CountBorderPoints", typeof(int));
            dateTablePolygons.Columns.Add("ListBorderPoints", typeof(List<Location>));
            dateTablePolygons.Columns.Add("X", typeof(double));
            dateTablePolygons.Columns.Add("Y", typeof(double));
            // Критериев загружено пользователем для каждого объекта социальной инфраструктуры
            for (int i = 0; i < listUserCriterion.Count; i++)
                dateTablePolygons.Columns.Add("Criterion" + i.ToString(), typeof(double));

            // Заполнение таблицы данными из списка полигонов
            for (int i = 0; i < listUserPolygons.Count; i++)
            {
                DataRow row = dateTablePolygons.NewRow();
                row["ID"] = listUserPolygons[i].idPolygon;
                row["CountBorderPoints"] = listUserPolygons[i].countBoundaryPoints;
                row["ListBorderPoints"] = listUserPolygons[i].listBoundaryPoints;
                row["X"] = listUserPolygons[i].xCenterOfPolygon;
                row["Y"] = listUserPolygons[i].yCenterOfPolygon;
                for (int j = 0; j < listUserCriterion.Count; j++)
                    row["Criterion" + j.ToString()] = listUserPolygons[i].valuesForEveryCriterionOfPolygon[j];
                dateTablePolygons.Rows.Add(row);
            }
            return dateTablePolygons;
        }

        /// <summary>
        /// Определение цвета, в который будет окрашен полигон
        /// </summary>
        /// <param name="valueForColoring">Число для раскраски</param>
        /// <param name="minValueColoring">Минимум раскраски</param>
        /// <param name="maxValueColoring">Максимум раскраски</param>
        /// <param name="countOfGrids">Количество оттенков</param>
        /// <param name="countOfSteps">Количество шагов</param>
        /// <returns>Номер оттенка</returns>
        public int GetnumberShade(double valueForColoring, double minValueColoring, double maxValueColoring, int countOfGrids, double countOfSteps)
        {
            // Границы левого и правого прямоугольника
            double leftBorder, rightBorder;
            // Номер оттенка для раскраски
            int numberShade = -1;

            // Для каждого прямоугольника
            for (int i = 0; i < countOfGrids; i++)
            {
                // Левый крайн - минимум + шаг*на количество уже сделанных шагов
                leftBorder = minValueColoring + countOfSteps * i;
                // Правый край = левый + шаг
                rightBorder = leftBorder + countOfSteps;

                // Если число для раскраски больше левого и меньше правого
                if ((valueForColoring >= leftBorder) && (valueForColoring < rightBorder))
                    numberShade = i;
            }

            // Если число меньше минимума, то раскраска - 0 - белая - самая левая граница
            if (valueForColoring <= minValueColoring)
                numberShade = 0;
            // Если число больше максимум, то раскраска - 1 - самая яркая - максимальный оттенок
            if (valueForColoring >= maxValueColoring)
                numberShade = countOfGrids - 1;
            return numberShade;
        }

        /// <summary>
        /// Вычисление для каждого полигона у слоя sublayerUserPoints количества вошедших в него критериев
        /// </summary>
        public void CreateListBufferZones()
        {
            // Очистка списка буферных зон
            listPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            // Цикл по всем буферным зонам пользователя
            for (int i = 0; i < sublayerUserPoints.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона создание нового списка
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем полигонам зоны анализа
                for (int j = 0; j < sublayerPolygonSquare.listWithPolygons.Count; j++)
                {
                    // Центр полигона
                    PointLatLng point = new PointLatLng(sublayerPolygonSquare.listWithPolygons[j].xCenterOfPolygon,
                        sublayerPolygonSquare.listWithPolygons[j].yCenterOfPolygon);

                    // Если центр полигона вошел в радиус буферной зоны, то суммируем все частные критерии данного полигона
                    if (sublayerUserPoints.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                            valuesCriterion[k] += sublayerPolygonSquare.listWithPolygons[j].valuesForEveryCriterionOfPolygon[k];
                    }
                }
                // Когда мы для буферной зоны нашли количество вошедших в него полигонов и просуммировали все частные критерии
                // В список заносим данные об этой буферной зоне
                BufferZone zone = new BufferZone
                {
                    idBufferZone = tempID,
                    x = sublayerUserPoints.listWithLocation[i].x,
                    y = sublayerUserPoints.listWithLocation[i].y,
                    lengthRadiusSearch = radiusBufferZone,
                    arrayValuesCriterionOnZone = valuesCriterion
                };
                listPointsBufferZone.Add(zone);
                // Увиличить ID на 1 для следующей буферной зоны
                tempID++;
            }
        }

        /// <summary>
        /// Вычисление для каждого полигона у слоя sublayerAutoPoints количества вошедших в него критериев
        /// </summary>
        public void CreateListAutoBufferZones()
        {
            // Очистка списка буферных зон
            listAutoPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            // Цикл по всем буферным зонам пользователя
            for (int i = 0; i < sublayerAutoPoints.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона создание нового списка
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем полигонам зоны анализа
                for (int j = 0; j < sublayerPolygonSquare.listWithPolygons.Count; j++)
                {
                    // Центр полигона
                    PointLatLng point = new PointLatLng(sublayerPolygonSquare.listWithPolygons[j].xCenterOfPolygon,
                        sublayerPolygonSquare.listWithPolygons[j].yCenterOfPolygon);

                    // Если центр полигона вошел в радиус буферной зоны, то суммируем все частные критерии данного полигона
                    if (sublayerAutoPoints.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                            valuesCriterion[k] += sublayerPolygonSquare.listWithPolygons[j].valuesForEveryCriterionOfPolygon[k];
                    }
                }

                // Флаг проверки, не является ли авто-кандидат со всем нулями в частных критериях
                bool flagCheckAutoZoneIsNotNull = false;
                for (int l = 0; l < valuesCriterion.Count; l++)
                {
                    if (valuesCriterion[l] > 0)
                        flagCheckAutoZoneIsNotNull = true;
                }

                // Добавили точку для авто-оптимума, только если она имеет не нули в частных критериях
                if (flagCheckAutoZoneIsNotNull)
                {
                    BufferZone zone = new BufferZone
                    {
                        idBufferZone = tempID,
                        x = sublayerAutoPoints.listWithLocation[i].x,
                        y = sublayerAutoPoints.listWithLocation[i].y,
                        lengthRadiusSearch = radiusBufferZone,
                        arrayValuesCriterionOnZone = valuesCriterion
                    };
                    listAutoPointsBufferZone.Add(zone);
                    // Увиличить ID на 1 для следующей буферной зоны
                    tempID++;
                }
            }
        }

        /// <summary>
        /// Найти максимум и минимум по выбранному критерию для площадной раскраски
        /// </summary>
        /// <param name="indexOfSelectedCriterion"></param>
        public void SetMaximumAndMinumForCriterionSquareColoring(int indexOfSelectedCriterion)
        {
            // Список значений критерия
            List<double> valuesCriterion = new List<double>();
            for (int i = 0; i < listUserPolygons.Count; i++)
                valuesCriterion.Add(listUserPolygons[i].valuesForEveryCriterionOfPolygon[indexOfSelectedCriterion]);

            // Найти минимум и максимум в списке значений выбранного критерия
            minCriterionPolygonColoring = valuesCriterion.Min();
            maxCriterionPolygonColoring = valuesCriterion.Max();
        }

        /// <summary>
        /// Найти максимум и минимум по выбранному критерию для точечной раскраски
        /// </summary>
        /// <param name="indexOfSelectedCriterion"></param>
        public void SetMaximumAndMinumForCriterionIconColoring(int indexOfSelectedCriterion)
        {
            // Список значений критерия
            List<double> valuesCriterion = new List<double>();
            for (int i = 0; i < listUserPolygons.Count; i++)
                valuesCriterion.Add(listUserPolygons[i].valuesForEveryCriterionOfPolygon[indexOfSelectedCriterion]);

            // Найти минимум и максимум в списке значений выбранного критерия
            minCriterionIconColoring = valuesCriterion.Min();
            maxCriterionIconColoring = valuesCriterion.Max();
        }

        /// <summary>
        /// Найти суммарное значение населения в файле
        /// </summary>
        /// <returns>Общее количество населения в файле</returns>
        public long CalculateCountPopulation()
        {
            long countPopulation = 0;
            for (int i = 0; i < listUserPolygons.Count; i++)
                // На 0 месте (то есть 1-ым критерием в csv) идет население
                countPopulation += Convert.ToInt32(listUserPolygons[i].valuesForEveryCriterionOfPolygon[0]);
            return countPopulation;
        }

        /// <summary>
        /// Найти суммарное значение объектов социальной инфраструктуры в файле
        /// </summary>
        /// <returns>Общее количество объектов социальной инфраструктуры в файле</returns>
        public int CalculateCountFacility()
        {
            int countFacility = 0;
            for (int i = 0; i < listUserPolygons.Count; i++)
                // На 1 месте (то есть 2-ым критерием в csv) идет количество объектов социальной инфраструктуры
                countFacility += Convert.ToInt32(listUserPolygons[i].valuesForEveryCriterionOfPolygon[1]);
            return countFacility;
        }

        /// <summary>
        /// Вычисление для каждого полигона у слоя sublayerNormaFacility количества вошедших в него критериев
        /// </summary>
        public void CreateListNormaBufferZones()
        {
            // Очистка списка буферных зон
            listNormaPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            for (int i = 0; i < sublayerNormaFacility.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона создание нового списка
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем полигонам зоны анализа
                for (int j = 0; j < sublayerPolygonSquare.listWithPolygons.Count; j++)
                {
                    // Центр полигона
                    PointLatLng point = new PointLatLng(sublayerPolygonSquare.listWithPolygons[j].xCenterOfPolygon,
                        sublayerPolygonSquare.listWithPolygons[j].yCenterOfPolygon);

                    // Если центр полигона вошел в радиус буферной зоны, то суммируем все частные критерии данного полигона
                    if (sublayerNormaFacility.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                            valuesCriterion[k] += sublayerPolygonSquare.listWithPolygons[j].valuesForEveryCriterionOfPolygon[k];
                    }
                }

                // Флаг проверки, не является ли авто-кандидат со всем нулями в частных критериях
                bool flagCheckAutoZoneIsNotNull = false;
                for (int l = 0; l < valuesCriterion.Count; l++)
                {
                    if (valuesCriterion[l] > 0)
                        flagCheckAutoZoneIsNotNull = true;
                }

                // Добавили точку для нормы, только если она имеет не нули в частных критериях
                if (flagCheckAutoZoneIsNotNull)
                {
                    BufferZone zone = new BufferZone
                    {
                        idBufferZone = tempID,
                        x = sublayerNormaFacility.listWithLocation[i].x,
                        y = sublayerNormaFacility.listWithLocation[i].y,
                        lengthRadiusSearch = radiusBufferZone,
                        arrayValuesCriterionOnZone = valuesCriterion
                    };
                    listNormaPointsBufferZone.Add(zone);
                    // Увиличить ID на 1 для следующей буферной зоны
                    tempID++;
                }
            }
        }

        /// <summary>
        /// Вернуть слой граничных точек зоны анализа
        /// </summary>
        /// <returns>Слой с зоной анализа</returns>
        public SublayerLocation GetSublayerBorderPointsTerritory()
        {
            return sublayerBorderPointsTerritory;
        }

        /// <summary>
        /// Вернуть слой объектов социальной инфраструктуры
        /// </summary>
        /// <returns>Слой с объектами социальной инфраструктуры</returns>
        public SublayerFacility GetSublayerFacility()
        {
            return sublayerFacility;
        }

        /// <summary>
        /// Вернуть слой полигонов
        /// </summary>
        /// <returns>Слой с полигонами</returns>
        public SublayerPolygon GetSublayerPolygonIcon()
        {
            return sublayerPolygonrIcon;
        }

        /// <summary>
        /// Вернуть слой полигонов
        /// </summary>
        /// <returns>Слой с полигонами</returns>
        public SublayerPolygon GetSublayerPolygonCheck()
        {
            return sublayerPolygonCheck;
        }

        /// <summary>
        /// Вернуть слой пользовательских кандидатов
        /// </summary>
        /// <returns>Слой с пользовательскими кандидатами</returns>
        public SublayerLocation GetSublayerUserPoints()
        {
            return sublayerUserPoints;
        }

        /// <summary>
        /// Вернуть слой авто-оптимумов
        /// </summary>
        /// <returns>Слой с авто-оптимумами</returns>
        public SublayerLocation GetSublayerAutoUserPoints()
        {
            return sublayerAutoPoints;
        }

        /// <summary>
        /// Вернуть слой нормы на душу населения
        /// </summary>
        /// <returns>Слой с нормой</returns>
        public SublayerLocation GetSublayerNorma()
        {
            return sublayerNormaFacility;
        }
    }
}