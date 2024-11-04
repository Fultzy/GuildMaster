using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Jobs;
using IdleGame.Models;
using IdleGame.Services;
using MaterialSkin.Controls;

namespace IdleGame.UI.Tabs
{
    public class WoodCuttingTab
    {
        private bool IsDebug = Environment.GetEnvironmentVariable("IsDebug") == "true";
        public string CurrentTreeString { get; set; }
        private RadioButton CurrentTree;

        public WoodCuttingTab(MainForm form)
        {
            //button events
            form.ToggleWoodCuttingButton.Click += ToggleWoodCuttingButton_Click;

            // radio Button events
            // TODO: Automate this entire control using joblist
            form.OakTreeCheckbox.Click += OakTreeCheckbox_Click;
            form.BirchTreeCheckbox.Click += BirchTreeCheckbox_Click;
            form.MapleTreeCheckbox.Click += MapleTreeCheckbox_Click;
            
            // default tree
            Load(form);
        }

        public void Update(MainForm form)
        {
            if (!form.IsDisposed) // prevent error on close
            {
                var player = form.thisPlayer;
                form?.Invoke((MethodInvoker)delegate
                {
                    // Update XP Progress bar
                    var lastLevelXP = player.CalculateNextLevelXP(player.WoodCuttingLevel - 1);
                    var adjustedXP = player.WoodCuttingXP - lastLevelXP;
                    var adjustedNextLevelXP = player.WoodCuttingXPToNextLevel - lastLevelXP;

                    var progress = (int)((double)adjustedXP / adjustedNextLevelXP * 100);

                    form.WoodCuttingXpProgressBar.Value = progress;

                    if (IsDebug) Console.WriteLine($"woodcuttingXPbar value: {progress}/100 = {adjustedXP}/{adjustedNextLevelXP} * 100");

                    form.WoodCuttingXpProgressBar.Text = $"Level: {player.WoodCuttingLevel}  {adjustedXP}/{adjustedNextLevelXP}";

                    // Needs to fix JobService for reward item
                    //form.WoodCuttingTreeLogCountLable.Text = Player.Instance.IBanker.GetItemCount("Oak Log").ToString();
                });
            }
        }

        private void ToggleWoodCuttingButton_Click(object sender, EventArgs e)
        {
            var button = (MaterialButton)sender;
            var form = button.FindForm() as MainForm;
            var manager = form.thisPlayer.IJob;

            if (manager.CurrentJob.Type == "WoodCutting")
            {
                manager.StopCurrentJob();
                button.Text = "Start";
                button.UseAccentColor = false;
            }
            else
            {
                var job = manager.GetJobFor(CurrentTreeString).CreateNew();
                manager.StartJob(job);

                button.Text = "Stop";
                button.UseAccentColor = true;
            }
        }

        // temporary solution
        private void OakTreeCheckbox_Click(object sender, EventArgs e)
        {
            var control = (Control)sender;
            var form = control.FindForm() as MainForm;
            var modifyer = 0;
            CurrentTree = (RadioButton)sender;
            CurrentTree.Checked = true;
            CurrentTreeString = "Oak Tree";
            form.thisPlayer.UISettings.CurrentTreeString = CurrentTreeString;
            
            form.BirchTreeCheckbox.Checked = false;
            form.MapleTreeCheckbox.Checked = false;
            //form.WillowTreeCheckbox.Checked = false;
            //form.YewTreeCheckbox.Checked = false;
            //form.MagicTreeCheckbox.Checked = false;

            form.WoodCuttingintervalLabel.Text = $"Time: {(double)(form.thisPlayer.IJob.CurrentJob.Duration + modifyer)/ 1000}s";

            form.SelectedTreePictureBox.Image = form.OakTreePictureBox.Image;
            form.SelectedTreeLabel.Text = CurrentTreeString;
        }

        // temporary solution
        private void BirchTreeCheckbox_Click(object sender, EventArgs e)
        {
            var control = (Control)sender;
            var form = control.FindForm() as MainForm;
            var modifyer = 1000;
            CurrentTree = (RadioButton)sender;
            CurrentTree.Checked = true;
            CurrentTreeString = "Birch Tree";
            form.thisPlayer.UISettings.CurrentTreeString = CurrentTreeString;

            form.OakTreeCheckbox.Checked = false;
            form.MapleTreeCheckbox.Checked = false;
            //form.WillowTreeCheckbox.Checked = false;
            //form.YewTreeCheckbox.Checked = false;
            //form.MagicTreeCheckbox.Checked = false;

            form.WoodCuttingintervalLabel.Text = $"Time: {(double)(form.thisPlayer.IJob.CurrentJob.Duration + modifyer) / 1000}s";

            form.SelectedTreePictureBox.Image = form.BirchTreePictureBox.Image;
            form.SelectedTreeLabel.Text = CurrentTreeString;
        }
        
        // temporary solution
        private void MapleTreeCheckbox_Click(object sender, EventArgs e)
        {
            var control = (Control)sender;
            var form = control.FindForm() as MainForm;
            var modifyer = 2000;
            CurrentTree = (RadioButton)sender;
            CurrentTree.Checked = true;
            CurrentTreeString = "Maple Tree";
            form.thisPlayer.UISettings.CurrentTreeString = CurrentTreeString;

            form.OakTreeCheckbox.Checked = false;
            form.BirchTreeCheckbox.Checked = false;
            //form.WillowTreeCheckbox.Checked = false;
            //form.YewTreeCheckbox.Checked = false;
            //form.MagicTreeCheckbox.Checked = false;

            form.WoodCuttingintervalLabel.Text = $"Time: {(double)(form.thisPlayer.IJob.CurrentJob.Duration + modifyer) / 1000}s";

            form.SelectedTreePictureBox.Image = form.MapleTreePictureBox.Image;
            form.SelectedTreeLabel.Text = CurrentTreeString;
        }

        public void Load(MainForm form)
        {
            CurrentTreeString = form.thisPlayer.UISettings.CurrentTreeString;
            if (IsDebug) Console.WriteLine("Current Tree: " + CurrentTreeString);
            
            switch (CurrentTreeString)
            {
                case "Oak Tree":
                    OakTreeCheckbox_Click(form.OakTreeCheckbox, null);
                    break;
                
                case "Birch Tree":
                    BirchTreeCheckbox_Click(form.BirchTreeCheckbox, null);
                    break;

                case "Maple Tree":
                    MapleTreeCheckbox_Click(form.MapleTreeCheckbox, null);
                    break;

                default:
                    OakTreeCheckbox_Click(form.OakTreeCheckbox, null);
                    break;
            }
        }
    }
}
