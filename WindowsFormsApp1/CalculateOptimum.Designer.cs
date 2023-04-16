namespace Optimum
{
    partial class CalculateOptimum
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculateOptimum));
            this.labelInfo = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelResidents = new System.Windows.Forms.Label();
            this.labelRetired = new System.Windows.Forms.Label();
            this.labelPharmacy = new System.Windows.Forms.Label();
            this.textBoxWeightResidents = new System.Windows.Forms.TextBox();
            this.textBoxWeightRetired = new System.Windows.Forms.TextBox();
            this.textBoxWeightPharmacy = new System.Windows.Forms.TextBox();
            this.labelOptimum = new System.Windows.Forms.Label();
            this.label_X = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.LoadPriorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SavePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxProrities = new System.Windows.Forms.GroupBox();
            this.groupBoxCoordinatesOptimum = new System.Windows.Forms.GroupBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.groupBoxProrities.SuspendLayout();
            this.groupBoxCoordinatesOptimum.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(12, 89);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(799, 46);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "Для вычисления оптимальной точки вам необходимо задать приоритет каждого критерия" +
    ".\r\nЗначения критериев находятся на интервале [0,1] и сумма всех весов должна быт" +
    "ь равна единице.";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView.BackgroundColor = System.Drawing.Color.PeachPuff;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightCoral;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 40;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(12, 307);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(835, 370);
            this.dataGridView.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "    X";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 59;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "    Y";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 59;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "     Радиус";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 104;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "     Аптеки";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 105;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "      Жители";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 113;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "     Пенсионеры";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 144;
            // 
            // labelResidents
            // 
            this.labelResidents.AutoSize = true;
            this.labelResidents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelResidents.Location = new System.Drawing.Point(15, 65);
            this.labelResidents.Name = "labelResidents";
            this.labelResidents.Size = new System.Drawing.Size(150, 21);
            this.labelResidents.TabIndex = 2;
            this.labelResidents.Text = "Критерий \"Жители\"";
            this.toolTip.SetToolTip(this.labelResidents, "Задайте число в интервале [0,1], которое характеризует важность критерия \"Жители\"" +
        ".\r\nЧем больше будет число, тем больше вы хотите охватить жителей.");
            // 
            // labelRetired
            // 
            this.labelRetired.AutoSize = true;
            this.labelRetired.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRetired.Location = new System.Drawing.Point(15, 105);
            this.labelRetired.Name = "labelRetired";
            this.labelRetired.Size = new System.Drawing.Size(185, 21);
            this.labelRetired.TabIndex = 4;
            this.labelRetired.Text = "Критерий \"Пенсионеры\"";
            this.toolTip.SetToolTip(this.labelRetired, "Задайте число в интервале [0,1], которое характеризует важность критерия \"Жители\"" +
        ".\r\nЧем больше будет число, тем больше вы хотите охватить пенсионеров.\r\n");
            // 
            // labelPharmacy
            // 
            this.labelPharmacy.AutoSize = true;
            this.labelPharmacy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPharmacy.Location = new System.Drawing.Point(15, 25);
            this.labelPharmacy.Name = "labelPharmacy";
            this.labelPharmacy.Size = new System.Drawing.Size(146, 21);
            this.labelPharmacy.TabIndex = 0;
            this.labelPharmacy.Text = "Критерий \"Аптеки\"";
            this.toolTip.SetToolTip(this.labelPharmacy, "Задайте число в интервале [0,1], которое характеризует важность критерия \"Аптеки\"" +
        ".\r\nЧем больше будет число, тем больше вы опасаетесь конкуренции.");
            // 
            // textBoxWeightResidents
            // 
            this.textBoxWeightResidents.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightResidents.Location = new System.Drawing.Point(224, 65);
            this.textBoxWeightResidents.Name = "textBoxWeightResidents";
            this.textBoxWeightResidents.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightResidents.TabIndex = 3;
            // 
            // textBoxWeightRetired
            // 
            this.textBoxWeightRetired.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightRetired.Location = new System.Drawing.Point(224, 105);
            this.textBoxWeightRetired.Name = "textBoxWeightRetired";
            this.textBoxWeightRetired.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightRetired.TabIndex = 5;
            // 
            // textBoxWeightPharmacy
            // 
            this.textBoxWeightPharmacy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxWeightPharmacy.Location = new System.Drawing.Point(224, 25);
            this.textBoxWeightPharmacy.Name = "textBoxWeightPharmacy";
            this.textBoxWeightPharmacy.Size = new System.Drawing.Size(63, 29);
            this.textBoxWeightPharmacy.TabIndex = 1;
            // 
            // labelOptimum
            // 
            this.labelOptimum.AutoSize = true;
            this.labelOptimum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOptimum.Location = new System.Drawing.Point(10, 25);
            this.labelOptimum.Name = "labelOptimum";
            this.labelOptimum.Size = new System.Drawing.Size(155, 21);
            this.labelOptimum.TabIndex = 0;
            this.labelOptimum.Text = "Оптимальная точка:";
            // 
            // label_X
            // 
            this.label_X.AutoSize = true;
            this.label_X.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_X.Location = new System.Drawing.Point(10, 55);
            this.label_X.Name = "label_X";
            this.label_X.Size = new System.Drawing.Size(20, 21);
            this.label_X.TabIndex = 1;
            this.label_X.Text = "x:";
            // 
            // label_Y
            // 
            this.label_Y.AutoSize = true;
            this.label_Y.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Y.Location = new System.Drawing.Point(10, 85);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(21, 21);
            this.label_Y.TabIndex = 3;
            this.label_Y.Text = "y:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelY.Location = new System.Drawing.Point(35, 85);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(18, 21);
            this.labelY.TabIndex = 4;
            this.labelY.Text = "  ";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX.Location = new System.Drawing.Point(35, 55);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(18, 21);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "  ";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Bisque;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadPriorityToolStripMenuItem,
            this.SearchToolStripMenuItem,
            this.SavePointToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(861, 70);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // LoadPriorityToolStripMenuItem
            // 
            this.LoadPriorityToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadPriorityToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconPriority;
            this.LoadPriorityToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.LoadPriorityToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LoadPriorityToolStripMenuItem.Name = "LoadPriorityToolStripMenuItem";
            this.LoadPriorityToolStripMenuItem.Size = new System.Drawing.Size(122, 66);
            this.LoadPriorityToolStripMenuItem.Text = "Подтвердить приоритеты";
            this.LoadPriorityToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.LoadPriorityToolStripMenuItem.Click += new System.EventHandler(this.LoadPriorityToolStripMenuItem_Click);
            // 
            // SearchToolStripMenuItem
            // 
            this.SearchToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconSearch;
            this.SearchToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SearchToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem";
            this.SearchToolStripMenuItem.Size = new System.Drawing.Size(79, 66);
            this.SearchToolStripMenuItem.Text = "Найти оптимум";
            this.SearchToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SearchToolStripMenuItem.Click += new System.EventHandler(this.SearchToolStripMenuItem_Click);
            // 
            // SavePointToolStripMenuItem
            // 
            this.SavePointToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SavePointToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconSavePoint;
            this.SavePointToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SavePointToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SavePointToolStripMenuItem.Name = "SavePointToolStripMenuItem";
            this.SavePointToolStripMenuItem.Size = new System.Drawing.Size(149, 66);
            this.SavePointToolStripMenuItem.Text = "Сохранить оптимум и вернуться";
            this.SavePointToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SavePointToolStripMenuItem.Click += new System.EventHandler(this.SavePointToolStripMenuItem_Click);
            // 
            // groupBoxProrities
            // 
            this.groupBoxProrities.Controls.Add(this.textBoxWeightPharmacy);
            this.groupBoxProrities.Controls.Add(this.labelResidents);
            this.groupBoxProrities.Controls.Add(this.labelRetired);
            this.groupBoxProrities.Controls.Add(this.textBoxWeightResidents);
            this.groupBoxProrities.Controls.Add(this.labelPharmacy);
            this.groupBoxProrities.Controls.Add(this.textBoxWeightRetired);
            this.groupBoxProrities.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxProrities.Location = new System.Drawing.Point(12, 150);
            this.groupBoxProrities.Name = "groupBoxProrities";
            this.groupBoxProrities.Size = new System.Drawing.Size(304, 142);
            this.groupBoxProrities.TabIndex = 0;
            this.groupBoxProrities.TabStop = false;
            this.groupBoxProrities.Text = "Установка приоритетов";
            // 
            // groupBoxCoordinatesOptimum
            // 
            this.groupBoxCoordinatesOptimum.Controls.Add(this.labelOptimum);
            this.groupBoxCoordinatesOptimum.Controls.Add(this.label_X);
            this.groupBoxCoordinatesOptimum.Controls.Add(this.labelY);
            this.groupBoxCoordinatesOptimum.Controls.Add(this.label_Y);
            this.groupBoxCoordinatesOptimum.Controls.Add(this.labelX);
            this.groupBoxCoordinatesOptimum.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxCoordinatesOptimum.Location = new System.Drawing.Point(370, 150);
            this.groupBoxCoordinatesOptimum.Name = "groupBoxCoordinatesOptimum";
            this.groupBoxCoordinatesOptimum.Size = new System.Drawing.Size(262, 142);
            this.groupBoxCoordinatesOptimum.TabIndex = 2;
            this.groupBoxCoordinatesOptimum.TabStop = false;
            this.groupBoxCoordinatesOptimum.Text = "Результат поиска";
            this.toolTip.SetToolTip(this.groupBoxCoordinatesOptimum, "Координаты лучшей точки. \r\nПеред закрытием формы сохраните её и на главном окне в" +
        "ы сможете оставить на карте только оптимум.");
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // CalculateOptimum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(861, 687);
            this.Controls.Add(this.groupBoxCoordinatesOptimum);
            this.Controls.Add(this.groupBoxProrities);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(877, 726);
            this.MinimumSize = new System.Drawing.Size(877, 726);
            this.Name = "CalculateOptimum";
            this.Text = "Поиск оптимального размещения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CalculateOptimum_FormClosing);
            this.Load += new System.EventHandler(this.CalculateOptimum_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalculateOptimum_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBoxProrities.ResumeLayout(false);
            this.groupBoxProrities.PerformLayout();
            this.groupBoxCoordinatesOptimum.ResumeLayout(false);
            this.groupBoxCoordinatesOptimum.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelResidents;
        private System.Windows.Forms.Label labelRetired;
        private System.Windows.Forms.Label labelPharmacy;
        private System.Windows.Forms.TextBox textBoxWeightResidents;
        private System.Windows.Forms.TextBox textBoxWeightRetired;
        private System.Windows.Forms.TextBox textBoxWeightPharmacy;
        private System.Windows.Forms.Label labelOptimum;
        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem LoadPriorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SavePointToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxProrities;
        private System.Windows.Forms.GroupBox groupBoxCoordinatesOptimum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ToolTip toolTip;
    }
}