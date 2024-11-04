using System;
using System.IO;

namespace IdleGame.Services
{
    public static class PathService
    {
        
        public static string GetDataDirectory()
        {
            if (Environment.GetEnvironmentVariable("IsTesting") == "true")
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IdleGame", "Testing", "Data");
            }
            else
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "Data");
            }
        }

        public static string GetSaveDirectory()
        {
            if (Environment.GetEnvironmentVariable("IsTesting") == "true")
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IdleGame", "Testing");
            }
            else
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IdleGame");
            }
        }

        public static string GetJobsDirectory()
        {
            return Path.Combine(GetDataDirectory(), "Jobs");
        }

        public static string GetRewardsPath()
        {
            return Path.Combine(GetDataDirectory(), "RewardTable.json");
        }

        public static string GetLootPath() 
        {
            return Path.Combine(GetDataDirectory(), "LootTable.json");
        }

        public static string GetSavePath(string playerName)
        {
            if (playerName == null) return null;
            return Path.Combine(GetSaveDirectory(), playerName + ".json");
        }

        public static string GetAudioDirectory()
        {
            return Path.Combine(GetDataDirectory(), "Audio");
        }

        public static string GetEffectsDirectory()
        {
            return Path.Combine(GetAudioDirectory(), "Effects");
        }

        public static string GetImageDirectory()
        {
            return Path.Combine(GetDataDirectory(), "Images");
        }

        public static string GetImagePath(string imageName)
        {
            return Path.Combine(GetImageDirectory(), imageName + ".png");
        }

        public static string GetItemImagePath(string imageName)
        {
            return Path.Combine(GetImageDirectory(), "Items", imageName + ".png");
        }

        public static string GetIconPath(string iconName)
        {
            return Path.Combine(GetImageDirectory(), "Icons", iconName + ".png");
        }

        public static string GetUIImagePath(string imageName)
        {
            return Path.Combine(GetImageDirectory(), "UI", imageName + ".png");
        }

    }
}
