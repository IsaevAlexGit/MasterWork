using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Device.Location;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;

namespace Optimum
{
    //! Класс для окна с нормой на душу населения
    public partial class SearchNormPerCapita : Form
    {
        //! Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        //! Локализация карты
        private LanguageType _languageOfMap;
        //! Слой для маркеров нормы
        private SublayerLocation _sublayerNorma;
        //! Список для отрисовки буферных зон
        private List<BufferZone> _tempListBufferZoneCandidate;
        private MapModel _mapModel;
        private MapView _mapView;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация формы
        /// </summary>
        /// <param name="language">Локализация карты</param>
        /// <param name="mapModel">Объект MapModel</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public SearchNormPerCapita(LanguageType language, MapModel mapModel)
        {
            _languageOfMap = language;
            _mapModel = mapModel;

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Загрузка карты
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void gmap_Load(object sender, EventArgs e)
        {
            // Инициализация карты
            _mapView = new MapView(_mapModel);
            _mapView.InitGMapControl(gmapNorm, _mapModel.centerMap);
            GMapProvider.Language = _languageOfMap;

            // Контекстное меню для выбора поставщика карты
            ToolStripMenuItem GoogleMenuItem = new ToolStripMenuItem("Установить Google-карту");
            ToolStripMenuItem BingHybridMapMenuItem = new ToolStripMenuItem("Установить BingHybridMap-карту");
            ToolStripMenuItem ArcGISMenuItem = new ToolStripMenuItem("Установить ArcGIS-карту");
            ToolStripMenuItem WikiMapiaMapMenuItem = new ToolStripMenuItem("Установить WikiMapiaMap-карту");
            ToolStripMenuItem GoogleTerrainMapMenuItem = new ToolStripMenuItem("Установить GoogleTerrainMap-карту");
            ToolStripMenuItem BingMapMenuItem = new ToolStripMenuItem("Установить BingMap-карту");
            ToolStripMenuItem OpenCycleTransportMapMenuItem = new ToolStripMenuItem("Установить OpenCycleTransportMap-карту");
            ToolStripMenuItem GoogleHybridMapMenuItem = new ToolStripMenuItem("Установить GoogleHybridMap-карту");
            ToolStripMenuItem OpenCycleLandscapeMapMenuItem = new ToolStripMenuItem("Установить OpenCycleLandscapeMap-карту");

            // Инициализация контестного меню в зависимости от выбранной локализации карты
            if (_languageOfMap == LanguageType.English)
                contextMenu.Items.AddRange(new[]
                {
                    GoogleMenuItem,
                    BingHybridMapMenuItem,
                    ArcGISMenuItem,
                    WikiMapiaMapMenuItem,
                    GoogleTerrainMapMenuItem,
                    BingMapMenuItem,
                    OpenCycleTransportMapMenuItem
                });
            else
                contextMenu.Items.AddRange(new[]
                {
                    GoogleMenuItem,
                    GoogleTerrainMapMenuItem,
                    GoogleHybridMapMenuItem,
                    OpenCycleLandscapeMapMenuItem
                });
            gmapNorm.ContextMenuStrip = contextMenu;

            GoogleMenuItem.Click += GoogleMenuItem_Click;
            BingHybridMapMenuItem.Click += BingHybridMapMenuItem_Click;
            WikiMapiaMapMenuItem.Click += WikiMapiaMapMenuItem_Click;
            ArcGISMenuItem.Click += ArcGISMenuItem_Click;
            GoogleTerrainMapMenuItem.Click += GoogleTerrainMapMenuItem_Click;
            BingMapMenuItem.Click += BingMapMenuItem_Click;
            OpenCycleTransportMapMenuItem.Click += OpenCycleTransportMapMenuItem_Click;
            GoogleHybridMapMenuItem.Click += GoogleHybridMapMenuItem_Click;
            OpenCycleLandscapeMapMenuItem.Click += OpenCycleLandscapeMapMenuItem_Click;
        }

        //! Шаг ползунка
        private const int STEP_TRACK_BAR = 50;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SearchNormPerCapita_Load(object sender, EventArgs e)
        {
            BackColor = labelNorm.BackColor = labelLegendForMap.BackColor = labelPeople.BackColor = panelLegend.BackColor =
                labelFacility.BackColor = labelOptimum.BackColor = labelRadiusLong.BackColor = labelStateOfNormTerritory.BackColor = _mapModel.mainColor;

            buttonFindBest.BackColor = buttonOpenWebManual.BackColor = buttonSaveMap.BackColor = buttonHideBest.BackColor =
                trackBarRadius.BackColor = buttonShowBest.BackColor = _mapModel.secondaryColor;

            if (_mapModel.nameObjectFacility.Length <= 15)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                groupBoxFacility.Font = new Font("Segoe UI", 11);
            }
            if (_mapModel.nameObjectFacility.Length >= 15 && _mapModel.nameObjectFacility.Length <= 20)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                groupBoxFacility.Font = new Font("Segoe UI", 8);
            }

