using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IdleGame.Jobs;
using IdleGame.Models;
using Newtonsoft.Json;

namespace IdleGame.Services
{
    
    public class JobService
    {
        private bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";

        public Job CurrentJob = new RestingJob(); // Default resting
        public Job PreviousJob = new RestingJob(); // Default resting
        private List<Job> JobList { get;  set; }


        //////// Job Control ////////
        public void StartJob(Job job)
        {
            // Stop any currently running Job
            if (CurrentJob.Name != "Resting") StopCurrentJob();

            // Subscribe to task completion
            if (CurrentJob != job)
            {
                job.OnComplete += HandleJobCompletion; 
            }

            lock (CurrentJob)
            {
                // Code to be executed while the job is locked
                CurrentJob = job; 
                CurrentJob.Start();
            }
        }

        public void StopCurrentJob()
        {
            // Unsubscribe from job completion
            CurrentJob.OnComplete -= HandleJobCompletion;
            CurrentJob?.Stop();

            PreviousJob = CurrentJob;
            CurrentJob = new RestingJob(); // Reset Resting
        }

        private void HandleJobCompletion(Job completedJob)
        {
            // Handle rewards or other logic after Job completion
            var form = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (form != null)
            {
                var player = form.thisPlayer;
                completedJob.Reward.ApplyTo(player);
            }
            else
            {
                Console.WriteLine("MainForm not found");
            }


            // Stop Current Job
            StopCurrentJob();

            // Create a new instance of the job to prevent reusing a finished job
            var job = GetJobFor(PreviousJob.Name).CreateNew();

            // Automatically restart the Job
            StartJob(job);
        }


        //////// Job Data ////////
        public List<Job> LoadJobs()
        {
            JobList = new List<Job>();

            if (IsDebug) Console.WriteLine($"\n~~ Loading Jobs from File - Fix This");
            var jobsPath = PathService.GetJobsDirectory();
            var woodCuttingPath = jobsPath + "/WoodCuttingJobs.Json";
            var woodCuttingjson = File.ReadAllText(woodCuttingPath);
            var woodCuttingjobs = JsonConvert.DeserializeObject<List<WoodCuttingJob>>(woodCuttingjson);
            
            // WoodCutting Jobs
            foreach (var job in woodCuttingjobs)
            {
                JobList.Add(job);
                if (false) Console.WriteLine($"New Job: {job}");
            }

            // Misc Jobs
            JobList.Add(new RestingJob());
            if (false) Console.WriteLine($"Loaded {JobList.Count} Jobs");

            RewardService.JobList = JobList;
            return JobList;
        }

        public Job GetJobFor(string jobName)
        {
            if (JobList == null)
            {
                LoadJobs();
            }

            // TODO: LoadJobs is required to prevent Changes to Jobs. fix this
            var result = LoadJobs().FirstOrDefault(job => job.Name == jobName);
            if (result != null)
            {
                // disabled for spam till fixed
                if (false) Console.WriteLine($"\n~~ Found Job:\n{result}");
                return result;
            }
            else
            {
                // list all known jobs
                if (true) // Debug
                {
                    foreach (var job in JobList)
                    {
                        Console.WriteLine($"Known Job: {job}");
                    }

                    Console.WriteLine($"Job '{jobName}' not found");
                }

                return null;
            }
        }

        public override string ToString()
        {
            return $"Current Job: {CurrentJob}";
        }
    }
}
