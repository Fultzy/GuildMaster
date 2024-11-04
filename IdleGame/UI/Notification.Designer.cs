namespace IdleGame.UI
{
    partial class Notification
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notification));
            this.IconImageBox = new System.Windows.Forms.PictureBox();
            this.MessageLabel = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.IconImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // IconImageBox
            // 
            this.IconImageBox.Image = ((System.Drawing.Image)(resources.GetObject("IconImageBox.ImageName")));
            this.IconImageBox.Location = new System.Drawing.Point(3, 3);
            this.IconImageBox.Name = "IconImageBox";
            this.IconImageBox.Size = new System.Drawing.Size(15, 19);
            this.IconImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.IconImageBox.TabIndex = 0;
            this.IconImageBox.TabStop = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Depth = 0;
            this.MessageLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MessageLabel.Location = new System.Drawing.Point(24, 3);
            this.MessageLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(32, 19);
            this.MessageLabel.TabIndex = 1;
            this.MessageLabel.Text = "Title";
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CausesValidation = false;
            this.Controls.Add(this.IconImageBox);
            this.Controls.Add(this.MessageLabel);
            this.MaximumSize = new System.Drawing.Size(300, 30);
            this.MinimumSize = new System.Drawing.Size(70, 5);
            this.Name = "Notification";
            this.Size = new System.Drawing.Size(70, 25);
            this.Load += new System.EventHandler(this.NotificationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IconImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox IconImageBox;
        private MaterialSkin.Controls.MaterialLabel MessageLabel;
    }
}