            labelFacility.Text = _mapModel.nameObjectFacility;

            // Отображение объектов социальной инфраструктуры по умолчанию скрыть
            radioButtonHideFacility.Checked = true;
            radioButtonHideFacility_Click(sender, e);

            _sublayerNorma = _mapModel.sublayerNormaFacility;

            // Настройка кнопок
            buttonFindBest.FlatAppearance.BorderSize = 0;
            buttonOpenWebManual.FlatAppearance.BorderSize = 0;
            buttonSaveMap.FlatAppearance.BorderSize = 0;
            buttonHideBest.FlatAppearance.BorderSize = 0;
            buttonShowBest.FlatAppearance.BorderSize = 0;

            buttonFindBest.FlatStyle = FlatStyle.Flat;
            buttonOpenWebManual.FlatStyle = FlatStyle.Flat;
            buttonSaveMap.FlatStyle = FlatStyle.Flat;
            buttonHideBest.FlatStyle = FlatStyle.Flat;
            buttonShowBest.FlatStyle = FlatStyle.Flat;

            // Ползунок от 6 до Х, текущее значение 6
            // Мы передаем в радиус значение с ползунка помноженное на STEP_TRACK_BAR
            // То есть 6 это STEP_TRACK_BAR*6 метров
            trackBarRadius.Minimum = 6;
            // Максимальный радиус зависит от рассматриваемой площади
            trackBarRadius.Maximum = _FindRadius() / STEP_TRACK_BAR;
            // При входе отображать значение на ползунке текущего радиуса
            if (_mapModel.radiusNormaBufferZone / STEP_TRACK_BAR > trackBarRadius.Maximum)
                trackBarRadius.Value = 6;
            else
                trackBarRadius.Value = _mapModel.radiusNormaBufferZone / STEP_TRACK_BAR;
            labelRadiusLong.Text = "Радиус: " + (trackBarRadius.Value * STEP_TRACK_BAR).ToString() + " м.";

            // Настройка внешнего вида ползунка
            trackBarRadius.Cursor = Cursors.Hand;
            trackBarRadius.Orientation = Orientation.Horizontal;
            trackBarRadius.TickStyle = TickStyle.Both;

            // Размеры окна
            ClientSize = new Size(1374, 751);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Условные обозначения карты
            pictureBoxAutoNorma.Image = Properties.Resources.iconBestOptimum;
            pictureBoxFacility.Image = _mapModel.iconFacility;

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            // Начальные значения текстовых полей для нормы
            textBoxSummFacility.Text = _mapModel.implementedRateOfFacility.ToString();
            textBoxSummPopilation.Text = _mapModel.implementedRateOfPopulation.ToString();

            // Скрыть надпись изначально о состоянии нормы
            labelStateOfNormTerritory.Visible = false;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <returns>Радиус в метрах</returns>
        private int _FindRadius()
        {
            // Список крайних точек зоны анализа
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;

            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки зоны анализа
            for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
            {
                if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                    minX = listLocationBorderTerritory.listWithLocation[i].x;
                if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                    minY = listLocationBorderTerritory.listWithLocation[i].y;

                if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                    maxX = listLocationBorderTerritory.listWithLocation[i].x;
                if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                    maxY = listLocationBorderTerritory.listWithLocation[i].y;
            }

            GeoCoordinate leftPoint = new GeoCoordinate(minX, minY);
            GeoCoordinate rightPoint = new GeoCoordinate(maxX, maxY);
            int radius = Convert.ToInt32(leftPoint.GetDistanceTo(rightPoint) / 5);
            // Радиус - это 1/5 от диагонали загруженной зоны анализа
            return radius;
        }

