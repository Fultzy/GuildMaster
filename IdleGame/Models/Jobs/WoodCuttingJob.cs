using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.Services;
using IdleGame.UI;

namespace IdleGame.Jobs
{
    public class WoodCuttingJob : Job
    {
        private bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";
        
        /// <summary>
        /// Constructor for WoodCutting Job
        /// </summary>
        public WoodCuttingJob(int duration = 0, Reward reward = null, string jobName = null)
        {
            Type = "WoodCutting";
            Name = jobName;
            Duration = duration;

            if (reward == null && jobName != null)
            {
                Reward = RewardService.GetRewardFor(jobName);
                if (IsDebug)
                { 
                    Console.WriteLine($"Reward for {jobName}: {Reward.ToString()}");
                    Console.WriteLine(RewardService.RewardListToString());
                }
            }
            else
            {
                Reward = reward;
            }
        }

        ////////////////////////////////////////////
        // Method Overrides
        /// <summary>
        /// Overrides the base UpdateUi method to update the WoodCutting Tab
        /// </summary>
        public override void UpdateUi()
        {
            var form = (MainForm)Application.OpenForms["MainForm"];
            
            if (form.IsDisposed || form == null) return;
            form.IUIService.UpdateWoodCuttingTab(form);
        }

        /// <summary>
        /// Overrides the base ProgressChanged method to update the WoodCutting Tab
        /// </summary>
        /// <param name="progress"></param>
        public override void OnProgressChanged(int progress)
        {
            if (progress > 100) progress = 100;

            var form = (MainForm)Application.OpenForms["MainForm"];
            if (form.IsDisposed) return;

            form.Invoke((MethodInvoker)delegate
            {
                // Update Progress bar
                form.WoodCuttingProgressBar.Value = progress;
                form.AllWorkProgressBar.Value = progress;
            });
        }

        /// <summary>
        /// Overrides the base method to create a new WoodCutting Job
        /// </summary>
        /// <returns>a WoodCutting Job</returns>
        public override Job CreateNew()
        {
            var job = new WoodCuttingJob(Duration, Reward, Name);
            return job;
        }

    }
}
