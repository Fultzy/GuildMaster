using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using IdleGame.Services;
using IdleGame.UI;

namespace IdleGame.Models
{
    public class Player
    {
        // Services
        public JobService IJob = new JobService();
        public BankerService IBanker = new BankerService();
        public ShopService IShop = new ShopService();
        public UISettings UISettings = new UISettings();

        ////////////////////////////////////////////
        // Player Info
        public string Name;
        public int Coins = 0;
        public DateTime LastSave;

        // Player Stats
        public int Level = 1;
        public int XP = 0;
        public int XPToNextLevel = 100;
        public int Health = 100;
        public int MaxHealth = 100;
        public int Attack = 1;
        public int Defence = 1;

        // Skill Levels
        public int WoodCuttingLevel = 1;
        public int CombatLevel = 1;

        // Skill XP
        public int WoodCuttingXP = 0;
        public int WoodCuttingXPToNextLevel = 100;
        public int CombatXP = 0;
        public int CombatXPToNextLevel = 100;

        // Skill XP Rates
        public double WoodCuttingXPRate = 1.0;
        public double CombatXPRate = 1.0;

        // intervol times
        public int WoodCuttingInterval = 2500;
        public int CombatIntervol = 5000;

        // Equiptment
        public Item Head { get; set; }
        public Item Chest { get; set; }
        public Item Back { get; set; }
        public Item Legs { get; set; }
        public Item Feet { get; set; }
        public Item Hands { get; set; }
        public Item LeftHand { get; set; }
        public Item RightHand { get; set; }
        public List<Item> Rings { get; set; }
        public Item Neck { get; set; }

        ////////////////////////////////////////////
        // Player Methods
        public void GainXP(int xp)
        {
            XP += xp;
            if (XP >= XPToNextLevel)
            {
                Level++;
                XPToNextLevel = CalculateNextLevelXP(Level);
                Notifyer.Notify($"Level Up! You are now level {Level}");
            }
        }

        public void GainSkillXP(string skill, int xp)
        {
            if (skill == "WoodCutting")
            {
                WoodCuttingXP += (int)(xp * WoodCuttingXPRate);
                if (WoodCuttingXP >= WoodCuttingXPToNextLevel)
                {
                    WoodCuttingLevel++;
                    WoodCuttingXPToNextLevel = CalculateNextLevelXP(WoodCuttingLevel);
                    Notifyer.Notify($"WoodCutting Level Up! You are now level {WoodCuttingLevel}");
                }
            }
            else if (skill == "Combat")
            {
                CombatXP += (int)(xp * CombatXPRate);
                if (CombatXP >= CombatXPToNextLevel)
                {
                    CombatLevel++;
                    CombatXPToNextLevel = CalculateNextLevelXP(CombatLevel);
                    Notifyer.Notify($"Combat Level Up! You are now level {CombatLevel}");
                }
            }
        }

        public int CalculateNextLevelXP(int currentLevel)
        {
            int baseXP = 100; // Xp required for level 1
            double exponent = 1.5; // Exponent for curve

            return (int)(baseXP * Math.Pow(currentLevel, exponent));
        }

        public override string ToString()
        {
            return $"Player: {Name}, Level: {Level}, XP: {XP}, Coins: {Coins}";
        }

        public void WriteAll()
        {
            Console.WriteLine($"\nPlayer: {Name}, Level: {Level}, XP: {XP}, Coins: {Coins}");
            Console.WriteLine($"LastSave: {LastSave.ToString()}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}, Attack: {Attack}, Defence: {Defence}");
            Console.WriteLine($"WoodCutting: {WoodCuttingLevel}, XP: {WoodCuttingXP}/{WoodCuttingXPToNextLevel}");
            Console.WriteLine($"Combat: {CombatLevel}, XP: {CombatXP}/{CombatXPToNextLevel}");

            Console.WriteLine("\nEquiptmyments:");
            Console.WriteLine($"Head: {Head}");
            Console.WriteLine($"Chest: {Chest}");
            Console.WriteLine($"Back: {Back}");
            Console.WriteLine($"Legs: {Legs}");
            Console.WriteLine($"Feet: {Feet}");
            Console.WriteLine($"Hands: {Hands}");
            Console.WriteLine($"LeftHand: {LeftHand}");
            Console.WriteLine($"RightHand: {RightHand}");
            Console.WriteLine($"Rings: {Rings}");
            Console.WriteLine($"Neck: {Neck}");


            Console.WriteLine($"\nJobService:\n{IJob.ToString()}");
            Console.WriteLine($"\nBankerService:\n{IBanker.ToString()}");
            Console.WriteLine($"ShopService:\n{IShop.ToString()}");
            Console.WriteLine($"\nUISettings:\n{UISettings.ToString()}");
        }
    }
}
