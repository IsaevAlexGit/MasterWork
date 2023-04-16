namespace Optimum
{
    partial class SettingRadius
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingRadius));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.InputRadiusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelRadiusLong = new System.Windows.Forms.Label();
            this.labelRadius = new System.Windows.Forms.Label();
            this.trackBarRadius = new System.Windows.Forms.TrackBar();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Bisque;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InputRadiusToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(531, 70);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // InputRadiusToolStripMenuItem
            // 
            this.InputRadiusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InputRadiusToolStripMenuItem.Image = global::Optimum.Properties.Resources.iconRadiusBack;
            this.InputRadiusToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InputRadiusToolStripMenuItem.Name = "InputRadiusToolStripMenuItem";
            this.InputRadiusToolStripMenuItem.Size = new System.Drawing.Size(127, 66);
            this.InputRadiusToolStripMenuItem.Text = "Задать радиус и вернуться";
            this.InputRadiusToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.InputRadiusToolStripMenuItem.Click += new System.EventHandler(this.InputRadiusToolStripMenuItem_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(10, 80);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(486, 80);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Задайте радиус поиска, и приложение будет проводить\r\nконкурентный анализ и анализ" +
    " населения в указанном диапазоне.\r\n\r\nРадиус должен быть указан в метрах в интерв" +
    "але [300, 3000].";
            // 
            // labelRadiusLong
            // 
            this.labelRadiusLong.AutoSize = true;
            this.labelRadiusLong.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRadiusLong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadiusLong.Location = new System.Drawing.Point(441, 182);
            this.labelRadiusLong.Name = "labelRadiusLong";
            this.labelRadiusLong.Size = new System.Drawing.Size(55, 21);
            this.labelRadiusLong.TabIndex = 91;
            this.labelRadiusLong.Text = "300 м.";
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.BackColor = System.Drawing.Color.PeachPuff;
            this.labelRadius.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadius.Location = new System.Drawing.Point(12, 182);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(48, 17);
            this.labelRadius.TabIndex = 83;
            this.labelRadius.Text = "Радиус";
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.BackColor = System.Drawing.Color.PeachPuff;
            this.trackBarRadius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarRadius.Location = new System.Drawing.Point(66, 173);
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(347, 45);
            this.trackBarRadius.TabIndex = 92;
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarRadius_Scroll);
            // 
            // SettingRadius
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(531, 232);
            this.Controls.Add(this.trackBarRadius);
            this.Controls.Add(this.labelRadiusLong);
            this.Controls.Add(this.labelRadius);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(547, 271);
            this.MinimumSize = new System.Drawing.Size(547, 271);
            this.Name = "SettingRadius";
            this.Text = "Установка радиуса поиска";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingRadius_FormClosing);
            this.Load += new System.EventHandler(this.SettingRadius_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingRadius_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem InputRadiusToolStripMenuItem;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelRadiusLong;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.TrackBar trackBarRadius;
    }
}