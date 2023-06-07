namespace Optimum
{
    partial class StartSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartSettings));
            this.groupBoxLoadFiles = new System.Windows.Forms.GroupBox();
            this.buttonLoadBorderTerritory = new System.Windows.Forms.Button();
            this.buttonLoadFacilities = new System.Windows.Forms.Button();
            this.buttonLoadPolygons = new System.Windows.Forms.Button();
            this.groupBoxCriterion = new System.Windows.Forms.GroupBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.labelDirection = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboCountCriterion = new System.Windows.Forms.ComboBox();
            this.labelCountCriterion = new System.Windows.Forms.Label();
            this.labelWeight = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.labelNameCriterion = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBoxNameFacility = new System.Windows.Forms.GroupBox();
            this.labelNameFacility = new System.Windows.Forms.Label();
            this.textBoxNameFacility = new System.Windows.Forms.TextBox();
            this.groupBoxIconFacility = new System.Windows.Forms.GroupBox();
            this.labelLoadIcon = new System.Windows.Forms.Label();
            this.pictureIconFacility = new System.Windows.Forms.PictureBox();
            this.buttonLoadIconFacility = new System.Windows.Forms.Button();
            this.buttonValidateAndSave = new System.Windows.Forms.Button();
            this.labelAboutWeights = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxLoadFiles.SuspendLayout();
            this.groupBoxCriterion.SuspendLayout();
            this.groupBoxNameFacility.SuspendLayout();
            this.groupBoxIconFacility.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconFacility)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLoadFiles
            // 
            this.groupBoxLoadFiles.Controls.Add(this.buttonLoadBorderTerritory);
            this.groupBoxLoadFiles.Controls.Add(this.buttonLoadFacilities);
            this.groupBoxLoadFiles.Controls.Add(this.buttonLoadPolygons);
            this.groupBoxLoadFiles.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxLoadFiles.Location = new System.Drawing.Point(545, 222);
            this.groupBoxLoadFiles.Name = "groupBoxLoadFiles";
            this.groupBoxLoadFiles.Size = new System.Drawing.Size(246, 278);
            this.groupBoxLoadFiles.TabIndex = 33;
            this.groupBoxLoadFiles.TabStop = false;
            this.groupBoxLoadFiles.Text = "Загрузка файлов";
            // 
            // buttonLoadBorderTerritory
            // 
            this.buttonLoadBorderTerritory.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonLoadBorderTerritory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadBorderTerritory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadBorderTerritory.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadBorderTerritory.Image = ((System.Drawing.Image)(resources.GetObject("buttonLoadBorderTerritory.Image")));
            this.buttonLoadBorderTerritory.Location = new System.Drawing.Point(13, 35);
            this.buttonLoadBorderTerritory.Name = "buttonLoadBorderTerritory";
            this.buttonLoadBorderTerritory.Size = new System.Drawing.Size(217, 65);
            this.buttonLoadBorderTerritory.TabIndex = 0;
            this.buttonLoadBorderTerritory.Text = "Файл с границами зоны анализа";
            this.buttonLoadBorderTerritory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonLoadBorderTerritory, "Загрузите файл с граничными точками зоны анализа.");
            this.buttonLoadBorderTerritory.UseVisualStyleBackColor = false;
            this.buttonLoadBorderTerritory.Click += new System.EventHandler(this.buttonLoadBorderTerritory_Click);
            // 
            // buttonLoadFacilities
            // 
            this.buttonLoadFacilities.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonLoadFacilities.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadFacilities.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadFacilities.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadFacilities.Image = ((System.Drawing.Image)(resources.GetObject("buttonLoadFacilities.Image")));
            this.buttonLoadFacilities.Location = new System.Drawing.Point(13, 195);
            this.buttonLoadFacilities.Name = "buttonLoadFacilities";
            this.buttonLoadFacilities.Size = new System.Drawing.Size(217, 65);
            this.buttonLoadFacilities.TabIndex = 2;
            this.buttonLoadFacilities.Text = "Файл с объектами инфраструктуры";
            this.buttonLoadFacilities.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonLoadFacilities, "Загрузите файл с объектами инфраструктуры, которые будут отображаться на карте.");
            this.buttonLoadFacilities.UseVisualStyleBackColor = false;
            this.buttonLoadFacilities.Click += new System.EventHandler(this.buttonLoadFacilities_Click);
            // 
            // buttonLoadPolygons
            // 
            this.buttonLoadPolygons.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonLoadPolygons.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadPolygons.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadPolygons.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadPolygons.Image = ((System.Drawing.Image)(resources.GetObject("buttonLoadPolygons.Image")));
            this.buttonLoadPolygons.Location = new System.Drawing.Point(13, 115);
            this.buttonLoadPolygons.Name = "buttonLoadPolygons";
            this.buttonLoadPolygons.Size = new System.Drawing.Size(217, 65);
            this.buttonLoadPolygons.TabIndex = 1;
            this.buttonLoadPolygons.Text = "Файл с полигонами зоны анализа";
            this.buttonLoadPolygons.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.buttonLoadPolygons, "Загрузите файл с полигонами и частными критериями оптимальности.");
            this.buttonLoadPolygons.UseVisualStyleBackColor = false;
            this.buttonLoadPolygons.Click += new System.EventHandler(this.buttonLoadPolygons_Click);
            // 
            // groupBoxCriterion
            // 
            this.groupBoxCriterion.Controls.Add(this.comboBox8);
            this.groupBoxCriterion.Controls.Add(this.comboBox7);
            this.groupBoxCriterion.Controls.Add(this.comboBox6);
            this.groupBoxCriterion.Controls.Add(this.comboBox5);
            this.groupBoxCriterion.Controls.Add(this.comboBox4);
            this.groupBoxCriterion.Controls.Add(this.comboBox3);
            this.groupBoxCriterion.Controls.Add(this.labelDirection);
            this.groupBoxCriterion.Controls.Add(this.textBox13);
            this.groupBoxCriterion.Controls.Add(this.textBox14);
            this.groupBoxCriterion.Controls.Add(this.textBox7);
            this.groupBoxCriterion.Controls.Add(this.textBox8);
            this.groupBoxCriterion.Controls.Add(this.textBox9);
            this.groupBoxCriterion.Controls.Add(this.textBox10);
            this.groupBoxCriterion.Controls.Add(this.textBox11);
            this.groupBoxCriterion.Controls.Add(this.textBox12);
            this.groupBoxCriterion.Controls.Add(this.comboBox2);
            this.groupBoxCriterion.Controls.Add(this.comboCountCriterion);
            this.groupBoxCriterion.Controls.Add(this.labelCountCriterion);
            this.groupBoxCriterion.Controls.Add(this.labelWeight);
            this.groupBoxCriterion.Controls.Add(this.textBox1);
            this.groupBoxCriterion.Controls.Add(this.textBox6);
            this.groupBoxCriterion.Controls.Add(this.labelNameCriterion);
            this.groupBoxCriterion.Controls.Add(this.textBox5);
            this.groupBoxCriterion.Controls.Add(this.textBox2);
            this.groupBoxCriterion.Controls.Add(this.textBox4);
            this.groupBoxCriterion.Controls.Add(this.textBox3);
            this.groupBoxCriterion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxCriterion.Location = new System.Drawing.Point(21, 222);
            this.groupBoxCriterion.Name = "groupBoxCriterion";
            this.groupBoxCriterion.Size = new System.Drawing.Size(504, 365);
            this.groupBoxCriterion.TabIndex = 0;
            this.groupBoxCriterion.TabStop = false;
            this.groupBoxCriterion.Text = "Критерии оптимальности и весовые коэффициенты";
            // 
            // comboBox8
            // 
            this.comboBox8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox8.Location = new System.Drawing.Point(299, 319);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(82, 29);
            this.comboBox8.TabIndex = 20;
            // 
            // comboBox7
            // 
            this.comboBox7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox7.Location = new System.Drawing.Point(299, 284);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(82, 29);
            this.comboBox7.TabIndex = 17;
            // 
            // comboBox6
            // 
            this.comboBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox6.Location = new System.Drawing.Point(299, 249);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(82, 29);
            this.comboBox6.TabIndex = 14;
            // 
            // comboBox5
            // 
            this.comboBox5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox5.Location = new System.Drawing.Point(299, 214);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(82, 29);
            this.comboBox5.TabIndex = 11;
            // 
            // comboBox4
            // 
            this.comboBox4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox4.Location = new System.Drawing.Point(299, 176);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(82, 29);
            this.comboBox4.TabIndex = 8;
            this.comboBox4.Text = "min";
            // 
            // comboBox3
            // 
            this.comboBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox3.Location = new System.Drawing.Point(299, 141);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(82, 29);
            this.comboBox3.TabIndex = 5;
            this.comboBox3.Text = "min";
            // 
            // labelDirection
            // 
            this.labelDirection.AutoSize = true;
            this.labelDirection.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDirection.Location = new System.Drawing.Point(294, 86);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(91, 17);
            this.labelDirection.TabIndex = 107;
            this.labelDirection.Text = "Направление:";
            // 
            // textBox13
            // 
            this.textBox13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox13.Location = new System.Drawing.Point(397, 319);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(91, 29);
            this.textBox13.TabIndex = 21;
            // 
            // textBox14
            // 
            this.textBox14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox14.Location = new System.Drawing.Point(13, 319);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(274, 29);
            this.textBox14.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(13, 214);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(274, 29);
            this.textBox7.TabIndex = 10;
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(397, 284);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(91, 29);
            this.textBox8.TabIndex = 18;
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox9.Location = new System.Drawing.Point(397, 249);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(91, 29);
            this.textBox9.TabIndex = 15;
            // 
            // textBox10
            // 
            this.textBox10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(13, 249);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(274, 29);
            this.textBox10.TabIndex = 13;
            // 
            // textBox11
            // 
            this.textBox11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox11.Location = new System.Drawing.Point(397, 214);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(91, 29);
            this.textBox11.TabIndex = 12;
            // 
            // textBox12
            // 
            this.textBox12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox12.Location = new System.Drawing.Point(13, 284);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(274, 29);
            this.textBox12.TabIndex = 16;
            // 
            // comboBox2
            // 
            this.comboBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "max",
            "min"});
            this.comboBox2.Location = new System.Drawing.Point(299, 106);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(82, 29);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.Text = "max";
            // 
            // comboCountCriterion
            // 
            this.comboCountCriterion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboCountCriterion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboCountCriterion.FormattingEnabled = true;
            this.comboCountCriterion.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboCountCriterion.Location = new System.Drawing.Point(13, 44);
            this.comboCountCriterion.Name = "comboCountCriterion";
            this.comboCountCriterion.Size = new System.Drawing.Size(144, 29);
            this.comboCountCriterion.TabIndex = 1;
            this.comboCountCriterion.SelectedIndexChanged += new System.EventHandler(this.comboCountCriterion_SelectedIndexChanged);
            // 
            // labelCountCriterion
            // 
            this.labelCountCriterion.AutoSize = true;
            this.labelCountCriterion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountCriterion.Location = new System.Drawing.Point(10, 24);
            this.labelCountCriterion.Name = "labelCountCriterion";
            this.labelCountCriterion.Size = new System.Drawing.Size(147, 17);
            this.labelCountCriterion.TabIndex = 0;
            this.labelCountCriterion.Text = "Количество критериев:";
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeight.Location = new System.Drawing.Point(396, 86);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(31, 17);
            this.labelWeight.TabIndex = 25;
            this.labelWeight.Text = "Вес:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(13, 106);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(274, 29);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Критерий 1";
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox6.Location = new System.Drawing.Point(397, 176);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(91, 29);
            this.textBox6.TabIndex = 9;
            this.textBox6.Text = "0,3";
            // 
            // labelNameCriterion
            // 
            this.labelNameCriterion.AutoSize = true;
            this.labelNameCriterion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNameCriterion.Location = new System.Drawing.Point(10, 86);
            this.labelNameCriterion.Name = "labelNameCriterion";
            this.labelNameCriterion.Size = new System.Drawing.Size(127, 17);
            this.labelNameCriterion.TabIndex = 16;
            this.labelNameCriterion.Text = "Название критерия:";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox5.Location = new System.Drawing.Point(397, 141);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(91, 29);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "0,3";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(13, 141);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(274, 29);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Критерий 2";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox4.Location = new System.Drawing.Point(397, 106);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(91, 29);
            this.textBox4.TabIndex = 3;
            this.textBox4.Text = "0,4";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(13, 176);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(274, 29);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "Критерий 3";
            // 
            // groupBoxNameFacility
            // 
            this.groupBoxNameFacility.Controls.Add(this.labelNameFacility);
            this.groupBoxNameFacility.Controls.Add(this.textBoxNameFacility);
            this.groupBoxNameFacility.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxNameFacility.Location = new System.Drawing.Point(290, 12);
            this.groupBoxNameFacility.Name = "groupBoxNameFacility";
            this.groupBoxNameFacility.Size = new System.Drawing.Size(368, 112);
            this.groupBoxNameFacility.TabIndex = 32;
            this.groupBoxNameFacility.TabStop = false;
            this.groupBoxNameFacility.Text = "Название объекта";
            // 
            // labelNameFacility
            // 
            this.labelNameFacility.AutoSize = true;
            this.labelNameFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNameFacility.Location = new System.Drawing.Point(8, 25);
            this.labelNameFacility.Name = "labelNameFacility";
            this.labelNameFacility.Size = new System.Drawing.Size(311, 17);
            this.labelNameFacility.TabIndex = 0;
            this.labelNameFacility.Text = "Задать тип инфраструктуры или название бизнеса:";
            // 
            // textBoxNameFacility
            // 
            this.textBoxNameFacility.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNameFacility.Location = new System.Drawing.Point(10, 64);
            this.textBoxNameFacility.Name = "textBoxNameFacility";
            this.textBoxNameFacility.Size = new System.Drawing.Size(346, 29);
            this.textBoxNameFacility.TabIndex = 0;
            this.textBoxNameFacility.Text = "Мой бизнес";
            // 
            // groupBoxIconFacility
            // 
            this.groupBoxIconFacility.Controls.Add(this.labelLoadIcon);
            this.groupBoxIconFacility.Controls.Add(this.pictureIconFacility);
            this.groupBoxIconFacility.Controls.Add(this.buttonLoadIconFacility);
            this.groupBoxIconFacility.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxIconFacility.Location = new System.Drawing.Point(21, 12);
            this.groupBoxIconFacility.Name = "groupBoxIconFacility";
            this.groupBoxIconFacility.Size = new System.Drawing.Size(251, 112);
            this.groupBoxIconFacility.TabIndex = 0;
            this.groupBoxIconFacility.TabStop = false;
            this.groupBoxIconFacility.Text = "Значок объекта";
            // 
            // labelLoadIcon
            // 
            this.labelLoadIcon.AutoSize = true;
            this.labelLoadIcon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLoadIcon.Location = new System.Drawing.Point(8, 25);
            this.labelLoadIcon.Name = "labelLoadIcon";
            this.labelLoadIcon.Size = new System.Drawing.Size(219, 17);
            this.labelLoadIcon.TabIndex = 0;
            this.labelLoadIcon.Text = "Загрузить значок объекта на карте";
            // 
            // pictureIconFacility
            // 
            this.pictureIconFacility.Image = ((System.Drawing.Image)(resources.GetObject("pictureIconFacility.Image")));
            this.pictureIconFacility.Location = new System.Drawing.Point(11, 50);
            this.pictureIconFacility.Name = "pictureIconFacility";
            this.pictureIconFacility.Size = new System.Drawing.Size(50, 50);
            this.pictureIconFacility.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureIconFacility.TabIndex = 6;
            this.pictureIconFacility.TabStop = false;
            // 
            // buttonLoadIconFacility
            // 
            this.buttonLoadIconFacility.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonLoadIconFacility.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadIconFacility.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadIconFacility.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLoadIconFacility.Location = new System.Drawing.Point(72, 60);
            this.buttonLoadIconFacility.Name = "buttonLoadIconFacility";
            this.buttonLoadIconFacility.Size = new System.Drawing.Size(129, 36);
            this.buttonLoadIconFacility.TabIndex = 0;
            this.buttonLoadIconFacility.Text = "Загрузить значок";
            this.toolTip.SetToolTip(this.buttonLoadIconFacility, "Загрузите значок объекта инфраструктуры, который будет отображаться на карте.");
            this.buttonLoadIconFacility.UseVisualStyleBackColor = false;
            this.buttonLoadIconFacility.Click += new System.EventHandler(this.buttonLoadIconFacility_Click);
            // 
            // buttonValidateAndSave
            // 
            this.buttonValidateAndSave.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonValidateAndSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonValidateAndSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonValidateAndSave.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonValidateAndSave.Location = new System.Drawing.Point(587, 515);
            this.buttonValidateAndSave.Name = "buttonValidateAndSave";
            this.buttonValidateAndSave.Size = new System.Drawing.Size(162, 55);
            this.buttonValidateAndSave.TabIndex = 0;
            this.buttonValidateAndSave.Text = "Сохранить";
            this.buttonValidateAndSave.UseVisualStyleBackColor = false;
            this.buttonValidateAndSave.Click += new System.EventHandler(this.buttonValidateAndSave_Click);
            // 
            // labelAboutWeights
            // 
            this.labelAboutWeights.AutoSize = true;
            this.labelAboutWeights.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAboutWeights.Location = new System.Drawing.Point(21, 140);
            this.labelAboutWeights.Name = "labelAboutWeights";
            this.labelAboutWeights.Size = new System.Drawing.Size(744, 69);
            this.labelAboutWeights.TabIndex = 94;
            this.labelAboutWeights.Text = resources.GetString("labelAboutWeights.Text");
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // StartSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(810, 599);
            this.Controls.Add(this.labelAboutWeights);
            this.Controls.Add(this.buttonValidateAndSave);
            this.Controls.Add(this.groupBoxLoadFiles);
            this.Controls.Add(this.groupBoxCriterion);
            this.Controls.Add(this.groupBoxNameFacility);
            this.Controls.Add(this.groupBoxIconFacility);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(826, 638);
            this.MinimumSize = new System.Drawing.Size(826, 638);
            this.Name = "StartSettings";
            this.Text = "Начальная настройка";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            this.groupBoxLoadFiles.ResumeLayout(false);
            this.groupBoxCriterion.ResumeLayout(false);
            this.groupBoxCriterion.PerformLayout();
            this.groupBoxNameFacility.ResumeLayout(false);
            this.groupBoxNameFacility.PerformLayout();
            this.groupBoxIconFacility.ResumeLayout(false);
            this.groupBoxIconFacility.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconFacility)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLoadFiles;
        private System.Windows.Forms.Button buttonLoadPolygons;
        private System.Windows.Forms.GroupBox groupBoxCriterion;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboCountCriterion;
        private System.Windows.Forms.Label labelCountCriterion;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label labelNameCriterion;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBoxNameFacility;
        private System.Windows.Forms.Label labelNameFacility;
        private System.Windows.Forms.TextBox textBoxNameFacility;
        private System.Windows.Forms.GroupBox groupBoxIconFacility;
        private System.Windows.Forms.Label labelLoadIcon;
        private System.Windows.Forms.PictureBox pictureIconFacility;
        private System.Windows.Forms.Button buttonLoadIconFacility;
        private System.Windows.Forms.Button buttonLoadBorderTerritory;
        private System.Windows.Forms.Button buttonLoadFacilities;
        private System.Windows.Forms.Button buttonValidateAndSave;
        private System.Windows.Forms.Label labelAboutWeights;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ToolTip toolTip;
    }
}