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
            this.buttonLoadPriority = new System.Windows.Forms.Button();
            this.buttonInputRadius = new System.Windows.Forms.Button();
            this.buttonInputNorm = new System.Windows.Forms.Button();
            this.buttonFindBest = new System.Windows.Forms.Button();
            this.buttonSaveMap = new System.Windows.Forms.Button();
            this.buttonOpenWebManual = new System.Windows.Forms.Button();
            this.buttonHideBest = new System.Windows.Forms.Button();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.labelPharmacy = new System.Windows.Forms.Label();
            this.pictureBoxPharmacy = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxAuto = new System.Windows.Forms.PictureBox();
            this.labelLegendForMap = new System.Windows.Forms.Label();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.gmapNorm = new GMap.NET.WindowsForms.GMapControl();
            this.textBoxWeightPharmacy = new System.Windows.Forms.TextBox();
            this.labelPharma = new System.Windows.Forms.Label();
            this.textBoxWeightResidents = new System.Windows.Forms.TextBox();
            this.labelRes = new System.Windows.Forms.Label();
            this.textBoxWeightRetired = new System.Windows.Forms.TextBox();
            this.labelRet = new System.Windows.Forms.Label();
            this.labelRadius = new System.Windows.Forms.Label();
            this.textBoxCountOSI = new System.Windows.Forms.TextBox();
            this.labelNorm = new System.Windows.Forms.Label();
            this.textBoxPeople = new System.Windows.Forms.TextBox();
            this.labelPeople = new System.Windows.Forms.Label();
            this.labelStateOfNormCity = new System.Windows.Forms.Label();
            this.groupBoxWeights = new System.Windows.Forms.GroupBox();
            this.groupBoxRadius = new System.Windows.Forms.GroupBox();
            this.labelRadiusLong = new System.Windows.Forms.Label();
            this.groupBoxNorm = new System.Windows.Forms.GroupBox();
            this.radioButtonShowOSI = new System.Windows.Forms.RadioButton();
            this.radioButtonHideOSI = new System.Windows.Forms.RadioButton();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.panelLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPharmacy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAuto)).BeginInit();
            this.groupBoxWeights.SuspendLayout();
            this.groupBoxRadius.SuspendLayout();
            this.groupBoxNorm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
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
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // buttonLoadPriority
            // 
            this.buttonLoadPriority.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonLoadPriority.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadPriority.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadPriority.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadPriority.Image = ((System.Drawing.Image)(resources.GetObject("buttonLoadPriority.Image")));
            this.buttonLoadPriority.Location = new System.Drawing.Point(15, 142);
            this.buttonLoadPriority.Name = "buttonLoadPriority";
            this.buttonLoadPriority.Size = new System.Drawing.Size(221, 54);
            this.buttonLoadPriority.TabIndex = 89;
            this.buttonLoadPriority.Text = "Задать веса критериев";
            this.buttonLoadPriority.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonLoadPriority, "Задать веса важности для каждого из критериев.");
            this.buttonLoadPriority.UseVisualStyleBackColor = false;
            this.buttonLoadPriority.Click += new System.EventHandler(this.LoadPriority_Click);
            // 
            // buttonInputRadius
            // 
            this.buttonInputRadius.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonInputRadius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInputRadius.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonInputRadius.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInputRadius.Image = ((System.Drawing.Image)(resources.GetObject("buttonInputRadius.Image")));
            this.buttonInputRadius.Location = new System.Drawing.Point(15, 73);
            this.buttonInputRadius.Name = "buttonInputRadius";
            this.buttonInputRadius.Size = new System.Drawing.Size(173, 56);
            this.buttonInputRadius.TabIndex = 90;
            this.buttonInputRadius.Text = "Задать радиус";
            this.buttonInputRadius.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonInputRadius, "Задать радиус буферной зоны для поиска.");
            this.buttonInputRadius.UseVisualStyleBackColor = false;
            this.buttonInputRadius.Click += new System.EventHandler(this.InputRadius_Click);
            // 
            // buttonInputNorm
            // 
            this.buttonInputNorm.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonInputNorm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInputNorm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonInputNorm.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInputNorm.Image = ((System.Drawing.Image)(resources.GetObject("buttonInputNorm.Image")));
            this.buttonInputNorm.Location = new System.Drawing.Point(47, 95);
            this.buttonInputNorm.Name = "buttonInputNorm";
            this.buttonInputNorm.Size = new System.Drawing.Size(178, 50);
            this.buttonInputNorm.TabIndex = 91;
            this.buttonInputNorm.Text = "Задать норму";
            this.buttonInputNorm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonInputNorm, "Задать количество объектов инфраструктуры на количество населения при норме.");
            this.buttonInputNorm.UseVisualStyleBackColor = false;
            this.buttonInputNorm.Click += new System.EventHandler(this.buttonInputNorm_Click);
            // 
            // buttonFindBest
            // 
            this.buttonFindBest.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonFindBest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFindBest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindBest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFindBest.Image = ((System.Drawing.Image)(resources.GetObject("buttonFindBest.Image")));
            this.buttonFindBest.Location = new System.Drawing.Point(12, 530);
            this.buttonFindBest.Name = "buttonFindBest";
            this.buttonFindBest.Size = new System.Drawing.Size(131, 62);
            this.buttonFindBest.TabIndex = 92;
            this.buttonFindBest.Text = "Найти лучшие";
            this.buttonFindBest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonFindBest, "Найти на карте оптимальные точки для открытия новых объектов.");
            this.buttonFindBest.UseVisualStyleBackColor = false;
            this.buttonFindBest.Click += new System.EventHandler(this.buttonFindBest_Click);
            // 
            // buttonSaveMap
            // 
            this.buttonSaveMap.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonSaveMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSaveMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSaveMap.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveMap.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveMap.Image")));
            this.buttonSaveMap.Location = new System.Drawing.Point(150, 601);
            this.buttonSaveMap.Name = "buttonSaveMap";
            this.buttonSaveMap.Size = new System.Drawing.Size(116, 91);
            this.buttonSaveMap.TabIndex = 93;
            this.buttonSaveMap.Text = "Сохранить карту";
            this.buttonSaveMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip.SetToolTip(this.buttonSaveMap, "Сохранить карту в формате png.");
            this.buttonSaveMap.UseVisualStyleBackColor = false;
            this.buttonSaveMap.Click += new System.EventHandler(this.buttonSaveMap_Click);
            // 
            // buttonOpenWebManual
            // 
            this.buttonOpenWebManual.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonOpenWebManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpenWebManual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenWebManual.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOpenWebManual.Image = global::Optimum.Properties.Resources.iconPDF;
            this.buttonOpenWebManual.Location = new System.Drawing.Point(27, 601);
            this.buttonOpenWebManual.Name = "buttonOpenWebManual";
            this.buttonOpenWebManual.Size = new System.Drawing.Size(116, 91);
            this.buttonOpenWebManual.TabIndex = 94;
            this.buttonOpenWebManual.Text = "Руководство пользователя";
            this.buttonOpenWebManual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip.SetToolTip(this.buttonOpenWebManual, "Ознакомиться с руководством пользователя.");
            this.buttonOpenWebManual.UseVisualStyleBackColor = false;
            this.buttonOpenWebManual.Click += new System.EventHandler(this.buttonOpenWebManual_Click);
            // 
            // buttonHideBest
            // 
            this.buttonHideBest.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonHideBest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHideBest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHideBest.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHideBest.Image = ((System.Drawing.Image)(resources.GetObject("buttonHideBest.Image")));
            this.buttonHideBest.Location = new System.Drawing.Point(146, 530);
            this.buttonHideBest.Name = "buttonHideBest";
            this.buttonHideBest.Size = new System.Drawing.Size(130, 62);
            this.buttonHideBest.TabIndex = 101;
            this.buttonHideBest.Text = "Скрыть лучшие";
            this.buttonHideBest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonHideBest, "Найти на карте оптимальные точки для открытия новых объектов.");
            this.buttonHideBest.UseVisualStyleBackColor = false;
            this.buttonHideBest.Click += new System.EventHandler(this.buttonHideBest_Click);
            // 
            // panelLegend
            // 
            this.panelLegend.Controls.Add(this.labelPharmacy);
            this.panelLegend.Controls.Add(this.pictureBoxPharmacy);
            this.panelLegend.Controls.Add(this.label7);
            this.panelLegend.Controls.Add(this.pictureBoxAuto);
            this.panelLegend.Controls.Add(this.labelLegendForMap);
            this.panelLegend.Location = new System.Drawing.Point(1164, 601);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(185, 91);
            this.panelLegend.TabIndex = 75;
            // 
            // labelPharmacy
            // 
            this.labelPharmacy.AutoSize = true;
            this.labelPharmacy.BackColor = System.Drawing.Color.PeachPuff;
            this.labelPharmacy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPharmacy.Location = new System.Drawing.Point(35, 30);
            this.labelPharmacy.Name = "labelPharmacy";
            this.labelPharmacy.Size = new System.Drawing.Size(48, 17);
            this.labelPharmacy.TabIndex = 108;
            this.labelPharmacy.Text = "Аптеки";
            // 
            // pictureBoxPharmacy
            // 
            this.pictureBoxPharmacy.Location = new System.Drawing.Point(5, 25);
            this.pictureBoxPharmacy.Name = "pictureBoxPharmacy";
            this.pictureBoxPharmacy.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxPharmacy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPharmacy.TabIndex = 109;
            this.pictureBoxPharmacy.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.PeachPuff;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(35, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 17);
            this.label7.TabIndex = 107;
            this.label7.Text = "Автопоиск";
            // 
            // pictureBoxAuto
            // 
            this.pictureBoxAuto.Location = new System.Drawing.Point(5, 55);
            this.pictureBoxAuto.Name = "pictureBoxAuto";
            this.pictureBoxAuto.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxAuto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAuto.TabIndex = 106;
            this.pictureBoxAuto.TabStop = false;
            // 
            // labelLegendForMap
            // 
            this.labelLegendForMap.AutoSize = true;
            this.labelLegendForMap.BackColor = System.Drawing.Color.PeachPuff;
            this.labelLegendForMap.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLegendForMap.Location = new System.Drawing.Point(3, 5);
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
            this.gmapNorm.Location = new System.Drawing.Point(291, 37);
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
            this.gmapNorm.Size = new System.Drawing.Size(1058, 655);
            this.gmapNorm.TabIndex = 64;
            this.gmapNorm.Zoom = 0D;
            this.gmapNorm.Load += new System.EventHandler(this.gmap_Load);
            // 
            // textBoxWeightPharmacy
            // 
            this.textBoxWeightPharmacy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightPharmacy.Location = new System.Drawing.Point(173, 28);
            this.textBoxWeightPharmacy.Name = "textBoxWeightPharmacy";
            this.textBoxWeightPharmacy.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightPharmacy.TabIndex = 78;
            // 
            // labelPharma
            // 
            this.labelPharma.AutoSize = true;
            this.labelPharma.BackColor = System.Drawing.Color.PeachPuff;
            this.labelPharma.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPharma.Location = new System.Drawing.Point(15, 30);
            this.labelPharma.Name = "labelPharma";
            this.labelPharma.Size = new System.Drawing.Size(119, 17);
            this.labelPharma.TabIndex = 77;
            this.labelPharma.Text = "Критерий \"Аптеки\"";
            // 
            // textBoxWeightResidents
            // 
            this.textBoxWeightResidents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightResidents.Location = new System.Drawing.Point(173, 68);
            this.textBoxWeightResidents.Name = "textBoxWeightResidents";
            this.textBoxWeightResidents.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightResidents.TabIndex = 80;
            // 
            // labelRes
            // 
            this.labelRes.AutoSize = true;
            this.labelRes.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRes.Location = new System.Drawing.Point(15, 70);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(123, 17);
            this.labelRes.TabIndex = 79;
            this.labelRes.Text = "Критерий \"Жители\"";
            // 
            // textBoxWeightRetired
            // 
            this.textBoxWeightRetired.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightRetired.Location = new System.Drawing.Point(173, 108);
            this.textBoxWeightRetired.Name = "textBoxWeightRetired";
            this.textBoxWeightRetired.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightRetired.TabIndex = 82;
            // 
            // labelRet
            // 
            this.labelRet.AutoSize = true;
            this.labelRet.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRet.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRet.Location = new System.Drawing.Point(15, 110);
            this.labelRet.Name = "labelRet";
            this.labelRet.Size = new System.Drawing.Size(154, 17);
            this.labelRet.TabIndex = 81;
            this.labelRet.Text = "Критерий \"Пенсионеры\"";
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRadius.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadius.Location = new System.Drawing.Point(15, 34);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(48, 17);
            this.labelRadius.TabIndex = 83;
            this.labelRadius.Text = "Радиус";
            // 
            // textBoxCountOSI
            // 
            this.textBoxCountOSI.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCountOSI.Location = new System.Drawing.Point(175, 24);
            this.textBoxCountOSI.Name = "textBoxCountOSI";
            this.textBoxCountOSI.Size = new System.Drawing.Size(79, 29);
            this.textBoxCountOSI.TabIndex = 86;
            // 
            // labelNorm
            // 
            this.labelNorm.AutoSize = true;
            this.labelNorm.BackColor = System.Drawing.Color.PeachPuff;
            this.labelNorm.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNorm.Location = new System.Drawing.Point(15, 30);
            this.labelNorm.Name = "labelNorm";
            this.labelNorm.Size = new System.Drawing.Size(110, 17);
            this.labelNorm.TabIndex = 85;
            this.labelNorm.Text = "Норма объектов";
            // 
            // textBoxPeople
            // 
            this.textBoxPeople.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPeople.Location = new System.Drawing.Point(175, 59);
            this.textBoxPeople.Name = "textBoxPeople";
            this.textBoxPeople.Size = new System.Drawing.Size(79, 29);
            this.textBoxPeople.TabIndex = 88;
            // 
            // labelPeople
            // 
            this.labelPeople.AutoSize = true;
            this.labelPeople.BackColor = System.Drawing.Color.PeachPuff;
            this.labelPeople.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPeople.Location = new System.Drawing.Point(15, 65);
            this.labelPeople.Name = "labelPeople";
            this.labelPeople.Size = new System.Drawing.Size(124, 17);
            this.labelPeople.TabIndex = 87;
            this.labelPeople.Text = "На душу населения";
            // 
            // labelStateOfNormCity
            // 
            this.labelStateOfNormCity.AutoSize = true;
            this.labelStateOfNormCity.BackColor = System.Drawing.Color.PeachPuff;
            this.labelStateOfNormCity.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStateOfNormCity.Location = new System.Drawing.Point(288, 9);
            this.labelStateOfNormCity.Name = "labelStateOfNormCity";
            this.labelStateOfNormCity.Size = new System.Drawing.Size(334, 21);
            this.labelStateOfNormCity.TabIndex = 95;
            this.labelStateOfNormCity.Text = "Состояние нормы ОСИ на душу населения";
            // 
            // groupBoxWeights
            // 
            this.groupBoxWeights.Controls.Add(this.textBoxWeightRetired);
            this.groupBoxWeights.Controls.Add(this.labelPharma);
            this.groupBoxWeights.Controls.Add(this.textBoxWeightPharmacy);
            this.groupBoxWeights.Controls.Add(this.labelRes);
            this.groupBoxWeights.Controls.Add(this.textBoxWeightResidents);
            this.groupBoxWeights.Controls.Add(this.labelRet);
            this.groupBoxWeights.Controls.Add(this.buttonLoadPriority);
            this.groupBoxWeights.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxWeights.Location = new System.Drawing.Point(12, 9);
            this.groupBoxWeights.Name = "groupBoxWeights";
            this.groupBoxWeights.Size = new System.Drawing.Size(264, 207);
            this.groupBoxWeights.TabIndex = 96;
            this.groupBoxWeights.TabStop = false;
            this.groupBoxWeights.Text = "Установка весовых коэффицинетов";
            // 
            // groupBoxRadius
            // 
            this.groupBoxRadius.Controls.Add(this.labelRadiusLong);
            this.groupBoxRadius.Controls.Add(this.buttonInputRadius);
            this.groupBoxRadius.Controls.Add(this.labelRadius);
            this.groupBoxRadius.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRadius.Location = new System.Drawing.Point(12, 222);
            this.groupBoxRadius.Name = "groupBoxRadius";
            this.groupBoxRadius.Size = new System.Drawing.Size(264, 141);
            this.groupBoxRadius.TabIndex = 97;
            this.groupBoxRadius.TabStop = false;
            this.groupBoxRadius.Text = "Установка радиуса";
            // 
            // labelRadiusLong
            // 
            this.labelRadiusLong.AutoSize = true;
            this.labelRadiusLong.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRadiusLong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadiusLong.Location = new System.Drawing.Point(188, 73);
            this.labelRadiusLong.Name = "labelRadiusLong";
            this.labelRadiusLong.Size = new System.Drawing.Size(55, 21);
            this.labelRadiusLong.TabIndex = 91;
            this.labelRadiusLong.Text = "300 м.";
            // 
            // groupBoxNorm
            // 
            this.groupBoxNorm.Controls.Add(this.labelPeople);
            this.groupBoxNorm.Controls.Add(this.labelNorm);
            this.groupBoxNorm.Controls.Add(this.textBoxCountOSI);
            this.groupBoxNorm.Controls.Add(this.textBoxPeople);
            this.groupBoxNorm.Controls.Add(this.buttonInputNorm);
            this.groupBoxNorm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxNorm.Location = new System.Drawing.Point(12, 369);
            this.groupBoxNorm.Name = "groupBoxNorm";
            this.groupBoxNorm.Size = new System.Drawing.Size(264, 155);
            this.groupBoxNorm.TabIndex = 98;
            this.groupBoxNorm.TabStop = false;
            this.groupBoxNorm.Text = "Установка нормы на душу населения";
            // 
            // radioButtonShowOSI
            // 
            this.radioButtonShowOSI.AutoSize = true;
            this.radioButtonShowOSI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonShowOSI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonShowOSI.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonShowOSI.Location = new System.Drawing.Point(291, 37);
            this.radioButtonShowOSI.Name = "radioButtonShowOSI";
            this.radioButtonShowOSI.Size = new System.Drawing.Size(138, 24);
            this.radioButtonShowOSI.TabIndex = 99;
            this.radioButtonShowOSI.TabStop = true;
            this.radioButtonShowOSI.Text = "Отобразить ОСИ";
            this.radioButtonShowOSI.UseVisualStyleBackColor = false;
            this.radioButtonShowOSI.Click += new System.EventHandler(this.radioButtonShowOSI_Click);
            // 
            // radioButtonHideOSI
            // 
            this.radioButtonHideOSI.AutoSize = true;
            this.radioButtonHideOSI.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonHideOSI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioButtonHideOSI.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonHideOSI.Location = new System.Drawing.Point(291, 61);
            this.radioButtonHideOSI.Name = "radioButtonHideOSI";
            this.radioButtonHideOSI.Size = new System.Drawing.Size(109, 24);
            this.radioButtonHideOSI.TabIndex = 100;
            this.radioButtonHideOSI.TabStop = true;
            this.radioButtonHideOSI.Text = "Скрыть ОСИ";
            this.radioButtonHideOSI.UseVisualStyleBackColor = false;
            this.radioButtonHideOSI.Click += new System.EventHandler(this.radioButtonHideOSI_Click);
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.BackColor = System.Drawing.Color.PeachPuff;
            this.trackBarRadius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarRadius.Location = new System.Drawing.Point(81, 246);
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(185, 45);
            this.trackBarRadius.TabIndex = 91;
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarTransparent_Scroll);
            // 
            // SearchNormPerCapita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(1370, 712);
            this.Controls.Add(this.trackBarRadius);
            this.Controls.Add(this.buttonHideBest);
            this.Controls.Add(this.radioButtonHideOSI);
            this.Controls.Add(this.radioButtonShowOSI);
            this.Controls.Add(this.groupBoxNorm);
            this.Controls.Add(this.groupBoxRadius);
            this.Controls.Add(this.groupBoxWeights);
            this.Controls.Add(this.labelStateOfNormCity);
            this.Controls.Add(this.buttonOpenWebManual);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.buttonFindBest);
            this.Controls.Add(this.buttonSaveMap);
            this.Controls.Add(this.gmapNorm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchNormPerCapita";
            this.Text = "Поиск нормы на душу населения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchNormPerCapita_FormClosing);
            this.Load += new System.EventHandler(this.SearchNormPerCapita_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchNormPerCapita_KeyDown);
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPharmacy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAuto)).EndInit();
            this.groupBoxWeights.ResumeLayout(false);
            this.groupBoxWeights.PerformLayout();
            this.groupBoxRadius.ResumeLayout(false);
            this.groupBoxRadius.PerformLayout();
            this.groupBoxNorm.ResumeLayout(false);
            this.groupBoxNorm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
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
        private System.Windows.Forms.TextBox textBoxWeightPharmacy;
        private System.Windows.Forms.Label labelPharma;
        private System.Windows.Forms.TextBox textBoxWeightResidents;
        private System.Windows.Forms.Label labelRes;
        private System.Windows.Forms.TextBox textBoxWeightRetired;
        private System.Windows.Forms.Label labelRet;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.TextBox textBoxCountOSI;
        private System.Windows.Forms.Label labelNorm;
        private System.Windows.Forms.TextBox textBoxPeople;
        private System.Windows.Forms.Label labelPeople;
        private System.Windows.Forms.Button buttonLoadPriority;
        private System.Windows.Forms.Button buttonInputRadius;
        private System.Windows.Forms.Button buttonInputNorm;
        private System.Windows.Forms.Button buttonFindBest;
        private System.Windows.Forms.Button buttonSaveMap;
        private System.Windows.Forms.Button buttonOpenWebManual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxAuto;
        private System.Windows.Forms.Label labelStateOfNormCity;
        private System.Windows.Forms.GroupBox groupBoxWeights;
        private System.Windows.Forms.GroupBox groupBoxRadius;
        private System.Windows.Forms.GroupBox groupBoxNorm;
        private System.Windows.Forms.RadioButton radioButtonShowOSI;
        private System.Windows.Forms.RadioButton radioButtonHideOSI;
        private System.Windows.Forms.Button buttonHideBest;
        private System.Windows.Forms.TrackBar trackBarRadius;
        private System.Windows.Forms.Label labelRadiusLong;
        private System.Windows.Forms.Label labelPharmacy;
        private System.Windows.Forms.PictureBox pictureBoxPharmacy;
    }
}