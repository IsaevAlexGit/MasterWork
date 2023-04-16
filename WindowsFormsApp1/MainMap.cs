using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Device.Location;
using System.Text;

namespace Optimum
{
    public partial class MainMap : Form
    {
        // Язык карты
        private LanguageType _languageOfMap;
        // Цвет интерфейса
        private Color _colorForApplication, _colorForElem;
        // значок объекта инфраструктуры
        private string _pathToIconFacility;
        // Название объекта инфраструктуры
        private string _nameFacility;
        // Лист критериев оптимальности
        private List<Criterion> _listCriteriaForSearch;
        // Список граничных точек территории
        private List<Location> _listPointsBorderTerritory;
        // Список полигонов из файла
        private List<Quar> _listQuartetsFromFile;
        // Список объектов инфраструктуры из файла
        private List<Facility> _listFacilitiesFromFile;
        // Точка центрирования карты
        Location _locationCenterMap;

        /// <summary>
        /// Создание окна
        /// </summary>
        public MainMap(LanguageType language, Color colorApp, Color colorElem, string pathToIcon, string nameFacility,
            List<Criterion> listCriterion, List<Quar> listQuartets, List<Facility> listFacilities, List<Location> listBorderPoints, Location center)
        {
            _languageOfMap = language;
            _colorForApplication = colorApp;
            _colorForElem = colorElem;
            // В условных обозначениях измнеить значок объекта инфраструктуры
            _pathToIconFacility = pathToIcon;
            _nameFacility = nameFacility;
            _listCriteriaForSearch = listCriterion;
            _listQuartetsFromFile = listQuartets;
            _listFacilitiesFromFile = listFacilities;
            _listPointsBorderTerritory = listBorderPoints;
            _locationCenterMap = center;

            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        private MapModel _mapModel;
        private MapView _mapView;

        /// <summary>
        /// Загрузка карты
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
            _mapModel.colorApplication = _colorForApplication;
            _mapModel.colorElements = _colorForElem;

            // Настройка значка и названия объекта инфраструктуры
            _mapModel.pathToIconObjectFacility = _pathToIconFacility;
            _mapModel.nameObjectFacility = _nameFacility;

            // Настройка списка частных критериев оптимальности
            _mapModel.listUserCriterion = _listCriteriaForSearch;

            // Найстрока списка с объектами инфраструктуры, полигонами и территорией
            _mapModel.listUserPointsBorderTerritory = _listPointsBorderTerritory;
            _mapModel.listUserQuartet = _listQuartetsFromFile;
            _mapModel.listUserFacilities = _listFacilitiesFromFile;

            // Инициализация слоев, списков
            _mapModel._InitializationSublayersAndLists();
        }

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        // Слой для кварталов
        private SublayerQuar _sublayerQuar;
        // Слой для маркеров пользователя
        private SublayerLocation _sublayerUserFacilityCandidate;
        // Слой для автомаркеров
        private SublayerLocation _sublayerAuto;
        // Работа с файлом
        private FileValidator _fileValidator;
        // Цвет раскраски кварталов
        private Color _colorForColoring;

        // Ползунок для изменения прозрачности полигонов
        TrackBar trackBarTransparent;
        // Ползунок для изменения толщины границ полигонов
        TrackBar trackBarIntensity;
        // Ползунок для изменения количества градаций оттенков
        TrackBar trackBarShades;
        // Выпадающий список с критериями для площадной раскраски
        ComboBox comboBoxColoringPolygon;
        // Выпадающим список с поставщиками карт
        ComboBox comboBoxProvidersMap;
        // Выпадающий список с количеством авто-оптимумов
        ComboBox comboBoxAuto;
        // Панель с легендой карты
        Panel panelLegendOfMap;
        // Надписи для ползунков
        Label labelChangeIntensity;
        Label labelChangeTransparent;
        // Нижняя выдвигающаяся панель
        AnimatedBottomPanel BottomPanel;

        // Радио-кнопки для объектов инфраструктуры
        RadioButton radioButtonShowFacility;
        RadioButton radioButtonClearFacility;
        // Радио-кнопки для отображения информации об объекте инфраструктуры
        RadioButton radioButtonShowAllInfo;
        RadioButton radioButtonShowNameInfo;
        // Радио-кнопки для отображения территории
        RadioButton radioButtonShowTerritory;
        RadioButton radioButtonClearTerritory;

