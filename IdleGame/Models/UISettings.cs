using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleGame.Models
{
    public class UISettings
    {
        // TODO: Add saved ui settings here

        // main form 
        public int CurrentTabIndex = 0;
        public bool Darkmode = true;

        // Bank Tab
        public Item SelectedBankItem = null;
        public int CurrentBankTab = 1;

        // Woodcutting Tab
        public string CurrentTreeString = "Oak Tree";

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"CurrentTabIndex: {CurrentTabIndex}");
            sb.AppendLine($"Darkmode: {Darkmode}");
            sb.AppendLine($"SelectedBankItem: {SelectedBankItem}");
            sb.AppendLine($"CurrentBankTab: {CurrentBankTab}");
            sb.AppendLine($"CurrentTreeString: {CurrentTreeString}");
            
            return sb.ToString();
        }
    }
}
