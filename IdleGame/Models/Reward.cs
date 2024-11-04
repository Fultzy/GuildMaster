using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Properties;
using IdleGame.Services;
using IdleGame.UI;

namespace IdleGame.Models
{
    public class Reward
    {
        public int XP { get; set; }
        public int Coin { get; set; }
        public List<Item> Items { get; set; }
        public Dictionary<string, int> SkillXP { get; set; }

        public Reward(int xp = 0, int coin = 0, List<Item> items = null, Dictionary<string,int> skillXp = null)
        {
            XP = xp;
            Coin = coin;

            Items = items;
            if (items == null)
            {
                Items = new List<Item>();
            }

            SkillXP = skillXp;
            if (skillXp == null)
            {
                SkillXP = new Dictionary<string, int>();
            }
        }

        public void ApplyTo(Player player)
        {
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    player.IBanker.Deposit(item);

                    var suf = item.Count > 1 ? "s" : "";
                    var message = $"+ {item.Count} {item.Name}{suf}";
                    Notifyer.Notify(message, ImageService.GetItemImage(item.ImageName));
                }
            }

            foreach (var skill in SkillXP)
            {
                player.GainSkillXP(skill.Key, skill.Value);

                Notifyer.Notify($"+ {skill.Value} {skill.Key} XP", Resources.XpIcon);
            }

            player.GainXP(XP);
            player.Coins += Coin;

            if (XP != 0) Notifyer.Notify($"+ {XP} XP", Resources.XpIcon);
            if (Coin != 0) Notifyer.Notify($"+ {Coin} Coins", Resources.Coin);
        }

        public override string ToString()
        {
            string itemNames = "";
            foreach (var item in Items)
            {
                itemNames += $"{item.Name}({item.Count}), ";
            }

            itemNames = itemNames.TrimEnd(',', ' ');

            string skillNames = "";
            foreach (var skill in SkillXP)
            {
                skillNames += $"{skill.Key}: {skill.Value}, ";
            }
            skillNames = skillNames.TrimEnd(',', ' ');


            return $"{XP} XP, {Coin} Coins and {Items.Count} item(s): {itemNames} skill(s): {skillNames}";
        }
    }
}
