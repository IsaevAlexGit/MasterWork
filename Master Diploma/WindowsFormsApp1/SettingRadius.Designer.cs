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
            this.InputRadius = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip.BackColor = System.Drawing.Color.PaleGreen;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InputRadius});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(694, 71);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // InputRadius
            // 
            this.InputRadius.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InputRadius.Image = global::Optimum.Properties.Resources.iconRadiusBack;
            this.InputRadius.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.InputRadius.Name = "InputRadius";
            this.InputRadius.Size = new System.Drawing.Size(159, 67);
            this.InputRadius.Text = "Задать радиус и вернуться";
            this.InputRadius.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.InputRadius.Click += new System.EventHandler(this.InputRadiusToolStripMenuItem_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(10, 85);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(554, 17);
            this.labelInfo.TabIndex = 2;
            this.labelInfo.Text = "Задайте радиус поиска, и приложение произведёт расчёты в новом указанном диапазон" +
    "е.";
            // 
            // labelRadiusLong
            // 
            this.labelRadiusLong.AutoSize = true;
            this.labelRadiusLong.BackColor = System.Drawing.Color.LightGreen;
            this.labelRadiusLong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadiusLong.Location = new System.Drawing.Point(602, 130);
            this.labelRadiusLong.Name = "labelRadiusLong";
            this.labelRadiusLong.Size = new System.Drawing.Size(55, 21);
            this.labelRadiusLong.TabIndex = 4;
            this.labelRadiusLong.Text = "300 м.";
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.BackColor = System.Drawing.Color.LightGreen;
            this.labelRadius.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadius.Location = new System.Drawing.Point(10, 130);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(60, 21);
            this.labelRadius.TabIndex = 3;
            this.labelRadius.Text = "Радиус";
            // 
            // trackBarRadius
            // 
            this.trackBarRadius.BackColor = System.Drawing.Color.PaleGreen;
            this.trackBarRadius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarRadius.Location = new System.Drawing.Point(80, 119);
            this.trackBarRadius.Name = "trackBarRadius";
            this.trackBarRadius.Size = new System.Drawing.Size(516, 45);
            this.trackBarRadius.TabIndex = 1;
            this.trackBarRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarRadius.Scroll += new System.EventHandler(this.trackBarRadius_Scroll);
            // 
            // SettingRadius
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(694, 176);
            this.Controls.Add(this.trackBarRadius);
            this.Controls.Add(this.labelRadiusLong);
            this.Controls.Add(this.labelRadius);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(710, 215);
            this.MinimumSize = new System.Drawing.Size(710, 215);
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
        private System.Windows.Forms.ToolStripMenuItem InputRadius;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelRadiusLong;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.TrackBar trackBarRadius;
    }
}