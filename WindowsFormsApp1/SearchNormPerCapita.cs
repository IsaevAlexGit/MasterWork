using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;

namespace Optimum
{
    public partial class SearchNormPerCapita : Form
    {
        MapModel _mapModel;
        MapView _mapView;
        private LanguageType _languageOfMap;
        // Слой для автомаркеров
        private SublayerLocation _sublayerNorma;
        // Временный список для отрисовки ОСИ, а потом поверх точек-нормы
        List<BufferZone> _listTemp;
        // Путь к файлу новому
        string path = " ";

        /// <summary>
        /// Инициализация формы с языком
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public SearchNormPerCapita(LanguageType language)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _languageOfMap = language;
            KeyPreview = true;
        }

        /// <summary>
        /// Инициализация формы с языком и новым файлом данных
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public SearchNormPerCapita(LanguageType language, string filepath)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _languageOfMap = language;
            path = filepath;
            KeyPreview = true;
        }

        /// <summary>
        /// Загрузка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gmap_Load(object sender, EventArgs e)
        {
            _mapModel = new MapModel();
            _mapView = new MapView(_mapModel);
            //_mapView.InitGMapControl(gmapNorm);
            GMapProvider.Language = _languageOfMap;

            // Контекстное меню для выбора поставщика карты
            ToolStripMenuItem YandexMenuItem = new ToolStripMenuItem("Установить Яндекс-карту");
            ToolStripMenuItem GoogleMenuItem = new ToolStripMenuItem("Установить Google-карту");
            ToolStripMenuItem OpenCycleMapMenuItem = new ToolStripMenuItem("Установить OpenCycleMap-карту");
            ToolStripMenuItem BingHybridMapMenuItem = new ToolStripMenuItem("Установить BingHybridMap-карту");
            ToolStripMenuItem YandexHybridMapMenuItem = new ToolStripMenuItem("Установить YandexHybridMap-карту");
            ToolStripMenuItem ArcGISMenuItem = new ToolStripMenuItem("Установить ArcGIS-карту");
            ToolStripMenuItem WikiMapiaMapMenuItem = new ToolStripMenuItem("Установить WikiMapiaMap-карту");

            // Инициализация контестного меню в зависимости от выбранного языка
            if (_languageOfMap == LanguageType.English)
                contextMenu.Items.AddRange(new[] { YandexMenuItem, GoogleMenuItem, BingHybridMapMenuItem,
                    YandexHybridMapMenuItem, ArcGISMenuItem, WikiMapiaMapMenuItem });
            else
                contextMenu.Items.AddRange(new[] { YandexMenuItem, GoogleMenuItem, OpenCycleMapMenuItem, YandexHybridMapMenuItem });
            gmapNorm.ContextMenuStrip = contextMenu;

            YandexMenuItem.Click += YandexMenuItem_Click;
            GoogleMenuItem.Click += GoogleMenuItem_Click;
            OpenCycleMapMenuItem.Click += OpenCycleMapMenuItem_Click;
            BingHybridMapMenuItem.Click += BingHybridMapMenuItem_Click;
            YandexHybridMapMenuItem.Click += YandexHybridMapMenuItem_Click;
            WikiMapiaMapMenuItem.Click += WikiMapiaMapMenuItem_Click;
            ArcGISMenuItem.Click += ArcGISMenuItem_Click;
        }

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchNormPerCapita_Load(object sender, EventArgs e)
        {
            // Если на главном окне файл изменили, то и тут его изменить на новый
            if (path != " ")
                _mapModel.ChangeFileForQuartet(path);

            // ОСИ по умолчанию скрыть
            radioButtonHideOSI.Checked = true;
            radioButtonHideOSI_Click(sender, e);

            _sublayerNorma = _mapModel.sublayerNormaPharmacy;

            // Настройка кнопок
            buttonLoadPriority.FlatAppearance.BorderSize = 0;
            buttonInputRadius.FlatAppearance.BorderSize = 0;
            buttonFindBest.FlatAppearance.BorderSize = 0;
            buttonOpenWebManual.FlatAppearance.BorderSize = 0;
            buttonSaveMap.FlatAppearance.BorderSize = 0;
            buttonInputNorm.FlatAppearance.BorderSize = 0;
            buttonHideBest.FlatAppearance.BorderSize = 0;

            buttonLoadPriority.FlatStyle = FlatStyle.Flat;
            buttonInputRadius.FlatStyle = FlatStyle.Flat;
            buttonFindBest.FlatStyle = FlatStyle.Flat;
            buttonOpenWebManual.FlatStyle = FlatStyle.Flat;
            buttonSaveMap.FlatStyle = FlatStyle.Flat;
            buttonInputNorm.FlatStyle = FlatStyle.Flat;
            buttonHideBest.FlatStyle = FlatStyle.Flat;

            // Ползунок от 6 до 30, текущее значение 6
            // Мы передаем в радиус значение с ползунка помноженное на 50
            // То есть 6 это 300 метров, 30 это 1500 метров
            trackBarRadius.Minimum = 6;
            trackBarRadius.Maximum = 30; // 60 потом сделать до 3000 метров
            trackBarRadius.Value = 6;
            labelRadiusLong.Text = (trackBarRadius.Value * 50).ToString() + " м.";

            // Размеры окна
            ClientSize = new Size(1374, 751);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Условные обозначения карты
            pictureBoxAuto.Image = Properties.Resources.iconBestOptimum;
            //pictureBoxPharmacy.Image = Properties.Resources.iconPharmacy;
            // Размер окна при его открытии
            WindowState = FormWindowState.Maximized;

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            // Установка текстовых по умолчанию
            //textBoxWeightResidents.Text = _mapModel.weightResidents.ToString();
            //textBoxWeightRetired.Text = _mapModel.weightRetired.ToString();
            //textBoxWeightPharmacy.Text = _mapModel.weightPharmacy.ToString();
            textBoxCountOSI.Text = _mapModel.implementedRateOfInfrastructureFacilities.ToString();
            textBoxPeople.Text = _mapModel.implementedPopulationRate.ToString();

            // Нет надписи изначально о состоянии нормы
            labelStateOfNormCity.Visible = false;
        }

        /// <summary>
        /// Обработка смены карты на YandexMap
        /// </summary>
        private void YandexMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.YandexMap;
        }

        /// <summary>
        /// Обработка смены карты на GoogleMap
        /// </summary>
        private void GoogleMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.GoogleMap;
        }

        /// <summary>
        /// Обработка смены карты на OpenCycleMap
        /// </summary>
        private void OpenCycleMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.OpenCycleMap;
        }

        /// <summary>
        /// Обработка смены карты на BingHybridMap
        /// </summary>
        private void BingHybridMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.BingHybridMap;
        }

        /// <summary>
        /// Обработка смены карты на YandexHybridMap
        /// </summary>
        private void YandexHybridMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.YandexHybridMap;
        }

        /// <summary>
        /// Обработка смены карты на ArcGIS_World_Topo_Map
        /// </summary>
        private void ArcGISMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.ArcGIS_World_Topo_Map;
        }

        /// <summary>
        /// Обработка смены карты на WikiMapiaMap
        /// </summary>
        private void WikiMapiaMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.WikiMapiaMap;
        }

        // Флаг успешной установки весов
        private bool _flagConfirmationWeight = false;
        /// <summary>
        /// Задать веса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadPriority_Click(object sender, EventArgs e)
        {
            // Сбросить флаг, так как кнопка нажата 
            _flagConfirmationWeight = false;
            // Спрятать надпись и флаг поиска точки
            labelStateOfNormCity.Visible = false;
            _flagFindBestAutoNorm = false;
            // Очистить карту от всего
            _mapView.ClearNormaPoints(_sublayerNorma);

            // Если все поля введены
            if (!string.IsNullOrEmpty(textBoxWeightResidents.Text) && !string.IsNullOrWhiteSpace(textBoxWeightResidents.Text) &&
                !string.IsNullOrEmpty(textBoxWeightRetired.Text) && !string.IsNullOrWhiteSpace(textBoxWeightRetired.Text) &&
                !string.IsNullOrEmpty(textBoxWeightPharmacy.Text) && !string.IsNullOrWhiteSpace(textBoxWeightPharmacy.Text))
            {
                double tempValue;
                bool testForResidents = double.TryParse(textBoxWeightResidents.Text, out tempValue);
                bool testForRetired = double.TryParse(textBoxWeightRetired.Text, out tempValue);
                bool testForPharmacy = double.TryParse(textBoxWeightPharmacy.Text, out tempValue);

                // Если во всех полях дробные числа
                if (testForResidents == true && testForRetired == true && testForPharmacy == true)
                {
                    double weihgtResidents = Convert.ToDouble(textBoxWeightResidents.Text);
                    double weihgtRetired = Convert.ToDouble(textBoxWeightRetired.Text);
                    double weihgtPharma = Convert.ToDouble(textBoxWeightPharmacy.Text);

                    // Если все веса в интервале [0,1]
                    if (weihgtResidents >= 0 && weihgtResidents <= 1
                        && weihgtRetired >= 0 && weihgtRetired <= 1
                        && weihgtPharma >= 0 && weihgtPharma <= 1)
                    {
                        // Если сумма весов равна 1
                        if (weihgtResidents + weihgtRetired + weihgtPharma == 1)
                        {
                            // Задать веса в модель
                            _mapModel.weightNormResidents = weihgtResidents;
                            _mapModel.weightNormRetired = weihgtRetired;
                            _mapModel.weightNormPharmacy = weihgtPharma;
                            // Веса заданы успешно
                            _flagConfirmationWeight = true;
                            // Спрятать надпись и флаг поиска точки
                            labelStateOfNormCity.Visible = false;
                            _flagFindBestAutoNorm = false;
                            // Очистить карту от всего
                            _mapView.ClearNormaPoints(_sublayerNorma);
                        }
                        else
                            MessageBox.Show("Сумма весов должна быть равна 1", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Все значения должны быть в пределах от 0 до 1", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("В поля должны быть введены только числа", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Все поля в блоке \"Установка приоритетов\" должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Флаг успешной установки радиуса
        private bool _flagSaveRadius = false;
        /// <summary>
        /// ввод радиуса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputRadius_Click(object sender, EventArgs e)
        {
            // Спрятать надпись и флаг поиска точки
            labelStateOfNormCity.Visible = false;
            _flagFindBestAutoNorm = false;
            // Очистить карту от всего
            _mapView.ClearNormaPoints(_sublayerNorma);
            _flagSaveRadius = false;

            // Сохранить положение ползунка * 50 метров (шаг)
            _mapModel.radiusNormBufferZone = trackBarRadius.Value * 50;
            // Переотрисовать буферные зоны
            // А почему тут radiusBufferZone а не radiusNormBufferZone

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //_mapView.DrawPointBufferZone(_mapModel.radiusBufferZone);

            // Радиус найден
            _flagSaveRadius = true;
            // Значит норма на карте не найдена
            labelStateOfNormCity.Visible = false;
            _flagFindBestAutoNorm = false;
            // Очистить карту от всего
            _mapView.ClearNormaPoints(_sublayerNorma);
        }

        // Флаг установки нормы на душу населения
        private bool _flagInputNorma = false;
        /// <summary>
        /// Задать норму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInputNorm_Click(object sender, EventArgs e)
        {
            // Спрятать надпись и флаг поиска точки
            labelStateOfNormCity.Visible = false;
            _flagFindBestAutoNorm = false;
            // Очистить карту от всего
            _mapView.ClearNormaPoints(_sublayerNorma);
            _flagInputNorma = false;

            // Если все поля введены
            if (!string.IsNullOrEmpty(textBoxCountOSI.Text) && !string.IsNullOrWhiteSpace(textBoxCountOSI.Text) &&
                !string.IsNullOrEmpty(textBoxPeople.Text) && !string.IsNullOrWhiteSpace(textBoxPeople.Text))
            {
                int tempValue;
                bool testForCountOSI = int.TryParse(textBoxCountOSI.Text, out tempValue);
                bool testForPeople = int.TryParse(textBoxPeople.Text, out tempValue);

                // Если во всех полях дробные числа
                if (testForCountOSI == true && testForPeople == true)
                {
                    int CountOSI = Convert.ToInt32(textBoxCountOSI.Text);
                    int People = Convert.ToInt32(textBoxPeople.Text);

                    // Если все веса больше 0
                    if (CountOSI >= 1 && People >= 1)
                    {
                        _mapModel.implementedRateOfInfrastructureFacilities = CountOSI;
                        _mapModel.implementedPopulationRate = People;
                        // если поля нормально заданы то тру
                        _flagInputNorma = true;
                        labelStateOfNormCity.Visible = false;
                        _flagFindBestAutoNorm = false;
                        //MessageBox.Show("Данные заданы верно", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Все значения должны быть больше 1", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("В поля должны быть введены только числа", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Все поля в блоке \"Установка нормы на душу населения\" должны быть заполнены",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Флаг успешного поиска нормы
        private bool _flagFindBestAutoNorm = false;
        /// <summary>
        /// Найти лучшие точки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFindBest_Click(object sender, EventArgs e)
        {
            //// Если заданы были норма и люди
            //if (_flagInputNorma == true && _flagSaveRadius == true && _flagConfirmationWeight == true)
            //{
            //    // Очистить карту от всего
            //    _mapView.ClearNormaPoints(_sublayerNorma);
            //    // Спрятать надпись об анализе карты
            //    labelStateOfNormCity.Visible = false;
            //    // Флаг поиска нормы
            //    _flagFindBestAutoNorm = false;
            //    // Флаг сохранения нормы в картинке
            //    _flagSaveOptimumPicture = false;

            //    // Получили количество людей в городе
            //    int globalPeopleCityFromUserFile = _mapModel.CalculateCountPeopleCity();
            //    // Получили количество ОСИ в городе
            //    int globalOSICityFromUserFile = _mapModel.CalculateCountOSICity();

            //    // Количество людей на 1 ОСИ при заданной норме
            //    double countPeopleToOneOSI = _mapModel.implementedPopulationRate / _mapModel.implementedRateOfInfrastructureFacilities;
            //    // Делим количество людей из файла на норму (1 ОСИ к людям) = количество ОСИ в норме в данном городе
            //    double countOSIthisCityNorm = Math.Ceiling(globalPeopleCityFromUserFile / countPeopleToOneOSI);

            //    // Если норма выше, чем текущее состояние ОСИ, то тут нехватка ОСИ
            //    if (countOSIthisCityNorm > globalOSICityFromUserFile)
            //    {
            //        // Разница в нужном и реальном количестве ОСИ в городе
            //        double DifferenceBetweenNormAndCity = Math.Ceiling(countOSIthisCityNorm - globalOSICityFromUserFile);
            //        // Округлить разницу до целого числа
            //        int CellingDifferenceBetweenNormAndCity = (int)DifferenceBetweenNormAndCity;

            //        // Список крайних точек Таганрога
            //        SublayerLocation listLocationBorderCity = _mapModel.GetSublayerBorderPointsTerritory();

            //        // Минимимум и максимум (х,у) за начало первая точка из списка
            //        double minX = listLocationBorderCity.listWithLocation[0].x;
            //        double minY = listLocationBorderCity.listWithLocation[0].y;
            //        double maxX = listLocationBorderCity.listWithLocation[0].x;
            //        double maxY = listLocationBorderCity.listWithLocation[0].y;

            //        // Поиск левой верхней и правой нижней точки
            //        for (int i = 0; i < listLocationBorderCity.listWithLocation.Count; i++)
            //        {
            //            if (minX > listLocationBorderCity.listWithLocation[i].x)
            //                minX = listLocationBorderCity.listWithLocation[i].x;
            //            if (minY > listLocationBorderCity.listWithLocation[i].y)
            //                minY = listLocationBorderCity.listWithLocation[i].y;
            //            if (maxX < listLocationBorderCity.listWithLocation[i].x)
            //                maxX = listLocationBorderCity.listWithLocation[i].x;
            //            if (maxY < listLocationBorderCity.listWithLocation[i].y)
            //                maxY = listLocationBorderCity.listWithLocation[i].y;
            //        }

            //        // Сохранили минимальный х
            //        double saveMinX = minX;

            //        // Список всех точек с полным обходом карты
            //        List<PointLatLng> listWithFullPassCity = new List<PointLatLng>();

            //        // Заполняем полигон города точками
            //        while (minY <= maxY)
            //        {
            //            while (minX <= maxX)
            //            {
            //                // Добавляем в список все точки прохода города
            //                listWithFullPassCity.Add(new PointLatLng(minX, minY));
            //                minX = minX + 0.003;
            //            }
            //            // Нарастили ось Y и еще раз по оси Х
            //            minY = minY + 0.005;
            //            minX = saveMinX;
            //        }

            //        // Идем по всем точкам глобального прохода
            //        for (int i = 0; i < listWithFullPassCity.Count; i++)
            //        {
            //            // Создаем точку
            //            PointLatLng pointLatLng = new PointLatLng(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng);
            //            // Если точка принадлежит какому-то кварталу
            //            if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
            //            {
            //                // Добавление точки в список авто-маркеров
            //                _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng));
            //            }
            //        }



            //        // Очистить список
            //        listWithFullPassCity.Clear();

            //        // Установка маркеров и буфреных зон, которые попали в полигоны
            //        _mapView.DrawNormaBufferZone(_mapModel.radiusNormBufferZone);

            //        // Получить список буферных зон, которые попали в Таганрог, но не 0, 0, 0
            //        _mapModel.CreateListNormaBufferZones();

            //        // Список буферных зон для поиска оптимума (среди которых нет 0, 0, 0)
            //        List<BufferZone> _listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

            //        // Если хороших (ненулевых буферных зон) меньше, чем столько сколько надо городу
            //        if (_listAutoBufferZones.Count < CellingDifferenceBetweenNormAndCity)
            //        {
            //            // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
            //            MathSomeAutoOptimum _math = new MathSomeAutoOptimum(_listAutoBufferZones, _mapModel.weightNormPharmacy,
            //                _mapModel.weightNormResidents, _mapModel.weightNormRetired, _listAutoBufferZones.Count);

            //            // Получить сколько можем дать оптимумов
            //            List<BufferZone> _listoptimalPoint = _math.GetArrayWithOptimums();

            //            // Отрисовать звездочки оптимальных точек
            //            _mapView.DrawNormaIdealPointBufferZone(_listoptimalPoint);
            //            _listTemp = _listoptimalPoint;

            //            // Флаг поиска авто-кандидатов
            //            _flagFindBestAutoNorm = true;

            //            labelStateOfNormCity.Visible = true;
            //            labelStateOfNormCity.Text = "В городе не хватает " + CellingDifferenceBetweenNormAndCity + " объекта(ов) инфраструктуры, мы можем посоветовать " + _listAutoBufferZones.Count + " наилучших мест. Это выгодный бизнес!";
            //        }
            //        // Если хороших (ненулевых буферных зон) больше, чем столько сколько надо городу
            //        else
            //        {
            //            // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
            //            MathSomeAutoOptimum _math = new MathSomeAutoOptimum(_listAutoBufferZones, _mapModel.weightNormPharmacy,
            //                _mapModel.weightNormResidents, _mapModel.weightNormRetired, CellingDifferenceBetweenNormAndCity);

            //            // Получить сколько можем дать оптимумов
            //            List<BufferZone> _listoptimalPoint = _math.GetArrayWithOptimums();

            //            // Отрисовать звездочки оптимальных точек (1-10)
            //            _mapView.DrawNormaIdealPointBufferZone(_listoptimalPoint);
            //            _listTemp = _listoptimalPoint;

            //            // Флаг поиска авто-кандидатов
            //            _flagFindBestAutoNorm = true;

            //            labelStateOfNormCity.Visible = true;
            //            labelStateOfNormCity.Text = "В городе не хватает " + CellingDifferenceBetweenNormAndCity + " объекта(ов) инфраструктуры. Это выгодный бизнес!";
            //        }

            //    }
            //    // Если норма ниже, чем текущее состояние ОСИ, то тут изыбток
            //    else
            //    {
            //        // Делим ОСИ в файле на ОСИ по норме * 100 %
            //        double SurplusPercentage = (globalOSICityFromUserFile / countOSIthisCityNorm) * 100;

            //        // Если % изыбтка от 100% до 120%, то еще можно 10 ОСИ открыть
            //        if (SurplusPercentage >= 100 && SurplusPercentage <= 120)
            //        {
            //            // Список крайних точек Таганрога
            //            SublayerLocation listLocationBorderCity = _mapModel.GetSublayerBorderPointsTerritory();

            //            // Минимимум и максимум (х,у) за начало первая точка из списка
            //            double minX = listLocationBorderCity.listWithLocation[0].x;
            //            double minY = listLocationBorderCity.listWithLocation[0].y;
            //            double maxX = listLocationBorderCity.listWithLocation[0].x;
            //            double maxY = listLocationBorderCity.listWithLocation[0].y;

            //            // Поиск левой верхней и правой нижней точки
            //            for (int i = 0; i < listLocationBorderCity.listWithLocation.Count; i++)
            //            {
            //                if (minX > listLocationBorderCity.listWithLocation[i].x)
            //                    minX = listLocationBorderCity.listWithLocation[i].x;
            //                if (minY > listLocationBorderCity.listWithLocation[i].y)
            //                    minY = listLocationBorderCity.listWithLocation[i].y;
            //                if (maxX < listLocationBorderCity.listWithLocation[i].x)
            //                    maxX = listLocationBorderCity.listWithLocation[i].x;
            //                if (maxY < listLocationBorderCity.listWithLocation[i].y)
            //                    maxY = listLocationBorderCity.listWithLocation[i].y;
            //            }

            //            // Сохранили минимальный х
            //            double saveMinX = minX;

            //            // Список всех точек с полным обходом карты
            //            List<PointLatLng> listWithFullPassCity = new List<PointLatLng>();

            //            // Заполняем полигон города точками
            //            while (minY <= maxY)
            //            {
            //                while (minX <= maxX)
            //                {
            //                    // Добавляем в список все точки прохода города
            //                    listWithFullPassCity.Add(new PointLatLng(minX, minY));
            //                    minX = minX + 0.003;
            //                }
            //                // Нарастили ось Y и еще раз по оси Х
            //                minY = minY + 0.005;
            //                minX = saveMinX;
            //            }

            //            // Идем по всем точкам глобального прохода
            //            for (int i = 0; i < listWithFullPassCity.Count; i++)
            //            {
            //                // Создаем точку
            //                PointLatLng pointLatLng = new PointLatLng(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng);
            //                // Если точка принадлежит какому-то кварталу
            //                if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
            //                {
            //                    // Добавление точки в список авто-маркеров
            //                    _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng));
            //                }
            //            }

            //            // Очистить список
            //            listWithFullPassCity.Clear();

            //            // Установка маркеров и буфреных зон, которые попали в полигоны
            //            _mapView.DrawNormaBufferZone(_mapModel.radiusNormBufferZone);

            //            // Получить список буферных зон, которые попали в Таганрог, но не 0, 0, 0
            //            _mapModel.CreateListNormaBufferZones();

            //            // Список буферных зон для поиска оптимума (среди которых нет 0, 0, 0)
            //            List<BufferZone> _listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

            //            // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
            //            MathSomeAutoOptimum _math = new MathSomeAutoOptimum(_listAutoBufferZones, _mapModel.weightNormPharmacy,
            //                _mapModel.weightNormResidents, _mapModel.weightNormRetired, 10);

            //            // Получить 10 оптимумов
            //            List<BufferZone> _listoptimalPoint = _math.GetArrayWithOptimums();

            //            // Отрисовать звездочки оптимальных точек 10 штук
            //            _mapView.DrawNormaIdealPointBufferZone(_listoptimalPoint);
            //            _listTemp = _listoptimalPoint;

            //            // Флаг поиска авто-кандидатов
            //            _flagFindBestAutoNorm = true;

            //            labelStateOfNormCity.Visible = true;
            //            labelStateOfNormCity.Text = "В городе хватает объектов инфраструктуры, при этом около 10 открыть ещё можно!";
            //        }

            //        // Если % изыбтка от 120%, то бизнес невыгодный, и дать 10 ОСИ при согласии пользователя
            //        if (SurplusPercentage > 120)
            //        {
            //            // При избытке ОСИ показать ли 10 оптимумов
            //            if (MessageBox.Show("В городе много данных объектов инфраструктуры. Всё равно показать 10 лучших точек?", "Вопрос",
            //                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //            {
            //                // Список крайних точек Таганрога
            //                SublayerLocation listLocationBorderCity = _mapModel.GetSublayerBorderPointsTerritory();

            //                // Минимимум и максимум (х,у) за начало первая точка из списка
            //                double minX = listLocationBorderCity.listWithLocation[0].x;
            //                double minY = listLocationBorderCity.listWithLocation[0].y;
            //                double maxX = listLocationBorderCity.listWithLocation[0].x;
            //                double maxY = listLocationBorderCity.listWithLocation[0].y;

            //                // Поиск левой верхней и правой нижней точки
            //                for (int i = 0; i < listLocationBorderCity.listWithLocation.Count; i++)
            //                {
            //                    if (minX > listLocationBorderCity.listWithLocation[i].x)
            //                        minX = listLocationBorderCity.listWithLocation[i].x;
            //                    if (minY > listLocationBorderCity.listWithLocation[i].y)
            //                        minY = listLocationBorderCity.listWithLocation[i].y;
            //                    if (maxX < listLocationBorderCity.listWithLocation[i].x)
            //                        maxX = listLocationBorderCity.listWithLocation[i].x;
            //                    if (maxY < listLocationBorderCity.listWithLocation[i].y)
            //                        maxY = listLocationBorderCity.listWithLocation[i].y;
            //                }

            //                // Сохранили минимальный х
            //                double saveMinX = minX;

            //                // Список всех точек с полным обходом карты
            //                List<PointLatLng> listWithFullPassCity = new List<PointLatLng>();

            //                // Заполняем полигон города точками
            //                while (minY <= maxY)
            //                {
            //                    while (minX <= maxX)
            //                    {
            //                        // Добавляем в список все точки прохода города
            //                        listWithFullPassCity.Add(new PointLatLng(minX, minY));
            //                        minX = minX + 0.003;
            //                    }
            //                    // Нарастили ось Y и еще раз по оси Х
            //                    minY = minY + 0.005;
            //                    minX = saveMinX;
            //                }

            //                // Идем по всем точкам глобального прохода
            //                for (int i = 0; i < listWithFullPassCity.Count; i++)
            //                {
            //                    // Создаем точку
            //                    PointLatLng pointLatLng = new PointLatLng(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng);
            //                    // Если точка принадлежит какому-то кварталу
            //                    if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
            //                    {
            //                        // Добавление точки в список авто-маркеров
            //                        _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassCity[i].Lat, listWithFullPassCity[i].Lng));
            //                    }
            //                }

            //                // Очистить список
            //                listWithFullPassCity.Clear();

            //                // Установка маркеров и буфреных зон, которые попали в полигоны
            //                _mapView.DrawNormaBufferZone(_mapModel.radiusNormBufferZone);

            //                // Получить список буферных зон, которые попали в Таганрог, но не 0, 0, 0
            //                _mapModel.CreateListNormaBufferZones();

            //                // Список буферных зон для поиска оптимума (среди которых нет 0, 0, 0)
            //                List<BufferZone> _listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

            //                // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
            //                MathSomeAutoOptimum _math = new MathSomeAutoOptimum(_listAutoBufferZones, _mapModel.weightNormPharmacy,
            //                _mapModel.weightNormResidents, _mapModel.weightNormRetired, 10);

            //                // Получить 10 оптимумов
            //                List<BufferZone> _listoptimalPoint = _math.GetArrayWithOptimums();

            //                // Отрисовать звездочки оптимальных точек 10
            //                _mapView.DrawNormaIdealPointBufferZone(_listoptimalPoint);
            //                _listTemp = _listoptimalPoint;

            //                // Флаг поиска авто-кандидатов
            //                _flagFindBestAutoNorm = true;

            //                // Нет надписи изначально о состоянии нормы
            //                labelStateOfNormCity.Visible = true;
            //                labelStateOfNormCity.Text = "В городе хватает объектов инфраструктуры. Это невыгодный бизнес!";
            //            }
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Задайте веса, радиус и норму", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Руководство пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenWebManual_Click(object sender, EventArgs e)
        {
            // Открытие HTML-руководства
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Флаг сохранения картинки
        private bool _flagSaveOptimumPicture = false;
        /// <summary>
        /// Сохранение карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveMap_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog SaveMapDialog = new SaveFileDialog())
                {
                    // Формат изображения
                    SaveMapDialog.Filter = "Image Files (*.png) | *.png";
                    // Название изображения
                    SaveMapDialog.FileName = "Текущее положение карты";
                    // Преобразование карты в картинку
                    Image image = gmapNorm.ToImage();
                    if (image != null)
                    {
                        using (image)
                        {
                            // Если пользователь подтвердил сохранение
                            if (SaveMapDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Сохранение по выбранному пользователем пути картинку в формате png
                                string fileName = SaveMapDialog.FileName;
                                if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                                    fileName += ".png";
                                image.Save(fileName);
                                _flagSaveOptimumPicture = true;
                                // Уведомление о том, что сохранение успешно
                                MessageBox.Show("Карта успешно сохранена в директории: " + Environment.NewLine + SaveMapDialog.FileName,
                                    "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении карты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchNormPerCapita_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил картинку
            if (_flagSaveOptimumPicture == true)
                e.Cancel = false;
            else
            {
                // Если пользователь искал норму, но не сохранил картинку
                if (_flagFindBestAutoNorm == true)
                {
                    // Если пользователь не сохранил картинку, но при этом искал норму
                    if (MessageBox.Show("Вы не сохранили картинку с нормой. Выйти?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
        }

        /// <summary>
        /// Закрытие на ESC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchNormPerCapita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        /// <summary>
        /// Показать ОСИ на карте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonShowOSI_Click(object sender, EventArgs e)
        {
            _mapView.DrawFacility();
            if (_flagFindBestAutoNorm)
                _mapView.DrawNormaIdealPointBufferZone(_listTemp);
        }

        /// <summary>
        /// Скрыть ОСИ с карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonHideOSI_Click(object sender, EventArgs e)
        {
            _mapView.ClearFacility();
        }

        /// <summary>
        /// Скрыть норму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHideBest_Click(object sender, EventArgs e)
        {
            // Спрятать надпись и флаг поиска точки
            labelStateOfNormCity.Visible = false;
            // Авто-кандидаты скрыты
            _flagFindBestAutoNorm = false;
            // Убрать все следы авто-поиска с карты
            _mapView.ClearNormaPoints(_sublayerNorma);
        }

        /// <summary>
        /// Скроллирование ползунка отображает текущее значение радиуса в метрах
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarTransparent_Scroll(object sender, EventArgs e)
        {
            labelRadiusLong.Text = (trackBarRadius.Value * 50).ToString() + " м.";
        }
    }
}