        // Групп-боксы
        GroupBox groupBoxFacility;
        GroupBox groupBoxInfoFacility;
        GroupBox groupBoxTerritory;
        GroupBox groupBoxPanelLegend;

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void Map_Load(object sender, EventArgs e)
        {
            // Установка цвета интерфейса и меню
            BackColor = _mapModel.colorApplication;
            menuStrip.BackColor = _mapModel.colorElements;

            // Размеры главного окна
            ClientSize = new Size(1374, 751);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            _sublayerQuar = _mapModel.sublayerQuarPolygon;

            _sublayerUserFacilityCandidate = _mapModel.sublayerUserFacility;
            _sublayerAuto = _mapModel.sublayerAutoUserPharmacy;
            _fileValidator = new FileValidator();

            // Создать обе выдвигающиеся панели
            SettingsAnimationPanels();

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            // Спрятать панель
            _DisplayLegendElements(false);

            if (_mapModel.nameObjectFacility.Length <= 12)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 11);
                labelFacility.Font = new Font("Segoe UI", 10);
            }
            if (_mapModel.nameObjectFacility.Length >= 13 && _mapModel.nameObjectFacility.Length <= 17)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 8);
                labelFacility.Font = new Font("Segoe UI", 8);
            }
            if (_mapModel.nameObjectFacility.Length >= 18)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
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
            // Добавляем "Очистить" к каждому списке
            comboBoxColoringIcons.Items.Add("Очистить");
            comboBoxColoringPolygon.Items.Add("Очистить");
            // Отмена редактирования списка + выставить "Очистить" по умолчанию
            comboBoxColoringIcons.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColoringIcons.SelectedItem = "Очистить";
            comboBoxColoringPolygon.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxColoringPolygon.SelectedItem = "Очистить";

            for (int i = 1; i <= 10; i++)
                comboBoxAuto.Items.Add(i.ToString());
            comboBoxAuto.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAuto.SelectedItem = "10";

            // Инициализация выпадающего списка в зависимости от выбранного языка
            if (_languageOfMap == LanguageType.English)
            {
                // Английский язык
                comboBoxProvidersMap.Items.Add("Yandex");
                comboBoxProvidersMap.Items.Add("Google");
                comboBoxProvidersMap.Items.Add("BingHybridMap");
                comboBoxProvidersMap.Items.Add("YandexHybridMap");
                comboBoxProvidersMap.Items.Add("ArcGIS");
                comboBoxProvidersMap.Items.Add("WikiMapiaMap");
            }
            else
            {
                // Русский язык
                comboBoxProvidersMap.Items.Add("Yandex");
                comboBoxProvidersMap.Items.Add("Google");
                comboBoxProvidersMap.Items.Add("OpenCycleMap");
                comboBoxProvidersMap.Items.Add("YandexHybridMap");
            }
            // Настройка выпадающего списка
            comboBoxProvidersMap.SelectedItem = "Google";
            comboBoxProvidersMap.DropDownStyle = ComboBoxStyle.DropDownList;

            // Наполнение условных обозначений
            // Выставить загруженную иконку
            Bitmap pathImageToBitmap = (Bitmap)Image.FromFile(_mapModel.pathToIconObjectFacility);
            Image iconFacility = pathImageToBitmap;
            pictureBoxFacility.Image = iconFacility;

            // Цвет фона у картинок
            pictureBoxMarker.BackColor = _mapModel.colorApplication;
            pictureBoxFacility.BackColor = _mapModel.colorApplication;
            pictureBoxAuto.BackColor = _mapModel.colorApplication;
            // Цвет фона у надписей
            labelUserMarker.BackColor = _mapModel.colorApplication;
            labelFacility.BackColor = _mapModel.colorApplication;
            labelAutoSearch.BackColor = _mapModel.colorApplication;
            labelLegendForMap.BackColor = _mapModel.colorApplication;

            // Начальный цвет раскраски (зеленый)
            _colorForColoring = Color.FromArgb(255, 0, 255, 0);

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

            // Иконка для маркера пользователя
            pictureBoxMarker.Image = Properties.Resources.iconUserFacility;
            // Иконка для маркера авто-оптимумов
            pictureBoxAuto.Image = Properties.Resources.iconBestOptimum;
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Изменение отображения выдвигающейся нижней панели
        /// </summary>
        /// <param name="_modeDisplay">флаг отображения</param>
        private void _DisplayLegendElements(bool _modeDisplay)
        {
            BottomPanel.Visible = _modeDisplay;
            groupBoxPanelLegend.Visible = _modeDisplay;
            labelChangeIntensity.Visible = _modeDisplay;
            labelChangeTransparent.Visible = _modeDisplay;
            trackBarIntensity.Visible = _modeDisplay;
            trackBarTransparent.Visible = _modeDisplay;
        }

        /// <summary>
        /// Создание панелей на главном окне
        /// </summary>
        private void SettingsAnimationPanels()
        {
            // Создание левой панели
            AnimatedLeftPanel LeftPanel = new AnimatedLeftPanel(new Size(312, 490), 70, Color.Gold, AnimatedLeftPanel.stateEnum.open);
            {
                // Прозрачный фон и не показывать границы панели
                LeftPanel.BackColor = Color.Transparent;
                LeftPanel.BorderStyle = BorderStyle.None;

                // Показывать объекты инфраструктуры
                radioButtonShowFacility = new RadioButton();
                {
                    radioButtonShowFacility.Text = "Отобразить";
                    radioButtonShowFacility.Font = new Font("Segoe UI", 12);
                    radioButtonShowFacility.Left = 8;
                    radioButtonShowFacility.Top = 20;
                    radioButtonShowFacility.Click += radioButtonShowFacility_Click;
                    radioButtonShowFacility.Size = new Size(150, 24);
                    radioButtonShowFacility.Cursor = Cursors.Hand;
                }

                // Не показывать объекты инфраструктуры
                radioButtonClearFacility = new RadioButton();
                {
                    radioButtonClearFacility.Text = "Очистить";
                    radioButtonClearFacility.Font = new Font("Segoe UI", 12);
                    radioButtonClearFacility.Left = 8;
                    radioButtonClearFacility.Top = 45;
                    radioButtonClearFacility.Click += radioButtonClearFacility_Click;
                    radioButtonClearFacility.Size = new Size(150, 24);
                    radioButtonClearFacility.Cursor = Cursors.Hand;
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
                LeftPanel.Controls.Add(groupBoxFacility);

                // Показывать информацию об объектах инфраструктуры
                radioButtonShowAllInfo = new RadioButton();
                {
                    radioButtonShowAllInfo.Text = "Все данные";
                    radioButtonShowAllInfo.Font = new Font("Segoe UI", 12);
                    radioButtonShowAllInfo.Left = 8;
                    radioButtonShowAllInfo.Top = 45;
                    radioButtonShowAllInfo.Click += radioButtonShowAllInfo_Click;
                    radioButtonShowAllInfo.Size = new Size(150, 24);
                    radioButtonShowAllInfo.Cursor = Cursors.Hand;
                }

                // Не показывать информацию об объектах инфраструктуры
                radioButtonShowNameInfo = new RadioButton();
                {
                    radioButtonShowNameInfo.Text = "Название объекта";
                    radioButtonShowNameInfo.Font = new Font("Segoe UI", 12);
                    radioButtonShowNameInfo.Left = 8;
                    radioButtonShowNameInfo.Top = 22;
                    radioButtonShowNameInfo.Click += radioButtonClearShowNameInfo_Click;
                    radioButtonShowNameInfo.Size = new Size(220, 24);
                    radioButtonShowNameInfo.Cursor = Cursors.Hand;
                }

                groupBoxInfoFacility = new GroupBox();
                {
                    groupBoxInfoFacility.Text = "Данные при наведении";
                    groupBoxInfoFacility.Font = new Font("Segoe UI", 12);
                    groupBoxInfoFacility.Size = new Size(200, 75);
                    groupBoxInfoFacility.Left = 10;
                    groupBoxInfoFacility.Top = 95;
                }
                groupBoxInfoFacility.Controls.Add(radioButtonShowNameInfo);
                groupBoxInfoFacility.Controls.Add(radioButtonShowAllInfo);
                LeftPanel.Controls.Add(groupBoxInfoFacility);

                // Показывать территорию
                radioButtonShowTerritory = new RadioButton();
                {
                    radioButtonShowTerritory.Text = "Отобразить";
                    radioButtonShowTerritory.Font = new Font("Segoe UI", 12);
                    radioButtonShowTerritory.Left = 8;
                    radioButtonShowTerritory.Top = 22;
                    radioButtonShowTerritory.Click += radioButtonShowTerritory_Click;
                    radioButtonShowTerritory.Size = new Size(150, 24);
                    radioButtonShowTerritory.Cursor = Cursors.Hand;
                }

                // Не показывать территорию
                radioButtonClearTerritory = new RadioButton();
                {
                    radioButtonClearTerritory.Text = "Очистить";
                    radioButtonClearTerritory.Font = new Font("Segoe UI", 12);
                    radioButtonClearTerritory.Left = 8;
                    radioButtonClearTerritory.Top = 45;
                    radioButtonClearTerritory.Click += radioButtonClearTerritory_Click;
                    radioButtonClearTerritory.Size = new Size(150, 24);
                    radioButtonClearTerritory.Cursor = Cursors.Hand;
                }

                groupBoxTerritory = new GroupBox();
                {
                    groupBoxTerritory.Text = "Загруженная территория";
                    groupBoxTerritory.Font = new Font("Segoe UI", 11);
                    groupBoxTerritory.Size = new Size(200, 75);
                    groupBoxTerritory.Left = 10;
                    groupBoxTerritory.Top = 180;
                }
                groupBoxTerritory.Controls.Add(radioButtonShowTerritory);
                groupBoxTerritory.Controls.Add(radioButtonClearTerritory);
                LeftPanel.Controls.Add(groupBoxTerritory);

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
                    comboBoxColoringPolygon.BackColor = _mapModel.colorApplication;
                    comboBoxColoringPolygon.Cursor = Cursors.Hand;
                }
                LeftPanel.Controls.Add(labelPolygonColoring);
                LeftPanel.Controls.Add(comboBoxColoringPolygon);

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
                    comboBoxColoringIcons.BackColor = _mapModel.colorApplication;
                    comboBoxColoringIcons.Cursor = Cursors.Hand;
                }
                LeftPanel.Controls.Add(labelIconColoring);
                LeftPanel.Controls.Add(comboBoxColoringIcons);

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
                    comboBoxAuto.BackColor = _mapModel.colorApplication;
                    comboBoxAuto.Cursor = Cursors.Hand;
                }

                LeftPanel.Controls.Add(labelCountOfOptimum);
                LeftPanel.Controls.Add(comboBoxAuto);

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
                    comboBoxProvidersMap.Width = 150;
                    comboBoxProvidersMap.SelectedIndexChanged += comboBoxProvidersMap_SelectedIndexChanged;
                    comboBoxProvidersMap.BackColor = _mapModel.colorApplication;
                    comboBoxProvidersMap.Cursor = Cursors.Hand;
                }
                LeftPanel.Controls.Add(labelProviderMap);
                LeftPanel.Controls.Add(comboBoxProvidersMap);

            }
            Controls.Add(LeftPanel);
            gmap.SendToBack();

            // Создание нижней панели
            BottomPanel = new AnimatedBottomPanel(new Size(1182, 150), 540, Color.White, AnimatedBottomPanel.stateEnum.open, _mapModel);
            {
                // Прозрачный фон и не показывать границы панели
                BottomPanel.BorderStyle = BorderStyle.None;
                BottomPanel.BackColor = Color.Transparent;
                BottomPanel.Left = 180;

                // Панель с легендой карты
                panelLegendOfMap = new Panel();
                {
                    panelLegendOfMap.Font = new Font("Segoe UI", 12);
                    panelLegendOfMap.Left = 10;
                    panelLegendOfMap.Top = 25;
                    panelLegendOfMap.Click += panelLegendOfMap_Click;
                    panelLegendOfMap.BackColor = _mapModel.colorApplication;
                    panelLegendOfMap.Size = new Size(505, 60);
                    panelLegendOfMap.Cursor = Cursors.Hand;
                }

                groupBoxPanelLegend = new GroupBox();
                {
                    groupBoxPanelLegend.Text = "Легенда карты";
                    groupBoxPanelLegend.Font = new Font("Segoe UI", 12);
                    groupBoxPanelLegend.Size = new Size(532, 98);
                    groupBoxPanelLegend.Left = 10;
                    groupBoxPanelLegend.Top = 35;
                }
                groupBoxPanelLegend.Controls.Add(panelLegendOfMap);
                BottomPanel.Controls.Add(groupBoxPanelLegend);

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
                    trackBarTransparent.BackColor = _mapModel.colorElements;
                    trackBarTransparent.Cursor = Cursors.Hand;
                    trackBarTransparent.Orientation = Orientation.Horizontal;
                    trackBarTransparent.Size = new Size(200, 45);
                    trackBarTransparent.TickStyle = TickStyle.Both;
                    trackBarTransparent.Scroll += trackBarTransparent_Scroll;
                }
                BottomPanel.Controls.Add(labelChangeTransparent);
                BottomPanel.Controls.Add(trackBarTransparent);

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
                    trackBarIntensity.BackColor = _mapModel.colorElements;
                    trackBarIntensity.Cursor = Cursors.Hand;
                    trackBarIntensity.Orientation = Orientation.Horizontal;
                    trackBarIntensity.Size = new Size(180, 45);
                    trackBarIntensity.TickStyle = TickStyle.Both;
                    trackBarIntensity.Scroll += trackBarIntensity_Scroll;
                }

                BottomPanel.Controls.Add(labelChangeIntensity);
                BottomPanel.Controls.Add(trackBarIntensity);

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
                    trackBarShades.BackColor = _mapModel.colorElements;
                    trackBarShades.Cursor = Cursors.Hand;
                    trackBarShades.Orientation = Orientation.Horizontal;
                    trackBarShades.Size = new Size(200, 45);
                    trackBarShades.TickStyle = TickStyle.Both;
                    trackBarShades.Scroll += trackBarShades_Scroll;
                }

                BottomPanel.Controls.Add(labelChangeShades);
                BottomPanel.Controls.Add(trackBarShades);

            }
            Controls.Add(BottomPanel);
            gmap.SendToBack();
        }

        /// <summary>
        /// Показать объекты инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonShowFacility_Click(object sender, EventArgs e)
        {
            _mapView.DrawFacility();
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Убрать объекты инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonClearFacility_Click(object sender, EventArgs e)
        {
            _mapView.ClearFacility();
        }

        /// <summary>
        /// Перерисовать слой с маркерами объектов инфраструктуры
        /// </summary>
        private void _CheckSelectedRadioButtonFacility(object sender, EventArgs e)
        {
            if (radioButtonShowFacility.Checked)
                radioButtonShowFacility_Click(sender, e);
            else
                radioButtonClearFacility_Click(sender, e);
        }

        /// <summary>
        /// Показать только название объектов инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonClearShowNameInfo_Click(object sender, EventArgs e)
        {
            _mapView.SetTextForFacility(1);
            _CheckSelectedRadioButtonFacility(sender, e);
        }

        /// <summary>
        /// Показать всю информацию об объектах инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonShowAllInfo_Click(object sender, EventArgs e)
        {
            _mapView.SetTextForFacility(2);
            _CheckSelectedRadioButtonFacility(sender, e);
        }

        /// <summary>
        /// Обработка щелчка по маркеру
        /// </summary>
        /// <param name="item">Маркер</param>
        /// <param name="e">Кнопка, которой сделали щелчок</param>
        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            // Если нажали левой кнопкой мыши по маркеру
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

            // Если нажали правой кнопкой мыши для удаления маркера
            if (e.Button == MouseButtons.Right)
            {

                // Подтвердить удаление маркера
                // if (MessageBox.Show("Удалить маркер?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                // {

                List<OptimalZone> _optimalZones = _mapModel._optimalZones;
                for (int i = 0; i < _optimalZones.Count; i++)
                {
                    _optimalZones[i].optimal.x = Math.Round(_optimalZones[i].optimal.x, 6);
                    _optimalZones[i].optimal.y = Math.Round(_optimalZones[i].optimal.y, 6);
                }

                for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count; i++)
                {
                    // Если координаты выбранной точки совпали с одной точкой из списка
                    if (item.Position.Lat == _sublayerUserFacilityCandidate.listWithLocation[i].x &&
                        item.Position.Lng == _sublayerUserFacilityCandidate.listWithLocation[i].y)
                    {
                        // Координаты текущей точки
                        double roundX_Location = Math.Round(item.Position.Lat, 6);
                        double roundY_Location = Math.Round(item.Position.Lng, 6);

                        bool flag = false;
                        for (int j = 0; j < _optimalZones.Count; j++)
                        {
                            // Удаление маркера, который был оптимальным
                            if (_optimalZones[j].optimal.x == roundX_Location && _optimalZones[j].optimal.y == roundY_Location)
                            {
                                // Убрать данный маркер с карты
                                _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                                _mapModel._optimalZones.Remove(_optimalZones[j]);
                                // Перерисовать все маркеры на карте
                                _mapView.DrawPointBufferZone();
                                flag = true;
                                if (_mapModel._optimalZones.Count == 0)
                                {
                                    // Обнулить флаг поиска оптимума
                                    _searchOptimum = false;
                                }
                            }
                        }
                        // Удаление неоптимальной точки
                        if (!flag)
                        {
                            // Убрать данный маркер с карты
                            _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                            // Перерисовать все маркеры на карте
                            _mapView.DrawPointBufferZone();
                        }
                        if (IsAutoShow && !IsShadowAuto)
                            // Перерисовать с новым радиусом авто-кандидатов
                            _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
                    }
                    // }
                }

            }
        }

        /// <summary>
        /// Отобразить территорию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonShowTerritory_Click(object sender, EventArgs e)
        {
            _mapView.DrawTerritory();
            // Перерисовать полигоны, если надо
            comboBoxColoringPolygon_SelectedIndexChanged(sender, e);
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            _CheckSelectedRadioButtonFacility(sender, e);
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Скрыть территорию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonClearTerritory_Click(object sender, EventArgs e)
        {
            _mapView.ClearTerritory();
        }

        /// <summary>
        /// Перерисовать слой с маркерами объектов инфраструктуры
        /// </summary>
        private void _CheckSelectedRadioButtonTerritory(object sender, EventArgs e)
        {
            if (radioButtonShowTerritory.Checked)
                radioButtonShowTerritory_Click(sender, e);
            else
                radioButtonClearTerritory_Click(sender, e);
        }

        /// <summary>
        /// Перерисовать всю карту
        /// </summary>
        private void _RedrawMap(object sender, EventArgs e)
        {
            comboBoxColoringPolygon_SelectedIndexChanged(sender, e);
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать на карте объекты инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать на карте территорию
            _CheckSelectedRadioButtonTerritory(sender, e);
            // Перерисовать буферные зоны
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Центрирование карты около загруженной области
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CenteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mapView.ChangeCenterMap(_mapModel.centerMap);
        }

        // Флаг входа в приложение
        private bool googleStart = true;
        /// <summary>
        /// Обработка выбора элемента в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxProvidersMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Если что-то выбрали в списке
            if (!string.IsNullOrEmpty(comboBoxProvidersMap.Text))
            {
                // Получить поставщика карты
                string _selectedProvider = comboBoxProvidersMap.Text;
                if (_selectedProvider == "Yandex")
                {
                    gmap.MapProvider = GMapProviders.YandexMap;
                    _RedrawMap(sender, e);
                }
                // Если выбран Google
                if (_selectedProvider == "Google")
                {
                    // Если правда, то это первый вход и ничего не надо делать
                    if (googleStart)
                        googleStart = false;
                    // Если не первый выбор
                    else
                    {
                        gmap.MapProvider = GMapProviders.GoogleMap;
                        _RedrawMap(sender, e);
                    }
                }
                if (_selectedProvider == "ArcGIS")
                {
                    gmap.MapProvider = GMapProviders.ArcGIS_World_Topo_Map;
                    _RedrawMap(sender, e);
                }
                if (_selectedProvider == "BingHybridMap")
                {
                    gmap.MapProvider = GMapProviders.BingHybridMap;
                    _RedrawMap(sender, e);
                }
                if (_selectedProvider == "OpenCycleMap")
                {
                    gmap.MapProvider = GMapProviders.OpenCycleMap;
                    _RedrawMap(sender, e);
                }
                if (_selectedProvider == "YandexHybridMap")
                {
                    gmap.MapProvider = GMapProviders.YandexHybridMap;
                    _RedrawMap(sender, e);
                }
                if (_selectedProvider == "WikiMapiaMap")
                {
                    gmap.MapProvider = GMapProviders.WikiMapiaMap;
                    _RedrawMap(sender, e);
                }
            }
        }

        // Номер критерия, по которому раскрашивает полигоны
        int indexOfSelectedCriterion = -1;
        /// <summary>
        /// Изменение критерия в площадной раскраски
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxColoringPolygon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Название выбранного критерия для раскраски
            string NameCriterionPolygonColoring = comboBoxColoringPolygon.Text;

            // Если выбрано "Очистить", то
            if (NameCriterionPolygonColoring == "Очистить")
            {
                // Убираем нижнюю панель
                _DisplayLegendElements(false);
                // Очищаем слой с раскраской
                _mapView.ClearPolygonQuar(_sublayerQuar);
            }
            // Если выбран какой-то критерий
            else
            {
                // Ищем индекс выбранного критерия для раскраски
                for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
                    if (_mapModel.listUserCriterion[i].nameOfCriterion == NameCriterionPolygonColoring)
                        indexOfSelectedCriterion = i;

                // Отображаем нижнюю панель
                _DisplayLegendElements(true);
                // Отобразить раскраску
                _DrawPolygonForQuartet();
                // Перерисовать объекты инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                // Перерисовать точечную раскраску
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                gmap.Refresh();
                // Перерисовать буферные зоны
                _mapView.DrawPointBufferZone();
                if (IsAutoShow && !IsShadowAuto)
                    // Перерисовать с новым радиусом авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
            }
        }

        /// <summary>
        /// Тик таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCheckPanel_Tick(object sender, EventArgs e)
        {
            // Панель перерисовывается только, если она была скрыта и вновь открыта
            if (_mapModel.stateOfBottomPanel == "Open")
            {
                _RedrawMap(sender, e);
                _mapModel.stateOfBottomPanel = "Close";
            }
        }

        // Отрисовка границ
        private Pen _penBoundary;
        // Начальная толщина границ
        private int _intensity = 1;
        // Начальная прозрачность полигонов
        private int _transparent = 255;
        /// <summary>
        /// Отрисовка кварталов
        /// </summary>
        private void _DrawPolygonForQuartet()
        {
            // Установка толщины границ
            if (_intensity >= 1)
                _penBoundary = new Pen(Color.FromArgb(255, 255, 0, 0), _intensity);
            else
                _penBoundary = new Pen(Color.FromArgb(0, 0, 0, 0), 0);

            // Удаление слоя
            _sublayerQuar.overlay.Polygons.Clear();
            _sublayerQuar.overlay.Markers.Clear();
            gmap.Overlays.Remove(_sublayerQuar.overlay);
            _sublayerQuar.overlay.Clear();

            // Заполнение таблицы данными из списка с кварталами
            DataTable dataquartets = _mapModel.CreateTableFromQuartets();
            int countOfQuartets = dataquartets.Rows.Count;

            // Для каждого квартала создается список граничных точек этого квартала
            List<PointLatLng>[] listBoundaryForEveryQuartet = new List<PointLatLng>[countOfQuartets];
            for (int i = 0; i < listBoundaryForEveryQuartet.Length; i++)
                listBoundaryForEveryQuartet[i] = new List<PointLatLng>();

            // Получить минимум, максимум для раскраски, количество оттенков, а также массив оттенков выбранного цвета
            _LegendQuartet(out Color[] colorPolygon, out double min, out double max, out int grids, out double step);

            for (int j = 0; j < countOfQuartets; j++)
            {
                _mapModel.tempListForQuar = new List<PointLatLng>();
                for (int k = 0; k < _sublayerQuar.listWithQuar[j].listBoundaryPoints.Count; k++)
                {
                    Location tempLocation = new Location(_sublayerQuar.listWithQuar[j].listBoundaryPoints[k].x,
                        _sublayerQuar.listWithQuar[j].listBoundaryPoints[k].y);
                    PointLatLng tempLocationForPolygon = new PointLatLng(tempLocation.x, tempLocation.y);
                    _mapModel.tempListForQuar.Add(tempLocationForPolygon);
                }
                listBoundaryForEveryQuartet[j] = _mapModel.tempListForQuar;
            }

            for (int j = 0; j < countOfQuartets; j++)
            {
                // Создание полигона для квартала
                var mapPolygon = new GMapPolygon(listBoundaryForEveryQuartet[j], "Quartet" + j.ToString());
                // Установка толщины границ у квартала
                mapPolygon.Stroke = _penBoundary;

                // Определение оттенка, в который необходимо окрасить квартал
                int ShadeNumber = 0;
                ShadeNumber = _mapModel.GetnumberShade(Convert.ToDouble(dataquartets.Rows[j][5 + indexOfSelectedCriterion]), min, max, grids, step);

                // Заливка полигона определенным цветом
                mapPolygon.Fill = new SolidBrush(colorPolygon[ShadeNumber]);
                // Добавление на слой квартала
                _sublayerQuar.overlay.Polygons.Add(mapPolygon);
            }
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerQuar.overlay);
            _sublayerQuar.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Легенда карты
        /// </summary>
        /// <param name="colorPolygon">Массив оттенков определенного цвета</param>
        /// <param name="min">Минимум для раскраски</param>
        /// <param name="max">Максимум для раскраски</param>
        /// <param name="grids">Количество оттенков</param>
        /// <param name="step">Интервал каждого оттенка</param>
        private void _LegendQuartet(out Color[] colorPolygon, out double minValueColoring,
            out double maxValueColoring, out int countOfGrids, out double countOfSteps)
        {
            Graphics graphics = panelLegendOfMap.CreateGraphics();
            graphics.Clear(_mapModel.colorApplication);

            // Временные переменные для работы с цветом
            int red, green, blue;
            int hred, hgreen, hblue;

            // Первым будет всегда белый цвет у минимума
            // От RGB белого цвета будет вычитаться RGB выбранного цвета, чтобы получить оттенки
            Color tempColor = Color.FromArgb(255, 255, 255, 255);
            // Ободок прямоугольников у оттенков
            Pen penBoundaryOfRectangle = new Pen(Color.Black, 0.002f);

            // Получение минимума, максимума и количества оттенков для выбранного критерия
            _mapModel.SetMaximumAndMinumForCriterionPolygonColoring(indexOfSelectedCriterion);
            countOfGrids = _mapModel.numberOfShadesCriterion;
            minValueColoring = _mapModel.minCriterionPolygonColoring;
            maxValueColoring = _mapModel.maxCriterionPolygonColoring;

            // Длина прямоугольника с цветом зависит от длины панели
            int widthOneRectangle = (panelLegendOfMap.Width - 20) / countOfGrids;
            // Шаг градаций
            countOfSteps = maxValueColoring / countOfGrids;
            // Округлить шаг до одной точки после запятой
            countOfSteps = Math.Round(countOfSteps, 1);

            // Отрисовка прямоугольников на панеле
            graphics = panelLegendOfMap.CreateGraphics();
            // Прямоугольников с оттенками будет столько, сколько градаций цветов
            Rectangle[] arrayOfRactangles = new Rectangle[countOfGrids];
            Brush[] brush = new SolidBrush[countOfGrids];
            colorPolygon = new Color[countOfGrids];

            // Заполнение цветами и шрифт надписей
            Font arialFont;
            if (countOfSteps < 9999)
                arialFont = new Font("Segoe UI", 10);
            else
                arialFont = new Font("Segoe UI", 6);
            Font timesFont = new Font("Segoe UI", 10);

            // От RGB белого цвета будет вычитаться RGB выбранного цвета, чтобы получить оттенки
            hred = (tempColor.R - _colorForColoring.R) / countOfGrids;
            hgreen = (tempColor.G - _colorForColoring.G) / countOfGrids;
            hblue = (tempColor.B - _colorForColoring.B) / countOfGrids;
            // Заполнение прямоугольников оттенками, для каждого прямоугольника находим RGB
            for (int i = 0; i < countOfGrids; i++)
            {
                red = tempColor.R - hred * i;
                green = tempColor.G - hgreen * i;
                blue = tempColor.B - hblue * i;

                // Заливка полигона
                colorPolygon[i] = Color.FromArgb(_transparent, red, green, blue);
                // Отображение прямоугольника по координанатам на панеле
                arrayOfRactangles[i] = new Rectangle(widthOneRectangle * i, 0, widthOneRectangle, 20);
                brush[i] = new SolidBrush(colorPolygon[i]);
                // Заливка прямоугольника на панеле определенным оттенком
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
            graphics.DrawString("Раскраска по критерию: " + _mapModel.listUserCriterion[indexOfSelectedCriterion].nameOfCriterion, timesFont, Brushes.Black, 0, 40);
            graphics.DrawString("Сменить цвет", timesFont, Brushes.Black, 410, 40);
        }

        /// <summary>
        /// Обработка изменения прозрачности цвета заливки полигона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarTransparent_Scroll(object sender, EventArgs e)
        {
            // От 0 до 5, то есть от 0(без заливки) до 255(полной заливки)
            _transparent = trackBarTransparent.Value * 51;
            // Перерисовать полигоны
            _DrawPolygonForQuartet();
            // Перерисовать объекты инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать буферные зоны
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Обработка изменения жирности границ для кварталов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarIntensity_Scroll(object sender, EventArgs e)
        {
            // Значение с ползунка задает толщину границ
            _intensity = trackBarIntensity.Value;
            // Перерисовать полигоны
            _DrawPolygonForQuartet();
            // Перерисовать объекты инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать буферные зоны
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Обработка изменения жирности границ для кварталов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarShades_Scroll(object sender, EventArgs e)
        {
            // Значение с ползунка задает толщину границ
            _mapModel.numberOfShadesCriterion = trackBarShades.Value;
            // Перерисовать полигоны
            _DrawPolygonForQuartet();
            // Перерисовать объекты инфраструктуры
            _CheckSelectedRadioButtonFacility(sender, e);
            // Перерисовать точечную раскраску
            comboBoxColoringIcons_SelectedIndexChanged(sender, e);
            // Перерисовать буферные зоны
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        /// <summary>
        /// Обработка щелчка по легенде карты
        /// </summary>
        private void panelLegendOfMap_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            // Расширеный выбор цвета и оттенков
            colorDialog.FullOpen = true;
            colorDialog.Color = this.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Сохранение выбранного цвета
                _colorForColoring = colorDialog.Color;
                // Перерисовка раскраски
                _DrawPolygonForQuartet();
                // Обновить карту
                gmap.Refresh();
                // Перерисовать объекты инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                _mapView.DrawPointBufferZone();
                if (IsAutoShow && !IsShadowAuto)
                    // Перерисовать с новым радиусом авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
            }
        }

        // Индекс критерия для точечной раскраски
        int indexOfSelectedCriterionIcon = -1;
        /// <summary>
        /// Изменение критерия в точечной раскраски
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxColoringIcons_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Название выбранного критерия для раскраски
            string NameCriterionIconColoring = comboBoxColoringIcons.Text;

            // Если выбрано "Очистить", то
            if (NameCriterionIconColoring == "Очистить")
                // Очистить раскраску
                _mapView.ClearIconColoring();

            // Если выбран какой-то критерий
            else
            {
                // Ищем индекс выбранного критерия для раскраски
                for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
                    if (_mapModel.listUserCriterion[i].nameOfCriterion == NameCriterionIconColoring)
                        indexOfSelectedCriterionIcon = i;

                // Найти минимум и максимум по выбранному критерию
                _mapModel.SetMaximumAndMinumForCriterionIconColoring(indexOfSelectedCriterionIcon);
                int countOfGrids = _mapModel.numberOfShadesCriterion;
                double maxValueColoring = _mapModel.maxCriterionIconColoring;
                double minValueColoring = _mapModel.minCriterionIconColoring;

                // Нарисовать точечную раскраску
                _mapView.ShowIconColoring(maxValueColoring, minValueColoring, countOfGrids, indexOfSelectedCriterionIcon, NameCriterionIconColoring);
                // Перерисовать объекты инфраструктуры
                _CheckSelectedRadioButtonFacility(sender, e);
                _mapView.DrawPointBufferZone();
                if (IsAutoShow && !IsShadowAuto)
                    // Перерисовать с новым радиусом авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
            }
        }

        /// <summary>
        /// Свернуть форму
        /// </summary>
        private void MainMap_SizeChanged(object sender, EventArgs e)
        {
            // Если форму сворачивали и при этом была выбрана какая-то раскраска - перерисовать легенду карты
            if ((WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Minimized) && comboBoxColoringPolygon.Text != "Очистить")
            {
                _DrawPolygonForQuartet();
                _CheckSelectedRadioButtonFacility(sender, e);
                comboBoxColoringIcons_SelectedIndexChanged(sender, e);
                _mapView.DrawPointBufferZone();
                if (IsAutoShow && !IsShadowAuto)
                    // Перерисовать с новым радиусом авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
            }
        }

        /// <summary>
        /// Сохранение фотографии
        /// </summary>
        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
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
                    Image image = gmap.ToImage();
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
        /// Установка маркера-кандидата на карте
        /// </summary>
        private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Координаты на карте, по которым поставили маркер
                double x = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
                double y = gmap.FromLocalToLatLng(e.X, e.Y).Lng;
                PointLatLng pointLatLng = new PointLatLng(x, y);

                // В списке маркеров не больше 30 точек
                if (_sublayerUserFacilityCandidate.listWithLocation.Count < _mapModel.limitUserPoines)
                {
                    // Если точка в пределах территории
                    if (_mapView.CheckInsidePointTerritory(pointLatLng) == true)
                    {
                        if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 1)
                        {
                            bool fff = false;
                            for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count; i++)
                            {
                                GeoCoordinate newPoint = new GeoCoordinate(x, y);
                                GeoCoordinate everyPoint = new GeoCoordinate(_sublayerUserFacilityCandidate.listWithLocation[i].x,
                                    _sublayerUserFacilityCandidate.listWithLocation[i].y);
                                int dlina = Convert.ToInt32(newPoint.GetDistanceTo(everyPoint));
                                if (dlina < 25)
                                    fff = true;
                            }

                            if (!fff)
                            {
                                // Если точка не принадлежит никакому полигону
                                if (_mapView.CheckInsidePointPolygon(pointLatLng) == false)
                                {
                                    // Ситуация, когда маркер в городе, но находится не в квартале (Дубки, набережная, заводы)
                                    if (MessageBox.Show("Точка не принадлежит полигонам. Продолжить?", "Предупреждение",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        // Добавление точки в список маркеров, поставленных пользователем
                                        _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                        // Установка маркера
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
                                if (IsAutoShow && !IsShadowAuto)
                                    // Перерисовать с новым радиусом авто-кандидатов
                                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
                            }
                            else
                                MessageBox.Show("Между кандидатами должно быть не менее 25 метров друг от друга",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Если точка не принадлежит никакому полигону
                            if (_mapView.CheckInsidePointPolygon(pointLatLng) == false)
                            {
                                // Ситуация, когда маркер в городе, но находится не в квартале (Дубки, набережная, заводы)
                                if (MessageBox.Show("Точка не принадлежит полигонам. Продолжить?", "Предупреждение",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    // Добавление точки в список маркеров, поставленных пользователем
                                    _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(x, y));
                                    // Установка маркера
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
                            if (IsAutoShow && !IsShadowAuto)
                                // Перерисовать с новым радиусом авто-кандидатов
                                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
                        }
                    }
                    else
                        MessageBox.Show("Маркер должен быть расположен в границах загруженной территории",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Нельзя разместить более " + _mapModel.limitUserPoines.ToString() + " маркеров",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Открытие формы для ввода радиуса
        /// </summary>
        private void InputRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingRadius form = new SettingRadius(_mapModel);
            form.ShowDialog();
            // Переотрисовать буферные зоны
            _mapView.DrawPointBufferZone();
            if (_searchOptimum)
                SearchOptimumToolStripMenuItem_Click(sender, e);
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                FindAutoToolStripMenuItem_Click(sender, e);
        }






        /// <summary>
        /// Настройка загруженных данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsToolStripMenu_Click(object sender, EventArgs e)
        {
            Settings form = new Settings(_mapModel);
            form.ShowDialog();


            if (_mapModel.nameObjectFacility.Length <= 12)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 11);
                labelFacility.Font = new Font("Segoe UI", 10);
            }
            if (_mapModel.nameObjectFacility.Length >= 13 && _mapModel.nameObjectFacility.Length <= 17)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 8);
                labelFacility.Font = new Font("Segoe UI", 8);
            }
            if (_mapModel.nameObjectFacility.Length >= 18)
            {
                // Название Группбокса - это название объекта инфраструктуры
                groupBoxFacility.Text = _mapModel.nameObjectFacility;
                // Выставить название объекта инфраструктуры пользователя
                labelFacility.Text = _mapModel.nameObjectFacility;
                groupBoxFacility.Font = new Font("Segoe UI", 6);
                labelFacility.Font = new Font("Segoe UI", 6);
            }






            // Выставить загруженную иконку
            pictureBoxFacility.Image = _mapModel.iconFacility;

            // Если ввели новую территорию 
            if (_mapModel.flagChangeTerritory)
            {
                // Оптимумы в любом случае удаляем
                _mapModel._optimalZones.Clear();
                _searchOptimum = false;

                // Идем по всем кандидатам и если они принадлежат территории оставляем, не вошедшие удаляем
                if (_sublayerUserFacilityCandidate.listWithLocation.Count > 0)
                {
                    for (int i = 0; i < _sublayerUserFacilityCandidate.listWithLocation.Count;)
                    {
                        PointLatLng point = new PointLatLng(_sublayerUserFacilityCandidate.listWithLocation[i].x, _sublayerUserFacilityCandidate.listWithLocation[i].y);
                        // Если точка в пределах территории
                        if (_mapView.CheckInsidePointTerritory(point) != true)
                        {
                            _sublayerUserFacilityCandidate.listWithLocation.Remove(_sublayerUserFacilityCandidate.listWithLocation[i]);
                            i = 0;
                        }
                    }
                }

                if (IsAutoShow && !IsShadowAuto)
                    DeleteAutoToolStripMenuItem_Click(sender, e);

                // Если новая территория, то сбросить радиус, так как сохранился от прошлой территории и может быть гораздо больше, чем нужно
                _mapModel.radiusBufferZone = 500;

                // Перецентрировать карту
                _mapView.ChangeCenterMap(_mapModel.centerMap);
                _mapModel.flagChangeTerritory = false;
            }

            // если ввели новые полигоны сносим оптимумы
            if (_mapModel.flagChangePolygon)
            {
                // Если есть и были найдены оптимумы
                if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 2 && _mapModel._optimalZones.Count != 0)
                    SearchOptimumToolStripMenuItem_Click(sender, e);

                if (IsAutoShow && !IsShadowAuto)
                    FindAutoToolStripMenuItem_Click(sender, e);

                _mapModel.flagChangePolygon = false;
            }

            // если ввели новые критерии и стояли оптимумы до этого то найти заново их
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
                // Добавляем "Очистить" к каждому списке
                comboBoxColoringIcons.Items.Add("Очистить");
                comboBoxColoringPolygon.Items.Add("Очистить");
                // Отмена редактирования списка + выставить "Очистить" по умолчанию
                comboBoxColoringIcons.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxColoringIcons.SelectedItem = "Очистить";
                comboBoxColoringPolygon.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxColoringPolygon.SelectedItem = "Очистить";

                // Если есть кандидаты и были найдены оптимумы
                if (_sublayerUserFacilityCandidate.listWithLocation.Count >= 2 && _mapModel._optimalZones.Count != 0)
                    SearchOptimumToolStripMenuItem_Click(sender, e);

                if (IsAutoShow && !IsShadowAuto)
                    FindAutoToolStripMenuItem_Click(sender, e);
            }
            _mapModel.flagChangeCriterion = false;
            _RedrawMap(sender, e);
        }

        private bool _searchOptimum = false;
        private List<OptimalZone> _optimalPoints;
        private MathOptimumModel _math;
        /// <summary>
        /// Открытие формы для поиска оптимума
        /// </summary>
        private void SearchOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Для каждого поставленного маркера создать буферную зону
            _mapModel.CreateListBufferZones();

            // Если на карте более двух маркеров
            if (_mapModel.listPointsBufferZone.Count >= 2)
            {
                //    List<double> l1 = new List<double> { 9, 9391, 3404 };
                //BufferZone z1 = new BufferZone(1, 47.217411, 38.862419, 500, l1);

                //List<double> l2 = new List<double> { 6, 6185, 2244 };
                //BufferZone z2 = new BufferZone(2, 47.233149, 38.876838, 500, l2);

                //List<double> l3 = new List<double> { 2, 2726, 989 };
                //BufferZone z3 = new BufferZone(3, 47.245503, 38.891773, 500, l3);

                //List<double> l4 = new List<double> { 9, 5054, 1834 };
                //BufferZone z4 = new BufferZone(4, 47.249581, 38.918123, 500, l4);

                //List<double> l5 = new List<double> { 5, 4172, 1517 };
                //BufferZone z5 = new BufferZone(5, 47.220209, 38.921556, 500, l5);

                //_mapModel.listPointsBufferZone.Add(z1);
                //_mapModel.listPointsBufferZone.Add(z2);
                //_mapModel.listPointsBufferZone.Add(z3);
                //_mapModel.listPointsBufferZone.Add(z4);
                //_mapModel.listPointsBufferZone.Add(z5);

                //// Добавление точки в список маркеров, поставленных пользователем
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(47.217411, 38.862419));
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(47.233149, 38.876838));
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(47.245503, 38.891773));
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(47.249581, 38.918123));
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(47.220209, 38.921556));
                //// Установка маркера
                //_mapView.DrawPointBufferZone();

                ////Список крайних точек территории
                //SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

                //// Минимимум и максимум (х,у) для начала первая точка из списка
                //double minX = listLocationBorderTerritory.listWithLocation[0].x;
                //double minY = listLocationBorderTerritory.listWithLocation[0].y;

                //double maxX = listLocationBorderTerritory.listWithLocation[0].x;
                //double maxY = listLocationBorderTerritory.listWithLocation[0].y;

                //// Поиск левой верхней и правой нижней точки
                //for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
                //{
                //    if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                //        minX = listLocationBorderTerritory.listWithLocation[i].x;
                //    if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                //        minY = listLocationBorderTerritory.listWithLocation[i].y;

                //    if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                //        maxX = listLocationBorderTerritory.listWithLocation[i].x;
                //    if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                //        maxY = listLocationBorderTerritory.listWithLocation[i].y;
                //}


                //// Добавление точки в список маркеров, поставленных пользователем
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(minX, minY));
                //_sublayerUserFacilityCandidate.listWithLocation.Add(new Location(maxX, maxY));
                ////// Установка маркера
                //_mapView.DrawPointBufferZone();

                // Поиск оптимальной точки с заданными буферными зонами и весами важности
                _math = new MathOptimumModel(_mapModel.listPointsBufferZone, _mapModel.listUserCriterion);
                // Получить массив оптимумов
                _optimalPoints = _math.GetOptimum();
                // Сохранить оптимальные зоны
                _mapModel._optimalZones = _optimalPoints;
                // MessageBox.Show(_mapModel._optimalZones.Count.ToString());
                // После закрытия формы перерисовать точки, выделив оптимум зеленым цветом
                _mapView.DrawPointBufferZone();
                if (IsAutoShow && !IsShadowAuto)
                    // Перерисовать с новым радиусом авто-кандидатов
                    _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
                _searchOptimum = true;
            }
            else
                MessageBox.Show("Необходимо разместить не менее двух маркеров", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Оставить на карте отображение только оптимальной точки
        /// </summary>
        private void LeaveOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sublayerUserFacilityCandidate.listWithLocation.Clear();
            for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                _sublayerUserFacilityCandidate.listWithLocation.Add(new Location(_mapModel._optimalZones[i].optimal.x, _mapModel._optimalZones[i].optimal.y));
            _mapView.DrawPointBufferZone();
            if (IsAutoShow && !IsShadowAuto)
                // Перерисовать с новым радиусом авто-кандидатов
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
        }

        private bool _flagSaveOptimum = false;
        /// <summary>
        /// Сохранение оптимума в файл
        /// </summary>
        private void SaveOptimumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если на карте есть оптимумы
            if (_mapModel._optimalZones.Count != 0)
            {
                try
                {
                    using (SaveFileDialog SaveOptimumDialog = new SaveFileDialog())
                    {
                        SaveOptimumDialog.Filter = "Files (*.csv) | *.csv";
                        SaveOptimumDialog.InitialDirectory = Application.StartupPath + @"\Data\DataTest";
                        SaveOptimumDialog.Title = "Сохранение в CSV-файл данных об оптимальных точках";
                        SaveOptimumDialog.FileName = "Оптимальные точки";

                        // Если пользователь подтвердил сохранение
                        if (SaveOptimumDialog.ShowDialog() == DialogResult.OK)
                        {
                            string pathToFile = SaveOptimumDialog.FileName.ToString();
                            string isValidFile = _fileValidator.FileValidationCreateCSV(pathToFile);

                            // Если файл прошёл все проверки
                            if (isValidFile == "Успешно")
                            {
                                // Запись в файл информации об оптимальной точке
                                _fileValidator.WriteInfoOptimumToCSV(pathToFile, _mapModel._optimalZones);
                                // Флажок, что пользователь сохранил оптимальную точку в файл
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
        /// Нажатие на красный крестик формы
        /// </summary>
        private void Map_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил точку в csv-файл
            if (_flagSaveOptimum == true)
                e.Cancel = false;
            else
            {
                // Если пользователь искал оптимум
                if (_searchOptimum == true)
                {
                    // Если пользователь не сохранил оптимальную точку в csv-файл, но при этом искал оптимум
                    if (MessageBox.Show("Вы не сохранили оптимальную точку. Выйти?", "Предупреждение",
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
        /// Отобразить найденный оптимум на карте Google
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mapModel._optimalZones.Count != 0)
            {
                int fff = 0;
                int idBest = -1;
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (_mapModel._optimalZones[i].namesConvolutiones.Count > fff)
                    {
                        fff = _mapModel._optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel._optimalZones[i].optimal.x.ToString();
                        string y = _mapModel._optimalZones[i].optimal.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        System.Diagnostics.Process.Start("https://www.google.com/maps/@" + x + "," + y + ",18z");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимульные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить найденный оптимум на карте Яндекс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YandexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mapModel._optimalZones.Count != 0)
            {
                int fff = 0;
                int idBest = -1;
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (_mapModel._optimalZones[i].namesConvolutiones.Count > fff)
                    {
                        fff = _mapModel._optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel._optimalZones[i].optimal.x.ToString();
                        string y = _mapModel._optimalZones[i].optimal.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        System.Diagnostics.Process.Start("https://yandex.ru/maps/971/taganrog/?ll=" + y + "%2C" + x + "&z=18");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимульные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить найденный оптимум на карте 2ГИС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TwoGISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mapModel._optimalZones.Count != 0)
            {
                int fff = 0;
                int idBest = -1;
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (_mapModel._optimalZones[i].namesConvolutiones.Count > fff)
                    {
                        fff = _mapModel._optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel._optimalZones[i].optimal.x.ToString();
                        string y = _mapModel._optimalZones[i].optimal.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        System.Diagnostics.Process.Start("https://2gis.ru/taganrog/geo/16326171458235811/" + y + "%2C" + x + "?m=" + y + "%2C" + x + "%2F" + "17.84");
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимульные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Отобразить найденный оптимум на карте OpenStreetMap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenStreetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mapModel._optimalZones.Count != 0)
            {
                int fff = 0;
                int idBest = -1;
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (_mapModel._optimalZones[i].namesConvolutiones.Count > fff)
                    {
                        fff = _mapModel._optimalZones[i].namesConvolutiones.Count;
                        idBest = i;
                    }
                }
                for (int i = 0; i < _mapModel._optimalZones.Count; i++)
                {
                    if (i == idBest)
                    {
                        string x = _mapModel._optimalZones[i].optimal.x.ToString();
                        string y = _mapModel._optimalZones[i].optimal.y.ToString();

                        string[] str = x.Split(new char[] { ',' });
                        x = str[0] + "." + str[1];

                        str = y.Split(new char[] { ',' });
                        y = str[0] + "." + str[1];

                        System.Diagnostics.Process.Start("https://www.openstreetmap.org/#map=19/" + x + "/" + y);
                    }
                }
            }
            else
                MessageBox.Show("Найдите оптимульные точки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Флаг показа авто-точек
        bool IsAutoShow = false;
        bool IsShadowAuto = false;
        /// <summary>
        /// Показать авто-оптимумы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAutoShow)
            {
                IsShadowAuto = false;
                _sublayerAuto.overlay.IsVisibile = true;
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);
            }
            else
                MessageBox.Show("Найдите авто-оптимумы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Скрыть авто-оптимумы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAutoShow)
            {
                IsShadowAuto = true;
                _sublayerAuto.overlay.IsVisibile = false;
            }
            else
                MessageBox.Show("Найдите авто-оптимумы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса
        /// </summary>
        /// <returns>Радиус в метрах</returns>
        private double FindDiagonalTerritory()
        {
            // Список крайних точек территории
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;

            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки
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

        List<BufferZone> _listOptimalPoint;
        /// <summary>
        /// Найти авто-оптимумы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Очищение карты от авто-оптимумов
            _sublayerAuto.listWithLocation.Clear();
            _sublayerAuto.overlay.Polygons.Clear();
            _sublayerAuto.overlay.Markers.Clear();
            _sublayerAuto.overlay.Clear();

            // Количество оптимумов для поиска
            int countOfOptimum = Convert.ToInt32(comboBoxAuto.Text);

            // Список крайних точек территории
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;
            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки
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

            // minX = minX + 0.009; minY = minY + 0.007;
            // minX = minX + 0.003; minY = minY + 0.005;

            double stepX = Math.Round(FindDiagonalTerritory() / 5908666, 3); // 0.003
            double stepY = Math.Round(FindDiagonalTerritory() / 3545200, 3); // 0.005

            // Заполняем полигон города точками
            while (minY <= maxY)
            {
                while (minX <= maxX)
                {
                    // Добавляем в список все точки прохода города
                    listWithFullPassTerritory.Add(new PointLatLng(minX, minY));
                    minX = minX + stepX;
                }
                // Нарастили ось Y и еще раз по оси Х
                minY = minY + stepY;
                minX = saveMinX;
            }

            // Идем по всем точкам глобального прохода
            for (int i = 0; i < listWithFullPassTerritory.Count; i++)
            {
                // Создаем точку
                PointLatLng pointLatLng = new PointLatLng(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng);
                // Если точка принадлежит какому-то кварталу
                if (_mapView.CheckInsidePointPolygon(pointLatLng) == true)
                {
                    // Добавление точки в список авто-маркеров
                    _sublayerAuto.listWithLocation.Add(new Location(listWithFullPassTerritory[i].Lat, listWithFullPassTerritory[i].Lng));
                }
            }

            // Очистить список
            listWithFullPassTerritory.Clear();

            // Установка маркеров и буфреных зон, которые попали в полигоны
            _mapView.DrawAutoPointBufferZone(_mapModel.radiusBufferZone);

            // Получить список буферных зон, которые попали в Таганрог, но не 0, 0, 0
            _mapModel.CreateListAutoBufferZones();

            // Список буферных зон для поиска оптимума (среди которых нет 0, 0, 0)
            List<BufferZone> _listAutoBufferZones = _mapModel.listAutoPointsBufferZone;

            // ситуация, когда все нули и авто найти нечего
            if (_listAutoBufferZones.Count == 0)
            {
                // Строка с ошибкой
                var errorCriterion = new StringBuilder();
                errorCriterion.AppendLine("Не удалось найти ни одного оптимума");
                errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // ситуация, когда можем дать меньше чем просят
            if (_listAutoBufferZones.Count > 0 && _listAutoBufferZones.Count < countOfOptimum)
            {
                // Строка с ошибкой
                var errorCriterion = new StringBuilder();
                errorCriterion.AppendLine("Не удалось найти требуемое количество оптимумов");
                errorCriterion.AppendLine("Попробуйте увеличить радиус поиска");
                errorCriterion.AppendLine("Запрошено " + countOfOptimum + " оптимумов");
                errorCriterion.AppendLine("Найдено " + _listAutoBufferZones.Count + " оптимумов");

                // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
                MathAutoOptimumModel _math = new MathAutoOptimumModel(_listAutoBufferZones, _mapModel.listUserCriterion, _listAutoBufferZones.Count);

                // Получить (1-10) оптимумов
                _listOptimalPoint = _math.GetArrayWithOptimums();

                // Отрисовать звездочки оптимальных точек (1-10)
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);

                // Флаг поиска авто-кандидатов
                IsAutoShow = true;

                if (IsShadowAuto)
                    _sublayerAuto.overlay.IsVisibile = false;
                else
                    _sublayerAuto.overlay.IsVisibile = true;

                MessageBox.Show(errorCriterion.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (_listAutoBufferZones.Count >= countOfOptimum)
            {
                // Поиск оптимальной точки с авто-буферными зонами, весами важности и количеством оптимумов
                MathAutoOptimumModel _math = new MathAutoOptimumModel(_listAutoBufferZones, _mapModel.listUserCriterion, countOfOptimum);

                // Получить (1-10) оптимумов
                _listOptimalPoint = _math.GetArrayWithOptimums();

                // Отрисовать звездочки оптимальных точек (1-10)
                _mapView.DrawAutoIdealPointBufferZone(_listOptimalPoint);

                // Флаг поиска авто-кандидатов
                IsAutoShow = true;

                if (IsShadowAuto)
                    _sublayerAuto.overlay.IsVisibile = false;
                else
                    _sublayerAuto.overlay.IsVisibile = true;
            } 
        }

        /// <summary>
        /// Удалить авто-оптимумы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsAutoShow)
            {
                // Авто-кандидаты скрыты
                IsAutoShow = false;
                // Убрать все следы авто-поиска с карты
                _mapView.ClearIdealPoints(_sublayerAuto);
            }
        }

        /// <summary>
        /// Норма на душу населения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NormPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если файл загружен для нормы
            if (_mapModel.flagLoadFileNorma)
            {
                SearchNormPerCapita form = new SearchNormPerCapita(_languageOfMap);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// Переход на окно "О нас"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutUSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Открытие руководства пользователя
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\UserManual.pdf");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}