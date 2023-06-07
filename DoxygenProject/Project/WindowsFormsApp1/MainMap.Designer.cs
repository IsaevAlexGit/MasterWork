namespace Optimum
{
    partial class MainMap
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMap));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.SettingsToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CenteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchOptimumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindAutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowAutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HideAutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeaveOptimumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveOptimumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptimumInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GoogleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.YandexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TwoGISToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenStreetMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NormPeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.directorySearcher2 = new System.DirectoryServices.DirectorySearcher();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.labelAutoSearch = new System.Windows.Forms.Label();
            this.pictureBoxAuto = new System.Windows.Forms.PictureBox();
            this.labelUserMarker = new System.Windows.Forms.Label();
            this.labelFacility = new System.Windows.Forms.Label();
            this.labelLegendForMap = new System.Windows.Forms.Label();
            this.pictureBoxFacility = new System.Windows.Forms.PictureBox();
            this.pictureBoxMarker = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerCheckPanel = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.panelLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAuto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMarker)).BeginInit();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(33, 85);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1313, 576);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.Load += new System.EventHandler(this.gMapControl_Load);
            this.gmap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseDoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.PaleGreen;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenu,
            this.CenteringToolStripMenuItem,
            this.InputRadiusToolStripMenuItem,
            this.SearchOptimumToolStripMenuItem,
            this.AutoSearchToolStripMenuItem,
            this.LeaveOptimumToolStripMenuItem,
            this.SaveOptimumToolStripMenuItem,
            this.OptimumInBrowserToolStripMenuItem,
            this.SaveMapToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.NormPeopleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1358, 70);
            this.menuStrip.TabIndex = 60;
            this.menuStrip.Text = "menuStrip1";
            // 
            // SettingsToolStripMenu
            // 
            this.SettingsToolStripMenu.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SettingsToolStripMenu.Image = ((System.Drawing.Image)(resources.GetObject("SettingsToolStripMenu.Image")));
            this.SettingsToolStripMenu.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsToolStripMenu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SettingsToolStripMenu.Name = "SettingsToolStripMenu";
            this.SettingsToolStripMenu.Size = new System.Drawing.Size(62, 66);
            this.SettingsToolStripMenu.Text = "Настройки";
            this.SettingsToolStripMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SettingsToolStripMenu.Click += new System.EventHandler(this.SettingsToolStripMenu_Click);
            // 
            // CenteringToolStripMenuItem
            // 
            this.CenteringToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CenteringToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CenteringToolStripMenuItem.Image")));
            this.CenteringToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CenteringToolStripMenuItem.Name = "CenteringToolStripMenuItem";
            this.CenteringToolStripMenuItem.Size = new System.Drawing.Size(109, 66);
            this.CenteringToolStripMenuItem.Text = "Центрирование карты";
            this.CenteringToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CenteringToolStripMenuItem.Click += new System.EventHandler(this.CenteringToolStripMenuItem_Click);
            // 
            // InputRadiusToolStripMenuItem
            // 
            this.InputRadiusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InputRadiusToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconLoadRadius;
            this.InputRadiusToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.InputRadiusToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InputRadiusToolStripMenuItem.Name = "InputRadiusToolStripMenuItem";
            this.InputRadiusToolStripMenuItem.Size = new System.Drawing.Size(76, 66);
            this.InputRadiusToolStripMenuItem.Text = "Задать радиус";
            this.InputRadiusToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.InputRadiusToolStripMenuItem.Click += new System.EventHandler(this.InputRadiusToolStripMenuItem_Click);
            // 
            // SearchOptimumToolStripMenuItem
            // 
            this.SearchOptimumToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchOptimumToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconSearchOptimal;
            this.SearchOptimumToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SearchOptimumToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SearchOptimumToolStripMenuItem.Name = "SearchOptimumToolStripMenuItem";
            this.SearchOptimumToolStripMenuItem.Size = new System.Drawing.Size(79, 66);
            this.SearchOptimumToolStripMenuItem.Text = "Найти оптимум";
            this.SearchOptimumToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SearchOptimumToolStripMenuItem.Click += new System.EventHandler(this.SearchOptimumToolStripMenuItem_Click);
            // 
            // AutoSearchToolStripMenuItem
            // 
            this.AutoSearchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindAutoToolStripMenuItem,
            this.ShowAutoToolStripMenuItem,
            this.HideAutoToolStripMenuItem,
            this.DeleteAutoToolStripMenuItem});
            this.AutoSearchToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AutoSearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AutoSearchToolStripMenuItem.Image")));
            this.AutoSearchToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AutoSearchToolStripMenuItem.Name = "AutoSearchToolStripMenuItem";
            this.AutoSearchToolStripMenuItem.Size = new System.Drawing.Size(62, 66);
            this.AutoSearchToolStripMenuItem.Text = "Автопоиск";
            this.AutoSearchToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FindAutoToolStripMenuItem
            // 
            this.FindAutoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("FindAutoToolStripMenuItem.Image")));
            this.FindAutoToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.FindAutoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FindAutoToolStripMenuItem.Name = "FindAutoToolStripMenuItem";
            this.FindAutoToolStripMenuItem.Size = new System.Drawing.Size(135, 38);
            this.FindAutoToolStripMenuItem.Text = "Найти";
            this.FindAutoToolStripMenuItem.Click += new System.EventHandler(this.FindAutoToolStripMenuItem_Click);
            // 
            // ShowAutoToolStripMenuItem
            // 
            this.ShowAutoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ShowAutoToolStripMenuItem.Image")));
            this.ShowAutoToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ShowAutoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowAutoToolStripMenuItem.Name = "ShowAutoToolStripMenuItem";
            this.ShowAutoToolStripMenuItem.Size = new System.Drawing.Size(135, 38);
            this.ShowAutoToolStripMenuItem.Text = "Отобразить";
            this.ShowAutoToolStripMenuItem.Click += new System.EventHandler(this.ShowAutoToolStripMenuItem_Click);
            // 
            // HideAutoToolStripMenuItem
            // 
            this.HideAutoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("HideAutoToolStripMenuItem.Image")));
            this.HideAutoToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HideAutoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.HideAutoToolStripMenuItem.Name = "HideAutoToolStripMenuItem";
            this.HideAutoToolStripMenuItem.Size = new System.Drawing.Size(135, 38);
            this.HideAutoToolStripMenuItem.Text = "Скрыть";
            this.HideAutoToolStripMenuItem.Click += new System.EventHandler(this.HideAutoToolStripMenuItem_Click);
            // 
            // DeleteAutoToolStripMenuItem
            // 
            this.DeleteAutoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAutoToolStripMenuItem.Image")));
            this.DeleteAutoToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DeleteAutoToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteAutoToolStripMenuItem.Name = "DeleteAutoToolStripMenuItem";
            this.DeleteAutoToolStripMenuItem.Size = new System.Drawing.Size(135, 38);
            this.DeleteAutoToolStripMenuItem.Text = "Удалить";
            this.DeleteAutoToolStripMenuItem.Click += new System.EventHandler(this.DeleteAutoToolStripMenuItem_Click);
            // 
            // LeaveOptimumToolStripMenuItem
            // 
            this.LeaveOptimumToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LeaveOptimumToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconViewOptimal;
            this.LeaveOptimumToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.LeaveOptimumToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LeaveOptimumToolStripMenuItem.Name = "LeaveOptimumToolStripMenuItem";
            this.LeaveOptimumToolStripMenuItem.Size = new System.Drawing.Size(97, 66);
            this.LeaveOptimumToolStripMenuItem.Text = "Скрыть кандидатов";
            this.LeaveOptimumToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.LeaveOptimumToolStripMenuItem.Click += new System.EventHandler(this.LeaveOptimumToolStripMenuItem_Click);
            // 
            // SaveOptimumToolStripMenuItem
            // 
            this.SaveOptimumToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveOptimumToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconSaveOptimum;
            this.SaveOptimumToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SaveOptimumToolStripMenuItem.Name = "SaveOptimumToolStripMenuItem";
            this.SaveOptimumToolStripMenuItem.Size = new System.Drawing.Size(104, 66);
            this.SaveOptimumToolStripMenuItem.Text = "Сохранить оптимумы";
            this.SaveOptimumToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SaveOptimumToolStripMenuItem.Click += new System.EventHandler(this.SaveOptimumToolStripMenuItem_Click);
            // 
            // OptimumInBrowserToolStripMenuItem
            // 
            this.OptimumInBrowserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GoogleToolStripMenuItem,
            this.YandexToolStripMenuItem,
            this.TwoGISToolStripMenuItem,
            this.OpenStreetMapToolStripMenuItem});
            this.OptimumInBrowserToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OptimumInBrowserToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OptimumInBrowserToolStripMenuItem.Image")));
            this.OptimumInBrowserToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OptimumInBrowserToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OptimumInBrowserToolStripMenuItem.Name = "OptimumInBrowserToolStripMenuItem";
            this.OptimumInBrowserToolStripMenuItem.Size = new System.Drawing.Size(101, 66);
            this.OptimumInBrowserToolStripMenuItem.Text = "Оптимум в браузере";
            this.OptimumInBrowserToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // GoogleToolStripMenuItem
            // 
            this.GoogleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("GoogleToolStripMenuItem.Image")));
            this.GoogleToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GoogleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.GoogleToolStripMenuItem.Name = "GoogleToolStripMenuItem";
            this.GoogleToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.GoogleToolStripMenuItem.Text = "Карты Google";
            this.GoogleToolStripMenuItem.Click += new System.EventHandler(this.GoogleToolStripMenuItem_Click);
            // 
            // YandexToolStripMenuItem
            // 
            this.YandexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("YandexToolStripMenuItem.Image")));
            this.YandexToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.YandexToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.YandexToolStripMenuItem.Name = "YandexToolStripMenuItem";
            this.YandexToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.YandexToolStripMenuItem.Text = "Яндекс.Карты";
            this.YandexToolStripMenuItem.Click += new System.EventHandler(this.YandexToolStripMenuItem_Click);
            // 
            // TwoGISToolStripMenuItem
            // 
            this.TwoGISToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("TwoGISToolStripMenuItem.Image")));
            this.TwoGISToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TwoGISToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TwoGISToolStripMenuItem.Name = "TwoGISToolStripMenuItem";
            this.TwoGISToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.TwoGISToolStripMenuItem.Text = "2ГИС";
            this.TwoGISToolStripMenuItem.Click += new System.EventHandler(this.TwoGISToolStripMenuItem_Click);
            // 
            // OpenStreetMapToolStripMenuItem
            // 
            this.OpenStreetMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenStreetMapToolStripMenuItem.Image")));
            this.OpenStreetMapToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OpenStreetMapToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenStreetMapToolStripMenuItem.Name = "OpenStreetMapToolStripMenuItem";
            this.OpenStreetMapToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.OpenStreetMapToolStripMenuItem.Text = "OpenStreetMap";
            this.OpenStreetMapToolStripMenuItem.Click += new System.EventHandler(this.OpenStreetMapToolStripMenuItem_Click);
            // 
            // SaveMapToolStripMenuItem
            // 
            this.SaveMapToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveMapToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconSaveMap;
            this.SaveMapToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SaveMapToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SaveMapToolStripMenuItem.Name = "SaveMapToolStripMenuItem";
            this.SaveMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMapToolStripMenuItem.Size = new System.Drawing.Size(85, 66);
            this.SaveMapToolStripMenuItem.Text = "Сохранить карту";
            this.SaveMapToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SaveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveMapToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HelpToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconPDF_v2;
            this.HelpToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HelpToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(68, 66);
            this.HelpToolStripMenuItem.Text = "Руководство";
            this.HelpToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // NormPeopleToolStripMenuItem
            // 
            this.NormPeopleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NormPeopleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NormPeopleToolStripMenuItem.Image")));
            this.NormPeopleToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NormPeopleToolStripMenuItem.Name = "NormPeopleToolStripMenuItem";
            this.NormPeopleToolStripMenuItem.Size = new System.Drawing.Size(124, 66);
            this.NormPeopleToolStripMenuItem.Text = "Норма на душу населения";
            this.NormPeopleToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.NormPeopleToolStripMenuItem.Click += new System.EventHandler(this.NormPeopleToolStripMenuItem_Click);
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
            // panelLegend
            // 
            this.panelLegend.Controls.Add(this.labelAutoSearch);
            this.panelLegend.Controls.Add(this.pictureBoxAuto);
            this.panelLegend.Controls.Add(this.labelUserMarker);
            this.panelLegend.Controls.Add(this.labelFacility);
            this.panelLegend.Controls.Add(this.labelLegendForMap);
            this.panelLegend.Controls.Add(this.pictureBoxFacility);
            this.panelLegend.Controls.Add(this.pictureBoxMarker);
            this.panelLegend.Location = new System.Drawing.Point(1170, 85);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(186, 120);
            this.panelLegend.TabIndex = 12;
            // 
            // labelAutoSearch
            // 
            this.labelAutoSearch.AutoSize = true;
            this.labelAutoSearch.BackColor = System.Drawing.Color.LightGreen;
            this.labelAutoSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutoSearch.Location = new System.Drawing.Point(34, 88);
            this.labelAutoSearch.Name = "labelAutoSearch";
            this.labelAutoSearch.Size = new System.Drawing.Size(144, 17);
            this.labelAutoSearch.TabIndex = 105;
            this.labelAutoSearch.Text = "Кандидаты автопоиска";
            // 
            // pictureBoxAuto
            // 
            this.pictureBoxAuto.Location = new System.Drawing.Point(5, 85);
            this.pictureBoxAuto.Name = "pictureBoxAuto";
            this.pictureBoxAuto.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxAuto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAuto.TabIndex = 104;
            this.pictureBoxAuto.TabStop = false;
            // 
            // labelUserMarker
            // 
            this.labelUserMarker.AutoSize = true;
            this.labelUserMarker.BackColor = System.Drawing.Color.LightGreen;
            this.labelUserMarker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelUserMarker.Location = new System.Drawing.Point(34, 32);
            this.labelUserMarker.Name = "labelUserMarker";
            this.labelUserMarker.Size = new System.Drawing.Size(101, 17);
            this.labelUserMarker.TabIndex = 1;
            this.labelUserMarker.Text = "Точка-кандидат";
            // 
            // labelFacility
            // 
            this.labelFacility.AutoSize = true;
            this.labelFacility.BackColor = System.Drawing.Color.LightGreen;
            this.labelFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFacility.Location = new System.Drawing.Point(34, 60);
            this.labelFacility.Name = "labelFacility";
            this.labelFacility.Size = new System.Drawing.Size(106, 17);
            this.labelFacility.TabIndex = 2;
            this.labelFacility.Text = "Инфраструктура";
            // 
            // labelLegendForMap
            // 
            this.labelLegendForMap.AutoSize = true;
            this.labelLegendForMap.BackColor = System.Drawing.Color.LightGreen;
            this.labelLegendForMap.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLegendForMap.Location = new System.Drawing.Point(7, 5);
            this.labelLegendForMap.Name = "labelLegendForMap";
            this.labelLegendForMap.Size = new System.Drawing.Size(180, 17);
            this.labelLegendForMap.TabIndex = 0;
            this.labelLegendForMap.Text = "УСЛОВНЫЕ ОБОЗНАЧЕНИЯ";
            // 
            // pictureBoxFacility
            // 
            this.pictureBoxFacility.Location = new System.Drawing.Point(5, 55);
            this.pictureBoxFacility.Name = "pictureBoxFacility";
            this.pictureBoxFacility.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxFacility.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFacility.TabIndex = 99;
            this.pictureBoxFacility.TabStop = false;
            // 
            // pictureBoxMarker
            // 
            this.pictureBoxMarker.Location = new System.Drawing.Point(5, 25);
            this.pictureBoxMarker.Name = "pictureBoxMarker";
            this.pictureBoxMarker.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxMarker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMarker.TabIndex = 100;
            this.pictureBoxMarker.TabStop = false;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // timerCheckPanel
            // 
            this.timerCheckPanel.Tick += new System.EventHandler(this.timerCheckPanel_Tick);
            // 
            // MainMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(1358, 712);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gmap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1374, 751);
            this.MinimumSize = new System.Drawing.Size(1364, 726);
            this.Name = "MainMap";
            this.Text = "Оптимум";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Map_FormClosing);
            this.Load += new System.EventHandler(this.Map_Load);
            this.SizeChanged += new System.EventHandler(this.MainMap_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainMap_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAuto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFacility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMarker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.DirectoryServices.DirectorySearcher directorySearcher2;
        private System.Windows.Forms.ToolStripMenuItem SearchOptimumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LeaveOptimumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InputRadiusToolStripMenuItem;
        private System.Windows.Forms.Panel panelLegendMap;
        private System.Windows.Forms.PictureBox pictureBoxFacility;
        private System.Windows.Forms.PictureBox pictureBoxMarker;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label labelLegendForMap;
        private System.Windows.Forms.Label labelUserMarker;
        private System.Windows.Forms.Label labelFacility;
        private System.Windows.Forms.ToolStripMenuItem SaveOptimumToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem OptimumInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GoogleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem YandexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TwoGISToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenStreetMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowAutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HideAutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NormPeopleToolStripMenuItem;
        private System.Windows.Forms.Label labelAutoSearch;
        private System.Windows.Forms.PictureBox pictureBoxAuto;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenu;
        private System.Windows.Forms.ComboBox comboBoxColoringIcons;
        public System.Windows.Forms.Timer timerCheckPanel;
        private System.Windows.Forms.ToolStripMenuItem CenteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindAutoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteAutoToolStripMenuItem;
    }
}

