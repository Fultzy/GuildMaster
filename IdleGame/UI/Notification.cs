using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Services;

namespace IdleGame.UI
{
    public partial class Notification : UserControl
    {
        private bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";

        public string Message { get; set; }
        public string ImageName { get; set; }

        private Timer Timer;

        public Notification(string message, Image image = null)
        {
            InitializeComponent();

            Message = message;
            IconImageBox.Image = image;
        }

        private void NotificationControl_Load(object sender, EventArgs e)
        {
            // set loction to the bottom middle of the form. 
            var form = (MainForm)Application.OpenForms["MainForm"];
            if (form == null)
            {
                if (IsDebug) Console.WriteLine("MainForm is null");
                return;
            }

            // Set Message
            if (MessageLabel != null)
            {
                MessageLabel.Text = Message;
            }

            // Set Timer to remove notification after 5 seconds
            Timer = new Timer();
            Timer.Interval = 2500;
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsDebug) Console.WriteLine("Disposing Notification");
            Timer.Stop();

            if (Parent != null)
            {
                Parent.Controls.Remove(this);
                Dispose();
            }
        }
    }
}
