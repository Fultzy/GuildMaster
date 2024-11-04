using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;

namespace IdleGame.Jobs
{
    internal class RestingJob : Job
    {
        public RestingJob(int duration=0, Reward reward=null, string name = "Resting")
        {
            Name = name;
            Type = "Resting";
            Duration = duration;
        }

        public override void OnProgressChanged(int progress)
        {
            var form = Application.OpenForms["MainForm"];
            
            if (form == null) return;
            if (progress > 100) progress = 100;

            form.Invoke((MethodInvoker)delegate
            {
                // Update UI
                
            });
        }

        public override Job CreateNew()
        {
            return new RestingJob(Duration, Reward);
        }
    }
}
