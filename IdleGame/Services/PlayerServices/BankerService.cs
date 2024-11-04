using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Models;

namespace IdleGame.Services
{
    public class BankerService
    {
        private readonly bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";
        public int TabSize { get; set; }
        public Dictionary<string,int> BankGlossary { get; set; } // ItemName, TabNumber

        // BankTabs
        public List<Item> BankTab1 { get; set; }
        public List<Item> BankTab2 { get; set; }
        public List<Item> BankTab3 { get; set; }
        public List<Item> BankTab4 { get; set; }
        public List<Item> BankTab5 { get; set; }

        public bool BankTab2Unlocked { get; set; }
        public bool BankTab3Unlocked { get; set; }
        public bool BankTab4Unlocked { get; set; }
        public bool BankTab5Unlocked { get; set; }

        // Event to update the UI
        private EventHandler onBankContentsChanged;

        public BankerService()
        {
            TabSize = 10;
            BankGlossary = new Dictionary<string, int>();
            BankTab1 = new List<Item>();
            BankTab2 = new List<Item>();
            BankTab3 = new List<Item>();
            BankTab4 = new List<Item>();
            BankTab5 = new List<Item>();
            BankTab2Unlocked = false;
            BankTab3Unlocked = false;
            BankTab4Unlocked = false;
            BankTab5Unlocked = false;
        }

        public List<Item> GetBankTab(int tabnum)
        {
            switch (tabnum)
            {
                case 1:
                    return BankTab1;
                case 2:
                    return BankTab2;
                case 3:
                    return BankTab3;
                case 4:
                    return BankTab4;
                case 5:
                    return BankTab5;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Finds an item in the bank without knowing the tab number
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns>an Item or Null if no item is found</returns>
        public Item GetItem(string itemName)
        {
            if (itemName == null) return null;

            // Find Tab for this item
            if (BankGlossary.ContainsKey(itemName))
            {
                // Find the item in the tab
                var tabnum = BankGlossary[itemName];
                if (IsDebug) Console.WriteLine($"Item belongs to tab {tabnum}");
                switch (tabnum)
                {
                    case 1:
                        return BankTab1.First(i => i.Name == itemName);
                    case 2:
                        return BankTab2.First(i => i.Name == itemName);
                    case 3:
                        return BankTab3.First(i => i.Name == itemName);
                    case 4:
                        return BankTab4.First(i => i.Name == itemName);
                    case 5:
                        return BankTab5.First(i => i.Name == itemName);
                    default:
                        return null;
                }
            }
            else
            {
                // Item not found
                if (true) Console.WriteLine($"Item {itemName} not found in bank");
                return null;
            }
        }


        public void Deposit(Item item)
        {
            if (item == null) return;
            if (IsDebug) Console.WriteLine($"\n~~ Depositing {item.Name}({item.Count}) into bank");
            if (BankGlossary.ContainsKey(item.Name))
            {
                var tabnum = BankGlossary[item.Name];
                switch (tabnum)
                {
                    case 1:
                        DepositItemToTab(BankTab1, item);
                        break;
                    case 2:
                        DepositItemToTab(BankTab2, item);
                        break;
                    case 3:
                        DepositItemToTab(BankTab3, item);
                        break;
                    case 4:
                        DepositItemToTab(BankTab4, item);
                        break;
                    case 5:
                        DepositItemToTab(BankTab5, item);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Find the first not full tab
                if (IsDebug) Console.WriteLine($"Depositing {item.Name} into first available tab");
                if (BankTab1.Count < TabSize)
                {
                    BankGlossary.Add(item.Name, 1);
                    BankTab1.Add(item);
                }
                else if (BankTab2.Count < TabSize && BankTab2Unlocked)
                {
                    BankGlossary.Add(item.Name, 2);
                    BankTab2.Add(item);
                }
                else if (BankTab3.Count < TabSize & BankTab3Unlocked)
                {
                    BankGlossary.Add(item.Name, 3);
                    BankTab3.Add(item);
                }
                else if (BankTab4.Count < TabSize & BankTab4Unlocked)
                {
                    BankGlossary.Add(item.Name, 4);
                    BankTab4.Add(item);
                }
                else if (BankTab5.Count < TabSize & BankTab5Unlocked)
                {
                    BankGlossary.Add(item.Name, 5);
                    BankTab5.Add(item);
                }
                else
                {
                    throw new Exception("Bank is full");
                }
                
            }

            onBankContentsChanged?.Invoke(this, EventArgs.Empty);
        }


        private void DepositItemToTab(List<Item> tab, Item item)
        {
            // check if this item is already in the tab
            if (tab.Any(i => i.Name == item.Name))
            {
                if (IsDebug) Console.WriteLine($"Item {item.Name} already in tab");
                var existingItem = tab.First(i => i.Name == item.Name);
                existingItem.Count += item.Count;
                if (IsDebug) Console.WriteLine($"Added {item.Count} to existing item {existingItem.Name}");
            }
            else
            {
                if (tab.Count < TabSize)
                {
                    tab.Add(item);
                    if (IsDebug) Console.WriteLine($"Added {item.Name} to tab");
                }
                else
                {
                    // TODO: Handle the case when the tab is full
                }
            }
        }

        public void Withdraw(Item item, int count)
        {
            throw new NotImplementedException();

            //BankGlossary.Remove(item);
        }

        public event EventHandler BankContentsChanged
        {
            add => onBankContentsChanged = (EventHandler)Delegate.Combine(onBankContentsChanged, value);
            remove => onBankContentsChanged = (EventHandler)Delegate.Remove(onBankContentsChanged, value);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"TabSize: {TabSize}");
            sb.AppendLine($"BankTab1: {BankTab1.Count}/{TabSize}");
            sb.AppendLine($"BankTab2: {BankTab2.Count}/{TabSize}");
            sb.AppendLine($"BankTab3: {BankTab3.Count}/{TabSize}");
            sb.AppendLine($"BankTab4: {BankTab4.Count}/{TabSize}");
            sb.AppendLine($"BankTab5: {BankTab5.Count}/{TabSize}");
            sb.AppendLine($"BankTab2Unlocked: {BankTab2Unlocked}");
            sb.AppendLine($"BankTab3Unlocked: {BankTab3Unlocked}");
            sb.AppendLine($"BankTab4Unlocked: {BankTab4Unlocked}");
            sb.AppendLine($"BankTab5Unlocked: {BankTab5Unlocked}");

            return sb.ToString();
        }
    }
}
