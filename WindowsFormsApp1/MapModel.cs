using System;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Optimum
{
    public class MapModel
    {







        // Слои из библиотеки GMap.NET для отображения их на карте








        // Список граничных точек города для отображения на карте
        public List<Location> listPointsCity = new List<Location>();
        // Список граничных точек города для проверки вхождения точек пользователя в его границы
        public List<Location> listWithBorderPointsCityForUser = new List<Location>();






        // Настройка для того, чтобы программа работала на английской версии Windows
        private readonly CultureInfo _cultureInfo = new CultureInfo("ru-RU");






        public int radiusNormBufferZone = 500;

        // Временный список для граничных точек квартала
        public List<PointLatLng> tempListForQuartet;

        // Временный список для граничных точек квартала
        public List<PointLatLng> tempListForQuar;



        public double weightNormResidents = 0.33;
        public double weightNormRetired = 0.33;
        public double weightNormPharmacy = 0.34;

        // Норма ОСИ и населения
        public int implementedRateOfInfrastructureFacilities = 1;
        public int implementedPopulationRate = 1900;



        // Слой с автомаркерами
        private readonly GMapOverlay _overlayNorma = new GMapOverlay("Норма");
        // Список автобуферных зон
        public List<BufferZone> listNormaPointsBufferZone = new List<BufferZone>();
        // Слой с автомаркерами
        public SublayerLocation sublayerNormaPharmacy = new SublayerLocation("Норма", @"Icon/iconBestOptimum.png");
        // Список автоточек
        public List<Location> listNorma = new List<Location>();



        // Цвет интерфейса
        public Color colorApplication;
        public Color colorElements;

        // Название объекта инфраструктуры
        public string nameObjectFacility;
        // Загруженный пользователем значок для отображения аптек
        public string pathToIconObjectFacility;
        public Image iconFacility;
        // public string iconPharmacyUser = @"Icon/iconPharmacy_v3.png";

        // Список критериев оптимальности
        public List<Criterion> listUserCriterion = new List<Criterion>();

        // Точка центрирования карты
        public Location centerMap = new Location();

        // Флаги изменились ли файлы территории полигонов и критерии в настройках
        public bool flagChangePolygon = false;
        public bool flagChangeTerritory = false;
        public bool flagChangeCriterion = false;
        public bool flagLoadFileNorma = false;

        // Слой со всеми аптеками
        private readonly GMapOverlay _overlayFacility = new GMapOverlay("ОСИ");
        // Слой с полигонами кварталов города
        public SublayerFacility sublayerFacility = new SublayerFacility("", "");
        // Список объектов инфраструктуры из файла
        public List<Facility> listUserFacilities;


        // Слой с граничными точками города для проверки вхождения точек пользователя
        private readonly GMapOverlay _overlayWithBorderPointsTerritoryForUser = new GMapOverlay("ГраничныеТочкиТерритории");
        // Слой с граничными точками территории
        public SublayerLocation sublayerBorderPointsTerritory = new SublayerLocation("", "");
        // Список граничных точек территории
        public List<Location> listUserPointsBorderTerritory;


        // Слой с полигонами кварталов города
        private readonly GMapOverlay _overlayQuar = new GMapOverlay("");
        // Слой со всеми аптеками
        public SublayerQuar sublayerQuarPolygon = new SublayerQuar("", "");
        // Слой для раскраски по значкам
        private readonly GMapOverlay _overlayQuarIcon = new GMapOverlay("");
        public SublayerQuar sublayerQuarIcon = new SublayerQuar("", "");
        // Слой для проверки вхождения точки в квартал
        private readonly GMapOverlay _overlayQuarCheck = new GMapOverlay("");
        // Слой для проверки вхождения точки в квартал
        public SublayerQuar sublayerQuarCheck = new SublayerQuar("", "");
        // Список полигонов из файла
        public List<Quar> listUserQuartet;

        // Массив значков для точечной раскраски
        public List<Image> array_icons = new List<Image>();

        // Состояние нижней панели "Open"-"Close"
        public string stateOfBottomPanel = "";


        // Слой с маркерами пользователя
        private readonly GMapOverlay _overlayUserFacility = new GMapOverlay("ПользовательскиеТочки");
        // Слой с маркерами пользователя
        public SublayerLocation sublayerUserFacility = new SublayerLocation("", @"Icon/iconUserFacility.png");
        // Список точек, поставленных пользователем на карте
        public List<Location> listPointsUser = new List<Location>();
        // Бизнес-ограничение на установку пользовательских маркеров на карте
        public int limitUserPoines = 30;
        // Радиус буферной зоны, по умолчнаию 500 метров        
        public int radiusBufferZone = 500;


        // Список всех оптимумов
        public List<OptimalZone> _optimalZones = new List<OptimalZone>();


        // Слой с автомаркерами
        private readonly GMapOverlay _overlayAutoUserPharmacy = new GMapOverlay("АвтоОСИ");
        // Список автобуферных зон
        public List<BufferZone> listAutoPointsBufferZone = new List<BufferZone>();
        // Слой с автомаркерами
        public SublayerLocation sublayerAutoUserPharmacy = new SublayerLocation("АвтоОСИ", @"Icon/iconBestOptimum.png");
        // Список автоточек
        public List<Location> listAutoPointsUser = new List<Location>();






        /// <summary>
        /// Инициализация всех слоев
        /// </summary>
        public MapModel() { }

        /// <summary>
        /// Инициализация слоем и списков данными
        /// </summary>
        public void _InitializationSublayersAndLists()
        {
            try
            {
                // Заполнить массив иконок для точечной раскраски
                for (int i = 1; i <= 10; i++)
                {
                    string icon = @"Icon/IconsColoring/" + i.ToString() + ".png";
                    Bitmap bitmapIcon = (Bitmap)Bitmap.FromFile(icon);
                    Image imageIcon = (Image)bitmapIcon;
                    array_icons.Add(imageIcon);
                }

                // Преобразовать путь к файлу в Image
                Bitmap bb = (Bitmap)Image.FromFile(pathToIconObjectFacility);
                iconFacility = bb;

                // Задать название этого слоя как название ОСИ
                sublayerFacility.nameOfOverlay = nameObjectFacility;
                // Задать загруженную иконку слою
                sublayerFacility.iconOfOverlay = pathToIconObjectFacility;
                // Слой для ОСИ
                sublayerFacility.listWithFacility = listUserFacilities;
                sublayerFacility.overlay = _overlayFacility;
                sublayerFacility.overlay.IsVisibile = false;

                // Слой для граничных точек территории
                sublayerBorderPointsTerritory.listWithLocation = listUserPointsBorderTerritory;
                sublayerBorderPointsTerritory.overlay = _overlayWithBorderPointsTerritoryForUser;
                sublayerBorderPointsTerritory.overlay.IsVisibile = false;
                // Слой для кварталов - рисование полигонов
                sublayerQuarPolygon.listWithQuar = listUserQuartet;
                sublayerQuarPolygon.overlay = _overlayQuar;
                sublayerQuarPolygon.overlay.IsVisibile = false;
                // Слой для кварталов - рисование точек
                sublayerQuarIcon.listWithQuar = sublayerQuarPolygon.listWithQuar;
                sublayerQuarIcon.overlay = _overlayQuarIcon;
                sublayerQuarIcon.overlay.IsVisibile = false;
                // Слой для кварталов - принадлежит ли точка пользователя поставленная на карте одному из полигонов
                sublayerQuarCheck.listWithQuar = sublayerQuarPolygon.listWithQuar;
                sublayerQuarCheck.overlay = _overlayQuarCheck;
                sublayerQuarCheck.overlay.IsVisibile = false;
                // Слой для маркеров, который установил пользователь
                sublayerUserFacility.listWithLocation = listPointsUser;
                sublayerUserFacility.overlay = _overlayUserFacility;
                sublayerUserFacility.overlay.IsVisibile = false;




                // Авто-поиск
                sublayerAutoUserPharmacy.listWithLocation = listAutoPointsUser;
                sublayerAutoUserPharmacy.overlay = _overlayAutoUserPharmacy;
                sublayerAutoUserPharmacy.overlay.IsVisibile = false;
                // Норма на душу населения
                sublayerNormaPharmacy.listWithLocation = listNorma;
                sublayerNormaPharmacy.overlay = _overlayNorma;
                sublayerNormaPharmacy.overlay.IsVisibile = false;
            }
            catch
            {
                // Аварийный выход
                Environment.Exit(0);
            }
        }







        // По умолчанию количество оттенков для раскраски полигонов по любому критерию
        public int numberOfShadesCriterion = 8;
        public double maxCriterionPolygonColoring, minCriterionPolygonColoring;

        /// <summary>
        /// Найти максимум и минимум по выбранному критерию для раскраски
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




        // По умолчанию количество оттенков для раскраски полигонов по любому критерию
        public double maxCriterionIconColoring, minCriterionIconColoring;

        /// <summary>
        /// Найти максимум и минимум по выбранному критерию для раскраски
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






        ///// <summary>
        ///// Заполнение списка граничных точек города для проверки вхождения в него точек пользователя
        ///// </summary>
        ///// <param name="_FileNameForReading">Путь к файлу</param>
        ///// <returns>Список граничных точек для города</returns>
        //private List<Location> _ReadingBorderCityForUser(string fileNameForReading)
        //{
        //    using (StreamReader filereader = new StreamReader(fileNameForReading, Encoding.GetEncoding(1251)))
        //    {
        //        while (!filereader.EndOfStream)
        //        {
        //            string OneString = filereader.ReadLine();
        //            string[] SplitOneString = OneString.Split(new char[] { ';' });
        //            listWithBorderPointsCityForUser.Add(new Location(Convert.ToDouble(SplitOneString[0], _cultureInfo),
        //                Convert.ToDouble(SplitOneString[1], _cultureInfo)));
        //        }
        //    }
        //    return listWithBorderPointsCityForUser;
        //}

        ///// <summary>
        ///// Заполнение списка граничных точек города
        ///// </summary>
        ///// <param name="_FileNameForReading">Путь к файлу</param>
        ///// <returns>Список граничных точек для города</returns>
        //private List<Location> _ReadingPointsCity(string fileNameForReading)
        //{
        //    using (StreamReader filereader = new StreamReader(fileNameForReading, Encoding.GetEncoding(1251)))
        //    {
        //        while (!filereader.EndOfStream)
        //        {
        //            string OneString = filereader.ReadLine();
        //            string[] SplitOneString = OneString.Split(new char[] { ';' });
        //            listPointsCity.Add(new Location(Convert.ToDouble(SplitOneString[0], _cultureInfo),
        //                Convert.ToDouble(SplitOneString[1], _cultureInfo)));
        //        }
        //    }
        //    return listPointsCity;
        //}

        ///// <summary>
        ///// Заполнение списка аптек
        ///// </summary>
        ///// <param name="_FileNameForReading">Путь к файлу</param>
        ///// <returns>Список с данными об аптеках города</returns>
        //private List<SocialFacility> _ReadingPointsPharmacy(string fileNameForReading)
        //{
        //    using (StreamReader filereader = new StreamReader(fileNameForReading, Encoding.GetEncoding(1251)))
        //    {
        //        while (!filereader.EndOfStream)
        //        {
        //            string OneString = filereader.ReadLine();
        //            string[] SplitOneString = OneString.Split(new char[] { ';' });
        //            listPointsPharmacy.Add(new SocialFacility
        //                (Convert.ToInt32(SplitOneString[0]), Convert.ToDouble(SplitOneString[1], _cultureInfo),
        //                Convert.ToDouble(SplitOneString[2], _cultureInfo), SplitOneString[3], SplitOneString[4], SplitOneString[5],
        //                SplitOneString[6], SplitOneString[7], Convert.ToDouble(SplitOneString[8], _cultureInfo),
        //                Convert.ToDouble(SplitOneString[9], _cultureInfo)));
        //        }
        //    }
        //    return listPointsPharmacy;
        //}



        ///// <summary>
        ///// Заполнение списка кварталов
        ///// </summary>
        ///// <param name="_FileNameForReading">Путь к файлу</param>
        ///// <returns>Список с данными о кварталах города</returns>
        //private List<Quartet> _ReadingPointsQuartets(string fileNameForReading)
        //{
        //    listPointsQuartet.Clear();
        //    List<Location> tempListPoints = new List<Location>();
        //    using (StreamReader filereader = new StreamReader(fileNameForReading, Encoding.GetEncoding(1251)))
        //    {
        //        while (!filereader.EndOfStream)
        //        {
        //            string OneString = filereader.ReadLine();
        //            string[] SplitOneString = OneString.Split(new char[] { ';' });

        //            int ID = Convert.ToInt32(SplitOneString[0]);
        //            int CountBoundaryPoints = Convert.ToInt32(SplitOneString[1]);
        //            tempListPoints = new List<Location>();

        //            for (int j = 2; j <= CountBoundaryPoints * 2;)
        //            {
        //                tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j], _cultureInfo),
        //                    Convert.ToDouble(SplitOneString[j + 1], _cultureInfo)));
        //                j += 2;
        //            }

        //            double CentreX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2], _cultureInfo);
        //            double CentreY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3], _cultureInfo);
        //            int CountOfPharmacy = Convert.ToInt32(SplitOneString[CountBoundaryPoints * 2 + 4]);
        //            int CountOfResidents = Convert.ToInt32(SplitOneString[CountBoundaryPoints * 2 + 5]);
        //            int CountOfRetired = Convert.ToInt32(SplitOneString[CountBoundaryPoints * 2 + 6]);
        //            listPointsQuartet.Add(new Quartet(ID, CountBoundaryPoints, tempListPoints, CentreX, CentreY,
        //                CountOfPharmacy, CountOfResidents, CountOfRetired));
        //        }
        //    }
        //    return listPointsQuartet;
        //}




        /// <summary>
        /// Создание таблицы из кварталов
        /// </summary>
        /// <returns>Таблица, заполненная данными из списка кварталов</returns>
        public DataTable CreateTableFromQuartets()
        {
            // Создание таблицы
            DataTable dateTableQuartets = new DataTable();
            // Добавление столбцов для таблицы
            dateTableQuartets.Columns.Add("ID", typeof(int));
            dateTableQuartets.Columns.Add("N", typeof(int));
            dateTableQuartets.Columns.Add("List", typeof(List<Location>));
            dateTableQuartets.Columns.Add("X", typeof(double));
            dateTableQuartets.Columns.Add("Y", typeof(double));
            // Критериев загружено пользователем для каждого объекта инфраструктуры
            for (int i = 0; i < listUserCriterion.Count; i++)
                dateTableQuartets.Columns.Add("Criterion" + i.ToString(), typeof(double));

            // Заполнение таблицы данными из списка кварталов
            for (int i = 0; i < listUserQuartet.Count; i++)
            {
                DataRow row = dateTableQuartets.NewRow();
                row["ID"] = listUserQuartet[i].idQuartet;
                row["N"] = listUserQuartet[i].countBoundaryPoints;
                row["List"] = listUserQuartet[i].listBoundaryPoints;
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





        // Список буферных зон
        public List<BufferZone> listPointsBufferZone = new List<BufferZone>();
        /// <summary>
        /// Вычисление для каждого полигона у слоя sublayerUserFacility количества вошедших в него критерий
        /// </summary>
        public void CreateListBufferZones()
        {
            // Очистка списка буферных зон
            listPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            // Цикл по всем окружностям пользователя
            for (int i = 0; i < sublayerUserFacility.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона обнуление счетчиков после предыдущих вычислений
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем кварталам города
                for (int j = 0; j < sublayerQuarPolygon.listWithQuar.Count; j++)
                {
                    // Центр квартала
                    PointLatLng point = new PointLatLng(sublayerQuarPolygon.listWithQuar[j].xCentreOfQuartet,
                        sublayerQuarPolygon.listWithQuar[j].yCentreOfQuartet);

                    // Если точка квартала вошла в радиус буферной зоны, то суммируем число аптек, жителей и пенсионеров в данном квартале
                    if (sublayerUserFacility.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                        {
                            valuesCriterion[k] += sublayerQuarPolygon.listWithQuar[j].valuesEveryCriterionForQuartet[k];
                        }
                    }
                }
                // Когда мы для круга нашли количество вошедших в него кварталов и просуммировали все аптеки, жителей и пенсионеров этих кварталов
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
        /// Вычисление для каждого полигона у слоя _SublayerUserPharmacy количества вошедших аптек, жителей и пенсионеров
        /// </summary>
        public void CreateListAutoBufferZones()
        {
            // Очистка списка буферных зон
            listAutoPointsBufferZone.Clear();
            // Начальные значения
            List<double> valuesCriterion;
            int tempID = 1;

            // Цикл по всем окружностям пользователя
            for (int i = 0; i < sublayerAutoUserPharmacy.overlay.Polygons.Count; i++)
            {
                // Перед обработкой очередного полигона обнуление счетчиков после предыдущих вычислений
                valuesCriterion = new List<double>();

                for (int q = 0; q < listUserCriterion.Count; q++)
                    valuesCriterion.Add(0);

                // Цикл по всем кварталам города
                for (int j = 0; j < sublayerQuarPolygon.listWithQuar.Count; j++)
                {
                    // Центр квартала
                    PointLatLng point = new PointLatLng(sublayerQuarPolygon.listWithQuar[j].xCentreOfQuartet,
                        sublayerQuarPolygon.listWithQuar[j].yCentreOfQuartet);

                    // Если точка квартала вошла в радиус буферной зоны, то суммируем число аптек, жителей и пенсионеров в данном квартале
                    if (sublayerAutoUserPharmacy.overlay.Polygons[i].IsInside(point))
                    {
                        for (int k = 0; k < listUserCriterion.Count; k++)
                        {
                            valuesCriterion[k] += sublayerQuarPolygon.listWithQuar[j].valuesEveryCriterionForQuartet[k];
                        }
                    }
                }

                bool flag = false;
                for (int l = 0; l < valuesCriterion.Count; l++)
                {
                    if (valuesCriterion[l] > 0)
                        flag = true;
                }

                // Добавили точку для авто-оптимума, только если она не одни нули имеет в частных критериях
                if (flag)
                {
                    BufferZone zone = new BufferZone
                    {
                        idBufferZone = tempID,
                        x = sublayerAutoUserPharmacy.listWithLocation[i].x,
                        y = sublayerAutoUserPharmacy.listWithLocation[i].y,
                        lengthRadiusSearch = radiusBufferZone,
                        arrayValuesCriterionOnZone = valuesCriterion
                    };
                    // Добавить в список буферную зону, в которой не 0, 0, 0
                    listAutoPointsBufferZone.Add(zone);
                    // Увиличить ID на 1 для следующей буферной зоны
                    tempID++;
                }
            }
        }

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

        ///// <summary>
        ///// Найти количество ОСИ в городе
        ///// </summary>
        ///// <returns></returns>
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





        /// <summary>
        /// Загрузить данные пользователя о кварталах и населении
        /// </summary>
        public void ChangeFileForQuartet(string fileName)
        {
            //// Сделать слой невидимым, убрать маркеры и полигоны, очистить слой
            //sublayerQuartetPolygon.overlay.IsVisibile = false;
            //sublayerQuartetPolygon.overlay.Markers.Clear();
            //sublayerQuartetPolygon.overlay.Polygons.Clear();
            //sublayerQuartetPolygon.overlay.Clear();

            //sublayerQuartetCheck.overlay.IsVisibile = false;
            //sublayerQuartetCheck.overlay.Markers.Clear();
            //sublayerQuartetCheck.overlay.Polygons.Clear();
            //sublayerQuartetCheck.overlay.Clear();

            //sublayerQuartetIcon.overlay.IsVisibile = false;
            //sublayerQuartetIcon.overlay.Markers.Clear();
            //sublayerQuartetIcon.overlay.Polygons.Clear();
            //sublayerQuartetIcon.overlay.Clear();

            //// Очистить список кварталов
            //listPointsQuartet.Clear();
            //// Считать кварталы пользователя
            //sublayerQuartetPolygon.listWithQuartets = _ReadingPointsQuartets(fileName);
            //sublayerQuartetCheck.listWithQuartets = sublayerQuartetPolygon.listWithQuartets;
            //sublayerQuartetIcon.listWithQuartets = sublayerQuartetPolygon.listWithQuartets;
            //sublayerQuartetPolygon.overlay.IsVisibile = true;
        }


        public SublayerLocation GetSublayerBorderPointsTerritory() { return sublayerBorderPointsTerritory; }
        public SublayerFacility GetSublayerFacility() { return sublayerFacility; }
        public SublayerQuar GetSublayerQuarIcon() { return sublayerQuarIcon; }
        public SublayerQuar GetSublayerQuarCheck() { return sublayerQuarCheck; }
        public SublayerLocation GetSublayerUserPoints() { return sublayerUserFacility; }


        public SublayerLocation GetSublayerAutoUserPoints() { return sublayerAutoUserPharmacy; }
        public SublayerLocation GetSublayerNorma() { return sublayerNormaPharmacy; }
    }
}