﻿namespace SCDL
{
    partial class FrmLogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogo));
            this.prgsLogo = new System.Windows.Forms.ProgressBar();
            this.TmrLogo = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // prgsLogo
            // 
            this.prgsLogo.Location = new System.Drawing.Point(2, 389);
            this.prgsLogo.Name = "prgsLogo";
            this.prgsLogo.Size = new System.Drawing.Size(525, 28);
            this.prgsLogo.TabIndex = 0;
            // 
            // TmrLogo
            // 
            this.TmrLogo.Tick += new System.EventHandler(this.TmrLogo_Tick);
            // 
            // FrmLogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(528, 420);
            this.ControlBox = false;
            this.Controls.Add(this.prgsLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLogo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLogo_Load);
            this.Click += new System.EventHandler(this.FrmLogo_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgsLogo;
        private System.Windows.Forms.Timer TmrLogo;
    }
}

