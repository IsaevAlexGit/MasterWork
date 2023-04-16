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

        // Название объекта инфраструктуры
        public string nameObjectFacility;
        // Загруженный пользователем значок для отображения объектов инфраструктуры - путь и картинка
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
        public List<Image> array_icons = new List<Image>();

        // Состояние нижней панели "Open"-"Close"
        public string stateOfBottomPanel = "";

        // Список оптимальных зон
        public List<OptimalZone> _optimalZones = new List<OptimalZone>();





        // Библиотечный слой со всеми объектами социальной инфраструктуры
        private readonly GMapOverlay _overlayFacility = new GMapOverlay("Объекты социальной инфраструктуры");
        // Слой со всеми объектами социальной инфраструктуры
        public SublayerFacility sublayerFacility = new SublayerFacility("", "");
        // Список объектов инфраструктуры из файла
        public List<Facility> listUserFacilities;




        // Библиотечный слой с граничными точками территории
        private readonly GMapOverlay _overlayWithBorderPointsTerritoryForUser = new GMapOverlay("Граничные точки территории");
        // Слой с граничными точками территории
        public SublayerLocation sublayerBorderPointsTerritory = new SublayerLocation("", "");
        // Список граничных точек территории
        public List<Location> listUserPointsBorderTerritory;




        // Библиотечный слой с полигонами
        private readonly GMapOverlay _overlayQuar = new GMapOverlay("");
        // Слой со всеми полигонами
        public SublayerQuar sublayerQuarPolygon = new SublayerQuar("", "");
        // Библиотечный слой с полигонами для точечной раскраски
        private readonly GMapOverlay _overlayQuarIcon = new GMapOverlay("");
        // Слой со всеми полигонами для точечной раскраски
        public SublayerQuar sublayerQuarIcon = new SublayerQuar("", "");
        // Библиотечный слой для проверки вхождения точки в полигоны
        private readonly GMapOverlay _overlayQuarCheck = new GMapOverlay("");
        // Слой для проверки вхождения точки в полигоны
        public SublayerQuar sublayerQuarCheck = new SublayerQuar("", "");
        // Список полигонов из файла
        public List<Quar> listUserQuartet;
        // Временный список для граничных точек полигонов
        public List<PointLatLng> tempListForQuar;
        // Количество оттенков для раскраски полигонов по критериям
        public int numberOfShadesCriterion = 8;
        public double maxCriterionPolygonColoring, minCriterionPolygonColoring;
        public double maxCriterionIconColoring, minCriterionIconColoring;




        // Библиотечный слой с маркерами пользователя
        private readonly GMapOverlay _overlayUserFacility = new GMapOverlay("Точки-кандидаты пользователя");
        // Слой с маркерами пользователя
        public SublayerLocation sublayerUserFacility = new SublayerLocation("", @"Icon/iconUserFacility.png");
        // Список точек-кандидатов пользователя
        public List<Location> listPointsUser = new List<Location>();
        // Список буферных зон пользователя
        public List<BufferZone> listPointsBufferZone = new List<BufferZone>();
        // Ограничение на установку пользовательских маркеров на карте
        public int limitUserPoines = 30;
        // Радиус буферных зон по умолчанию
        public int radiusBufferZone = 500;




        // Библиотечный слой с автомаркерами
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
        public int radiusNormBufferZone = 500;
        // Норма объекта инфраструктуры и населения
        public int implementedRateOfInfrastructureFacilities = 1;
        public int implementedPopulationRate = 1900;



        public MapModel() { }

        /// <summary>
        /// Инициализация слоев и списков данными
        /// </summary>
        public void _InitializationSublayersAndLists()
        {
            try
            {
                // Заполнить массив значков для точечной раскраски
                for (int i = 1; i <= 10; i++)
                    array_icons.Add((Bitmap)Image.FromFile(@"Icon/IconsColoring/" + i.ToString() + ".png"));

                // Преобразовать путь к файлу в Image
                Bitmap bitmapUserIconFacility = (Bitmap)Image.FromFile(pathToIconObjectFacility);
                iconFacility = bitmapUserIconFacility;

                // Задать название слоя как название объекта инфраструктуры
                sublayerFacility.nameOfOverlay = nameObjectFacility;
                // Задать загруженную иконку слою
                sublayerFacility.iconOfOverlay = pathToIconObjectFacility;
                // Слой для объектов инфраструктуры
                sublayerFacility.listWithFacility = listUserFacilities;
                sublayerFacility.overlay = _overlayFacility;
                sublayerFacility.overlay.IsVisibile = false;

                // Слой для граничных точек территории
                sublayerBorderPointsTerritory.listWithLocation = listUserPointsBorderTerritory;
                sublayerBorderPointsTerritory.overlay = _overlayWithBorderPointsTerritoryForUser;
                sublayerBorderPointsTerritory.overlay.IsVisibile = false;

                // Слой для полигонов - площадная раскраска
                sublayerQuarPolygon.listWithQuar = listUserQuartet;
                sublayerQuarPolygon.overlay = _overlayQuar;
                sublayerQuarPolygon.overlay.IsVisibile = false;
                // Слой для полигонов - точечная раскраска
                sublayerQuarIcon.listWithQuar = sublayerQuarPolygon.listWithQuar;
                sublayerQuarIcon.overlay = _overlayQuarIcon;
                sublayerQuarIcon.overlay.IsVisibile = false;
                // Слой для полигонов - принадлежит ли точка пользователя поставленная на карте одному из полигонов
                sublayerQuarCheck.listWithQuar = sublayerQuarPolygon.listWithQuar;
                sublayerQuarCheck.overlay = _overlayQuarCheck;
                sublayerQuarCheck.overlay.IsVisibile = false;

                // Слой для пользовательских кандидатов
                sublayerUserFacility.listWithLocation = listPointsUser;
                sublayerUserFacility.overlay = _overlayUserFacility;
                sublayerUserFacility.overlay.IsVisibile = false;

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
        public DataTable CreateTableFromQuartets()
        {
            // Создание таблицы
            DataTable dateTableQuartets = new DataTable();
            // Добавление столбцов для таблицы
            dateTableQuartets.Columns.Add("ID", typeof(int));
            dateTableQuartets.Columns.Add("CountBorderPoints", typeof(int));
            dateTableQuartets.Columns.Add("ListBorderPoints", typeof(List<Location>));
            dateTableQuartets.Columns.Add("X", typeof(double));
            dateTableQuartets.Columns.Add("Y", typeof(double));
            // Критериев загружено пользователем для каждого объекта инфраструктуры
            for (int i = 0; i < listUserCriterion.Count; i++)
                dateTableQuartets.Columns.Add("Criterion" + i.ToString(), typeof(double));

            // Заполнение таблицы данными из списка полигонов
            for (int i = 0; i < listUserQuartet.Count; i++)
            {
                DataRow row = dateTableQuartets.NewRow();
                row["ID"] = listUserQuartet[i].idQuartet;
                row["CountBorderPoints"] = listUserQuartet[i].countBoundaryPoints;
                row["ListBorderPoints"] = listUserQuartet[i].listBoundaryPoints;
                row["X"] = listUserQuartet[i].xCentreOfQuartet;
                row["Y"] = listUserQuartet[i].yCentreOfQuartet;
                for (int j = 0; j < listUserCriterion.Count; j++)
                    row["Criterion" + j.ToString()] = listUserQuartet[i].valuesEveryCriterionForQuartet[j];
                dateTableQuartets.Rows.Add(row);
            }
            return dateTableQuartets;
        }

        /// <summary>
        /// Определение цвета, в который будет окрашен полигон
        /// </summary>
        /// <param name="valueForColoring">Число для раскраски</param>
        /// <param name="minValueColoring">Минимум раскраски</param>
        /// <param name="maxValueColoring">Максимум раскраски</param>
        /// <param name="CountOfGrids">Количество оттенков</param>
        /// <param name="CountOfSteps">Количество шагов</param>
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
        /// Вычисление для каждого полигона у слоя sublayerUserFacility количества вошедших в него критериев
        /// </summary>
        public void CreateListBufferZones()
        {
            // Очистка списка буферных зон
            listPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            // Цикл по всем буферным зонам пользователя
            for (int i = 0; i < sublayerUserFacility.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона создание нового списка
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем полигонам территории
                for (int j = 0; j < sublayerQuarPolygon.listWithQuar.Count; j++)
                {
                    // Центр полигона
                    PointLatLng point = new PointLatLng(sublayerQuarPolygon.listWithQuar[j].xCentreOfQuartet,
                        sublayerQuarPolygon.listWithQuar[j].yCentreOfQuartet);

                    // Если центр полигона вошел в радиус буферной зоны, то суммируем все частные критерии данного полигона
                    if (sublayerUserFacility.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                            valuesCriterion[k] += sublayerQuarPolygon.listWithQuar[j].valuesEveryCriterionForQuartet[k];
                    }
                }
                // Когда мы для буферной зоны нашли количество вошедших в него полигонов и просуммировали все частные критерии
                // В список заносим данные о данной буферной зоне
                BufferZone zone = new BufferZone
                {
                    idBufferZone = tempID,
                    x = sublayerUserFacility.listWithLocation[i].x,
                    y = sublayerUserFacility.listWithLocation[i].y,
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

                // Цикл по всем полигонам территории
                for (int j = 0; j < sublayerQuarPolygon.listWithQuar.Count; j++)
                {
                    // Центр полигона
                    PointLatLng point = new PointLatLng(sublayerQuarPolygon.listWithQuar[j].xCentreOfQuartet,
                        sublayerQuarPolygon.listWithQuar[j].yCentreOfQuartet);

                    // Если центр полигона вошел в радиус буферной зоны, то суммируем все частные критерии данного полигона
                    if (sublayerAutoPoints.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                            valuesCriterion[k] += sublayerQuarPolygon.listWithQuar[j].valuesEveryCriterionForQuartet[k];
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
        public void SetMaximumAndMinumForCriterionPolygonColoring(int indexOfSelectedCriterion)
        {
            // Список значений критерия
            List<double> valuesCriterion = new List<double>();
            for (int i = 0; i < listUserQuartet.Count; i++)
            {
                int N = listUserQuartet[i].countBoundaryPoints;
                valuesCriterion.Add(listUserQuartet[i].valuesEveryCriterionForQuartet[indexOfSelectedCriterion]);
            }
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
            for (int i = 0; i < listUserQuartet.Count; i++)
            {
                int N = listUserQuartet[i].countBoundaryPoints;
                valuesCriterion.Add(listUserQuartet[i].valuesEveryCriterionForQuartet[indexOfSelectedCriterion]);
            }
            // Найти минимум и максимум в списке значений выбранного критерия
            minCriterionIconColoring = valuesCriterion.Min();
            maxCriterionIconColoring = valuesCriterion.Max();
        }

        // Геттеры слоев
        public SublayerLocation GetSublayerBorderPointsTerritory() { return sublayerBorderPointsTerritory; }
        public SublayerFacility GetSublayerFacility() { return sublayerFacility; }
        public SublayerQuar GetSublayerQuarIcon() { return sublayerQuarIcon; }
        public SublayerQuar GetSublayerQuarCheck() { return sublayerQuarCheck; }
        public SublayerLocation GetSublayerUserPoints() { return sublayerUserFacility; }
        public SublayerLocation GetSublayerAutoUserPoints() { return sublayerAutoPoints; }
        public SublayerLocation GetSublayerNorma() { return sublayerNormaFacility; }







        /// <summary>
        /// Найти количество жителей в городе
        /// </summary>
        /// <returns></returns>
        //public int CalculateCountPeopleCity()
        //{
        //    int peopleCity = 0;
        //    for (int i = 0; i < listUserQuartet.Count; i++)
        //        peopleCity += listUserQuartet[i].countOfResidents + listUserQuartet[i].countOfRetired;
        //    return peopleCity;
        //}

        /// <summary>
        /// Найти количество ОСИ в городе
        /// </summary>
        /// <returns></returns>
        //public int CalculateCountOSICity()
        //{
        //    int countOSICity = 0;
        //    for (int i = 0; i < listUserQuartet.Count; i++)
        //        countOSICity += listPointslistUserQuartetQuartet[i].countOfPharmacy;
        //    return countOSICity;
        //}

        /// <summary>
        /// Вычисление для каждого полигона у слоя _SublayerUserPharmacy количества вошедших аптек, жителей и пенсионеров
        /// </summary>
        public void CreateListNormaBufferZones()
        {
            //// Очистка списка буферных зон
            //listNormaPointsBufferZone.Clear();
            //// Начальные значения
            //int tempPharma = 0;
            //int tempResidents = 0;
            //int tempRetired = 0;
            //int tempID = 1;

            //for (int i = 0; i < sublayerNormaPharmacy.overlay.Polygons.Count; i++)
            //{
            //    // Перед обработкой очередного полигона обнуление счетчиков после предыдущих вычислений
            //    tempPharma = 0;
            //    tempResidents = 0;
            //    tempRetired = 0;

            //    // Цикл по всем кварталам города
            //    for (int j = 0; j < sublayerQuartetPolygon.listWithQuartets.Count; j++)
            //    {
            //        // Центр квартала
            //        PointLatLng point = new PointLatLng(sublayerQuartetPolygon.listWithQuartets[j].xCentreOfQuartet,
            //            sublayerQuartetPolygon.listWithQuartets[j].yCentreOfQuartet);
            //        // Если точка квартала вошла в радиус буферной зоны, то суммируем число аптек, жителей и пенсионеров в данном квартале
            //        if (sublayerNormaPharmacy.overlay.Polygons[i].IsInside(point))
            //        {
            //            tempPharma += sublayerQuartetPolygon.listWithQuartets[j].countOfPharmacy;
            //            tempResidents += sublayerQuartetPolygon.listWithQuartets[j].countOfResidents;
            //            tempRetired += sublayerQuartetPolygon.listWithQuartets[j].countOfRetired;
            //        }
            //    }
            //    // Когда мы для круга нашли количество вошедших в него кварталов и просуммировали все аптеки, жителей и пенсионеров этих кварталов
            //    // В список заносим данные о данной буферной зоне

            //    // Если в точке (0, 0, 0), то нам не нужна такая точка для рассмотрения
            //    if (tempPharma != 0 || tempResidents != 0 || tempRetired != 0)
            //    {
            //        BufferZone zone = new BufferZone
            //        {
            //            idBufferZone = tempID,
            //            x = sublayerNormaPharmacy.listWithLocation[i].x,
            //            y = sublayerNormaPharmacy.listWithLocation[i].y,
            //            lengthRadiusSearch = radiusBufferZone,
            //            countOfPharmacy = tempPharma,
            //            countOfResidents = tempResidents,
            //            countOfRetired = tempRetired
            //        };
            //        // Добавить в список буферную зону, в которой не 0, 0, 0
            //        listNormaPointsBufferZone.Add(zone);
            //        // Увиличить ID на 1 для следующей буферной зоны
            //        tempID++;
            //    }
            //}
        }  
    }
}