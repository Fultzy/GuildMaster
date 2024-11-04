using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdleGame.Models
{
    public abstract class Job : IDisposable
    {   
        private bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";

        public string Name { get; set; } // Name of the job
        public string Type { get; set; } // Type of the job
        public int Duration { get; internal set; } // Duration in ms
        public int Progress { get; private set; } // Progress in %
        public bool IsRunning { get; private set; } // Is the job running?
        public event Action<Job> OnComplete; // Event to trigger on completion
        public Reward Reward; // Reward for completing the job

        public Job()
        {
            Progress = 0;
            IsRunning = false;
        }

        /// <summary>
        /// Override this method to update the UI when the job is complete
        /// </summary>
        public virtual void UpdateUi() { }

        /// <summary>
        /// Override this method to update the UI when the job progress changes.
        /// This method is called every time the job progress changes
        /// </summary>
        public virtual void OnProgressChanged(int progress){ }

        /// <summary>
        /// Override this method to create a new instance of this job
        /// </summary>
        public abstract Job CreateNew();

        public void Start()
        {
            IsRunning = true;
            Progress = 0;

            StartProgress();
        }

        public void Stop()
        {
            IsRunning = false;
            Progress = 0;

            OnProgressChanged(Progress);
        }

        private async void StartProgress()
        {
            if (Name == null) throw new Exception("Job name must be set before starting.");
            if (Type == null) throw new Exception("Job type must be set before starting.");
            if (Duration == 0) throw new Exception("Job duration must be set before starting.");
            if (Progress > 0) throw new Exception("Job progress must be 0 before starting.");
            if (!IsRunning) throw new Exception("Job must be running.");

            if (IsDebug) Console.WriteLine($"\n~~ Starting job:\n{this}");
            var ticks = Duration / 100;
            
            while (IsRunning && Progress < 100)
            {
                await Task.Delay(ticks); // Tick every 1% of the duration
                Progress++;
                OnProgressChanged(Progress);
                
                if (Progress >= 100)
                {
                    IsRunning = false;
                    if (IsDebug) Console.WriteLine($"\n~~ Finished job:\n{this}");
                    UpdateUi();
                    OnComplete?.Invoke(this); // Trigger completion event
                }
            }
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Type: {Type}");
            sb.AppendLine($"Duration: {Duration}");
            sb.AppendLine($"Progress: {Progress}");
            sb.AppendLine($"IsRunning: {IsRunning}");
            if (Name != "Resting") sb.AppendLine($"Reward: {Reward.ToString()}");
            
            return sb.ToString();
        }

        // TODO: Implement IDisposable
        void IDisposable.Dispose() => throw new NotImplementedException();
    }
}
