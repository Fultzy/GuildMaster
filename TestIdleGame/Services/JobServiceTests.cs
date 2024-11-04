using IdleGame.Services;
using IdleGame.Jobs;
using IdleGame.Models;
using System;


namespace TestIdleGame.Services;
public class JobServiceTests
{
    [Fact]
    public void LoadJobs_ReturnsValidJobs()
    {
        // Arrange
        Environment.SetEnvironmentVariable("IsTesting", "True");
        var jobService = new JobService();

        // Act
        var jobs = jobService.LoadJobs();
        var jobNames = jobs.Select(j => j.Name).ToList();

        // Assert
        Assert.NotNull(jobs);
        Assert.NotEmpty(jobs);
        
        // assert that all jobs are valid
        foreach (var job in jobs)
        {
            Assert.NotNull(job);
            Assert.NotEmpty(job.Name);
            Assert.NotEmpty(job.Type);

            if (job.Name != "Resting")
            {
                Assert.True(job.Duration > 0);

                // assert that all jobs have valid rewards
                Assert.NotNull(job.Reward);
                Assert.True(job.Reward.XP >= 0);
                Assert.True(job.Reward.Coin >= 0);
                Assert.NotNull(job.Reward.Items);
                Assert.NotNull(job.Reward.SkillXP);
            }
        }

        // assert that all jobs are unique
        Assert.Equal(jobNames.Count, jobNames.Distinct().Count());
    }
}