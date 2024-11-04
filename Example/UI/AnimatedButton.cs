using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin.Controls;

namespace Example.UI
{
    public partial class AnimatedButton : MaterialButton
    {
        public bool IsPositive { get; set; }
        public SoundPlayer ClickSoundPlayer { get; set; }

        public AnimatedButton()
        {
            this.AutoSize = false;
            this.MouseEnter += AnimatedButton_MouseEnter;
            this.MouseLeave += AnimatedButton_MouseLeave;
            this.Click += AnimatedButton_Click;
            
            InitializeComponent();
        }

        private async void AnimatedButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Clicked");

            if (IsPositive)
            {
                ClickSoundPlayer = new SoundPlayer("C:/code/IdleGame/IdleGame/bin/Debug/Data/Audio/Effects/ClickPopPositive.wav");
            }
            else
            {
                ClickSoundPlayer = new SoundPlayer("C:/code/IdleGame/IdleGame/bin/Debug/Data/Audio/Effects/ClickPopNegative.wav");
            }

            // Play the click sound
            ClickSoundPlayer.Play();

            this.Width += 10;
            this.Height += 10;

            this.Left -= 5;
            this.Top -= 5;
            await Task.Delay(100);
            this.Width -= 10;
            this.Height -= 10;

            this.Left += 5;
            this.Top += 5;
        }

        private void AnimatedButton_MouseLeave(object sender, EventArgs e)
        {
            this.Width -= 10;
            this.Height -= 10;

            this.Left += 5;
            this.Top += 5;
        }

        private void AnimatedButton_MouseEnter(object sender, EventArgs e)
        {
            this.Width += 10;
            this.Height += 10;

            this.Left -= 5;
            this.Top -= 5;
        }
    }
}
