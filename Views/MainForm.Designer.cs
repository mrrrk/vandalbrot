namespace Vandalbrot.Views {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.TopPanel = new System.Windows.Forms.Panel();
            this.RangeText = new System.Windows.Forms.TextBox();
            this.BitmapPictureBox = new System.Windows.Forms.PictureBox();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BitmapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.RangeText);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(413, 35);
            this.TopPanel.TabIndex = 0;
            // 
            // RangeText
            // 
            this.RangeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RangeText.Location = new System.Drawing.Point(105, 9);
            this.RangeText.Name = "RangeText";
            this.RangeText.ReadOnly = true;
            this.RangeText.Size = new System.Drawing.Size(296, 20);
            this.RangeText.TabIndex = 0;
            // 
            // BitmapPictureBox
            // 
            this.BitmapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BitmapPictureBox.Location = new System.Drawing.Point(0, 35);
            this.BitmapPictureBox.Name = "BitmapPictureBox";
            this.BitmapPictureBox.Size = new System.Drawing.Size(413, 389);
            this.BitmapPictureBox.TabIndex = 1;
            this.BitmapPictureBox.TabStop = false;
            this.BitmapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BitmapPictureBox_MouseDown);
            this.BitmapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BitmapPictureBox_MouseMove);
            this.BitmapPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BitmapPictureBox_MouseUp);
            this.BitmapPictureBox.Resize += new System.EventHandler(this.BitmapPictureBox_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 424);
            this.Controls.Add(this.BitmapPictureBox);
            this.Controls.Add(this.TopPanel);
            this.Name = "MainForm";
            this.Text = "Vandalbrot";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BitmapPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.TextBox RangeText;
        private System.Windows.Forms.PictureBox BitmapPictureBox;
    }
}