        //! Флаг отображены ли объеты социальной инфраструктуры
        private bool _flagFacilityShow = false;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отобразить объекты социальной инфраструктуры
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void radioButtonShowFacility_Click(object sender, EventArgs e)
        {
            _flagFacilityShow = true;
            _mapView.DrawFacility();
            if (_flagFindNormaPoints)
                _mapView.DrawNormaIdealPointBufferZone(_tempListBufferZoneCandidate);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть объекты социальной инфраструктуры
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void radioButtonHideFacility_Click(object sender, EventArgs e)
        {
            _flagFacilityShow = false;
            _mapView.ClearFacility();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Перерисовать все слои
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _RedrawMap(object sender, EventArgs e)
        {
            if (_flagFacilityShow)
                radioButtonShowFacility_Click(sender, e);
            if (_flagFindNormaPoints)
                _mapView.DrawNormaIdealPointBufferZone(_tempListBufferZoneCandidate);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на GoogleMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void GoogleMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.GoogleMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на BingHybridMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void BingHybridMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.BingHybridMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на ArcGIS
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void ArcGISMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.ArcGIS_World_Topo_Map;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на WikiMapiaMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void WikiMapiaMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.WikiMapiaMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на GoogleTerrainMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void GoogleTerrainMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.GoogleTerrainMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на BingMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void BingMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.BingMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на OpenCycleTransportMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void OpenCycleTransportMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.OpenCycleTransportMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на GoogleHybridMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void GoogleHybridMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.GoogleHybridMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка смены карты на OpenCycleLandscapeMap
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void OpenCycleLandscapeMapMenuItem_Click(object sender, EventArgs e)
        {
            gmapNorm.MapProvider = GMapProviders.OpenCycleLandscapeMap;
            _RedrawMap(sender, e);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Валидация текстовых полей с числом нормы и населением
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <returns>Состояние корректности поле</returns>
        private string _CheckVadidateNorma()
        {
            // Если все поля введены
            if (!string.IsNullOrEmpty(textBoxSummFacility.Text) && !string.IsNullOrWhiteSpace(textBoxSummFacility.Text) &&
                !string.IsNullOrEmpty(textBoxSummPopilation.Text) && !string.IsNullOrWhiteSpace(textBoxSummPopilation.Text))
            {
                int tempValue;
                bool testForSummFacility = int.TryParse(textBoxSummFacility.Text, out tempValue);
                bool testForPopulation = int.TryParse(textBoxSummPopilation.Text, out tempValue);

                // Если во всех полях целые значения
                if (testForSummFacility == true && testForPopulation == true)
                {
                    int countFacility = Convert.ToInt32(textBoxSummFacility.Text);
                    int countPopulation = Convert.ToInt32(textBoxSummPopilation.Text);

                    // Если все значения больше 1
                    if (countFacility >= 1 && countPopulation >= 1)
                    {
                        // Населения на объект должно быть больше
                        if (countFacility < countPopulation)
                        {
                            // Сохранить значения нормы
                            _mapModel.implementedRateOfFacility = countFacility;
                            _mapModel.implementedRateOfPopulation = countPopulation;
                            return "Успешно";
                        }
                        else
                            return "Некорректно задана норма";
                    }
                    else
                        return "Все значения должны быть больше 1";
                }
                else
                    return "В поля должны быть введены только целые числа";
            }
            else
                return "Все поля в блоке \"Установка нормы на душу населения\" должны быть заполнены";
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <returns>Радиус в метрах</returns>
        private double _FindDiagonalTerritory()
        {
            // Список крайних точек зоны анализа
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;

            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки зоны анализа
            for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
            {
                if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                    minX = listLocationBorderTerritory.listWithLocation[i].x;
                if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                    minY = listLocationBorderTerritory.listWithLocation[i].y;

                if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                    maxX = listLocationBorderTerritory.listWithLocation[i].x;
                if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                    maxY = listLocationBorderTerritory.listWithLocation[i].y;
            }

            GeoCoordinate leftPoint = new GeoCoordinate(minX, minY);
            GeoCoordinate rightPoint = new GeoCoordinate(maxX, maxY);
            double diagonalTerritory = leftPoint.GetDistanceTo(rightPoint);
            return diagonalTerritory;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Очистить карту от нормы
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void HideResultsOfSearchNorm()
        {
            // Очистить карту от всего
            _mapView.ClearNormaPoints(_sublayerNorma);
            _sublayerNorma.overlay.IsVisibile = false;
            // Спрятать надпись о предыдущем анализе карты
            labelStateOfNormTerritory.Visible = false;
            // Флаг поиска нормы
            _flagFindNormaPoints = false;
            // Флаг сохранения нормы в картинке
            _flagSaveNorma = false;
        }

        //! Флаг успешного поиска нормы
        private bool _flagFindNormaPoints = false;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Найти ному
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void buttonFindBest_Click(object sender, EventArgs e)
        {
            // Для теста: 320 надо 43; 200 надо 340 даем 304; 400 даем 10; 420 очень плохо

            // Валидация заполненных полей
            string checkStatusNorma = _CheckVadidateNorma();
            if (checkStatusNorma == "Успешно")
            {
                // Сохранить радиус поиска
                _mapModel.radiusNormaBufferZone = trackBarRadius.Value * STEP_TRACK_BAR;

                // Получили население зоны анализа
                long globalPopulationTerritoryFromUserFile = _mapModel.CalculateCountPopulation();
                // Получили количество объектов социальной инфраструктуры на всей зоне анализа
                int globalFacilityTerritoryFromUserFile = _mapModel.CalculateCountFacility();

                // Если объектов социальной инфраструктуры меньше, чем людей
                if (globalFacilityTerritoryFromUserFile < globalPopulationTerritoryFromUserFile)
                {
                    // Количество людей на 1 объект социальной инфраструктуры при заданной норме
                    double countPopulationToOneFacility = _mapModel.implementedRateOfPopulation / _mapModel.implementedRateOfFacility;
                    // Делим население на норму (1 объект социальной инфраструктуры к людям) 
                    // = количество объектов социальной инфраструктуры в норме в зоне анализа
                    double countFacilityThisTerritoryNorm = Math.Ceiling(globalPopulationTerritoryFromUserFile / countPopulationToOneFacility);

                    // Если норма выше, чем текущее состояние объектов социальной инфраструктуры, то тут нехватка объектов социальной инфраструктуры
                    if (countFacilityThisTerritoryNorm > globalFacilityTerritoryFromUserFile)
                    {
                        // Разница в нужном и реальном количестве объектов социальной инфраструктуры в зоне анализа
                        double differenceBetweenNormAndTerritory = Math.Ceiling(countFacilityThisTerritoryNorm - globalFacilityTerritoryFromUserFile);
                        // Округлить разницу до целого числа
                        int cellingDifferenceBetweenNormAndTerritory = (int)differenceBetweenNormAndTerritory;

                        // Список крайних точек зоны анализа
                        SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

                        // Минимимум и максимум (х,у) за начало первая точка из списка
                        double minX = listLocationBorderTerritory.listWithLocation[0].x;
                        double minY = listLocationBorderTerritory.listWithLocation[0].y;
                        double maxX = listLocationBorderTerritory.listWithLocation[0].x;
                        double maxY = listLocationBorderTerritory.listWithLocation[0].y;

                        // Поиск левой верхней и правой нижней точки зоны анализа
                        for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
                        {
                            if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                                minX = listLocationBorderTerritory.listWithLocation[i].x;
                            if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                                minY = listLocationBorderTerritory.listWithLocation[i].y;
                            if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                                maxX = listLocationBorderTerritory.listWithLocation[i].x;
                            if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                                maxY = listLocationBorderTerritory.listWithLocation[i].y;
                        }

                        // Сохранили минимальный х
                        double saveMinX = minX;

                        // Список всех точек с полным обходом карты
                        List<PointLatLng> listWithFullPassTerritory = new List<PointLatLng>();

                        double stepX = Math.Round(_FindDiagonalTerritory() / 5908666, 3); // примерно 0.003
                        double stepY = Math.Round(_FindDiagonalTerritory() / 3545200, 3); // примерно 0.005

                        // Заполняем полигон зоны анализа точками
                        while (minY <= maxY)
                        {
                            while (minX <= maxX)
                            {
                                // Добавляем в список все точки прохода зоны анализа
                                listWithFullPassTerritory.Add(new PointLatLng(minX, minY));
                                minX = minX + stepX;
                            }
                            // Нарастили ось Y и еще раз по оси Х
                            minY = minY + stepY;
                            minX = saveMinX;
                        }

                        HideResultsOfSearchNorm();
                        _sublayerNorma.overlay.IsVisibile = true;

                        // Проход по всем точкам глобального прохода
                        for (int i = 0; i < listWithFullPassTerritory.Count; i++)
                        {
                            // Создаем точку
                            PointLatLng pointLatLng = new PointLatLng(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng);
                            // Если точка принадлежит какому-то полигону
                            if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
                                // Добавление точки в список авто-маркеров
                                _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng));
                        }

                        // Очистить список
                        listWithFullPassTerritory.Clear();

                        // Установка маркеров и буфреных зон, которые попали в полигоны
                        _mapView.DrawNormaBufferZone(_mapModel.radiusNormaBufferZone);

                        // Получить список буферных зон, которые попали в зону анализа
                        _mapModel.CreateListNormaBufferZones();

                        // Список буферных зон для поиска оптимума
                        List<BufferZone> listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

                        // Ситуация, когда все авто-точки имеют нули в критериях, надо увеличить радиус
                        if (listAutoBufferZones.Count == 0)
                        {
                            // Строка с ошибкой
                            var errorCriterion = new StringBuilder();
                            errorCriterion.AppendLine("Не удалось найти ни одного оптимума");
                            errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                            MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // Если хороших (ненулевых буферных зон) меньше, чем столько, сколько надо для зоны анализа
                        else if (listAutoBufferZones.Count > 0 && listAutoBufferZones.Count < cellingDifferenceBetweenNormAndTerritory)
                        {
                            MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones, _mapModel.listUserCriterion, listAutoBufferZones.Count);

                            // Получить точки для нормы, сколько можем найти
                            List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                            // Отрисовать звездочки оптимальных точек
                            _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                            _tempListBufferZoneCandidate = listOptimalPoint;

                            // Флаг поиска нормы
                            _flagFindNormaPoints = true;

                            labelStateOfNormTerritory.Visible = true;
                            labelStateOfNormTerritory.Text = "В зоне анализа не хватает " +
                                cellingDifferenceBetweenNormAndTerritory + " объекта(ов) инфраструктуры, мы можем посоветовать " +
                                listAutoBufferZones.Count + " наилучших мест. Это выгодный бизнес!";
                        }
                        // Если хороших (ненулевых буферных зон) больше, чем столько, сколько надо для зоны анализа
                        else
                        {
                            MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones,
                                _mapModel.listUserCriterion, cellingDifferenceBetweenNormAndTerritory);

                            // Получить точки для нормы, сколько требуется для нормы
                            List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                            // Отрисовать звездочки оптимальных точек (1-10)
                            _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                            _tempListBufferZoneCandidate = listOptimalPoint;

                            // Флаг поиска нормы    
                            _flagFindNormaPoints = true;

                            labelStateOfNormTerritory.Visible = true;
                            labelStateOfNormTerritory.Text = "В зоне анализа не хватает " +
                                cellingDifferenceBetweenNormAndTerritory + " объекта(ов) инфраструктуры. Это выгодный бизнес!";
                        }

                    }
                    // Если норма ниже, чем текущее состояние объектов социальной инфраструктуры, то тут изыбток
                    else
                    {
                        // Делим объекты социальной инфраструктуры в файле на объекты социальной инфраструктуры по норме * 100 %
                        double surplusPercentage = (globalFacilityTerritoryFromUserFile / countFacilityThisTerritoryNorm) * 100;

                        // Если % изыбтка от 100% до 120%, то еще можно 10 объектов социальной инфраструктуры открыть
                        if (surplusPercentage >= 100 && surplusPercentage <= 120)
                        {
                            // Список крайних точек зоны анализа
                            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

                            // Минимимум и максимум (х,у) за начало первая точка из списка
                            double minX = listLocationBorderTerritory.listWithLocation[0].x;
                            double minY = listLocationBorderTerritory.listWithLocation[0].y;
                            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
                            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

                            // Поиск левой верхней и правой нижней точки зоны анализа
                            for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
                            {
                                if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                                    minX = listLocationBorderTerritory.listWithLocation[i].x;
                                if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                                    minY = listLocationBorderTerritory.listWithLocation[i].y;
                                if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                                    maxX = listLocationBorderTerritory.listWithLocation[i].x;
                                if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                                    maxY = listLocationBorderTerritory.listWithLocation[i].y;
                            }

                            // Сохранили минимальный х
                            double saveMinX = minX;

                            // Список всех точек с полным обходом карты
                            List<PointLatLng> listWithFullPassTerritory = new List<PointLatLng>();

                            double stepX = Math.Round(_FindDiagonalTerritory() / 5908666, 3); // примерно 0.003
                            double stepY = Math.Round(_FindDiagonalTerritory() / 3545200, 3); // примерно 0.005

                            // Заполняем полигон зоны анализа точками
                            while (minY <= maxY)
                            {
                                while (minX <= maxX)
                                {
                                    // Добавляем в список все точки прохода зоны анализа
                                    listWithFullPassTerritory.Add(new PointLatLng(minX, minY));
                                    minX = minX + stepX;
                                }
                                // Нарастили ось Y и еще раз по оси Х
                                minY = minY + stepY;
                                minX = saveMinX;
                            }

                            HideResultsOfSearchNorm();
                            _sublayerNorma.overlay.IsVisibile = true;

                            // Идем по всем точкам глобального прохода
                            for (int i = 0; i < listWithFullPassTerritory.Count; i++)
                            {
                                // Создаем точку
                                PointLatLng pointLatLng = new PointLatLng(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng);
                                // Если точка принадлежит какому-то полигону
                                if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
                                    // Добавление точки в список точек нормы
                                    _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng));
                            }

                            // Очистить список
                            listWithFullPassTerritory.Clear();

                            // Установка маркеров и буфреных зон, которые попали в полигоны
                            _mapView.DrawNormaBufferZone(_mapModel.radiusNormaBufferZone);

                            // Получить список буферных зон, которые попали в зону анализа
                            _mapModel.CreateListNormaBufferZones();

                            // Список буферных зон для поиска оптимума
                            List<BufferZone> listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

                            // Ситуация, когда все авто-точки имеют нули в критериях, надо увеличить радиус
                            if (listAutoBufferZones.Count == 0)
                            {
                                // Строка с ошибкой
                                var errorCriterion = new StringBuilder();
                                errorCriterion.AppendLine("Не удалось найти ни одного оптимума");
                                errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                                MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            // Если хороших (ненулевых буферных зон) меньше, чем 10
                            else if (listAutoBufferZones.Count > 0 && listAutoBufferZones.Count < 10)
                            {
                                MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones,
                                    _mapModel.listUserCriterion, listAutoBufferZones.Count);

                                // Получить точки для нормы от 1 до 10
                                List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                                // Отрисовать звездочки оптимальных точек
                                _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                                _tempListBufferZoneCandidate = listOptimalPoint;

                                // Флаг поиска нормы
                                _flagFindNormaPoints = true;

                                labelStateOfNormTerritory.Visible = true;
                                labelStateOfNormTerritory.Text = "В зоне анализа хватает объектов инфраструктуры, но " +
                                    listAutoBufferZones.Count + " открыть ещё можно!";
                            }
                            else
                            {
                                MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones, _mapModel.listUserCriterion, 10);

                                // Получить точки для нормы 10 штук
                                List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                                // Отрисовать звездочки оптимальных точек 10 штук
                                _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                                _tempListBufferZoneCandidate = listOptimalPoint;

                                // Флаг поиска нормы
                                _flagFindNormaPoints = true;

                                labelStateOfNormTerritory.Visible = true;
                                labelStateOfNormTerritory.Text = "В зоне анализа хватает объектов инфраструктуры, при этом около 10 открыть ещё можно!";
                            }
                        }
                        // Если % изыбтка более 120%, то бизнес невыгодный, и дать 10 объектов социальной инфраструктуры при согласии пользователя
                        if (surplusPercentage > 120)
                        {
                            // При избытке объектов социальной инфраструктуры показать ли 10 оптимумов
                            if (MessageBox.Show("В зоне анализа много заданных объектов инфраструктуры. Показать несколько наилучших точек?", "Вопрос",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // Список крайних точек зоны анализа
                                SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

                                // Минимимум и максимум (х,у) за начало первая точка из списка
                                double minX = listLocationBorderTerritory.listWithLocation[0].x;
                                double minY = listLocationBorderTerritory.listWithLocation[0].y;
                                double maxX = listLocationBorderTerritory.listWithLocation[0].x;
                                double maxY = listLocationBorderTerritory.listWithLocation[0].y;

                                // Поиск левой верхней и правой нижней точки зоны анализа
                                for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
                                {
                                    if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                                        minX = listLocationBorderTerritory.listWithLocation[i].x;
                                    if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                                        minY = listLocationBorderTerritory.listWithLocation[i].y;
                                    if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                                        maxX = listLocationBorderTerritory.listWithLocation[i].x;
                                    if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                                        maxY = listLocationBorderTerritory.listWithLocation[i].y;
                                }

                                // Сохранили минимальный х
                                double saveMinX = minX;

                                // Список всех точек с полным обходом карты
                                List<PointLatLng> listWithFullPassTerritory = new List<PointLatLng>();

                                double stepX = Math.Round(_FindDiagonalTerritory() / 5908666, 3); // примерно 0.003
                                double stepY = Math.Round(_FindDiagonalTerritory() / 3545200, 3); // примерно 0.005

                                // Заполняем полигон зоны анализа точками
                                while (minY <= maxY)
                                {
                                    while (minX <= maxX)
                                    {
                                        // Добавляем в список все точки прохода зоны анализа
                                        listWithFullPassTerritory.Add(new PointLatLng(minX, minY));
                                        minX = minX + stepX;
                                    }
                                    // Нарастили ось Y и еще раз по оси Х
                                    minY = minY + stepY;
                                    minX = saveMinX;
                                }

                                HideResultsOfSearchNorm();
                                _sublayerNorma.overlay.IsVisibile = true;

                                // Идем по всем точкам глобального прохода
                                for (int i = 0; i < listWithFullPassTerritory.Count; i++)
                                {
                                    // Создаем точку
                                    PointLatLng pointLatLng = new PointLatLng(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng);
                                    // Если точка принадлежит какому-то полигону
                                    if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)      
                                        // Добавление точки в список точек нормы
                                        _sublayerNorma.listWithLocation.Add(new Location(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng));
                                }

                                // Очистить список
                                listWithFullPassTerritory.Clear();

                                // Установка маркеров и буфреных зон, которые попали в полигоны
                                _mapView.DrawNormaBufferZone(_mapModel.radiusNormaBufferZone);

                                // Получить список буферных зон, которые попали в зону анализа
                                _mapModel.CreateListNormaBufferZones();

                                // Список буферных зон для поиска оптимума
                                List<BufferZone> listAutoBufferZones = _mapModel.listNormaPointsBufferZone;

                                // Ситуация, когда все точки имеют нули в критериях, надо увеличить радиус
                                if (listAutoBufferZones.Count == 0)
                                {
                                    // Строка с ошибкой
                                    var errorCriterion = new StringBuilder();
                                    errorCriterion.AppendLine("Не удалось найти ни одного оптимума");
                                    errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                                    MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (listAutoBufferZones.Count > 0 && listAutoBufferZones.Count < 10)
                                {
                                    MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones,
                                        _mapModel.listUserCriterion, listAutoBufferZones.Count);

                                    // Получить несколько оптимумов
                                    List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                                    // Отрисовать звездочки оптимальных точек
                                    _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                                    _tempListBufferZoneCandidate = listOptimalPoint;

                                    // Флаг поиска нормы
                                    _flagFindNormaPoints = true;

                                    // Нет надписи изначально о состоянии нормы
                                    labelStateOfNormTerritory.Visible = true;
                                    labelStateOfNormTerritory.Text = "В зоне анализа хватает объектов инфраструктуры. Это невыгодный бизнес!";
                                }
                                else
                                {
                                    MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones, _mapModel.listUserCriterion, 10);

                                    // Получить 10 оптимумов
                                    List<BufferZone> listOptimalPoint = math.GetArrayWithOptimums();

                                    // Отрисовать звездочки оптимальных точек 10
                                    _mapView.DrawNormaIdealPointBufferZone(listOptimalPoint);
                                    _tempListBufferZoneCandidate = listOptimalPoint;

                                    // Флаг поиска нормы
                                    _flagFindNormaPoints = true;

                                    // Нет надписи изначально о состоянии нормы
                                    labelStateOfNormTerritory.Visible = true;
                                    labelStateOfNormTerritory.Text = "В зоне анализа хватает объектов инфраструктуры. Это невыгодный бизнес!";
                                }
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Неверно указаны данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(checkStatusNorma, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Открытие руководства пользователя в HTML
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void buttonOpenWebManual_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка щелчка по объекту социальной инфраструктуры
        /// </summary>
        /// <param name="item">Маркер</param>
        /// <param name="e">Кнопка, которой сделали щелчок</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // Если нажали левой кнопкой мыши по маркеру, то открыть окно с информацией об объекте социальной инфраструктуры
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < _mapModel.listUserFacilities.Count; i++)
                {
                    // Если координаты выбранной совпали с одной точкой из списка
                    if (item.Position.Lat == _mapModel.listUserFacilities[i].x && item.Position.Lng == _mapModel.listUserFacilities[i].y)
                    {
                        FacilityInfo form = new FacilityInfo(_mapModel.listUserFacilities[i], _mapModel);
                        form.ShowDialog();
                    }
                }
            }
        }

        //! Флаг сохранения нормы в виде изображения
        private bool _flagSaveNorma = false;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Сохранение карты
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void buttonSaveMap_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveMapDialog = new SaveFileDialog())
                {
                    // Формат изображения
                    saveMapDialog.Filter = "Image Files (*.png) | *.png";
                    // Название изображения
                    saveMapDialog.FileName = "Оптимальные точки для нормы на душу населения";
                    // Преобразование карты в картинку
                    Image image = gmapNorm.ToImage();
                    if (image != null)
                    {
                        using (image)
                        {
                            // Если пользователь подтвердил сохранение
                            if (saveMapDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Сохранение по выбранному пользователем пути картинку в формате png
                                string fileName = saveMapDialog.FileName;
                                if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                                    fileName += ".png";
                                image.Save(fileName);
                                // Норма сохранена
                                _flagSaveNorma = true;
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

        /*!
        \version 1.0
        */
        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SearchNormPerCapita_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил норму в виде изображения
            if (_flagSaveNorma == true)
                e.Cancel = false;
            else
            {
                // Если пользователь искал норму, но не сохранил изображение
                if (_flagFindNormaPoints == true)
                {
                    // Если пользователь не сохранил картинку, но при этом искал норму
                    if (MessageBox.Show("Вы не сохранили изображение карты с нормой. Выйти?", "Предупреждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        e.Cancel = false;
                    else
                        e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Закрытие на ESC
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SearchNormPerCapita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отобразить норму
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void buttonShowBest_Click(object sender, EventArgs e)
        {
            if (_flagFindNormaPoints)
                _sublayerNorma.overlay.IsVisibile = true;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть норму
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void buttonHideBest_Click(object sender, EventArgs e)
        {
            if (_flagFindNormaPoints)
                _sublayerNorma.overlay.IsVisibile = false;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скроллирование ползунка отображает текущее значение радиуса в метрах
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void trackBarTransparent_Scroll(object sender, EventArgs e)
        {
            // Переписать текст в надписи на текущее значение радиуса в метрах
            labelRadiusLong.Text = "Радиус: " + (trackBarRadius.Value * STEP_TRACK_BAR).ToString() + " м.";
        }
    }
}