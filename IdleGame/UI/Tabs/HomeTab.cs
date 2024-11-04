using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;

namespace IdleGame.UI.Tabs
{
    public class HomeTab
    {
        public void Update(MainForm form)
        {
            if (!form.IsDisposed) // prevent error on close
            {
                form.Invoke((MethodInvoker)delegate
                {
                    // Update UI
                    //form.PlayerNameLabel.Text = Player.Instance().Name;
                    //form.LevelLabel.Text = "Level: " + Player.Instance().Level;
                    //form.XPLabel.Text = "XP: " + Player.Instance().XP + "/" + Player.Instance().XPToNextLevel;
                    //form.CoinsLabel.Text = "Coins: " + Player.Instance().Coins;

                    // Player Stats
                    //form.LevelLabel.Text = "Level: " + Player.Instance.Level;
                    //XPLabel.Text = "XP: " + Player.Instance.XP + "/" + Player.Instance.XPToNextLevel;
                    //form.HealthLabel.Text = "Health: " + Player.Instance.Health + "/" + Player.Instance.MaxHealth;
                    //form.AttackLabel.Text = "Attack: " + Player.Instance.Attack;
                    //DefenceLabel.Text = "Defence: " + Player.Instance.Defence;

                    // Skill Levels
                    //form.WoodCuttingLevelLabel.Text = "WoodCutting Level: " + Player.Instance.WoodCuttingLevel;
                    //form.CombatLevelLabel.Text = "Combat Level: " + Player.Instance.CombatLevel;

                });
            }
        }
    }
}
