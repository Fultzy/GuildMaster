using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.Models
{
    public class Item
    {
        // base info
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Value { get; set; }
        public string Rarity { get; set; }
        public string ImageName { get; set; }
        private Image Image { get; set; }
        
        // Quest Item
        public bool IsQuestItem { get; set; }
        //public List<Quest> AssociatedQuests { get; set; }
        
        // Equiptment
        public bool IsEquiptment { get; set; }
        public string EquiptmentType { get; set; }

        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        
        public bool IsConsumable { get; set; }
        //public List<Effect> Effects { get; set; }


        // Constructors
        public Item() 
        {
            if (Count >= 0)
            {
                Count = 1;
            }

        }

        public void LoadImage()
        {
            
        }

        public override string ToString() 
        {
            return $"Name: {Name}, Description: {Description}, Count: {Count}, Value: {Value}, Rarity: {Rarity}, ImageName: {ImageName}";
        }
    }
}
