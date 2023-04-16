using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Globalization;

namespace Optimum
{
    public class FileValidator
    {
        // Слой для проверки принадлежности точек границам территории
        private SublayerLocation _sublayerBorderPointsTerritory = new SublayerLocation();
        // Настройка для того, чтобы программа работала на английской версии Windows
        private readonly CultureInfo _cultureInfo = new CultureInfo("ru-RU");

        // Константы для ошибок чтения файла
        private const string NOT_EXTST_FILE = "Такого файла не существует";
        private const string NOT_CSV_FILE = "Выберите файл .csv расширения";
        private const string NULL_NAME_FILE = "Имя файла отсутствует";
        private const string FILE_READ_ONLY = "Данный файл доступен только для чтения";
        private const string FILE_EMPTY = "Данный файл не содержит данных";
        private const string FILE_HAS_BIG_SIZE = "Данный файл слишком большого размера";
        private const string SUCCESSFUL_LOAD = "Успешно";

        // Константы для проверки объектов социальной инфраструктуры из файла пользователя
        private const string COORDINATES_OUTSIDE_TERRITORY = "Координаты находятся за границами территории";
        private const string UNSUCCESSFUL_ATTEMPT_READ_DATA = "Неверный формат хранения данных";

        // Константы для проверки кварталов из файла пользователя
        private const string NUMBER_OF_BOUNDARY_POINTS_LESS_TWO = "Количество граничных точек не может быть меньше двух";
        private const string NUMBER_OF_CRITERION_LESS_THAN_ZERO = "Количество критерия не может быть меньше нуля";

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileValidator() { }

