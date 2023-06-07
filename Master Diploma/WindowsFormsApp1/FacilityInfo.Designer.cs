namespace Optimum
{
    partial class FacilityInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacilityInfo));
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(13, 11);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(666, 17);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Если среди указанных данных есть ссылка, то по ней можно перейти, щёлкнув дважды " +
    "левой кнопкой мыши.";
            // 
            // FacilityInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.ClientSize = new System.Drawing.Size(688, 249);
            this.Controls.Add(this.labelInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(704, 288);
            this.MinimumSize = new System.Drawing.Size(704, 288);
            this.Name = "FacilityInfo";
            this.Text = "Информация об объекте инфраструктуры";
            this.Load += new System.EventHandler(this.FacilityInfo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FacilityInfo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelInfo;
    }
}