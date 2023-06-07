using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Device.Location;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace Optimum
{
    public partial class MainMap : Form
    {
        // Локализация карты
        private LanguageType _languageOfMap;
        // Цвет интерфейса и дополнительных элементов
        private Color _colorForApplication, _colorForElements;
        // Путь к значку объекта социальной инфраструктуры
        private string _pathToIconFacility;
        // Название объекта социальной инфраструктуры
        private string _nameFacility;
        // Список критериев оптимальности
        private List<Criterion> _listCriteriaForSearch;
        // Список граничных точек зоны анализа
        private List<Location> _listPointsBorderTerritory;
        // Список полигонов
        private List<Polygon> _listPolygonsFromFile;
        // Список объектов социальной инфраструктуры
        private List<Facility> _listFacilitiesFromFile;
        // Точка центрирования карты
        private Location _locationCenterMap;
        // Путь к файлу с полигонами
        private string _pathFilePolygons;

        private MapModel _mapModel;
        private MapView _mapView;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="language">Локализация карты</param>
        /// <param name="colorApp">Цвет интерфейса</param>
        /// <param name="colorElem">Цвет дополнительных элементов</param>
        /// <param name="pathToIcon">Путь к значку объекта социальной инфраструктуры</param>
        /// <param name="nameFacility">Название объекта социальной инфраструктуры</param>
        /// <param name="listCriterion">Список критериев оптимальности</param>
        /// <param name="listPolygons">Список полигонов</param>
        /// <param name="pathFilePolygons">Путь к файлу с полигонами</param>
        /// <param name="listFacilities">Список объектов социальной инфраструктуры</param>
        /// <param name="listBorderPoints">Список граничных точек зоны анализа</param>
        /// <param name="center">Точка центрирования карты</param>
        public MainMap(LanguageType language, Color colorApp, Color colorElem, string pathToIcon, string nameFacility, List<Criterion> listCriterion,
            List<Polygon> listPolygons, string pathFilePolygons, List<Facility> listFacilities, List<Location> listBorderPoints, Location center)
        {
            _languageOfMap = language;
            _colorForApplication = colorApp;
            _colorForElements = colorElem;
            _pathToIconFacility = pathToIcon;
            _nameFacility = nameFacility;
            _listCriteriaForSearch = listCriterion;
            _listPolygonsFromFile = listPolygons;
            _pathFilePolygons = pathFilePolygons;
            _listFacilitiesFromFile = listFacilities;
            _listPointsBorderTerritory = listBorderPoints;
            _locationCenterMap = center;

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        /// <summary>
        /// Загрузка карты при открытии окна
        /// </summary>
        private void gMapControl_Load(object sender, EventArgs e)
        {
            // Инициализация данных и настройка карты
            _mapModel = new MapModel();
            _mapView = new MapView(_mapModel);
            _mapView.InitGMapControl(gmap, _locationCenterMap);
            GMapProvider.Language = _languageOfMap;

            // Центрирование карты
            _mapModel.centerMap = _locationCenterMap;

            // Настройка цвета интерфейса
            _mapModel.mainColor = _colorForApplication;
            _mapModel.secondaryColor = _colorForElements;

            // Настройка значка и названия объекта инфраструктуры
            _mapModel.pathToIconObjectFacility = _pathToIconFacility;
            _mapModel.nameObjectFacility = _nameFacility;

            // Настройка списка частных критериев оптимальности
            _mapModel.listUserCriterion = _listCriteriaForSearch;

            // Найстрока списка с объектами социальной инфраструктуры, полигонами и зоной анализа
            _mapModel.listUserPointsBorderTerritory = _listPointsBorderTerritory;
            _mapModel.listUserPolygons = _listPolygonsFromFile;
            _mapModel.pathToFilePolygonUser = _pathFilePolygons;
            _mapModel.listUserFacilities = _listFacilitiesFromFile;

            // Инициализация слоев на карте
            _mapModel.InitializationSublayersAndLists();
        }

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        // Слой для полигонов
        private SublayerPolygon _sublayerPolygon;
        // Слой для пользовательских точек-кандидатов
        private SublayerLocation _sublayerUserFacilityCandidate;
        // Слой для авто-оптимумов
        private SublayerLocation _sublayerAuto;
        // Валидация файлов
        private FileValidator _fileValidator;
        // Цвет раскраски полигонов
        private Color _colorForSquareColoring;

        // Ползунок для изменения прозрачности полигонов
        private TrackBar trackBarTransparent;
        // Ползунок для изменения толщины границ полигонов
        private TrackBar trackBarIntensity;
        // Ползунок для изменения количества градаций оттенков
        private TrackBar trackBarShades;
        // Выпадающий список с критериями для площадной раскраски
        private ComboBox comboBoxColoringPolygon;
        // Выпадающий список с поставщиками карт
        private ComboBox comboBoxProvidersMap;
        // Выпадающий список с количеством авто-оптимумов
        private ComboBox comboBoxAuto;
        // Панель с легендой карты
        private Panel panelLegendOfMap;
        // Надписи для ползунков
        private Label labelChangeIntensity;
        private Label labelChangeTransparent;
        // Нижняя выдвигающаяся панель
        private AnimatedBottomPanel bottomPanel;

        // Радио-кнопки для объектов социальной инфраструктуры
        private RadioButton radioButtonShowFacility;
        private RadioButton radioButtonClearFacility;
        // Радио-кнопки для отображения информации об объектах социальной инфраструктуры
        private RadioButton radioButtonShowAllInfo;
        private RadioButton radioButtonShowNameInfo;
        // Радио-кнопки для отображения зоны анализа
        private RadioButton radioButtonShowTerritory;
        private RadioButton radioButtonClearTerritory;

        // Групп-боксы
        private GroupBox groupBoxFacility;
        private GroupBox groupBoxInfoFacility;
        private GroupBox groupBoxTerritory;
        private GroupBox groupBoxPanelLegend;

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void Map_Load(object sender, EventArgs e)
        {
            // Установка цвета интерфейса и меню
            BackColor = _mapModel.mainColor;
            menuStrip.BackColor = _mapModel.secondaryColor;

            // Размеры главного окна
            ClientSize = new Size(1374, 751);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Инициализация слоев на карте
            _sublayerPolygon = _mapModel.sublayerPolygonSquare;
            _sublayerUserFacilityCandidate = _mapModel.sublayerUserPoints;
            _sublayerAuto = _mapModel.sublayerAutoPoints;
            _fileValidator = new FileValidator();

            // Создать обе выдвигающиеся панели
            _SettingsAnimationPanels();

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            // Спрятать нижнюю панель
            _DisplayLegendElements(false);

            if (_mapModel.nameObjectFacility.Length <= 12)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 11);
                labelFacility.Font = new Font("Segoe UI", 10);
            }
            if (_mapModel.nameObjectFacility.Length >= 13 && _mapModel.nameObjectFacility.Length <= 17)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 8);
                labelFacility.Font = new Font("Segoe UI", 8);
            }
            if (_mapModel.nameObjectFacility.Length >= 18)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 6);
                labelFacility.Font = new Font("Segoe UI", 6);
            }

            // Начальная установка переключателей
            radioButtonClearFacility.Checked = true;
            radioButtonClearFacility_Click(sender, e);
            radioButtonShowNameInfo.Checked = true;
            radioButtonClearShowNameInfo_Click(sender, e);
            radioButtonClearTerritory.Checked = true;
            radioButtonClearTerritory_Click(sender, e);

            // Заполнение выпадающих списков с раскраской критериями
            for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
            {
                comboBoxColoringIcons.Items.Add(_mapModel.listUserCriterion[i].nameOfCriterion);
                comboBoxColoringPolygon.Items.Add(_mapModel.listUserCriterion[i].nameOfCriterion);
            }
            // Добавить "Скрыть" к каждому списку
            comboBoxColoringIcons.Items.Add("Скрыть");
            comboBoxColoringPolygon.Items.Add("Скрыть");
            // Отмена редактирования выпадающих списков + выставить "Скрыть" по умолчанию
            comboBoxColoringIcons.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColoringIcons.SelectedItem = "Скрыть";
            comboBoxColoringPolygon.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColoringPolygon.SelectedItem = "Скрыть";

            // Заполнение выпадающего списка вариантами от 1 до 10 для поиска авто-оптимумов
            for (int i = 1; i <= 10; i++)
                comboBoxAuto.Items.Add(i.ToString());
            comboBoxAuto.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAuto.SelectedItem = "10";

            // Инициализация выпадающего списка в зависимости от выбранной локализации карты
            if (_languageOfMap == LanguageType.English)
            {
                // Английская локализация
                comboBoxProvidersMap.Items.Add("Google");
                comboBoxProvidersMap.Items.Add("GoogleTerrainMap");
                comboBoxProvidersMap.Items.Add("BingHybridMap");
                comboBoxProvidersMap.Items.Add("BingMap");
                comboBoxProvidersMap.Items.Add("ArcGIS");
                comboBoxProvidersMap.Items.Add("OpenCycleTransportMap");
                comboBoxProvidersMap.Items.Add("WikiMapiaMap");
            }
            else
            {
                // Русская локализация
                comboBoxProvidersMap.Items.Add("Google");
                comboBoxProvidersMap.Items.Add("GoogleHybridMap");
                comboBoxProvidersMap.Items.Add("GoogleTerrainMap");
                comboBoxProvidersMap.Items.Add("OpenCycleLandscapeMap");
            }
            // Настройка выпадающего списка с поставщиками карт
            comboBoxProvidersMap.SelectedItem = "Google";
            comboBoxProvidersMap.DropDownStyle = ComboBoxStyle.DropDownList;

            // Выставить загруженный значок
            Bitmap pathImageToBitmap = (Bitmap)Image.FromFile(_mapModel.pathToIconObjectFacility);
            Image iconFacility = pathImageToBitmap;
            pictureBoxFacility.Image = iconFacility;

            // Цвет фона у картинок
            pictureBoxMarker.BackColor = _mapModel.mainColor;
            pictureBoxFacility.BackColor = _mapModel.mainColor;
            pictureBoxAuto.BackColor = _mapModel.mainColor;
            // Цвет фона у надписей
            labelUserMarker.BackColor = _mapModel.mainColor;
            labelFacility.BackColor = _mapModel.mainColor;
            labelAutoSearch.BackColor = _mapModel.mainColor;
            labelLegendForMap.BackColor = _mapModel.mainColor;

            // Начальный цвет площадной раскраски (зеленый)
            _colorForSquareColoring = Color.FromArgb(255, 0, 255, 0);

            // Минимум и максимум толщины границ
            trackBarIntensity.Minimum = 0;
            trackBarIntensity.Maximum = 3;
            trackBarIntensity.Value = 1;

            // Минимум и максимум прозрачности раскраски
            trackBarTransparent.Minimum = 0;
            trackBarTransparent.Maximum = 5;
            trackBarTransparent.Value = 5;

            // Минимум и максимум для градаций оттенков
            trackBarShades.Minimum = 1;
            trackBarShades.Maximum = 10;
            trackBarShades.Value = 8;

            // Таймер для перерисовки панели при ее сворачивании
            timerCheckPanel.Start();

            // Значок для маркера пользователя
            pictureBoxMarker.Image = Properties.Resources.iconUserFacility;
            // Значок для маркера авто-оптимумов
            pictureBoxAuto.Image = Properties.Resources.iconBestOptimum;
        }

        /// <summary>
        /// Изменение отображения выдвигающейся нижней панели
        /// </summary>
        /// <param name="modeDisplay">Флаг отображения</param>
        private void _DisplayLegendElements(bool modeDisplay)
        {
            bottomPanel.Visible = modeDisplay;
            groupBoxPanelLegend.Visible = modeDisplay;
            labelChangeIntensity.Visible = modeDisplay;
            labelChangeTransparent.Visible = modeDisplay;
            trackBarIntensity.Visible = modeDisplay;
            trackBarTransparent.Visible = modeDisplay;
        }

        /// <summary>
        ///  Создание анимированных панелей на главном окне
        /// </summary>
        private void _SettingsAnimationPanels()
        {
            // Создание левой панели
            AnimatedLeftPanel leftPanel = new AnimatedLeftPanel(new Size(312, 490), 70, _colorForApplication, AnimatedLeftPanel.stateEnum.open);
            {
                // Прозрачный фон, не показывать границы панели
                leftPanel.BackColor = Color.Transparent;
                leftPanel.BorderStyle = BorderStyle.None;

                // Показывать объекты социальной инфраструктуры
                radioButtonShowFacility = new RadioButton();
                {
                    radioButtonShowFacility.Text = "Отобразить";
                    radioButtonShowFacility.Font = new Font("Segoe UI", 12);
                    radioButtonShowFacility.Left = 8;
                    radioButtonShowFacility.Top = 20;
                    radioButtonShowFacility.Click += radioButtonShowFacility_Click;
                    radioButtonShowFacility.Size = new Size(150, 24);
                    radioButtonShowFacility.Cursor = Cursors.Hand;
                    radioButtonShowFacility.TabIndex = 1;
                }

                // Скрыть объекты социальной инфраструктуры
                radioButtonClearFacility = new RadioButton();
                {
                    radioButtonClearFacility.Text = "Скрыть";
                    radioButtonClearFacility.Font = new Font("Segoe UI", 12);
                    radioButtonClearFacility.Left = 8;
                    radioButtonClearFacility.Top = 45;
                    radioButtonClearFacility.Click += radioButtonClearFacility_Click;
                    radioButtonClearFacility.Size = new Size(150, 24);
                    radioButtonClearFacility.Cursor = Cursors.Hand;
                    radioButtonClearFacility.TabIndex = 2;
                }

                groupBoxFacility = new GroupBox();
                {
                    groupBoxFacility.Font = new Font("Segoe UI", 12);
                    groupBoxFacility.Size = new Size(200, 75);
                    groupBoxFacility.Left = 10;
                    groupBoxFacility.Top = 10;
                }
                groupBoxFacility.Controls.Add(radioButtonShowFacility);
                groupBoxFacility.Controls.Add(radioButtonClearFacility);
                leftPanel.Controls.Add(groupBoxFacility);

                // Показывать все данные об объектах социальной инфраструктуры при наведении на маркер
                radioButtonShowAllInfo = new RadioButton();
                {
                    radioButtonShowAllInfo.Text = "Все данные";
                    radioButtonShowAllInfo.Font = new Font("Segoe UI", 12);
                    radioButtonShowAllInfo.Left = 8;
                    radioButtonShowAllInfo.Top = 45;
                    radioButtonShowAllInfo.Click += radioButtonShowAllInfo_Click;
                    radioButtonShowAllInfo.Size = new Size(150, 24);
                    radioButtonShowAllInfo.Cursor = Cursors.Hand;
                    radioButtonShowAllInfo.TabIndex = 3;
                }

                // Показывать только название объекта социальной инфраструктуры при наведении на маркер
                radioButtonShowNameInfo = new RadioButton();
                {
                    radioButtonShowNameInfo.Text = "Название объекта";
                    radioButtonShowNameInfo.Font = new Font("Segoe UI", 12);
                    radioButtonShowNameInfo.Left = 8;
                    radioButtonShowNameInfo.Top = 22;
                    radioButtonShowNameInfo.Click += radioButtonClearShowNameInfo_Click;
                    radioButtonShowNameInfo.Size = new Size(220, 24);
                    radioButtonShowNameInfo.Cursor = Cursors.Hand;
                    radioButtonShowNameInfo.TabIndex = 4;
                }

                groupBoxInfoFacility = new GroupBox();
                {
                    groupBoxInfoFacility.Text = "Всплывающая подсказка";
                    groupBoxInfoFacility.Font = new Font("Segoe UI", 11);
                    groupBoxInfoFacility.Size = new Size(200, 75);
                    groupBoxInfoFacility.Left = 10;
                    groupBoxInfoFacility.Top = 95;
                }
                groupBoxInfoFacility.Controls.Add(radioButtonShowNameInfo);
                groupBoxInfoFacility.Controls.Add(radioButtonShowAllInfo);
                leftPanel.Controls.Add(groupBoxInfoFacility);

                // Показывать зону анализа
                radioButtonShowTerritory = new RadioButton();
                {
                    radioButtonShowTerritory.Text = "Отобразить";
                    radioButtonShowTerritory.Font = new Font("Segoe UI", 12);
                    radioButtonShowTerritory.Left = 8;
                    radioButtonShowTerritory.Top = 22;
                    radioButtonShowTerritory.Click += radioButtonShowTerritory_Click;
                    radioButtonShowTerritory.Size = new Size(150, 24);
                    radioButtonShowTerritory.Cursor = Cursors.Hand;
                    radioButtonShowTerritory.TabIndex = 5;
                }

                // Скрыть зону анализа
                radioButtonClearTerritory = new RadioButton();
                {
                    radioButtonClearTerritory.Text = "Скрыть";
                    radioButtonClearTerritory.Font = new Font("Segoe UI", 12);
                    radioButtonClearTerritory.Left = 8;
                    radioButtonClearTerritory.Top = 45;
                    radioButtonClearTerritory.Click += radioButtonClearTerritory_Click;
                    radioButtonClearTerritory.Size = new Size(150, 24);
                    radioButtonClearTerritory.Cursor = Cursors.Hand;
                    radioButtonClearTerritory.TabIndex = 6;
                }

                groupBoxTerritory = new GroupBox();
                {
                    groupBoxTerritory.Text = "Зона анализа";
                    groupBoxTerritory.Font = new Font("Segoe UI", 11);
                    groupBoxTerritory.Size = new Size(200, 75);
                    groupBoxTerritory.Left = 10;
                    groupBoxTerritory.Top = 180;
                }
                groupBoxTerritory.Controls.Add(radioButtonShowTerritory);
                groupBoxTerritory.Controls.Add(radioButtonClearTerritory);
                leftPanel.Controls.Add(groupBoxTerritory);

                // Надпись
                Label labelPolygonColoring = new Label();
                {
                    labelPolygonColoring.Text = "Площадная раскраска по критерию:";
                    labelPolygonColoring.Size = new Size(300, 25);
                    labelPolygonColoring.Font = new Font("Segoe UI", 12);
                    labelPolygonColoring.Left = 9;
                    labelPolygonColoring.Top = 260;
                }

                // Выпадающий список с критериями для площадной раскраски
                comboBoxColoringPolygon = new ComboBox();
                {
                    comboBoxColoringPolygon.Font = new Font("Segoe UI", 12);
                    comboBoxColoringPolygon.Left = 10;
                    comboBoxColoringPolygon.Top = 285;
                    comboBoxColoringPolygon.Width = 250;
                    comboBoxColoringPolygon.SelectedIndexChanged += comboBoxColoringPolygon_SelectedIndexChanged;
                    comboBoxColoringPolygon.MouseWheel += new MouseEventHandler(_ComboBoxMouseWheel);
                    comboBoxColoringPolygon.BackColor = _mapModel.mainColor;
                    comboBoxColoringPolygon.Cursor = Cursors.Hand;
                    comboBoxColoringPolygon.TabIndex = 7;
                }
                leftPanel.Controls.Add(labelPolygonColoring);
                leftPanel.Controls.Add(comboBoxColoringPolygon);

                // Надпись
                Label labelIconColoring = new Label();
                {
                    labelIconColoring.Text = "Точечная раскраска по критерию:";
                    labelIconColoring.Size = new Size(300, 25);
                    labelIconColoring.Font = new Font("Segoe UI", 12);
                    labelIconColoring.Left = 9;
                    labelIconColoring.Top = 320;
                }

                // Выпадающий список с критериями для точечной раскраски
                comboBoxColoringIcons = new ComboBox();
                {
                    comboBoxColoringIcons.Font = new Font("Segoe UI", 12);
                    comboBoxColoringIcons.Left = 10;
                    comboBoxColoringIcons.Top = 345;
                    comboBoxColoringIcons.Width = 250;
                    comboBoxColoringIcons.SelectedIndexChanged += comboBoxColoringIcons_SelectedIndexChanged;
                    comboBoxColoringIcons.MouseWheel += new MouseEventHandler(_ComboBoxMouseWheel);
                    comboBoxColoringIcons.BackColor = _mapModel.mainColor;
                    comboBoxColoringIcons.Cursor = Cursors.Hand;
                    comboBoxColoringIcons.TabIndex = 8;
                }
                leftPanel.Controls.Add(labelIconColoring);
                leftPanel.Controls.Add(comboBoxColoringIcons);

                // Надпись
                Label labelCountOfOptimum = new Label();
                {
                    labelCountOfOptimum.Text = "Количество оптимумов:";
                    labelCountOfOptimum.Size = new Size(190, 25);
                    labelCountOfOptimum.Font = new Font("Segoe UI", 12);
                    labelCountOfOptimum.Left = 9;
                    labelCountOfOptimum.Top = 383;
                }

                // Выпадающий список с критериями для точечной раскраски
                comboBoxAuto = new ComboBox();
                {
                    comboBoxAuto.Font = new Font("Segoe UI", 12);
                    comboBoxAuto.Left = 200;
                    comboBoxAuto.Top = 380;
                    comboBoxAuto.Width = 60;
                    comboBoxAuto.BackColor = _mapModel.mainColor;
                    comboBoxAuto.Cursor = Cursors.Hand;
                    comboBoxAuto.TabIndex = 9;
                }

                leftPanel.Controls.Add(labelCountOfOptimum);
                leftPanel.Controls.Add(comboBoxAuto);

                // Надпись
                Label labelProviderMap = new Label();
                {
                    labelProviderMap.Text = "Поставщик карты:";
                    labelProviderMap.Size = new Size(200, 25);
                    labelProviderMap.Font = new Font("Segoe UI", 12);
                    labelProviderMap.Left = 9;
                    labelProviderMap.Top = 412;
                }

                // Выпадающий список с поставщиками карт в зависимости от локализации карты
                comboBoxProvidersMap = new ComboBox();
                {
                    comboBoxProvidersMap.Font = new Font("Segoe UI", 12);
                    comboBoxProvidersMap.Left = 10;
                    comboBoxProvidersMap.Top = 437;
                    comboBoxProvidersMap.Width = 250;
                    comboBoxProvidersMap.SelectedIndexChanged += comboBoxProvidersMap_SelectedIndexChanged;
                    comboBoxProvidersMap.MouseWheel += new MouseEventHandler(_ComboBoxMouseWheel);
                    comboBoxProvidersMap.BackColor = _mapModel.mainColor;
                    comboBoxProvidersMap.Cursor = Cursors.Hand;
                    comboBoxProvidersMap.TabIndex = 10;
                }
                leftPanel.Controls.Add(labelProviderMap);
                leftPanel.Controls.Add(comboBoxProvidersMap);

            }
            Controls.Add(leftPanel);
            gmap.SendToBack();

            // Создание нижней панели
            bottomPanel = new AnimatedBottomPanel(new Size(1182, 150), 540, _colorForApplication, AnimatedBottomPanel.stateEnum.open, _mapModel);
            {
                // Прозрачный фон, не показывать границы панели
                bottomPanel.BorderStyle = BorderStyle.None;
                bottomPanel.BackColor = Color.Transparent;
                bottomPanel.Left = 180;

                // Панель с легендой карты
                panelLegendOfMap = new Panel();
                {
                    panelLegendOfMap.Font = new Font("Segoe UI", 12);
                    panelLegendOfMap.Left = 10;
                    panelLegendOfMap.Top = 25;
                    panelLegendOfMap.Click += panelLegendOfMap_Click;
                    panelLegendOfMap.BackColor = _mapModel.mainColor;
                    panelLegendOfMap.Size = new Size(505, 60);
                    panelLegendOfMap.Cursor = Cursors.Hand;
                }

                groupBoxPanelLegend = new GroupBox();
                {
                    groupBoxPanelLegend.Text = "Легенда карты";
                    groupBoxPanelLegend.Font = new Font("Segoe UI", 12);
                    groupBoxPanelLegend.Size = new Size(532, 98);
                    groupBoxPanelLegend.Left = 10;
                    groupBoxPanelLegend.Top = 43;
                }
                groupBoxPanelLegend.Controls.Add(panelLegendOfMap);
                bottomPanel.Controls.Add(groupBoxPanelLegend);

                // Надпись
                labelChangeTransparent = new Label();
                {
                    labelChangeTransparent.Text = "Прозрачность раскраски";
                    labelChangeTransparent.Size = new Size(170, 25);
                    labelChangeTransparent.Font = new Font("Segoe UI", 10);
                    labelChangeTransparent.Left = 546;
                    labelChangeTransparent.Top = 47;
                }

                // Ползунок прозрачности полигонов
                trackBarTransparent = new TrackBar();
                {
                    trackBarTransparent.Location = new Point(550, 75);
                    trackBarTransparent.BackColor = _mapModel.secondaryColor;
                    trackBarTransparent.Cursor = Cursors.Hand;
                    trackBarTransparent.Orientation = Orientation.Horizontal;
                    trackBarTransparent.Size = new Size(200, 45);
                    trackBarTransparent.TickStyle = TickStyle.Both;
                    trackBarTransparent.Scroll += trackBarTransparent_Scroll;
                }
                bottomPanel.Controls.Add(labelChangeTransparent);
                bottomPanel.Controls.Add(trackBarTransparent);

                // Надпись
                labelChangeIntensity = new Label();
                {
                    labelChangeIntensity.Text = "Толщина границ полигонов";
                    labelChangeIntensity.Size = new Size(200, 25);
                    labelChangeIntensity.Font = new Font("Segoe UI", 10);
                    labelChangeIntensity.Left = 765;
                    labelChangeIntensity.Top = 47;
                }

                // Ползунок толщины границ полигонов
                trackBarIntensity = new TrackBar();
                {
                    trackBarIntensity.Location = new Point(770, 75);
                    trackBarIntensity.BackColor = _mapModel.secondaryColor;
                    trackBarIntensity.Cursor = Cursors.Hand;
                    trackBarIntensity.Orientation = Orientation.Horizontal;
                    trackBarIntensity.Size = new Size(180, 45);
                    trackBarIntensity.TickStyle = TickStyle.Both;
                    trackBarIntensity.Scroll += trackBarIntensity_Scroll;
                }

                bottomPanel.Controls.Add(labelChangeIntensity);
                bottomPanel.Controls.Add(trackBarIntensity);

                // Надпись
                Label labelChangeShades = new Label();
                {
                    labelChangeShades.Text = "Количество оттенков раскраски";
                    labelChangeShades.Size = new Size(300, 25);
                    labelChangeShades.Font = new Font("Segoe UI", 10);
                    labelChangeShades.Left = 965;
                    labelChangeShades.Top = 47;
                }

                // Ползунок количества оттенков раскраски
                trackBarShades = new TrackBar();
                {
                    trackBarShades.Location = new Point(970, 75);
                    trackBarShades.BackColor = _mapModel.secondaryColor;
                    trackBarShades.Cursor = Cursors.Hand;
                    trackBarShades.Orientation = Orientation.Horizontal;
                    trackBarShades.Size = new Size(200, 45);
                    trackBarShades.TickStyle = TickStyle.Both;
                    trackBarShades.Scroll += trackBarShades_Scroll;
                }

                bottomPanel.Controls.Add(labelChangeShades);
                bottomPanel.Controls.Add(trackBarShades);

            }
            Controls.Add(bottomPanel);
            gmap.SendToBack();
        }

        /// <summary>
        /// Отключение скрола для выпадающего списка на колесо мыши
        /// </summary>
        private void _ComboBoxMouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        /// <summary>
        /// Отобразить на карте объекты социальной инфраструктуры
        /// </summary>
        private void radioButtonShowFacility_Click(object sender, EventArgs e)
        {
            // Отобразить объекты социальной инфраструктуры
            _mapView.DrawFacility();
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Скрыть на карте объекты социальной инфраструктуры
        /// </summary>
        private void radioButtonClearFacility_Click(object sender, EventArgs e)
        {
            _mapView.ClearFacility();
        }

        /// <summary>
        /// Проверить, отображены ли на карте объекты социальной инфраструктуры
        /// </summary>
        private void _CheckSelectedRadioButtonFacility(object sender, EventArgs e)
        {
            if (radioButtonShowFacility.Checked)
                radioButtonShowFacility_Click(sender, e);
            else
                radioButtonClearFacility_Click(sender, e);
        }

        /// <summary>
        /// Показать только название объектов социальной инфраструктуры при наведении на маркер
        /// </summary>
        private void radioButtonClearShowNameInfo_Click(object sender, EventArgs e)
        {
            _mapView.SetHoverTextForFacility(1);
            _CheckSelectedRadioButtonFacility(sender, e);
        }

        /// <summary>
        /// Показать всю информацию об объектах социальной инфраструктуры при наведении на маркер
        /// </summary>
        private void radioButtonShowAllInfo_Click(object sender, EventArgs e)
        {
            _mapView.SetHoverTextForFacility(2);
            _CheckSelectedRadioButtonFacility(sender, e);
        }

        /// <summary>
        /// Обработка клика по маркеру
        /// </summary>
        /// <param name="item">Маркер на карте</param>
        /// <param name="e">Кнопка, которой сделали щелчок по маркеру</param>
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

            // Если нажали правой кнопкой мыши по маркеру для его удаления
            if (e.Button == MouseButtons.Right)
            {
                // Получить список оптимальных точек
                List<OptimalZone> optimalZones = _mapModel.optimalZones;
                // Округлить все оптимальные точки до 7 знаков после запятой
                for (int i = 0; i < optimalZones.Count; i++)
                {
                    optimalZones[i].optimalZone.x = Math.Round(optimalZones[i].optimalZone.x, 6);
                    optimalZones[i].optimalZone.y = Math.Round(optimalZones[i].optimalZone.y, 6);
                }

                // Проход по всем маркерам на карте
                for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count; i++)
                {
                    // Если координаты выбранной точки совпали с одной точкой из списка
                    if (item.Position.Lat == _sublayerUserFacilityCandidate.listWithLocation[i].x &&
                        item.Position.Lng == _sublayerUserFacilityCandidate.listWithLocation[i].y)
                    {
                        // Координаты текущей точки
                        double roundX_Location = Math.Round(item.Position.Lat, 6);
                        double roundY_Location = Math.Round(item.Position.Lng, 6);

                        // Проверка, удалить пытаются оптимальную точку или точку-кандидат
                        bool flagDeleteOneOfTheOptimums = false;
                        for (int j = 0; j < optimalZones.Count; j++)
                        {
                            // Удаление одного из оптимумов
                            if (optimalZones[j].optimalZone.x == roundX_Location && optimalZones[j].optimalZone.y == roundY_Location)
                            {
                                // Убрать данный маркер с карты
                                _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                                // Удалить из списка оптимальных точек этот маркер
                                _mapModel.optimalZones.Remove(optimalZones[j]);
                                // Перерисовать все маркеры на карте
                                _mapView.DrawPointBufferZone();
                                // Флаг удаления оптимума
                                flagDeleteOneOfTheOptimums = true;
                                if (_mapModel.optimalZones.Count == 0)
                                    // Обнулить флаг найденных оптимумов, если последний оптимум был удален
                                    _searchOptimum = false;
                            }
                        }
                        // Если удаляют точку-кандидат
                        if (!flagDeleteOneOfTheOptimums)
                        {
                            // Убрать данный маркер с карты
                            _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                            // Перерисовать все маркеры на карте
                            _mapView.DrawPointBufferZone();
                        }
                        // Если были найдены авто-оптимумы, отобразить их
                        if (_isFindAutoCandidates && !_isAutoHide)
                            _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
                    }
                }
            }
        }

        /// <summary>
        /// Отобразить зону анализа
        /// </summary>
        private void radioButtonShowTerritory_Click(object sender, EventArgs e)
        {
            _mapView.DrawTerritory();
            // Перерисовать площадную, точечную раскраски, объекты социальной инфраструктуры, авто-оптимумы, точки-кандидаты
            comboBoxColoringPolygon_SelectedIndexChanged(sender, e);
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            _CheckSelectedRadioButtonFacility(sender, e);
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Скрыть зону анализа
        /// </summary>
        private void radioButtonClearTerritory_Click(object sender, EventArgs e)
        {
            _mapView.ClearTerritory();
        }

        /// <summary>
        /// Проверить, отображена ли на карте зона анализа
        /// </summary>
        private void _CheckSelectedRadioButtonTerritory(object sender, EventArgs e)
        {
            if (radioButtonShowTerritory.Checked)
                radioButtonShowTerritory_Click(sender, e);
            else
                radioButtonClearTerritory_Click(sender, e);
        }

        /// <summary>
        /// Перерисовать все слои на карте
        /// </summary>
        private void _RedrawMap(object sender, EventArgs e)
        {
            // Перерисовать на карте площадную раскраску
            comboBoxColoringPolygon_SelectedIndexChanged(sender, e);
            // Перерисовать на карте точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать на карте объекты социальной инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать на карте зону анализа
            _CheckSelectedRadioButtonTerritory(sender, e);
            // Перерисовать точки-кандидаты
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                // Перерисовать авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Центрирование карты
        /// </summary>
        private void CenteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapView.ChangeCenterMap(_mapModel.centerMap);
        }

        // Флаг входа в приложение
        private bool _flagStartApplication = true;
        /// <summary>
        /// Обработка выбора поставщика карты
        /// </summary>
        private void comboBoxProvidersMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получить поставщика карты из выпадающего списка
            string _selectedProvider = comboBoxProvidersMap.Text;

            // Если выбран Google
            if (_selectedProvider == "Google")
            {
                // Если первый вход в приложение
                if (_flagStartApplication)
                    _flagStartApplication = false;
                // Если просто выбрали карту Google
                else
                {
                    gmap.MapProvider = GMapProviders.GoogleMap;
                    _RedrawMap(sender, e);
                }
            }
            else if (_selectedProvider == "ArcGIS")
            {
                gmap.MapProvider = GMapProviders.ArcGIS_World_Street_Map;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "BingHybridMap")
            {
                gmap.MapProvider = GMapProviders.BingHybridMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "WikiMapiaMap")
            {
                gmap.MapProvider = GMapProviders.WikiMapiaMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "GoogleTerrainMap")
            {
                gmap.MapProvider = GMapProviders.GoogleTerrainMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "BingMap")
            {
                gmap.MapProvider = GMapProviders.BingMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "OpenCycleTransportMap")
            {
                gmap.MapProvider = GMapProviders.OpenCycleTransportMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "GoogleHybridMap")
            {
                gmap.MapProvider = GMapProviders.GoogleHybridMap;
                _RedrawMap(sender, e);
            }
            else if (_selectedProvider == "OpenCycleLandscapeMap")
            {
                gmap.MapProvider = GMapProviders.OpenCycleLandscapeMap;
                _RedrawMap(sender, e);
            }
        }

        // Номер критерия, по которому отображают площадную раскраску
        private int _indexOfSelectedCriterion = -1;
        /// <summary>
        /// Изменение критерия в площадной раскраске
        /// </summary>
        private void comboBoxColoringPolygon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Название выбранного критерия для раскраски
            string nameCriterionPolygonColoring = comboBoxColoringPolygon.Text;

            // Если выбрано "Скрыть", то
            if (nameCriterionPolygonColoring == "Скрыть")
            {
                // Убрать нижнюю панель
                _DisplayLegendElements(false);
                // Очистить слой с раскраской
                _mapView.ClearSquareColoring(_sublayerPolygon);
            }
            // Если выбран некоторый критерий
            else
            {
                // Найти индекс выбранного критерия для раскраски
                for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
                    if (_mapModel.listUserCriterion[i].nameOfCriterion == nameCriterionPolygonColoring)
                        _indexOfSelectedCriterion = i;

                // Отобразить нижнюю панель
                _DisplayLegendElements(true);
                // Отобразить площадную раскраску
                _DrawPolygonColoring();
                // Перерисовать объекты социальной инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать точечную раскраску
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                gmap.Refresh();
                // Перерисовать точки-кандидаты
                _mapView.DrawPointBufferZone();
                if (_isFindAutoCandidates && !_isAutoHide)
                    // Перерисовать авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
            }
        }

        /// <summary>
        /// Обработка тика таймера
        /// </summary>
        private void timerCheckPanel_Tick(object sender, EventArgs e)
        {
            // Панель перерисовывается только, если она была скрыта и вновь открыта
            if (_mapModel.stateOfBottomPanel == "Open")
            {
                // Перерисовать все слои
                _RedrawMap(sender, e);
                _mapModel.stateOfBottomPanel = "Close";
            }
        }

        // Отрисовка границ
        private Pen _penBoundary;
        // Начальная толщина границ
        private int _intensityPolygons = 1;
        // Начальная прозрачность полигонов
        private int _transparentPolygons = 255;
        /// <summary>
        /// Отрисовка полигонов
        /// </summary>
        private void _DrawPolygonColoring()
        {
            // Установка толщины границ
            if (_intensityPolygons >= 1)
                _penBoundary = new Pen(Color.FromArgb(255, 255, 0, 0), _intensityPolygons);
            else
                _penBoundary = new Pen(Color.FromArgb(0, 0, 0, 0), 0);

            // Удаление слоя площадной раскраски
            _sublayerPolygon.overlay.Polygons.Clear();
            _sublayerPolygon.overlay.Markers.Clear();
            gmap.Overlays.Remove(_sublayerPolygon.overlay);
            _sublayerPolygon.overlay.Clear();

            // Заполнение таблицы данными из списка с полигонами
            DataTable dataPolygons = _mapModel.CreateTableFromPolygons();
            int countOfPolygons = dataPolygons.Rows.Count;

            // Для каждого полигона создается список граничных точек этого полигона
            List<PointLatLng>[] listBoundaryForEveryPolygon = new List<PointLatLng>[countOfPolygons];
            for (int i = 0; i < listBoundaryForEveryPolygon.Length; i++)
                listBoundaryForEveryPolygon[i] = new List<PointLatLng>();

            // Получить минимум, максимум для раскраски, количество оттенков, а также массив оттенков выбранного цвета
            _LegendPolygon(out Color[] colorPolygon, out double min, out double max, out int grids, out double step);

            for (int j = 0; j < countOfPolygons; j++)
            {
                _mapModel.tempListForPolygons = new List<PointLatLng>();
                for (int k = 0; k < _sublayerPolygon.listWithPolygons[j].listBoundaryPoints.Count; k++)
                {
                    Location tempLocation = new Location(_sublayerPolygon.listWithPolygons[j].listBoundaryPoints[k].x,
                        _sublayerPolygon.listWithPolygons[j].listBoundaryPoints[k].y);
                    PointLatLng tempLocationForPolygon = new PointLatLng(tempLocation.x, tempLocation.y);
                    _mapModel.tempListForPolygons.Add(tempLocationForPolygon);
                }
                listBoundaryForEveryPolygon[j] = _mapModel.tempListForPolygons;
            }

            for (int j = 0; j < countOfPolygons; j++)
            {
                // Создание полигона
                var mapPolygon = new GMapPolygon(listBoundaryForEveryPolygon[j], "Polygon" + j.ToString());
                // Установка толщины границ у полигона
                mapPolygon.Stroke = _penBoundary;

                // Определение оттенка, в который необходимо окрасить полигон
                int shadeNumber = 0;
                shadeNumber = _mapModel.GetnumberShade(Convert.ToDouble(dataPolygons.Rows[j][5 + _indexOfSelectedCriterion]), min, max, grids, step);

                // Заливка полигона определенным оттенком
                mapPolygon.Fill = new SolidBrush(colorPolygon[shadeNumber]);
                // Добавление нового полигона на слой
                _sublayerPolygon.overlay.Polygons.Add(mapPolygon);
            }
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerPolygon.overlay);
            _sublayerPolygon.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Легенда карты
        /// </summary>
        /// <param name="colorPolygon">Массив оттенков определенного цвета</param>
        /// <param name="minValueColoring">Минимум для раскраски</param>
        /// <param name="maxValueColoring">Максимум для раскраски</param>
        /// <param name="countOfGrids">Количество оттенков</param>
        /// <param name="countOfSteps">Интервал каждого оттенка</param>
        private void _LegendPolygon(out Color[] colorPolygon, out double minValueColoring,
            out double maxValueColoring, out int countOfGrids, out double countOfSteps)
        {
            Graphics graphics = panelLegendOfMap.CreateGraphics();
            graphics.Clear(_mapModel.mainColor);

            // Переменные для работы с цветом
            int red, green, blue;
            int hred, hgreen, hblue;

            // Первым будет всегда белый цвет у минимума
            // От RGB белого цвета будет вычитаться RGB выбранного цвета, чтобы получить оттенки
            Color tempColor = Color.FromArgb(255, 255, 255, 255);
            // Ободок прямоугольников у оттенков
            Pen penBoundaryOfRectangle = new Pen(Color.Black, 0.002f);

            // Получение минимума, максимума и количества оттенков для выбранного критерия
            _mapModel.SetMaximumAndMinumForCriterionSquareColoring(_indexOfSelectedCriterion);
            countOfGrids = _mapModel.numberOfShadesCriterion;
            minValueColoring = _mapModel.minCriterionPolygonColoring;
            maxValueColoring = _mapModel.maxCriterionPolygonColoring;

            // Длина прямоугольника с цветом зависит от длины панели
            int widthOneRectangle = (panelLegendOfMap.Width - 20) / countOfGrids;
            // Шаг градаций
            countOfSteps = maxValueColoring / countOfGrids;
            // Округлить шаг до одной точки после запятой
            countOfSteps = Math.Round(countOfSteps, 1);

            // Отрисовка прямоугольников на панели
            graphics = panelLegendOfMap.CreateGraphics();
            // Прямоугольников с оттенками будет столько, сколько градаций цветов
            Rectangle[] arrayOfRactangles = new Rectangle[countOfGrids];
            Brush[] brush = new SolidBrush[countOfGrids];
            colorPolygon = new Color[countOfGrids];

            // В зависимости от чисел в легенде установить размер шрифта, чтобы все значения помещались
            Font arialFont;
            if (countOfSteps < 9999)
                arialFont = new Font("Segoe UI", 10);
            else
                arialFont = new Font("Segoe UI", 6);
            Font timesFont = new Font("Segoe UI", 10);

            // От RGB белого цвета будет вычитаться RGB выбранного цвета, чтобы получить оттенки
            hred = (tempColor.R - _colorForSquareColoring.R) / countOfGrids;
            hgreen = (tempColor.G - _colorForSquareColoring.G) / countOfGrids;
            hblue = (tempColor.B - _colorForSquareColoring.B) / countOfGrids;
            // Заполнение прямоугольников оттенками, для каждого прямоугольника находим RGB
            for (int i = 0; i < countOfGrids; i++)
            {
                red = tempColor.R - hred * i;
                green = tempColor.G - hgreen * i;
                blue = tempColor.B - hblue * i;

                // Заливка полигона
                colorPolygon[i] = Color.FromArgb(_transparentPolygons, red, green, blue);
                // Отображение прямоугольника по координанатам на панели
                arrayOfRactangles[i] = new Rectangle(widthOneRectangle * i, 0, widthOneRectangle, 20);
                brush[i] = new SolidBrush(colorPolygon[i]);
                // Заливка прямоугольника на панели определенным оттенком
                graphics.FillRectangle(brush[i], arrayOfRactangles[i]);
                // Отображение ободка у прямоугольника
                graphics.DrawRectangle(penBoundaryOfRectangle, arrayOfRactangles[i]);
                // Под каждым прямоугольником отображаем цифру шага
                if (i == 0)
                    graphics.DrawString(Convert.ToString(countOfSteps * i), arialFont, Brushes.Black, widthOneRectangle * i, 20);
                else
                    graphics.DrawString(Convert.ToString(countOfSteps * i), arialFont, Brushes.Black, widthOneRectangle * i - 10, 20);
            }
            // Отдельно дописываем в конец раскраски крайнее значение раскраски
            graphics.DrawString(_mapModel.maxCriterionPolygonColoring.ToString(), arialFont, Brushes.Black, widthOneRectangle * countOfGrids - 20, 20);
            // Выводим название критерия, по которому раскрашена карта в данный момент
            graphics.DrawString("Раскраска по критерию: " + _mapModel.listUserCriterion[_indexOfSelectedCriterion].nameOfCriterion,
                timesFont, Brushes.Black, 0, 40);
            graphics.DrawString("Сменить цвет", timesFont, Brushes.Black, 410, 40);
        }

        /// <summary>
        /// Обработка изменения прозрачности цвета заливки полигона
        /// </summary>
        private void trackBarTransparent_Scroll(object sender, EventArgs e)
        {
            // От 0 до 5, то есть от 0(без заливки) до 255(полной заливки)
            _transparentPolygons = trackBarTransparent.Value * 51;
            // Перерисовать полигоны
            _DrawPolygonColoring();
            // Перерисовать объекты социальной инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать точки-кандидаты
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                // Перерисовать авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Обработка изменения жирности границ для полигонов
        /// </summary>
        private void trackBarIntensity_Scroll(object sender, EventArgs e)
        {
            // Значение с ползунка задает толщину границ
            _intensityPolygons = trackBarIntensity.Value;
            // Перерисовать полигоны
            _DrawPolygonColoring();
            // Перерисовать объекты социальной инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать точки-кандидаты
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                // Перерисовать авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Обработка изменения количества оттенков раскраски
        /// </summary>
        private void trackBarShades_Scroll(object sender, EventArgs e)
        {
            // Значение с ползунка задает количство оттенков раскраски
            _mapModel.numberOfShadesCriterion = trackBarShades.Value;
            // Перерисовать полигоны
            _DrawPolygonColoring();
            // Перерисовать объекты социальной инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать точки-кандидаты
            _mapView.DrawPointBufferZone();
            if (_isFindAutoCandidates && !_isAutoHide)
                // Перерисовать авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        /// <summary>
        /// Обработка щелчка по легенде карты
        /// </summary>
        private void panelLegendOfMap_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Расширеный выбор цвета и оттенков
            colorDialog.FullOpen = true;
            colorDialog.Color = BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Сохранение выбранного цвета
                _colorForSquareColoring = colorDialog.Color;
                // Перерисовка раскраски
                _DrawPolygonColoring();
                // Обновить карту
                gmap.Refresh();
                // Перерисовать объекты социальной инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать точечную раскраску
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                // Перерисовать точки-кандидаты
                _mapView.DrawPointBufferZone();
                if (_isFindAutoCandidates && !_isAutoHide)
                    // Перерисовать авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
            }
        }

        // Индекс критерия для точечной раскраски
        private int _indexOfSelectedCriterionIcon = -1;
        /// <summary>
        /// Изменение критерия в точечной раскраски
        /// </summary>
        private void comboBoxColoringIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Название выбранного критерия для раскраски
            string nameCriterionIconColoring = comboBoxColoringIcons.Text;

            // Если выбрано "Скрыть", то
            if (nameCriterionIconColoring == "Скрыть")
                // Скрыть раскраску
                _mapView.ClearIconColoring();
            // Если выбран некоторый критерий
            else
            {
                // Ищем индекс выбранного критерия для раскраски
                for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
                    if (_mapModel.listUserCriterion[i].nameOfCriterion == nameCriterionIconColoring)
                        _indexOfSelectedCriterionIcon = i;

                // Найти минимум и максимум по выбранному критерию
                _mapModel.SetMaximumAndMinumForCriterionIconColoring(_indexOfSelectedCriterionIcon);
                int countOfGrids = _mapModel.numberOfShadesCriterion;
                double maxValueColoring = _mapModel.maxCriterionIconColoring;
                double minValueColoring = _mapModel.minCriterionIconColoring;

                // Перерисовать точечную раскраску
                _mapView.DrawIconColoring(maxValueColoring, minValueColoring, countOfGrids, _indexOfSelectedCriterionIcon, nameCriterionIconColoring);
                // Перерисовать объекты социальной инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать точки-кандидаты
                _mapView.DrawPointBufferZone();
                if (_isFindAutoCandidates && !_isAutoHide)
                    // Перерисовать авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
            }
        }

        /// <summary>
        /// Обработка сворачивания формы
        /// </summary>
        private void MainMap_SizeChanged(object sender, EventArgs e)
        {
            // Если форму сворачивали и при этом была выбрана площадная раскраска, то надо перерисовать легенду карты
            if ((WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Minimized) && comboBoxColoringPolygon.Text != "Скрыть")
            {
                // Перерисовать площадную раскраску
                _DrawPolygonColoring();
                // Перерисовать объекты социальной инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать точечную раскраску
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                // Перерисовать точки-кандидаты
                _mapView.DrawPointBufferZone();
                if (_isFindAutoCandidates && !_isAutoHide)
                    // Перерисовать авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
            }
        }

        /// <summary>
        /// Сохранение карты в виде изображения
        /// </summary>
        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveMapDialog = new SaveFileDialog())
                {
                    // Формат изображения
                    saveMapDialog.Filter = "Image Files (*.png) | *.png";
                    // Название изображения
                    saveMapDialog.FileName = "Текущее положение карты";
                    // Преобразование карты в картинку
                    Image image = gmap.ToImage();
                    if (image != null)
                    {
                        using (image)
                        {
                            // Если пользователь подтвердил сохранение
                            if (saveMapDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Сохранение по выбранному пользователем пути картинки в формате png
                                string fileName = saveMapDialog.FileName;
                                if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                                    fileName += ".png";
                                image.Save(fileName);
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
        /// Установка точки-кандидата на карте
        /// </summary>
        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Координаты на карте, куда кликнули дваждый ЛКМ
                double x = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                double y = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
                PointLatLng pointLatLng = new PointLatLng(x, y);

                // Нельзя установить на карте больше 30 точек-кандидатов
                if (_sublayerUserFacilityCandidate.listWithLocation.Count < _mapModel.LIMIT_USER_POINTS_ON_MAP)
                {
                    // Если точка в пределах зоны анализа
                    if (_mapView.CheckInsidePointTerritory(pointLatLng) == true)
                    {
                        // Сколько точек уже размещено на карте, если больше 1, надо проверить ее расстояние до других точек
                        if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 1)
                        {
                            // Новая точка не близко поставлена к уже стоящим (не ближе 25 метров)?
                            bool flagNewPointBeyondTheOthers = false;
                            for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count; i++)
                            {
                                // Взять поставленную координату
                                GeoCoordinate newPoint = new GeoCoordinate(x, y);
                                // Взять каждую из уже размещенных точек-кандидатов
                                GeoCoordinate everyPoint = new GeoCoordinate(_sublayerUserFacilityCandidate.listWithLocation[i].x,
                                    _sublayerUserFacilityCandidate.listWithLocation[i].y);
                                // Расстояние между ними
                                int distanceBetweenPoints = Convert.ToInt32(newPoint.GetDistanceTo(everyPoint));
                                // Если новая точка ближе, чем 25 метров к одной из уже поставленных
                                if (distanceBetweenPoints < _mapModel.DISTANCE_BETWEEN_USER_POINTS)
                                    flagNewPointBeyondTheOthers = true;
                            }

                            // Если точка близко расположена к одной из уже размещенных
                            if (!flagNewPointBeyondTheOthers)
                            {
                                // Если точка не принадлежит никакому полигону
                                if (_mapView.CheckInsidePointPolygon(pointLatLng) == false)
                                {
                                    // Ситуация, когда маркер в зоне анализа, но находится не в полигонах
                                    if (MessageBox.Show("Точка не принадлежит полигонам. Продолжить?", "Предупреждение",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        // Добавление точки в список маркеров, поставленных пользователем
                                        _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                        // Установка маркера на карте
                                        _mapView.DrawPointBufferZone();
                                    }
                                }
                                else
                                {
                                    // Добавление точки в список маркеров, поставленных пользователем
                                    _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                    // Установка маркера
                                    _mapView.DrawPointBufferZone();
                                }
                                // Перерисовать авто-оптимумы
                                if (_isFindAutoCandidates && !_isAutoHide)
                                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
                            }
                            else
                                MessageBox.Show("Между кандидатами должно быть не менее "
                                    + _mapModel.DISTANCE_BETWEEN_USER_POINTS.ToString() + " метров друг от друга",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Если точка не принадлежит никакому полигону
                            if (_mapView.CheckInsidePointPolygon(pointLatLng) == false)
                            {
                                // Ситуация, когда маркер в зоне анализа, но находится не в полигонах
                                if (MessageBox.Show("Точка не принадлежит полигонам. Продолжить?", "Предупреждение",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    // Добавление точки в список маркеров, поставленных пользователем
                                    _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                    // Установка маркера на карте
                                    _mapView.DrawPointBufferZone();
                                }
                            }
                            else
                            {
                                // Добавление точки в список маркеров, поставленных пользователем
                                _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                // Установка маркера
                                _mapView.DrawPointBufferZone();
                            }
                            // Перерисовать авто-оптимумы
                            if (_isFindAutoCandidates && !_isAutoHide)
                                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
                        }
                    }
                    else
                        MessageBox.Show("Маркер должен быть расположен в границах загруженной зоны анализа",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Нельзя разместить более " + _mapModel.LIMIT_USER_POINTS_ON_MAP.ToString() + " маркеров",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ввод радиуса поиска буферной зоны
        /// </summary>
        private void InputRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Открыть окно ввода радиуса
            SettingRadius form = new SettingRadius(_mapModel);
            form.ShowDialog();
            // После закрытия окна с радиусом
            // Переотрисовать точки-кандидаты с новым радиусом
            _mapView.DrawPointBufferZone();
            // Если были найдены оптимумы и на карте в данный момент больше 2 кандидатов, найти из заново с новым радиусом поиска
            if (_searchOptimum && _sublayerUserFacilityCandidate.listWithLocation.Count >= 2)
                SearchOptimumToolStripMenuItem_Click(sender, e);
            // Перерисовать с новым радиусом авто-кандидатов
            if (_isFindAutoCandidates && !_isAutoHide)
                FindAutoToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Модификация загруженных данных
        /// </summary>
        private void SettingsToolStripMenu_Click(object sender, EventArgs e)
        {
            // Открыть окно модификации данных
            Settings form = new Settings(_mapModel);
            form.ShowDialog();

            // После закрытия формы переделать отображение название объекта социальной инфраструктуры
            if (_mapModel.nameObjectFacility.Length <= 12)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 11);
                labelFacility.Font = new Font("Segoe UI", 10);
            }
            if (_mapModel.nameObjectFacility.Length >= 13 && _mapModel.nameObjectFacility.Length <= 17)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 8);
                labelFacility.Font = new Font("Segoe UI", 8);
            }
            if (_mapModel.nameObjectFacility.Length >= 18)
            {
                // Название Группбокса - это название объекта социальной инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта социальной инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 6);
                labelFacility.Font = new Font("Segoe UI", 6);
            }

            // Выставить загруженный значок объекта социальной инфраструктуры
            pictureBoxFacility.Image = _mapModel.iconFacility;

            // Если была задана новая зона анализа
            if (_mapModel.flagChangeTerritory)
            {
                // Оптимальные точки удаляются
                _mapModel.optimalZones.Clear();
                // Флаг найденных оптимумов
                _searchOptimum = false;

                // Все точки-кандидаты, принадлежащие новой зоне анализа оставляем, невошедшие удаляем
                if (_sublayerUserFacilityCandidate.listWithLocation.Count > 0)
                {
                    for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count;)
                    {
                        PointLatLng point = new PointLatLng(_sublayerUserFacilityCandidate.listWithLocation[i].x,
                            _sublayerUserFacilityCandidate.listWithLocation[i].y);
                        // Если точка в пределах зоны анализа
                        if (_mapView.CheckInsidePointTerritory(point) != true)
                        {
                            _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                            if (i != 0)
                                i--;
                        }
                        else
                            i++;
                    }
                }

                // Перерисовать авто-оптимумы
                if (_isFindAutoCandidates)
                    DeleteAutoToolStripMenuItem_Click(sender, e);

                // Сбросить заданный ранее радиус поиска
                _mapModel.radiusBufferZone = 500;

                // Перецентрировать карту на новою зону анализа
                _mapView.ChangeCenterMap(_mapModel.centerMap);
                _mapModel.flagChangeTerritory = false;
            }

            // Если задали новые полигоны
            if (_mapModel.flagChangePolygon)
            {
                // Если были размещены точки-кандидаты и были найдены оптимумы, то найти их заново
                if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 2 && _mapModel.optimalZones.Count != 0)
                    SearchOptimumToolStripMenuItem_Click(sender, e);

                // Если были найдены авто-оптимумы
                if (_isFindAutoCandidates && !_isAutoHide)
                    FindAutoToolStripMenuItem_Click(sender, e);
                _mapModel.flagChangePolygon = false;
            }

            // Если задали новые частные критерии
            if (_mapModel.flagChangeCriterion)
            {
                comboBoxColoringIcons.Items.Clear();
                comboBoxColoringPolygon.Items.Clear();

                // Заполнение выпадающих списков с раскраской критериями
                for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
                {
                    comboBoxColoringIcons.Items.Add(_mapModel.listUserCriterion[i].nameOfCriterion);
                    comboBoxColoringPolygon.Items.Add(_mapModel.listUserCriterion[i].nameOfCriterion);
                }
                // Добавляем "Очистить" к каждому списку
                comboBoxColoringIcons.Items.Add("Скрыть");
                comboBoxColoringPolygon.Items.Add("Скрыть");
                // Отмена редактирования списка, выставить "Скрыть" по умолчанию
                comboBoxColoringIcons.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxColoringIcons.SelectedItem = "Скрыть";
                comboBoxColoringPolygon.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxColoringPolygon.SelectedItem = "Скрыть";

                // Если были размещены точки-кандидаты и были найдены оптимумы, то найти их заново
                if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 2 && _mapModel.optimalZones.Count != 0)
                    SearchOptimumToolStripMenuItem_Click(sender, e);

                // Если были найдены авто-оптимумы
                if (_isFindAutoCandidates && !_isAutoHide)
                    FindAutoToolStripMenuItem_Click(sender, e);
            }
            _mapModel.flagChangeCriterion = false;
            // Перерисовать всю карту заново
            _RedrawMap(sender, e);
        }

        // Флаг поиска оптимальных точек
        private bool _searchOptimum = false;
        // Оптимальные точки
        private List<OptimalZone> _optimalPoints;
        // Объект MathOptimumModel
        private MathOptimumModel _math;
        /// <summary>
        /// Поиск оптимальных точек
        /// </summary>
        private void SearchOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Для каждой точки-кандидата создать буферную зону
            _mapModel.CreateListBufferZones();

            // Если на карте более двух кандидатов
            if (_mapModel.listPointsBufferZone.Count >= 2)
            {
                // Поиск оптимальных точек с заданными буферными зонами и критериями
                _math = new MathOptimumModel(_mapModel.listPointsBufferZone, _mapModel.listUserCriterion);
                // Получить массив оптимумов
                _optimalPoints = _math.GetOptimum();
                // Сохранить оптимальные точки
                _mapModel.optimalZones = _optimalPoints;

                // Перерисовать точки, выделив оптимумы зеленым цветом
                _mapView.DrawPointBufferZone();
                if (_isFindAutoCandidates && !_isAutoHide)
                    // Перерисовать авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
                // Флаг найденных оптимумов
                _searchOptimum = true;
            }
            else
                MessageBox.Show("Необходимо разместить не менее двух маркеров", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Оставить на карте только оптимальные точки
        /// </summary>
        private void LeaveOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Очистить точки-кандидаты
            _sublayerUserFacilityCandidate.listWithLocation.Clear();
            // Добавить в список точек-кандидатов оптимальные точки
            for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(_mapModel.optimalZones[i].optimalZone.x,
                    _mapModel.optimalZones[i].optimalZone.y));
            // Отрисовать точки-кандидаты (все будут зеленого цвета)
            _mapView.DrawPointBufferZone();

            if (_isFindAutoCandidates && !_isAutoHide)
                // Перерисовать авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);
        }

        // Флаг сохранения оптимума
        private bool _flagSaveOptimum = false;
        /// <summary>
        /// Сохранение оптимальных точек в CSV-файл
        /// </summary>
        private void SaveOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте есть оптимумы
            if (_mapModel.optimalZones.Count != 0)
            {
                try
                {
                    using (SaveFileDialog saveOptimumDialog = new SaveFileDialog())
                    {
                        saveOptimumDialog.Filter = "Files (*.csv) | *.csv";
                        saveOptimumDialog.InitialDirectory = Application.StartupPath + @"\Data\DataTest";
                        saveOptimumDialog.Title = "Сохранение в CSV-файл данных об оптимальных точках";
                        saveOptimumDialog.FileName = "Оптимальные точки";

                        // Если пользователь подтвердил сохранение
                        if (saveOptimumDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Путь к выбранному файлу
                            string pathToFile = saveOptimumDialog.FileName.ToString();
                            // Валидация выбранного файла
                            string isValidFile = _fileValidator.FileValidationCreateCSV(pathToFile);

                            // Если файл прошёл все проверки
                            if (isValidFile == "Успешно")
                            {
                                // Запись в файл информации обо всех оптимальных точках
                                _fileValidator.WriteToFileInfoOptimumZones(pathToFile, _mapModel.optimalZones, _mapModel.listUserCriterion);
                                // Флажок, что пользователь сохранил оптимальные точки в файл
                                _flagSaveOptimum = true;
                            }
                            else
                                MessageBox.Show(isValidFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при сохранении карты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Найдите оптимальные зоны", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Закрытие формы на красный крестик
        /// </summary>
        private void Map_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил точку в CSV-файл
            if (_flagSaveOptimum == true)
                e.Cancel = false;
            else
            {
                // Если пользователь искал оптимальные точки, но не сохранил их в файл
                if (_searchOptimum == true)
                {
                    // Если пользователь не сохранил оптимальные точки в CSV-файл, но при этом искал оптимум
                    if (MessageBox.Show("Вы не сохранили оптимальные точки. Выйти?", "Уведомление",
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
        /// Закрыть форму через ESC
        /// </summary>
        private void MainMap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        /// <summary>
        /// Отобразить наилучший оптимум на карте Google в браузере
        /// </summary>
        private void GoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте размещены оптимальные точки
            if (_mapModel.optimalZones.Count != 0)
            {
                int votes = 0;
                int idBest = -1;
                // Найти точку, за которую проголосовала большая часть сверток и узнать её индекс
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (_mapModel.optimalZones[i].namesConvolutiones.Count > votes)
                    {
                        votes = _mapModel.optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                // Самую популярную оптимальную точку открыть на выбранной карте в браузере
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel.optimalZones[i].optimalZone.x.ToString();
                        string y = _mapModel.optimalZones[i].optimalZone.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        // Открыть самую оптимальную точку в Google
                        System.Diagnostics.Process.Start("https://www.google.com/maps/@" + x + "," + y + ",18z");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимальные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить наилучший оптимум на карте Яндекс в браузере
        /// </summary>
        private void YandexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте размещены оптимальные точки
            if (_mapModel.optimalZones.Count != 0)
            {
                int votes = 0;
                int idBest = -1;
                // Найти точку, за которую проголосовала большая часть сверток и узнать ее индекс
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (_mapModel.optimalZones[i].namesConvolutiones.Count > votes)
                    {
                        votes = _mapModel.optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                // Самую популярную оптимальную точку открыть на выбранной карте в браузере
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel.optimalZones[i].optimalZone.x.ToString();
                        string y = _mapModel.optimalZones[i].optimalZone.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        // Открыть самую оптимальную точку в Яндексе
                        System.Diagnostics.Process.Start("https://yandex.ru/maps/?ll=" + y + "%2C" + x + "&z=18");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимальные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить наилучший оптимум на карте 2ГИС в браузере
        /// </summary>
        private void TwoGISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте размещены оптимальные точки
            if (_mapModel.optimalZones.Count != 0)
            {
                int votes = 0;
                int idBest = -1;
                // Найти точку, за которую проголосовала большая часть сверток и узнать ее индекс
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (_mapModel.optimalZones[i].namesConvolutiones.Count > votes)
                    {
                        votes = _mapModel.optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                // Самую популярную оптимальную точку открыть на выбранной карте в браузере
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel.optimalZones[i].optimalZone.x.ToString();
                        string y = _mapModel.optimalZones[i].optimalZone.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        // Открыть самую оптимальную точку в 2ГИС
                        System.Diagnostics.Process.Start("https://2gis.ru/geo/" + y + "%2C" + x + "?m=" + y + "%2C" + x + "%2F" + "17.84");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимальные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить наилучший оптимум на карте OpenStreetMap в браузере
        /// </summary>
        private void OpenStreetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте размещены оптимальные точки
            if (_mapModel.optimalZones.Count != 0)
            {
                int votes = 0;
                int idBest = -1;
                // Найти точку, за которую проголосовала большая часть сверток и узнать ее индекс
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (_mapModel.optimalZones[i].namesConvolutiones.Count > votes)
                    {
                        votes = _mapModel.optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                // Самую популярную оптимальную точку открыть на выбранной карте в браузере
                for (int i = 0; i < _mapModel.optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel.optimalZones[i].optimalZone.x.ToString();
                        string y = _mapModel.optimalZones[i].optimalZone.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        // Открыть самую оптимальную точку в ОpenStreetMap
                        System.Diagnostics.Process.Start("https://www.openstreetmap.org/#map=19/" + x + "/" + y);
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимальные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Флаг найденных авто-оптимумов
        private bool _isFindAutoCandidates = false;
        // Флаг спрятанных авто-оптимумов
        private bool _isAutoHide = false;
        /// <summary>
        /// Включить отображение авто-оптимумов
        /// </summary>
        private void ShowAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если авто-оптимумы были найдены
            if (_isFindAutoCandidates)
            {
                _isAutoHide = false;
                // Перерисовать на карте площадную раскраску
                comboBoxColoringPolygon_SelectedIndexChanged(sender, e);
                // Перерисовать на карте точечную раскраску
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                // Перерисовать на карте объекты социальной инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать на карте зону анализа
                _CheckSelectedRadioButtonTerritory(sender, e);
                // Перерисовать точки-кандидаты
                _mapView.DrawPointBufferZone();
                _sublayerAuto.overlay.IsVisibile = true;
            }
            else
                MessageBox.Show("Найдите авто-оптимумы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Выключить отображение авто-оптимумов
        /// </summary>
        private void HideAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если авто-оптимумы были найдены
            if (_isFindAutoCandidates)
            {
                _isAutoHide = true;
                _sublayerAuto.overlay.IsVisibile = false;
            }
            else
                MessageBox.Show("Найдите авто-оптимумы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса поиска
        /// </summary>
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

            // Создать две координаты на карте
            GeoCoordinate leftPoint = new GeoCoordinate(minX, minY);
            GeoCoordinate rightPoint = new GeoCoordinate(maxX, maxY);
            // Найти расстояние между ними
            double diagonalTerritory = leftPoint.GetDistanceTo(rightPoint);
            return diagonalTerritory;
        }

        // Список буферных зон авто-оптимумов
        private List<BufferZone> _listAutoOptimalPoint;
        /// <summary>
        /// Поиск заданного количества авто-оптимумов
        /// </summary>
        private void FindAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Очищение карты от слоя авто-оптимумов
            _sublayerAuto.listWithLocation.Clear();
            _sublayerAuto.overlay.Polygons.Clear();
            _sublayerAuto.overlay.Markers.Clear();
            _sublayerAuto.overlay.Clear();

            // Количество оптимумов для поиска
            int countOfOptimum = Convert.ToInt32(comboBoxAuto.Text);

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
                    // Добавляем в список все точки прохода по зоне анализа
                    listWithFullPassTerritory.Add(new PointLatLng(minX, minY));
                    minX = minX + stepX;
                }
                // Нарастили ось Y и еще раз по оси Х
                minY = minY + stepY;
                minX = saveMinX;
            }

            // Пройтись по всем точкам глобального прохода
            for (int i = 0; i < listWithFullPassTerritory.Count; i++)
            {
                // Создание точки
                PointLatLng pointLatLng = new PointLatLng(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng);
                // Если точка принадлежит одному из полигонов
                if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
                    // Добавление точки в список авто-маркеров
                    _sublayerAuto.listWithLocation.Add(new Location(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng));
            }

            // Очистить список
            listWithFullPassTerritory.Clear();

            // Установка маркеров и буфреных зон, которые попали в полигоны
            _mapView.DrawAutoPointBufferZone(_mapModel.radiusBufferZone);

            // Получить список буферных зон, которые попали в зону анализа и не нулевые
            _mapModel.CreateListAutoBufferZones();

            // Список буферных зон для поиска оптимума
            List<BufferZone> listAutoBufferZones = _mapModel.listAutoPointsBufferZone;

            // Ситуация, когда все авто-точки имеют нули в критериях, то надо увеличить радиус
            if (listAutoBufferZones.Count == 0)
            {
                // Строка с ошибкой
                var errorCriterion = new StringBuilder();
                errorCriterion.AppendLine("Не удалось найти ни одного оптимума");
                errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Авто-оптимум не был найден
                _isFindAutoCandidates = false;
                // Очистить карту от прошлых вычислений
                _mapView.ClearIdealPoints(_sublayerAuto);
            }

            // Ситуация, когда авто-точек нашлось меньше, чем требовалось
            if (listAutoBufferZones.Count > 0 && listAutoBufferZones.Count < countOfOptimum)
            {
                // Строка с ошибкой
                var errorCriterion = new StringBuilder();
                errorCriterion.AppendLine("Не удалось найти требуемое количество оптимумов");
                errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                errorCriterion.AppendLine("Запрошено " + countOfOptimum + " оптимумов");
                errorCriterion.AppendLine("Найдено " + listAutoBufferZones.Count + " оптимумов");

                // Поиск оптимальных точек
                MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones, _mapModel.listUserCriterion, listAutoBufferZones.Count);

                // Получить (1-10) оптимумов
                _listAutoOptimalPoint = math.GetArrayWithOptimums();

                // Отрисовать звездочки оптимальных точек (1-10)
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);

                // Флаг поиска авто-кандидатов
                _isFindAutoCandidates = true;

                if (_isAutoHide)
                    _sublayerAuto.overlay.IsVisibile = false;
                else
                    _sublayerAuto.overlay.IsVisibile = true;

                MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listAutoBufferZones.Count >= countOfOptimum)
            {
                // Поиск оптимальных точек
                MathOptimumModelAuto math = new MathOptimumModelAuto(listAutoBufferZones, _mapModel.listUserCriterion, countOfOptimum);

                // Получить (1-10) оптимумов
                _listAutoOptimalPoint = math.GetArrayWithOptimums();

                // Отрисовать звездочки оптимальных точек (1-10)
                _mapView.DrawAutoIdealPointBufferZone(_listAutoOptimalPoint);

                // Флаг поиска авто-кандидатов
                _isFindAutoCandidates = true;

                if (_isAutoHide)
                    _sublayerAuto.overlay.IsVisibile = false;
                else
                    _sublayerAuto.overlay.IsVisibile = true;
            }
        }

        /// <summary>
        /// Удаление авто-оптимумов
        /// </summary>
        private void DeleteAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если авто-оптимумы были найдены
            if (_isFindAutoCandidates)
            {
                // Авто-кандидаты удалены
                _isFindAutoCandidates = false;
                // Удалить слой авто-оптимумов с карты
                _mapView.ClearIdealPoints(_sublayerAuto);
            }
        }

        /// <summary>
        /// Норма на душу населения
        /// </summary>
        private void NormPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string isValidFile = _fileValidator.ValidateUserFileCSV(_mapModel.pathToFilePolygonUser);

            // Если файл прошёл все проверки
            if (isValidFile == "Успешно")
            {
                // Проверить, подходит ли файл с полигонами для нормы
                string isValidDataFile = _fileValidator.ValidateUserFileNorma(_mapModel.pathToFilePolygonUser,
                    _mapModel.sublayerBorderPointsTerritory.listWithLocation, _mapModel.listUserCriterion.Count);

                // Если файл прошёл все проверки
                if (isValidDataFile == "Успешно")
                {
                    // Флаг успешной загрузки файла с нормой
                    _mapModel.flagLoadFileNorma = true;
                    // Открытие окна с нормой
                    SearchNormPerCapita form = new SearchNormPerCapita(_languageOfMap, _mapModel);
                    form.ShowDialog();
                }
                else
                    MessageBox.Show(isValidDataFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show(isValidFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _CheckSelectedRadioButtonFacility(sender, e);
        }

        private void OptimumInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Открытие руководства пользователя в HTML
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}