namespace Optimum
{
    partial class SearchNormPerCapita
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchNormPerCapita));
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.directorySearcher2 = new System.DirectoryServices.DirectorySearcher();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonFindBest = new System.Windows.Forms.Button();
            this.buttonSaveMap = new System.Windows.Forms.Button();
            this.buttonOpenWebManual = new System.Windows.Forms.Button();
            this.buttonHideBest = new System.Windows.Forms.Button();
            this.radioButtonHideFacility = new System.Windows.Forms.RadioButton();
            this.radioButtonShowFacility = new System.Windows.Forms.RadioButton();
            this.buttonShowBest = new System.Windows.Forms.Button();
            this.labelRadiusLong = new System.Windows.Forms.Label();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.labelFacility = new System.Windows.Forms.Label();
            this.pictureBoxFacility = new System.Windows.Forms.PictureBox();
            this.labelOptimum = new System.Windows.Forms.Label();
            this.pictureBoxAutoNorma = new System.Windows.Forms.PictureBox();
            this.labelLegendForMap = new System.Windows.Forms.Label();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.gmapNorm = new GMap.NET.WindowsForms.GMapControl();
            this.textBoxSummFacility = new System.Windows.Forms.TextBox();
            this.labelNorm = new System.Windows.Forms.Label();
            this.textBoxSummPopilation = new System.Windows.Forms.TextBox();
            this.labelPeople = new System.Windows.Forms.Label();
            this.labelStateOfNormTerritory = new System.Windows.Forms.Label();
            this.groupBoxRadius = new System.Windows.Forms.GroupBox();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.groupBoxNorm = new System.Windows.Forms.GroupBox();
            this.groupBoxFacility = new System.Windows.Forms.GroupBox();
            this.panelLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAutoNorma)).BeginInit();
            this.groupBoxRadius.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            this.groupBoxNorm.SuspendLayout();
            this.groupBoxFacility.SuspendLayout();
            this.SuspendLayout();
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // directorySearcher2
            // 
            this.directorySearcher2.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher2.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher2.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // buttonFindBest
            // 
            this.buttonFindBest.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonFindBest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFindBest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindBest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFindBest.Image = ((System.Drawing.Image)(resources.GetObject("buttonFindBest.Image")));
            this.buttonFindBest.Location = new System.Drawing.Point(13, 310);
            this.buttonFindBest.Name = "buttonFindBest";
            this.buttonFindBest.Size = new System.Drawing.Size(171, 62);
            this.buttonFindBest.TabIndex = 3;
            this.buttonFindBest.Text = "Анализ объектов";
            this.buttonFindBest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonFindBest, "Рассчитать нехватку или избыточность, найти и показать на карте наилучшие местопо" +
        "ложения.");
            this.buttonFindBest.UseVisualStyleBackColor = false;
            this.buttonFindBest.Click += new System.EventHandler(this.buttonFindBest_Click);
            // 
            // buttonSaveMap
            // 
            this.buttonSaveMap.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonSaveMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSaveMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSaveMap.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveMap.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveMap.Image")));
            this.buttonSaveMap.Location = new System.Drawing.Point(13, 590);
            this.buttonSaveMap.Name = "buttonSaveMap";
            this.buttonSaveMap.Size = new System.Drawing.Size(171, 62);
            this.buttonSaveMap.TabIndex = 7;
            this.buttonSaveMap.Text = "Сохранить карту";
            this.buttonSaveMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonSaveMap, "Сохранить карту в формате png.");
            this.buttonSaveMap.UseVisualStyleBackColor = false;
            this.buttonSaveMap.Click += new System.EventHandler(this.buttonSaveMap_Click);
            // 
            // buttonOpenWebManual
            // 
            this.buttonOpenWebManual.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonOpenWebManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpenWebManual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenWebManual.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenWebManual.Image = global::Optimum.Properties.Resources.iconPDF;
            this.buttonOpenWebManual.Location = new System.Drawing.Point(13, 520);
            this.buttonOpenWebManual.Name = "buttonOpenWebManual";
            this.buttonOpenWebManual.Size = new System.Drawing.Size(171, 62);
            this.buttonOpenWebManual.TabIndex = 6;
            this.buttonOpenWebManual.Text = "Руководство пользователя";
            this.buttonOpenWebManual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonOpenWebManual, "Ознакомиться с руководством пользователя.");
            this.buttonOpenWebManual.UseVisualStyleBackColor = false;
            this.buttonOpenWebManual.Click += new System.EventHandler(this.buttonOpenWebManual_Click);
            // 
            // buttonHideBest
            // 
            this.buttonHideBest.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonHideBest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHideBest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHideBest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHideBest.Image = ((System.Drawing.Image)(resources.GetObject("buttonHideBest.Image")));
            this.buttonHideBest.Location = new System.Drawing.Point(13, 450);
            this.buttonHideBest.Name = "buttonHideBest";
            this.buttonHideBest.Size = new System.Drawing.Size(171, 62);
            this.buttonHideBest.TabIndex = 5;
            this.buttonHideBest.Text = "Скрыть рекомендации";
            this.buttonHideBest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonHideBest, "Найти на карте оптимальные точки для открытия новых объектов.");
            this.buttonHideBest.UseVisualStyleBackColor = false;
            this.buttonHideBest.Click += new System.EventHandler(this.buttonHideBest_Click);
            // 
            // radioButtonHideFacility
            // 
            this.radioButtonHideFacility.AutoSize = true;
            this.radioButtonHideFacility.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonHideFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonHideFacility.Location = new System.Drawing.Point(6, 48);
            this.radioButtonHideFacility.Name = "radioButtonHideFacility";
            this.radioButtonHideFacility.Size = new System.Drawing.Size(225, 21);
            this.radioButtonHideFacility.TabIndex = 1;
            this.radioButtonHideFacility.Text = "Скрыть объекты инфраструктуры";
            this.toolTip.SetToolTip(this.radioButtonHideFacility, "Убрать с карты все объекты социальной инфраструктуры зоны анализа.");
            this.radioButtonHideFacility.UseVisualStyleBackColor = true;
            this.radioButtonHideFacility.Click += new System.EventHandler(this.radioButtonHideFacility_Click);
            // 
            // radioButtonShowFacility
            // 
            this.radioButtonShowFacility.AutoSize = true;
            this.radioButtonShowFacility.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonShowFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonShowFacility.Location = new System.Drawing.Point(6, 23);
            this.radioButtonShowFacility.Name = "radioButtonShowFacility";
            this.radioButtonShowFacility.Size = new System.Drawing.Size(253, 21);
            this.radioButtonShowFacility.TabIndex = 1;
            this.radioButtonShowFacility.Text = "Отобразить объекты инфраструктуры";
            this.toolTip.SetToolTip(this.radioButtonShowFacility, "На карте отобразятся все объекты социальной инфраструктуры зоны анализа.");
            this.radioButtonShowFacility.UseVisualStyleBackColor = true;
            this.radioButtonShowFacility.Click += new System.EventHandler(this.radioButtonShowFacility_Click);
            // 
            // buttonShowBest
            // 
            this.buttonShowBest.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonShowBest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonShowBest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonShowBest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShowBest.Image = ((System.Drawing.Image)(resources.GetObject("buttonShowBest.Image")));
            this.buttonShowBest.Location = new System.Drawing.Point(13, 380);
            this.buttonShowBest.Name = "buttonShowBest";
            this.buttonShowBest.Size = new System.Drawing.Size(171, 62);
            this.buttonShowBest.TabIndex = 4;
            this.buttonShowBest.Text = "Показать рекомендации";
            this.buttonShowBest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonShowBest, "Найти на карте оптимальные точки для открытия новых объектов.");
            this.buttonShowBest.UseVisualStyleBackColor = false;
            this.buttonShowBest.Click += new System.EventHandler(this.buttonShowBest_Click);
            // 
            // labelRadiusLong
            // 
            this.labelRadiusLong.AutoSize = true;
            this.labelRadiusLong.BackColor = System.Drawing.Color.LightGreen;
            this.labelRadiusLong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelRadiusLong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadiusLong.Location = new System.Drawing.Point(6, 73);
            this.labelRadiusLong.Name = "labelRadiusLong";
            this.labelRadiusLong.Size = new System.Drawing.Size(48, 17);
            this.labelRadiusLong.TabIndex = 1;
            this.labelRadiusLong.Text = "Радиус";
            this.toolTip.SetToolTip(this.labelRadiusLong, "Радиус поиска для поиска наилучших местоположений.");
            // 
            // panelLegend
            // 
            this.panelLegend.Controls.Add(this.labelFacility);
            this.panelLegend.Controls.Add(this.pictureBoxFacility);
            this.panelLegend.Controls.Add(this.labelOptimum);
            this.panelLegend.Controls.Add(this.pictureBoxAutoNorma);
            this.panelLegend.Controls.Add(this.labelLegendForMap);
            this.panelLegend.Location = new System.Drawing.Point(1175, 35);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(185, 91);
            this.panelLegend.TabIndex = 75;
            // 
            // labelFacility
            // 
            this.labelFacility.AutoSize = true;
            this.labelFacility.BackColor = System.Drawing.Color.LightGreen;
            this.labelFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFacility.Location = new System.Drawing.Point(35, 30);
            this.labelFacility.Name = "labelFacility";
            this.labelFacility.Size = new System.Drawing.Size(106, 17);
            this.labelFacility.TabIndex = 108;
            this.labelFacility.Text = "Инфраструктура";
            // 
            // pictureBoxFacility
            // 
            this.pictureBoxFacility.Location = new System.Drawing.Point(5, 25);
            this.pictureBoxFacility.Name = "pictureBoxFacility";
            this.pictureBoxFacility.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxFacility.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFacility.TabIndex = 109;
            this.pictureBoxFacility.TabStop = false;
            // 
            // labelOptimum
            // 
            this.labelOptimum.AutoSize = true;
            this.labelOptimum.BackColor = System.Drawing.Color.LightGreen;
            this.labelOptimum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOptimum.Location = new System.Drawing.Point(35, 60);
            this.labelOptimum.Name = "labelOptimum";
            this.labelOptimum.Size = new System.Drawing.Size(128, 17);
            this.labelOptimum.TabIndex = 107;
            this.labelOptimum.Text = "Оптимальное место";
            // 
            // pictureBoxAutoNorma
            // 
            this.pictureBoxAutoNorma.Location = new System.Drawing.Point(5, 55);
            this.pictureBoxAutoNorma.Name = "pictureBoxAutoNorma";
            this.pictureBoxAutoNorma.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxAutoNorma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAutoNorma.TabIndex = 106;
            this.pictureBoxAutoNorma.TabStop = false;
            // 
            // labelLegendForMap
            // 
            this.labelLegendForMap.AutoSize = true;
            this.labelLegendForMap.BackColor = System.Drawing.Color.LightGreen;
            this.labelLegendForMap.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLegendForMap.Location = new System.Drawing.Point(8, 5);
            this.labelLegendForMap.Name = "labelLegendForMap";
            this.labelLegendForMap.Size = new System.Drawing.Size(180, 17);
            this.labelLegendForMap.TabIndex = 0;
            this.labelLegendForMap.Text = "УСЛОВНЫЕ ОБОЗНАЧЕНИЯ";
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // gmapNorm
            // 
            this.gmapNorm.Bearing = 0F;
            this.gmapNorm.CanDragMap = true;
            this.gmapNorm.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmapNorm.GrayScaleMode = false;
            this.gmapNorm.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmapNorm.LevelsKeepInMemmory = 5;
            this.gmapNorm.Location = new System.Drawing.Point(285, 37);
            this.gmapNorm.MarkersEnabled = true;
            this.gmapNorm.MaxZoom = 2;
            this.gmapNorm.MinZoom = 2;
            this.gmapNorm.MouseWheelZoomEnabled = true;
            this.gmapNorm.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmapNorm.Name = "gmapNorm";
            this.gmapNorm.NegativeMode = false;
            this.gmapNorm.PolygonsEnabled = true;
            this.gmapNorm.RetryLoadTile = 0;
            this.gmapNorm.RoutesEnabled = true;
            this.gmapNorm.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmapNorm.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmapNorm.ShowTileGridLines = false;
            this.gmapNorm.Size = new System.Drawing.Size(1074, 655);
            this.gmapNorm.TabIndex = 64;
            this.gmapNorm.Zoom = 0D;
            this.gmapNorm.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmapNorm.Load += new System.EventHandler(this.gmap_Load);
            // 
            // textBoxSummFacility
            // 
            this.textBoxSummFacility.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSummFacility.Location = new System.Drawing.Point(152, 24);
            this.textBoxSummFacility.Name = "textBoxSummFacility";
            this.textBoxSummFacility.Size = new System.Drawing.Size(102, 29);
            this.textBoxSummFacility.TabIndex = 1;
            // 
            // labelNorm
            // 
            this.labelNorm.AutoSize = true;
            this.labelNorm.BackColor = System.Drawing.Color.LightGreen;
            this.labelNorm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNorm.Location = new System.Drawing.Point(6, 30);
            this.labelNorm.Name = "labelNorm";
            this.labelNorm.Size = new System.Drawing.Size(110, 17);
            this.labelNorm.TabIndex = 85;
            this.labelNorm.Text = "Норма объектов";
            // 
            // textBoxSummPopilation
            // 
            this.textBoxSummPopilation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSummPopilation.Location = new System.Drawing.Point(152, 59);
            this.textBoxSummPopilation.Name = "textBoxSummPopilation";
            this.textBoxSummPopilation.Size = new System.Drawing.Size(102, 29);
            this.textBoxSummPopilation.TabIndex = 1;
            // 
            // labelPeople
            // 
            this.labelPeople.AutoSize = true;
            this.labelPeople.BackColor = System.Drawing.Color.LightGreen;
            this.labelPeople.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPeople.Location = new System.Drawing.Point(6, 65);
            this.labelPeople.Name = "labelPeople";
            this.labelPeople.Size = new System.Drawing.Size(124, 17);
            this.labelPeople.TabIndex = 87;
            this.labelPeople.Text = "На душу населения";
            // 
            // labelStateOfNormTerritory
            // 
            this.labelStateOfNormTerritory.AutoSize = true;
            this.labelStateOfNormTerritory.BackColor = System.Drawing.Color.LightGreen;
            this.labelStateOfNormTerritory.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStateOfNormTerritory.Location = new System.Drawing.Point(285, 9);
            this.labelStateOfNormTerritory.Name = "labelStateOfNormTerritory";
            this.labelStateOfNormTerritory.Size = new System.Drawing.Size(492, 21);
            this.labelStateOfNormTerritory.TabIndex = 95;
            this.labelStateOfNormTerritory.Text = "Состояние нормы объекта инфраструктуры на душу населения";
            // 
            // groupBoxRadius
            // 
            this.groupBoxRadius.Controls.Add(this.trackBarRadius);
            this.groupBoxRadius.Controls.Add(this.labelRadiusLong);
            this.groupBoxRadius.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRadius.Location = new System.Drawing.Point(13, 94);
            this.groupBoxRadius.Name = "groupBoxRadius";
            this.groupBoxRadius.Size = new System.Drawing.Size(264, 100);
            this.groupBoxRadius.TabIndex = 1;
            this.groupBoxRadius.TabStop = false;
            this.groupBoxRadius.Text = "Установка радиуса";
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.BackColor = System.Drawing.Color.PaleGreen;
            this.trackBarRadius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarRadius.Location = new System.Drawing.Point(10, 24);
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(244, 45);
            this.trackBarRadius.TabIndex = 0;
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarTransparent_Scroll);
            // 
            // groupBoxNorm
            // 
            this.groupBoxNorm.Controls.Add(this.labelPeople);
            this.groupBoxNorm.Controls.Add(this.labelNorm);
            this.groupBoxNorm.Controls.Add(this.textBoxSummFacility);
            this.groupBoxNorm.Controls.Add(this.textBoxSummPopilation);
            this.groupBoxNorm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxNorm.Location = new System.Drawing.Point(13, 200);
            this.groupBoxNorm.Name = "groupBoxNorm";
            this.groupBoxNorm.Size = new System.Drawing.Size(264, 101);
            this.groupBoxNorm.TabIndex = 2;
            this.groupBoxNorm.TabStop = false;
            this.groupBoxNorm.Text = "Установка нормы на душу населения";
            // 
            // groupBoxFacility
            // 
            this.groupBoxFacility.Controls.Add(this.radioButtonHideFacility);
            this.groupBoxFacility.Controls.Add(this.radioButtonShowFacility);
            this.groupBoxFacility.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxFacility.Location = new System.Drawing.Point(13, 12);
            this.groupBoxFacility.Name = "groupBoxFacility";
            this.groupBoxFacility.Size = new System.Drawing.Size(263, 79);
            this.groupBoxFacility.TabIndex = 0;
            this.groupBoxFacility.TabStop = false;
            this.groupBoxFacility.Text = "Отображение объекта";
            // 
            // SearchNormPerCapita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1370, 712);
            this.Controls.Add(this.buttonShowBest);
            this.Controls.Add(this.groupBoxFacility);
            this.Controls.Add(this.buttonHideBest);
            this.Controls.Add(this.groupBoxNorm);
            this.Controls.Add(this.groupBoxRadius);
            this.Controls.Add(this.labelStateOfNormTerritory);
            this.Controls.Add(this.buttonOpenWebManual);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.buttonFindBest);
            this.Controls.Add(this.buttonSaveMap);
            this.Controls.Add(this.gmapNorm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1386, 751);
            this.MinimumSize = new System.Drawing.Size(1364, 726);
            this.Name = "SearchNormPerCapita";
            this.Text = "Поиск нормы на душу населения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchNormPerCapita_FormClosing);
            this.Load += new System.EventHandler(this.SearchNormPerCapita_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchNormPerCapita_KeyDown);
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAutoNorma)).EndInit();
            this.groupBoxRadius.ResumeLayout(false);
            this.groupBoxRadius.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            this.groupBoxNorm.ResumeLayout(false);
            this.groupBoxNorm.PerformLayout();
            this.groupBoxFacility.ResumeLayout(false);
            this.groupBoxFacility.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.DirectoryServices.DirectorySearcher directorySearcher2;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label labelLegendForMap;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.FontDialog fontDialog1;
        private GMap.NET.WindowsForms.GMapControl gmapNorm;
        private System.Windows.Forms.TextBox textBoxSummFacility;
        private System.Windows.Forms.Label labelNorm;
        private System.Windows.Forms.TextBox textBoxSummPopilation;
        private System.Windows.Forms.Label labelPeople;
        private System.Windows.Forms.Button buttonFindBest;
        private System.Windows.Forms.Button buttonSaveMap;
        private System.Windows.Forms.Button buttonOpenWebManual;
        private System.Windows.Forms.Label labelOptimum;
        private System.Windows.Forms.PictureBox pictureBoxAutoNorma;
        private System.Windows.Forms.Label labelStateOfNormTerritory;
        private System.Windows.Forms.GroupBox groupBoxRadius;
        private System.Windows.Forms.GroupBox groupBoxNorm;
        private System.Windows.Forms.Button buttonHideBest;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label labelFacility;
        private System.Windows.Forms.PictureBox pictureBoxFacility;
        private System.Windows.Forms.Label labelRadiusLong;
        private System.Windows.Forms.GroupBox groupBoxFacility;
        private System.Windows.Forms.RadioButton radioButtonHideFacility;
        private System.Windows.Forms.RadioButton radioButtonShowFacility;
        private System.Windows.Forms.Button buttonShowBest;
    }
}