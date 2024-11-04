using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;

namespace IdleGame.Services
{
    public static class RewardService
    {
        private static bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";
        public static List<Job> JobList;

        public static Reward GetRewardFor(Job job)
        {
            if (job.Name == null) throw new Exception("Job name must be set before getting reward");
            if (JobList == null) throw new Exception("Job list has not been loaded");

            var reward = JobList.FirstOrDefault(j => j.Name == job.Name)?.Reward;
            if (IsDebug) Console.WriteLine($"Reward for {job.Name} is {reward.ToString()}");
            return reward;
        }

        public static Reward GetRewardFor(string jobName)
        {
            if (jobName == null) throw new Exception("Job name must be set before getting reward");
            if (JobList == null) throw new Exception("Job list has not been loaded");

            var reward = JobList.FirstOrDefault(j => j.Name == jobName)?.Reward;
            if (IsDebug) Console.WriteLine($"Reward for {jobName} is {reward.ToString()}");
            return reward;
        }

        public static string RewardListToString()
        {
            var sb = new StringBuilder();
            foreach (var job in JobList)
            {
                var rewardMsg = job.Reward == null ? "No reward" : job.Reward.ToString();
                sb.AppendLine($"{job.Name}: {rewardMsg}");
            }
            return sb.ToString();
        }
    }
}
