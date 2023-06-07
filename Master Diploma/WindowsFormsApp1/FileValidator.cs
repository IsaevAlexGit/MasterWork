using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Optimum
{
    // Класс "Валидация загружаемых и обрабатываемых файлов"
    public class FileValidator
    {
        // Слой для проверки принадлежности точек зоне анализа
        private SublayerLocation _sublayerBorderPointsTerritory = new SublayerLocation();
        // Настройка для того, чтобы программа работала на английской версии Windows
        private readonly CultureInfo _cultureInfo = new CultureInfo("ru-RU");

        // Константы для ошибок чтения любого CSV-файла
        private const string NOT_EXTST_FILE = "Такого файла не существует";
        private const string NOT_CSV_FILE = "Выберите файл .csv расширения";
        private const string NULL_NAME_FILE = "Имя файла отсутствует";
        private const string FILE_READ_ONLY = "Данный файл доступен только для чтения";
        private const string FILE_EMPTY = "Данный файл не содержит данных";
        private const string FILE_HAS_BIG_SIZE = "Данный файл слишком большого размера";
        private const string SUCCESSFUL_LOAD = "Успешно";

        // Константы для проверки объектов социальной инфраструктуры из файла пользователя
        private const string COORDINATES_OUTSIDE_TERRITORY = "Координаты находятся за границами зоны анализа";
        private const string UNSUCCESSFUL_ATTEMPT_READ_DATA = "Неверный формат хранения данных";

        // Константы для проверки полигонов из файла пользователя
        private const string COUNT_OF_BOUNDARY_POINTS_LESS_THREE = "Количество граничных точек не может быть меньше трех";
        private const string VALUE_OF_CRITERION_LESS_THAN_ZERO = "Значение критерия не может быть меньше нуля";

        // Константа для проверки файла для поиска нормы на душу населения
        private const string COUNT_OF_CRITERION_LESS_THAN_TWO = "Должно быть минимум два критерия: Население и Объекты инфраструктуры";

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileValidator() { }

        /// <summary>
        /// Проверка CSV-файла, из которого загружаются данные
        /// </summary>
        /// <param name="pathToFile">Путь к файлу</param>
        /// <returns>Результат проверки файла</returns>
        public string ValidateUserFileCSV(string pathToFile)
        {
            FileInfo infoFileCSV = new FileInfo(pathToFile);
            // Если файл существует
            if (File.Exists(pathToFile))
            {
                // Если файл имеет расширение .csv
                if (infoFileCSV.Extension == ".csv")
                {
                    // Если у файла есть имя
                    if (infoFileCSV.Name != ".csv")
                    {
                        // Если файл большого размера (больше 1Мб)
                        if (infoFileCSV.Length < 1024700)
                        {
                            // Чтение данных из файла
                            using (StreamReader readerExcel = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                            {
                                string allStringsFromFileUser = readerExcel.ReadToEnd();
                                // Если файл не пустой
                                if (allStringsFromFileUser.Length != 2)
                                    return SUCCESSFUL_LOAD;
                                else
                                    return FILE_EMPTY;
                            }
                        }
                        else
                            return FILE_HAS_BIG_SIZE;
                    }
                    else
                        return NULL_NAME_FILE;
                }
                else
                    return NOT_CSV_FILE;
            }
            else
                return NOT_EXTST_FILE;
        }

        /// <summary>
        /// Проверка корректности файла с граничными точками зоны анализа
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с граничными точками</param>
        /// <returns>Результат проверки файла</returns>
        public string ValidateUserFileTerritory(string pathToFile)
        {
            // Список граничных точек
            List<Location> listBorderPointsTerritory = new List<Location>();
            CultureInfo cultureInfo = new CultureInfo("ru-RU");
            // Если файл не смог прочитаться
            try
            {
                try
                {
                    using (StreamReader fileReader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!fileReader.EndOfStream)
                        {
                            string oneString = fileReader.ReadLine();
                            string[] splitOneString = oneString.Split(new char[] { ';' });
                            // Добавить координату в список граничных точек зоны анализа
                            listBorderPointsTerritory.Add(new Location(Convert.ToDouble(splitOneString[0], cultureInfo),
                                Convert.ToDouble(splitOneString[1], cultureInfo)));
                        }

                        // Граничных точек у зоны анализа не может быть меньше трех
                        if (listBorderPointsTerritory.Count >= 3)
                            return SUCCESSFUL_LOAD;
                        else
                            return COUNT_OF_BOUNDARY_POINTS_LESS_THREE;
                    }
                }
                catch (FormatException)
                {
                    return UNSUCCESSFUL_ATTEMPT_READ_DATA;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return UNSUCCESSFUL_ATTEMPT_READ_DATA;
            }
        }

        /// <summary>
        /// Проверка корректности файла с объектами социальной инфраструктуры на целостность данных
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с объектами социальной инфраструктуры</param>
        /// <param name="listBorderPointsTerritory">Список граничных точек зоны анализа</param>
        /// <returns>Результат проверки файла</returns>
        public string ValidateUserFileFacility(string pathToFile, List<Location> listBorderPointsTerritory)
        {
            // Список объектов социальной инфраструктуры
            List<Facility> listPointsFacility = new List<Facility>();

            // Создаем полигон из граничных точек зоны анализа
            List<PointLatLng> boundaryPointsTerritoryPolygon = new List<PointLatLng>();
            // Инициализация списка с граничными точками зоны анализа
            for (int i = 0; i < listBorderPointsTerritory.Count; i++)
                boundaryPointsTerritoryPolygon.Add(new PointLatLng(listBorderPointsTerritory[i].x, listBorderPointsTerritory[i].y));
            // Создание полигона из граничных точек зоны анализа
            var polygon = new GMapPolygon(boundaryPointsTerritoryPolygon, "BorderPointsTerritory");

            // Если файл не смог прочитаться
            try
            {
                try
                {
                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!readerCsvFile.EndOfStream)
                        {
                            string oneString = readerCsvFile.ReadLine();
                            string[] splitOneString = oneString.Split(new char[] { ';' });
                            // Принадлежность точки зоне анализа
                            bool isInsydeTerritory = false;

                            // Текущий объект социальной инфраструктуры
                            PointLatLng point = new PointLatLng(Convert.ToDouble(splitOneString[1]), Convert.ToDouble(splitOneString[2]));
                            // Проверка принадлежности объекта социальной инфраструктуры зоне анализа
                            if (polygon.IsInside(point))
                                isInsydeTerritory = true;
                            else
                                isInsydeTerritory = false;

                            if (isInsydeTerritory)
                            {
                                // Массив данных с информацией об объекте социальной инфраструктуры
                                List<string> info = new List<string>();
                                for (int i = 3; i < splitOneString.Length; i++)
                                    info.Add(splitOneString[i].ToString());

                                // Добавить объект социальной инфраструктуры в список
                                listPointsFacility.Add(new Facility(Convert.ToInt32(splitOneString[0]),
                                    Convert.ToDouble(splitOneString[1], _cultureInfo), Convert.ToDouble(splitOneString[2], _cultureInfo), info));
                            }
                            else
                                return COORDINATES_OUTSIDE_TERRITORY;
                        }
                        return SUCCESSFUL_LOAD;
                    }
                }
                catch (FormatException)
                {
                    return UNSUCCESSFUL_ATTEMPT_READ_DATA;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return UNSUCCESSFUL_ATTEMPT_READ_DATA;
            }
        }

        /// <summary>
        /// Проверка корректности файла с полигонами на целостность данных
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с полигонами</param>
        /// <param name="listBorderPointsTerritory">Список граничных точек зоны анализа</param>
        /// <param name="countCriterion">Количество критериев оптимальности</param>
        /// <returns>Результат проверки файла</returns>
        public string ValidateUserFilePolygons(string pathToFile, List<Location> listBorderPointsTerritory, int countCriterion)
        {
            // Список полигонов
            List<Polygon> listPointsPolygon = new List<Polygon>();

            // Создаем полигон из граничных точек зоны анализа
            List<PointLatLng> boundaryPointsTerritoryPolygon = new List<PointLatLng>();
            // Инициализация списка с граничными точками зоны анализа
            for (int i = 0; i < listBorderPointsTerritory.Count; i++)
                boundaryPointsTerritoryPolygon.Add(new PointLatLng(listBorderPointsTerritory[i].x, listBorderPointsTerritory[i].y));
            // Создание полигона из граничных точек зоны анализа
            var polygon = new GMapPolygon(boundaryPointsTerritoryPolygon, "BordersPointsTerritory");

            // Если файл не смог прочитаться
            try
            {
                try
                {
                    // Список граничных точек для каждого из полигонов
                    List<Location> tempListPoints = new List<Location>();
                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!readerCsvFile.EndOfStream)
                        {
                            string oneString = readerCsvFile.ReadLine();
                            string[] splitOneString = oneString.Split(new char[] { ';' });
                            // Считали ID полигона
                            int ID = Convert.ToInt32(splitOneString[0]);
                            // Считали количество граничных точек полигона
                            int countBoundaryPoints = Convert.ToInt32(splitOneString[1]);

                            // Граничных точек у полигона не меньше 3
                            if (countBoundaryPoints >= 3)
                            {
                                tempListPoints = new List<Location>();
                                for (int j = 2; j <= countBoundaryPoints * 2;)
                                {
                                    tempListPoints.Add(new Location(Convert.ToDouble(splitOneString[j]), Convert.ToDouble(splitOneString[j + 1])));
                                    j += 2;
                                }

                                // Принадлежность граничных точек полигона зоне анализа
                                int numberOfEnteredPoints = 0;
                                for (int i = 0; i < tempListPoints.Count; i++)
                                {
                                    // Проверка каждой точки полигона на принадлежность зоне анализа
                                    PointLatLng point = new PointLatLng(tempListPoints[i].x, tempListPoints[i].y);
                                    if (polygon.IsInside(point))
                                        numberOfEnteredPoints++;
                                }

                                // Если все граничные точки полигона принадлежат зоне анализа
                                if (numberOfEnteredPoints == tempListPoints.Count)
                                {
                                    // Координаты центральной точки полигона
                                    double centerX = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 2]);
                                    double centerY = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 3]);
                                    bool centerPointOfPolygonIsInsydeTerritory = false;
                                    PointLatLng point = new PointLatLng(centerX, centerY);

                                    // Проверка принадлежности центральной точки полигона зоне анализа
                                    if (polygon.IsInside(point))
                                        centerPointOfPolygonIsInsydeTerritory = true;
                                    else
                                        centerPointOfPolygonIsInsydeTerritory = false;

                                    if (centerPointOfPolygonIsInsydeTerritory)
                                    {
                                        // Список значений каждого критерия у полигона
                                        List<double> countOfEveryCriterion = new List<double>();

                                        int lastCriterion = countBoundaryPoints * 2 + 4 + countCriterion;
                                        for (int j = countBoundaryPoints * 2 + 4; j < lastCriterion; j++)
                                            countOfEveryCriterion.Add(Convert.ToDouble(splitOneString[j]));

                                        // Все ли критерии >= 0
                                        bool flagValueCriterionAboveZero = true;
                                        for (int k = 0; k < countOfEveryCriterion.Count; k++)
                                            if (countOfEveryCriterion[k] < 0)
                                                flagValueCriterionAboveZero = false;

                                        if (flagValueCriterionAboveZero)
                                            // Добавить полигон в список
                                            listPointsPolygon.Add(new Polygon(ID, countBoundaryPoints, tempListPoints,
                                                centerX, centerY, countOfEveryCriterion));
                                        else
                                            return VALUE_OF_CRITERION_LESS_THAN_ZERO;
                                    }
                                    else
                                        return COORDINATES_OUTSIDE_TERRITORY;
                                }
                                else
                                    return COORDINATES_OUTSIDE_TERRITORY;
                            }
                            else
                                return COUNT_OF_BOUNDARY_POINTS_LESS_THREE;
                        }
                        return SUCCESSFUL_LOAD;
                    }
                }
                catch (FormatException)
                {
                    return UNSUCCESSFUL_ATTEMPT_READ_DATA;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return UNSUCCESSFUL_ATTEMPT_READ_DATA;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="pathToFile"></param>
        ///// <returns>Результат проверки файла</returns>

        /// <summary>
        /// Проверка CSV-файла, в который записывают оптимальные точки, создавая файл
        /// </summary>
        /// <param name="pathToFile">Путь к файлу</param>
        /// <returns>Результат проверки файла</returns>
        public string FileValidationCreateCSV(string pathToFile)
        {
            FileInfo infoFileCSV = new FileInfo(pathToFile);
            // Если файл имеет расширение .csv
            if (infoFileCSV.Extension == ".csv")
            {
                // Если у файла есть имя
                if (infoFileCSV.Name != ".csv")
                {
                    return SUCCESSFUL_LOAD;
                }
                else
                    return NULL_NAME_FILE;
            }
            else
                return NOT_CSV_FILE;
        }

        /// <summary>
        /// Запись в CSV-файл информации обо всех найденных оптимальных точках
        /// </summary>
        /// <param name="pathFile">Путь к файлу для записи</param>
        /// <param name="optimalZones">Оптимальные точки</param>
        /// <param name="criterion">Список критериев</param>
        public void WriteToFileInfoOptimumZones(string pathFile, List<OptimalZone> optimalZones, List<Criterion> criterion)
        {
            StringBuilder writerCSV = new StringBuilder();
            for (int i = 0; i < optimalZones.Count; i++)
            {
                writerCSV.AppendLine("Оптимум " + (i + 1));
                writerCSV.AppendLine("Координата Х: " + optimalZones[i].optimalZone.x);
                writerCSV.AppendLine("Координата Y: " + optimalZones[i].optimalZone.y);
                writerCSV.AppendLine("Радиус поиска: " + optimalZones[i].optimalZone.lengthRadiusSearch + " м.");

                // Перечисление значений критериев в каждой оптимальной зоне
                for (int j = 0; j < optimalZones[i].optimalZone.arrayValuesCriterionOnZone.Count; j++)
                    writerCSV.AppendLine(criterion[j].nameOfCriterion + ": " + optimalZones[i].optimalZone.arrayValuesCriterionOnZone[j]);

                // Перечисление сверток, которые выбрали зону оптимальной
                if (optimalZones[i].namesConvolutiones.Count == 1)
                    writerCSV.Append("Выбрал оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритм: ");
                else if (optimalZones[i].namesConvolutiones.Count == 2 || optimalZones[i].namesConvolutiones.Count == 3 ||
                    optimalZones[i].namesConvolutiones.Count == 4)
                    writerCSV.Append("Выбрали оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритма: ");
                else
                    writerCSV.Append("Выбрали оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритмов: ");

                for (int k = 0; k < optimalZones[i].namesConvolutiones.Count - 1; k++)
                    writerCSV.Append(optimalZones[i].namesConvolutiones[k] + "   *   ");

                writerCSV.AppendLine(optimalZones[i].namesConvolutiones[optimalZones[i].namesConvolutiones.Count - 1]);
                writerCSV.AppendLine();
            }
            // Запись данных в CSV-файл
            File.WriteAllText(pathFile, writerCSV.ToString(), Encoding.GetEncoding(1251));
            writerCSV.Clear();
        }

        /// <summary>
        /// Проверка корректности файла с нормой на душу населения
        /// </summary>
        /// <param name="pathToFile">Путь к файлу</param>
        /// <param name="listBorderPointsTerritory">Граничные точки зоны анализа</param>
        /// <param name="countCriterion">Количество критериев оптимальности</param>
        /// <returns></returns>
        public string ValidateUserFileNorma(string pathToFile, List<Location> listBorderPointsTerritory, int countCriterion)
        {
            // Если критериев больше одного
            if (countCriterion > 1)
            {
                // Список полигонов
                List<Polygon> listPointsPolygon = new List<Polygon>();

                // Создаем полигон из граничных точек зоны анализа
                List<PointLatLng> boundaryPointsTerritoryPolygon = new List<PointLatLng>();
                // Инициализация списка с граничными точками зоны анализа
                for (int i = 0; i < listBorderPointsTerritory.Count; i++)
                    boundaryPointsTerritoryPolygon.Add(new PointLatLng(listBorderPointsTerritory[i].x, listBorderPointsTerritory[i].y));
                // Создание полигона из граничных точек зоны анализа
                var polygon = new GMapPolygon(boundaryPointsTerritoryPolygon, "BordersPointsTerritory");

                // Если файл не смог прочитаться
                try
                {
                    try
                    {
                        // Список граничных точек для каждого из полигонов
                        List<Location> tempListPoints = new List<Location>();
                        using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                        {
                            while (!readerCsvFile.EndOfStream)
                            {
                                string oneString = readerCsvFile.ReadLine();
                                string[] splitOneString = oneString.Split(new char[] { ';' });
                                // Считали ID полигона
                                int ID = Convert.ToInt32(splitOneString[0]);
                                // Считали количество граничных точек полигона
                                int countBoundaryPoints = Convert.ToInt32(splitOneString[1]);

                                // Граничных точек у полигона не меньше 3
                                if (countBoundaryPoints >= 3)
                                {
                                    tempListPoints = new List<Location>();
                                    for (int j = 2; j <= countBoundaryPoints * 2;)
                                    {
                                        tempListPoints.Add(new Location(Convert.ToDouble(splitOneString[j]), Convert.ToDouble(splitOneString[j + 1])));
                                        j += 2;
                                    }

                                    // Принадлежность граничных точек полигона зоне анализа
                                    int numberOfEnteredPoints = 0;
                                    for (int i = 0; i < tempListPoints.Count; i++)
                                    {
                                        // Проверка каждой точки полигона на принадлежность зоне анализа
                                        PointLatLng point = new PointLatLng(tempListPoints[i].x, tempListPoints[i].y);
                                        if (polygon.IsInside(point))
                                            numberOfEnteredPoints++;
                                    }

                                    // Если все граничные точки полигона принадлежат зоне анализа
                                    if (numberOfEnteredPoints == tempListPoints.Count)
                                    {
                                        // Координаты центральной точки полигона
                                        double centerX = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 2]);
                                        double centerY = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 3]);
                                        bool centerPointOfPolygonIsInsydeTerritory = false;
                                        PointLatLng point = new PointLatLng(centerX, centerY);

                                        // Проверка принадлежности центральной точки полигона зоне анализа
                                        if (polygon.IsInside(point))
                                            centerPointOfPolygonIsInsydeTerritory = true;
                                        else
                                            centerPointOfPolygonIsInsydeTerritory = false;

                                        if (centerPointOfPolygonIsInsydeTerritory)
                                        {
                                            // Список значений каждого критерия у полигона
                                            List<double> countOfEveryCriterion = new List<double>();

                                            int lastCriterion = countBoundaryPoints * 2 + 4 + countCriterion;
                                            for (int j = countBoundaryPoints * 2 + 4; j < lastCriterion; j++)
                                                countOfEveryCriterion.Add(Convert.ToDouble(splitOneString[j]));

                                            // Все ли критерии >= 0
                                            bool flagValueCriterionAboveZero = true;
                                            for (int k = 0; k < countOfEveryCriterion.Count; k++)
                                                if (countOfEveryCriterion[k] < 0)
                                                    flagValueCriterionAboveZero = false;

                                            if (flagValueCriterionAboveZero)
                                                // Добавить полигон в список
                                                listPointsPolygon.Add(new Polygon(ID, countBoundaryPoints, tempListPoints,
                                                    centerX, centerY, countOfEveryCriterion));
                                            else
                                                return VALUE_OF_CRITERION_LESS_THAN_ZERO;
                                        }
                                        else
                                            return COORDINATES_OUTSIDE_TERRITORY;
                                    }
                                    else
                                        return COORDINATES_OUTSIDE_TERRITORY;
                                }
                                else
                                    return COUNT_OF_BOUNDARY_POINTS_LESS_THREE;
                            }
                            return SUCCESSFUL_LOAD;
                        }
                    }
                    catch (FormatException)
                    {
                        return UNSUCCESSFUL_ATTEMPT_READ_DATA;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return UNSUCCESSFUL_ATTEMPT_READ_DATA;
                }
            }
            return COUNT_OF_CRITERION_LESS_THAN_TWO;
        }
    }
}