        /// <summary>
        ///  Проверка csv-файла, из которого загружаются данные
        /// </summary>
        /// <param name="pathToFile">Путь к файлу</param>
        /// <returns>Результат проверки файла</returns>
        public string FileUserValidation(string pathToFile)
        {
            FileInfo InfoFileCSV = new FileInfo(pathToFile);
            // Если файл существует
            if (File.Exists(pathToFile))
            {
                // Если файл имеет расширение csv
                if (InfoFileCSV.Extension == ".csv")
                {
                    // Если у файла есть имя
                    if (InfoFileCSV.Name != ".csv")
                    {
                        // Если файл большого размера (больше 1Мб)
                        if (InfoFileCSV.Length < 1024700)
                        {
                            // Чтение данных из файла
                            using (StreamReader readerExcel = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                            {
                                string AllStringsFromFileUser = readerExcel.ReadToEnd();
                                // Если файл не пустой
                                if (AllStringsFromFileUser.Length != 2)
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
        /// Проверка граничных точек территории
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с граничными точками</param>
        /// <returns>Результат проверки файла</returns>
        public string DataValidationUserBorder(string pathToFile)
        {
            // Список точек границы
            List<Location> _listBorderPointsTerritory = new List<Location>();
            CultureInfo _cultureInfo = new CultureInfo("ru-RU");
            // Если файл не смог прочитаться
            try
            {
                try
                {
                    using (StreamReader filereader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!filereader.EndOfStream)
                        {
                            string OneString = filereader.ReadLine();
                            string[] SplitOneString = OneString.Split(new char[] { ';' });
                            // Пытаем добавить координату в список
                            _listBorderPointsTerritory.Add(new Location(Convert.ToDouble(SplitOneString[0], _cultureInfo),
                                Convert.ToDouble(SplitOneString[1], _cultureInfo)));
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
        /// Проверка файла с объектами инфраструктуры на целостность данных
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с объектами инфраструктуры</param>
        /// <param name="listborder">Список граничных точек территории</param>
        /// <returns>Результат проверки файла</returns>
        public string DataValidationUserFacility(string pathToFile, List<Location> listborder)
        {
            // Список объектов инфраструктуры
            List<Facility> _listPointsFacility = new List<Facility>();

            // Создаем полигон из граничных точек
            List<PointLatLng> _boundaryPointsTerritoryPolygon = new List<PointLatLng>();
            // Инициализация списка с граничными точками территории
            for (int i = 0; i < listborder.Count; i++)
                _boundaryPointsTerritoryPolygon.Add(new PointLatLng(listborder[i].x, listborder[i].y));
            // Создание полигона из граничных точек
            var polygon = new GMapPolygon(_boundaryPointsTerritoryPolygon, "BordersPointsTerritory");

            // Если файл не смог прочитаться
            try
            {
                try
                {
                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!readerCsvFile.EndOfStream)
                        {
                            string OneString = readerCsvFile.ReadLine();
                            string[] SplitOneString = OneString.Split(new char[] { ';' });
                            // Принадлежность точки территории
                            bool IsInsydeTerritory = false;

                            // Текущий объект инфраструктуры
                            PointLatLng point = new PointLatLng(Convert.ToDouble(SplitOneString[1]), Convert.ToDouble(SplitOneString[2]));
                            // Проверка принадлежности объекта инфраструктуры территории
                            if (polygon.IsInside(point))
                                IsInsydeTerritory = true;
                            else
                                IsInsydeTerritory = false;

                            if (IsInsydeTerritory)
                            {
                                // Попытка считать данные
                                // Массив строк с информацией об объекте инфраструктуры
                                List<string> info = new List<string>();
                                for (int i = 3; i < SplitOneString.Length; i++)
                                    info.Add(SplitOneString[i].ToString());

                                // Добавить объект инфраструктуры в список
                                _listPointsFacility.Add(new Facility(Convert.ToInt32(SplitOneString[0]),
                                    Convert.ToDouble(SplitOneString[1], _cultureInfo), Convert.ToDouble(SplitOneString[2], _cultureInfo), info));
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
        /// Проверка файла с полигонами на целостность данных
        /// </summary>
        /// <param name="pathToFile">Путь к файлу с данными</param>
        /// <param name="listborder">Список граничных точек территории</param>
        /// <param name="countCriterion">Количество критериев</param>
        /// <returns>Результат проверки файла</returns>
        public string DataValidationUserQuar(string pathToFile, List<Location> listborder, int countCriterion)
        {
            // Список полигонов
            List<Quar> _ListPointsQuartet = new List<Quar>();

            // Создаем полигон из граничных точек
            List<PointLatLng> _boundaryPointsTerritoryPolygon = new List<PointLatLng>();
            // Инициализация списка с граничными точками территории
            for (int i = 0; i < listborder.Count; i++)
                _boundaryPointsTerritoryPolygon.Add(new PointLatLng(listborder[i].x, listborder[i].y));
            // Создание полигона из граничных точек
            var polygon = new GMapPolygon(_boundaryPointsTerritoryPolygon, "BordersPointsTerritory");

            // Если файл не смог прочитаться
            try
            {
                try
                {
                    List<Location> tempListPoints = new List<Location>();
                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!readerCsvFile.EndOfStream)
                        {
                            string OneString = readerCsvFile.ReadLine();
                            string[] SplitOneString = OneString.Split(new char[] { ';' });
                            // Считали ID полигона
                            int ID = Convert.ToInt32(SplitOneString[0]);
                            // Считали количество граничных точек
                            int CountBoundaryPoints = Convert.ToInt32(SplitOneString[1]);

                            // Граничных точек у полигона не меньше 3
                            if (CountBoundaryPoints >= 3)
                            {
                                tempListPoints = new List<Location>();
                                for (int j = 2; j <= CountBoundaryPoints * 2;)
                                {
                                    tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j]), Convert.ToDouble(SplitOneString[j + 1])));
                                    j += 2;
                                }

                                // Принадлежность граничных точек кварталов территории
                                int numberOfEnteredPoints = 0;
                                for (int i = 0; i < tempListPoints.Count; i++)
                                {
                                    // Текущая считанная точка границы
                                    PointLatLng point = new PointLatLng(tempListPoints[i].x, tempListPoints[i].y);
                                    if (polygon.IsInside(point))
                                        numberOfEnteredPoints++;
                                }

                                // Если все граничные точки квартала принадлежат территории
                                if (numberOfEnteredPoints == tempListPoints.Count)
                                {
                                    // Координаты центральной точки полигона
                                    double CentreX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2]);
                                    double CentreY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3]);
                                    bool CentrePointOfQuartetIsInsydeTerritory = false;
                                    PointLatLng point = new PointLatLng(CentreX, CentreY);

                                    // Проверка принадлежности центральной точки территории
                                    if (polygon.IsInside(point))
                                        CentrePointOfQuartetIsInsydeTerritory = true;
                                    else
                                        CentrePointOfQuartetIsInsydeTerritory = false;

                                    if (CentrePointOfQuartetIsInsydeTerritory)
                                    {
                                        // Список чисел значений каждого критерия у каждого полигона
                                        List<double> countOfEveryCriterion = new List<double>();

                                        // SplitOneString[0] = ID
                                        // SplitOneString[1] = количество точек
                                        // SplitOneString[CountBoundaryPoints * 2] = граничные точки
                                        // SplitOneString[CountBoundaryPoints * 2 + 2] = х центра
                                        // SplitOneString[CountBoundaryPoints * 2 + 3] = у центра
                                        // SplitOneString[CountBoundaryPoints * 2 + 4] = 1 критерий
                                        // SplitOneString[SplitOneString.Length] = последний критерий

                                        // ID, CountBoundaryPoints, CountBoundaryPoints*2, xcenter, ycenter
                                        int _lastCriterion = CountBoundaryPoints * 2 + 4 + countCriterion;

                                        for (int j = CountBoundaryPoints * 2 + 4; j < _lastCriterion; j++)
                                        {
                                            //System.Windows.Forms.MessageBox.Show(SplitOneString[j].ToString());
                                            countOfEveryCriterion.Add(Convert.ToDouble(SplitOneString[j]));
                                        }

                                        // Если все критерия больше или равны 0
                                        bool flag = true;
                                        for (int k = 0; k < countOfEveryCriterion.Count; k++)                                        
                                            if (countOfEveryCriterion[k] < 0)
                                                flag = false;
                                        
                                        // Если все критерии >= 0
                                        if (flag)
                                            // Добавить полигон в список
                                            _ListPointsQuartet.Add(new Quar(ID, CountBoundaryPoints, tempListPoints, CentreX, CentreY, countOfEveryCriterion));
                                        else
                                            return NUMBER_OF_CRITERION_LESS_THAN_ZERO;
                                    }
                                    else
                                        return COORDINATES_OUTSIDE_TERRITORY;
                                }
                                else
                                    return COORDINATES_OUTSIDE_TERRITORY;
                            }
                            else
                                return NUMBER_OF_BOUNDARY_POINTS_LESS_TWO;
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
        /// Проверка csv-файла, в который производят запись оптимальной точки, создавая файл
        /// </summary>
        /// <param name="pathToFile">Путь к файлу</param>
        /// <returns>Результат проверки файла</returns>
        public string FileValidationCreateCSV(string pathToFile)
        {
            FileInfo InfoFileCSV = new FileInfo(pathToFile);
            // Если файл имеет расширение csv
            if (InfoFileCSV.Extension == ".csv")
            {
                // Если у файла есть имя
                if (InfoFileCSV.Name != ".csv")
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
        /// Запись в csv-файл информации об оптимальной точке
        /// </summary>
        /// <param name="pathFile">Путь к файлу для записи</param>
        /// <param name="optimalZones">Оптимальные зоны для их сохранения</param>
        public void WriteInfoOptimumToCSV(string pathFile, List<OptimalZone> optimalZones)
        {
            StringBuilder WriterCSV = new StringBuilder();
            for (int i = 0; i < optimalZones.Count; i++)
            {
                WriterCSV.AppendLine("Оптимум " + (i + 1));
                WriterCSV.AppendLine("Координата Х: " + optimalZones[i].optimal.x);
                WriterCSV.AppendLine("Координата Х: " + optimalZones[i].optimal.y);
                WriterCSV.AppendLine("Радиус поиска: " + optimalZones[i].optimal.lengthRadiusSearch + " м.");

                for (int j = 0; j < optimalZones[i].optimal.arrayValuesCriterionOnZone.Count; j++)
                    WriterCSV.AppendLine("Критерий " + (j + 1) + ": " + optimalZones[i].optimal.arrayValuesCriterionOnZone[j]);

                if (optimalZones[i].namesConvolutiones.Count == 1)
                    WriterCSV.Append("Выбрал оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритм: ");
                else if (optimalZones[i].namesConvolutiones.Count == 2 || optimalZones[i].namesConvolutiones.Count == 3 ||
                    optimalZones[i].namesConvolutiones.Count == 4)
                    WriterCSV.Append("Выбрали оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритма: ");
                else
                    WriterCSV.Append("Выбрали оптимальной " + optimalZones[i].namesConvolutiones.Count + " алгоритмов: ");

                for (int k = 0; k < optimalZones[i].namesConvolutiones.Count - 1; k++)
                    WriterCSV.Append(optimalZones[i].namesConvolutiones[k] + "   *   ");

                WriterCSV.AppendLine(optimalZones[i].namesConvolutiones[optimalZones[i].namesConvolutiones.Count - 1]);
                WriterCSV.AppendLine();
            }
            // Запись данных
            File.WriteAllText(pathFile, WriterCSV.ToString(), Encoding.GetEncoding(1251));
            WriterCSV.Clear();
        }





        /// <summary>
        /// Проверка корректности файла с нормой на душу населения
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <param name="listborder"></param>
        /// <param name="countCriterion"></param>
        /// <returns></returns>
        public string DataValidationNorma(string pathToFile, List<Location> listborder, int countCriterion)
        {
            // Список полигонов
            List<Quar> _ListPointsQuartet = new List<Quar>();

            // Создаем полигон из граничных точек
            List<PointLatLng> _boundaryPointsTerritoryPolygon = new List<PointLatLng>();
            // Инициализация списка с граничными точками территории
            for (int i = 0; i < listborder.Count; i++)
                _boundaryPointsTerritoryPolygon.Add(new PointLatLng(listborder[i].x, listborder[i].y));
            // Создание полигона из граничных точек
            var polygon = new GMapPolygon(_boundaryPointsTerritoryPolygon, "BordersPointsTerritory");

            // Если файл не смог прочитаться
            try
            {
                try
                {
                    List<Location> tempListPoints = new List<Location>();
                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                    {
                        while (!readerCsvFile.EndOfStream)
                        {
                            string OneString = readerCsvFile.ReadLine();
                            string[] SplitOneString = OneString.Split(new char[] { ';' });
                            // Считали ID полигона
                            int ID = Convert.ToInt32(SplitOneString[0]);
                            // Считали количество граничных точек
                            int CountBoundaryPoints = Convert.ToInt32(SplitOneString[1]);

                            // Граничных точек у полигона не меньше 3
                            if (CountBoundaryPoints >= 3)
                            {
                                tempListPoints = new List<Location>();
                                for (int j = 2; j <= CountBoundaryPoints * 2;)
                                {
                                    tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j]), Convert.ToDouble(SplitOneString[j + 1])));
                                    j += 2;
                                }

                                // Принадлежность граничных точек кварталов территории
                                int numberOfEnteredPoints = 0;
                                for (int i = 0; i < tempListPoints.Count; i++)
                                {
                                    // Текущая считанная точка границы
                                    PointLatLng point = new PointLatLng(tempListPoints[i].x, tempListPoints[i].y);
                                    if (polygon.IsInside(point))
                                        numberOfEnteredPoints++;
                                }

                                // Если все граничные точки квартала принадлежат территории
                                if (numberOfEnteredPoints == tempListPoints.Count)
                                {
                                    // Координаты центральной точки полигона
                                    double CentreX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2]);
                                    double CentreY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3]);
                                    bool CentrePointOfQuartetIsInsydeTerritory = false;
                                    PointLatLng point = new PointLatLng(CentreX, CentreY);

                                    // Проверка принадлежности центральной точки территории
                                    if (polygon.IsInside(point))
                                        CentrePointOfQuartetIsInsydeTerritory = true;
                                    else
                                        CentrePointOfQuartetIsInsydeTerritory = false;

                                    if (CentrePointOfQuartetIsInsydeTerritory)
                                    {
                                        // Список чисел значений каждого критерия у каждого полигона
                                        List<double> countOfEveryCriterion = new List<double>();

                                        // SplitOneString[0] = ID
                                        // SplitOneString[1] = количество точек
                                        // SplitOneString[CountBoundaryPoints * 2] = граничные точки
                                        // SplitOneString[CountBoundaryPoints * 2 + 2] = х центра
                                        // SplitOneString[CountBoundaryPoints * 2 + 3] = у центра
                                        // SplitOneString[CountBoundaryPoints * 2 + 4] = 1 критерий
                                        // SplitOneString[SplitOneString.Length] = последний критерий

                                        // ID, CountBoundaryPoints, CountBoundaryPoints*2, xcenter, ycenter
                                        int _lastCriterion = CountBoundaryPoints * 2 + 4 + countCriterion;

                                        for (int j = CountBoundaryPoints * 2 + 4; j < _lastCriterion; j++)
                                        {
                                            //System.Windows.Forms.MessageBox.Show(SplitOneString[j].ToString());
                                            countOfEveryCriterion.Add(Convert.ToDouble(SplitOneString[j]));
                                        }

                                        // Если все критерия больше или равны 0
                                        bool flag = true;
                                        for (int k = 0; k < countOfEveryCriterion.Count; k++)
                                            if (countOfEveryCriterion[k] < 0)
                                                flag = false;

                                        // Если все критерии >= 0
                                        if (flag)
                                            // Добавить полигон в список
                                            _ListPointsQuartet.Add(new Quar(ID, CountBoundaryPoints, tempListPoints, CentreX, CentreY, countOfEveryCriterion));
                                        else
                                            return NUMBER_OF_CRITERION_LESS_THAN_ZERO;
                                    }
                                    else
                                        return COORDINATES_OUTSIDE_TERRITORY;
                                }
                                else
                                    return COORDINATES_OUTSIDE_TERRITORY;
                            }
                            else
                                return NUMBER_OF_BOUNDARY_POINTS_LESS_TWO;
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
    }
}