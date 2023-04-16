﻿namespace Optimum
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.labelAboutProgram = new System.Windows.Forms.Label();
            this.buttonProceed = new System.Windows.Forms.Button();
            this.labelNameProgram = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSelectColor = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.labelStateConnetion = new System.Windows.Forms.Label();
            this.pictureBoxConnection = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAboutProgram
            // 
            this.labelAboutProgram.AutoSize = true;
            this.labelAboutProgram.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAboutProgram.Location = new System.Drawing.Point(22, 179);
            this.labelAboutProgram.Name = "labelAboutProgram";
            this.labelAboutProgram.Size = new System.Drawing.Size(748, 42);
            this.labelAboutProgram.TabIndex = 4;
            this.labelAboutProgram.Text = "Картографическое приложение \"Оптимум\" поможет Вам выгодно разместить на карте гор" +
    "ода бизнес \r\nили найти хорошее место для открытия нового объекта социальной инфр" +
    "аструктуры.";
            this.labelAboutProgram.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonProceed
            // 
            this.buttonProceed.BackColor = System.Drawing.Color.Bisque;
            this.buttonProceed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonProceed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonProceed.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProceed.Location = new System.Drawing.Point(321, 381);
            this.buttonProceed.Name = "buttonProceed";
            this.buttonProceed.Size = new System.Drawing.Size(162, 55);
            this.buttonProceed.TabIndex = 1;
            this.buttonProceed.Text = "Продолжить";
            this.toolTip.SetToolTip(this.buttonProceed, "Выберите язык для работы с картой и приступайте к работе в приложении.");
            this.buttonProceed.UseVisualStyleBackColor = false;
            this.buttonProceed.Click += new System.EventHandler(this.buttonProceed_Click);
            // 
            // labelNameProgram
            // 
            this.labelNameProgram.AutoSize = true;
            this.labelNameProgram.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNameProgram.Location = new System.Drawing.Point(351, 143);
            this.labelNameProgram.Name = "labelNameProgram";
            this.labelNameProgram.Size = new System.Drawing.Size(94, 25);
            this.labelNameProgram.TabIndex = 3;
            this.labelNameProgram.Text = "Оптимум";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // buttonSelectColor
            // 
            this.buttonSelectColor.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonSelectColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSelectColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSelectColor.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectColor.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectColor.Image")));
            this.buttonSelectColor.Location = new System.Drawing.Point(12, 345);
            this.buttonSelectColor.Name = "buttonSelectColor";
            this.buttonSelectColor.Size = new System.Drawing.Size(121, 91);
            this.buttonSelectColor.TabIndex = 9;
            this.buttonSelectColor.Text = "Выбрать цвет интерфейса";
            this.buttonSelectColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip.SetToolTip(this.buttonSelectColor, "Выберете основной цвет приложения, если Вам не нравится цвет по умолчанию.");
            this.buttonSelectColor.UseVisualStyleBackColor = false;
            this.buttonSelectColor.Click += new System.EventHandler(this.buttonSelectColor_Click_1);
            // 
            // buttonPDF
            // 
            this.buttonPDF.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPDF.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPDF.Image = global::Optimum.Properties.Resources.iconPDF;
            this.buttonPDF.Location = new System.Drawing.Point(666, 345);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(116, 91);
            this.buttonPDF.TabIndex = 2;
            this.buttonPDF.Text = "Руководство пользователя";
            this.buttonPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip.SetToolTip(this.buttonPDF, "Ознакомиться с руководством пользователя.");
            this.buttonPDF.UseVisualStyleBackColor = false;
            this.buttonPDF.Click += new System.EventHandler(this.buttonPDF_Click);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.BackColor = System.Drawing.Color.White;
            this.comboBoxLanguage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxLanguage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Русская",
            "Английская"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(321, 254);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(162, 29);
            this.comboBoxLanguage.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(307, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "Выберите локализацию карты:";
            // 
            // labelStateConnetion
            // 
            this.labelStateConnetion.AutoSize = true;
            this.labelStateConnetion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStateConnetion.Location = new System.Drawing.Point(58, 24);
            this.labelStateConnetion.Name = "labelStateConnetion";
            this.labelStateConnetion.Size = new System.Drawing.Size(12, 17);
            this.labelStateConnetion.TabIndex = 10;
            this.labelStateConnetion.Text = " ";
            // 
            // pictureBoxConnection
            // 
            this.pictureBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxConnection.Name = "pictureBoxConnection";
            this.pictureBoxConnection.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxConnection.TabIndex = 6;
            this.pictureBoxConnection.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox.Image = global::Optimum.Properties.Resources.iconApplication;
            this.pictureBox.Location = new System.Drawing.Point(333, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(128, 128);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(794, 448);
            this.Controls.Add(this.labelStateConnetion);
            this.Controls.Add(this.buttonSelectColor);
            this.Controls.Add(this.pictureBoxConnection);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.labelNameProgram);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonProceed);
            this.Controls.Add(this.labelAboutProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(810, 487);
            this.MinimumSize = new System.Drawing.Size(810, 487);
            this.Name = "StartForm";
            this.Text = "Вход";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAboutProgram;
        private System.Windows.Forms.Button buttonProceed;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelNameProgram;
        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxConnection;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button buttonSelectColor;
        private System.Windows.Forms.Label labelStateConnetion;
        private System.Windows.Forms.Timer timer;
    }
}