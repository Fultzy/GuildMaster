namespace Example
{
    partial class ExampleAnimationForm
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
            this.animatedButton3 = new Example.UI.AnimatedButton();
            this.animatedButton1 = new Example.UI.AnimatedButton();
            this.animatedButton2 = new Example.UI.AnimatedButton();
            this.SuspendLayout();
            // 
            // animatedButton3
            // 
            this.animatedButton3.AutoSize = false;
            this.animatedButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.animatedButton3.ClickSoundPlayer = null;
            this.animatedButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.animatedButton3.Depth = 0;
            this.animatedButton3.HighEmphasis = true;
            this.animatedButton3.Icon = null;
            this.animatedButton3.IsPositive = false;
            this.animatedButton3.Location = new System.Drawing.Point(59, 172);
            this.animatedButton3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.animatedButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.animatedButton3.Name = "animatedButton3";
            this.animatedButton3.NoAccentTextColor = System.Drawing.Color.Empty;
            this.animatedButton3.Size = new System.Drawing.Size(177, 68);
            this.animatedButton3.TabIndex = 3;
            this.animatedButton3.Text = "animatedButton3";
            this.animatedButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.animatedButton3.UseAccentColor = true;
            this.animatedButton3.UseVisualStyleBackColor = true;
            // 
            // animatedButton1
            // 
            this.animatedButton1.AutoSize = false;
            this.animatedButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.animatedButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.animatedButton1.ClickSoundPlayer = null;
            this.animatedButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.animatedButton1.Depth = 0;
            this.animatedButton1.HighEmphasis = true;
            this.animatedButton1.Icon = null;
            this.animatedButton1.IsPositive = true;
            this.animatedButton1.Location = new System.Drawing.Point(59, 90);
            this.animatedButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.animatedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.animatedButton1.Name = "animatedButton1";
            this.animatedButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            this.animatedButton1.Size = new System.Drawing.Size(177, 56);
            this.animatedButton1.TabIndex = 2;
            this.animatedButton1.Text = "animatedButton1";
            this.animatedButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.animatedButton1.UseAccentColor = false;
            this.animatedButton1.UseVisualStyleBackColor = false;
            // 
            // animatedButton2
            // 
            this.animatedButton2.AutoSize = false;
            this.animatedButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.animatedButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.animatedButton2.ClickSoundPlayer = null;
            this.animatedButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.animatedButton2.Depth = 0;
            this.animatedButton2.HighEmphasis = true;
            this.animatedButton2.Icon = null;
            this.animatedButton2.IsPositive = false;
            this.animatedButton2.Location = new System.Drawing.Point(0, 0);
            this.animatedButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.animatedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.animatedButton2.Name = "animatedButton2";
            this.animatedButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.animatedButton2.Size = new System.Drawing.Size(75, 36);
            this.animatedButton2.TabIndex = 0;
            this.animatedButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.animatedButton2.UseAccentColor = false;
            this.animatedButton2.UseVisualStyleBackColor = false;
            // 
            // ExampleAnimationForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.animatedButton3);
            this.Controls.Add(this.animatedButton1);
            this.Name = "ExampleAnimationForm";
            this.ResumeLayout(false);

        }

        #endregion
        private UI.AnimatedButton animatedButton2;
        private UI.AnimatedButton animatedButton1;
        private UI.AnimatedButton animatedButton3;
    }
}

