using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Models;
using Newtonsoft.Json;

namespace IdleGame.Services
{
    public static class SavedDataService
    {
        private static bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";

        public static void SaveData(Player player)
        {
            if (player == null)
            {
                return;
            }

            Console.WriteLine($"\n~~ Saving {player.Name}'s data...");
            player.LastSave = DateTime.Now;

            // create directory if it does not exist
            Directory.CreateDirectory(PathService.GetSaveDirectory());

            // save data to file
            File.WriteAllText(PathService.GetSavePath(player.Name),  JsonConvert.SerializeObject(player));

            var timeToSave = (DateTime.Now - player.LastSave).TotalMilliseconds;
            Console.WriteLine($" Data saved - Took: {timeToSave}ms");
        }

        public static Player LoadData(Player player)
        {
            Console.WriteLine($"\n~~ Loading {player.Name}'s data...");
            var time = DateTime.Now;

            // check if file exists
            if (File.Exists(PathService.GetSavePath(player.Name)))
            {
                // load player data from file
                player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(PathService.GetSavePath(player.Name)));

            }
            else
            {
                // create new player
                Console.WriteLine("No save data found, Creating New...");
                new Player();
                SaveData(player);
            }
            
            if (IsDebug) player.WriteAll();
            Console.WriteLine($"Data loaded - Took: {(DateTime.Now - time).TotalMilliseconds}ms");
            return player;
        }

        public static List<Player> GetSavedDataList()
        {
            // get all files in save directory
            var write = Environment.GetEnvironmentVariable("IsDebug") == "true";
            var files = Directory.GetFiles(PathService.GetSaveDirectory());

            // create list of players
            var players = new List<Player>();

            // load player data from each file
            if (write) Console.WriteLine("\n~~ List Save Data...");
            foreach (var file in files)
            {
                var player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(file));
                if (write) Console.WriteLine("Loaded: " + player.ToString());
                players.Add(player);
                
            }

            return players;
        }

        public static void ResetData(Player player)
        {
            // delete save data file
            DeleteData(player);

            // create new player
            new Player();
        }

        public static void DeleteData(Player player)
        {
            // delete save data file
            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IdleGame", player.Name + ".json"));
        }
    }
}
