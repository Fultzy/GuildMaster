using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.UI.Tabs;
using MaterialSkin;
using MaterialSkin.Controls;

namespace IdleGame.UI
{
    public class UIService
    {
        public UISettings Settings { get; set; }

        // interfaces
        public HomeTab IHomeTab { get; set; }
        public BankTab IBankTab { get; set;}
        public WoodCuttingTab IWoodCuttingTab { get; set; }

        public UIService(MainForm form) 
        {
            IHomeTab = new HomeTab();
            IBankTab = new BankTab(form);
            IWoodCuttingTab = new WoodCuttingTab(form);
            Settings = form.thisPlayer.UISettings;
        }

        public void Load(MainForm form)
        {
            var player = form.thisPlayer;
            // Load Darkmode
            form.DarkModeSwitch.Checked = Settings.Darkmode;
            CheckDarkmode(Settings.Darkmode, form);

            // Set previous tab
            form.MainTabControl.SelectedIndex = Settings.CurrentTabIndex;

            // event subscriptions
            form.MainTabControl.SelectedIndexChanged += MainTabControl_TabIndexChanged;

            form.thisPlayer.IBanker.BankContentsChanged += (sender, e) => UpdateBankTab(form);


            UpdateUI(form);
        }

        public void UpdateUI(MainForm form)
        {
            // update all tabs
            UpdateHomeTab(form);
            UpdateWoodCuttingTab(form);
            UpdateBankTab(form);
        }

        public void UpdateBankTab(MainForm form)
        {
            if (IBankTab == null)
            {
                IBankTab = new BankTab(form);
            }

            IBankTab.Update(form);
        }

        public void UpdateHomeTab(MainForm form)
        {
            if (IHomeTab == null)
            {
                IHomeTab = new HomeTab();
            }

            IHomeTab.Update(form);
        }

        public void UpdateWoodCuttingTab(MainForm form)
        {
            if (IWoodCuttingTab == null)
            {
                IWoodCuttingTab = new WoodCuttingTab(form);
            }

            IWoodCuttingTab.Update(form);
        }

        public void CheckDarkmode(bool enable, MainForm form)
        {
            if (enable)
            {
                MaterialSkinManager.Instance.Theme = MaterialSkinManager.Themes.DARK;
                Settings.Darkmode = true;
                form.DarkModeIconPicturebox.Image = Properties.Resources.DarkModeMoon;
                form.thisPlayer.UISettings.Darkmode = true;
                form.Invalidate();
            }
            else
            {
                MaterialSkinManager.Instance.Theme = MaterialSkinManager.Themes.LIGHT;
                Settings.Darkmode = false;
                form.DarkModeIconPicturebox.Image = Properties.Resources.LightModeSun;
                form.thisPlayer.UISettings.Darkmode = false;
                form.Invalidate();
            }
        }

        // for saving the current tab index between runs
        private void MainTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            Settings.CurrentTabIndex = ((MaterialTabControl)sender).SelectedIndex;
        }
    }
}
