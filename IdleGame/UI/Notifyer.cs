using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleGame.UI
{
    /// <summary>
    /// this class is responsible for notifying the user of events and updates
    /// </summary>
    public static class Notifyer
    {
        private static bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";

        public static void Notify(string message, Image image = null)
        {
            if (message == "") Console.WriteLine("~Error~ Empty Notification");
            if (IsDebug) Console.WriteLine($"Creating Notification : {message}");

            // Get the main form
            var form = (MainForm)Application.OpenForms["MainForm"];
            if (form.IsDisposed || form == null) return;

            // Create a new notification
            var notification = new Notification(message, image);

            // Add the notification to the FlowLayoutPanel
            var notificationPanel = form.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (notificationPanel != null)
            {
                notificationPanel.Visible = true;
                notificationPanel.Controls.Add(notification);
                notification.BringToFront();
                notificationPanel.BringToFront();
            }
        }
    }
}
