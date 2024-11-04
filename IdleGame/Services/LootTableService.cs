using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Models;
using IdleGame.Properties;
using Newtonsoft.Json;

namespace IdleGame.Services
{
    public class LootTableService
    {
        private bool IsDebug = true;// Convert.ToBoolean(Environment.GetEnvironmentVariable("IsDebug"));
        public List<Item> Items { get; set; }

        private static LootTableService instance;
        public static LootTableService Instance()
        {
            if (instance == null)
            {
                instance = new LootTableService();
            }
            return instance;
        }

        public List<Item> LoadLootTable()
        {
            if (IsDebug) Console.WriteLine("Loading Item Data...");
            Items = new List<Item>();

            var jsonPath = Directory.GetCurrentDirectory() + "/Data/LootTable.json";
            var json = File.ReadAllText(jsonPath);
            var items = JsonConvert.DeserializeObject<List<Object>>(json);

            foreach (var item in items)
            {
                var itemJson = JsonConvert.SerializeObject(item);
                var itemObject = JsonConvert.DeserializeObject<Item>(itemJson);
                Items.Add(itemObject);

                if (IsDebug) Console.WriteLine(itemObject.Name);
            }

            return Items;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void AddItems(List<Item> items)
        {
            Items.AddRange(items);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public Item GetItem(string itemName)
        {
            if (Items == null || Items.Count == 0)
            {
                LoadLootTable();
            }

            return Items.Find(item => item.Name == itemName);
        }

        public Item GetRandomItem()
        {
            var random = new Random();
            var index = random.Next(0, Items.Count);

            return Items[index];
        }
    }
}
