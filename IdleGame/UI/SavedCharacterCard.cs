using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.Services;
using MaterialSkin.Controls;

namespace IdleGame.UI
{
    public partial class SavedCharacterCard : MaterialSkin.Controls.MaterialCard
    {
        public event EventHandler CharacterCardSelected;
        public event EventHandler ClearSaveFile;
        public event EventHandler CreateSaveFile;
        public SoundPlayer ClickSoundPlayer;
        public Player Player { get; set; }

        // has player
        private MaterialLabel PlayerNameLabel { get; set; }
        private MaterialLabel PlayerLevelLabel { get; set; }
        private MaterialLabel LastLoginLabel { get; set; }
        private MaterialLabel LastJobLabel { get; set; }
        private MaterialLabel TimeSinceLastLoginLabel { get; set; }
        public MaterialButton ClearSaveButton { get; set; }

        // no player
        private MaterialLabel EmptyLabel { get; set; }
        private MaterialButton CreateSaveButton { get; set; }


        public SavedCharacterCard(Player player = null)
        {
            // Base Properties
            this.MinimumSize = new System.Drawing.Size(450, 100);
            this.Size = new System.Drawing.Size(450, 100);
            this.Margin = new Padding(5,5,5,5);

            //Set sound player
            ClickSoundPlayer = new SoundPlayer(Path.Combine(PathService.GetEffectsDirectory(), "ClickPopPositive.wav"));

            if (player == null)
            {
                LoadAsEmpty();
            }
            else
            {
                LoadWithData(player);
            }

            InitializeComponent();
        }

        private async void HandleClickEvent(object sender, EventArgs e)
        {
            if (Player != null)
            {
                // Play the click sound
                ClickSoundPlayer.Play();
                this?.CharacterCardSelected(this, e);
            }
        }

        private void HandleMouseLeaveEvent(object sender, EventArgs e)
        {
            if (Player != null)
            {

            }
        }

        private void HandleMouseOverEvent(object sender, EventArgs e)
        {
            if (Player != null)
            {

            }
        }

        private void CreateSaveButton_Click(object sender, EventArgs e)
        {
            CreateSaveFile?.Invoke(this, e);
        }

        private void ClearSaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
            {
                ClearSaveFile?.Invoke(this, e);
            }
        }

        private void LoadAsEmpty()
        {
            EmptyLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                FontType = MaterialSkin.MaterialSkinManager.fontType.H5,
                Location = new System.Drawing.Point(30, 30),
                Name = "emptyLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = "~ Create New Character ~",
                HighEmphasis = false
            };

            CreateSaveButton = new AnimatedButton
            {
                Location = new System.Drawing.Point(365, 15),
                Name = "createSaveButton",
                Size = new System.Drawing.Size(65, 65),
                Text = "+",
                UseAccentColor = false,
                AutoSize = false
            };

            CreateSaveButton.Click += CreateSaveButton_Click;

            Controls.Add(EmptyLabel);
            Controls.Add(CreateSaveButton);
        }

        private void LoadWithData(Player player)
        {
            this.Player = player;

            PlayerNameLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                FontType = MaterialSkin.MaterialSkinManager.fontType.H6,
                Location = new System.Drawing.Point(8, 5),
                Name = "playerNameLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = player.Name,
            };

            PlayerLevelLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis,
                Location = new System.Drawing.Point(8, 29),
                Name = "playerLevelLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = "Level : " + player.Level,
            };

            LastLoginLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis,
                Location = new System.Drawing.Point(150, 29),
                Name = "lastLoginLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = "Last Login : " + player.LastSave,
            };

            LastJobLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                Location = new System.Drawing.Point(13, 43),
                Name = "lastJobLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = "Has been " + player.IJob.CurrentJob.Name,
                UseAccent = false
            };

            var timeSinceDateTime = DateTime.Now - player.LastSave;
            TimeSinceLastLoginLabel = new MaterialSkin.Controls.MaterialLabel
            {
                AutoSize = true,
                Location = new System.Drawing.Point(45, 62),
                Name = "timeSinceLastLoginLabel",
                Size = new System.Drawing.Size(35, 19),
                Text = $"for {timeSinceDateTime.Hours} hours and {timeSinceDateTime.Minutes} minutes",
                UseAccent = false
            };

            ClearSaveButton = new MaterialSkin.Controls.MaterialButton
            {
                Location = new System.Drawing.Point(390, 48),
                Name = "clearSaveButton",
                Size = new System.Drawing.Size(42, 36),
                Text = "X",
                UseAccentColor = true,
                AutoSize = false
            };

            ClearSaveButton.Click += ClearSaveButton_Click;

            PlayerNameLabel.Click += HandleClickEvent;
            PlayerLevelLabel.Click += HandleClickEvent;
            LastLoginLabel.Click += HandleClickEvent;
            LastJobLabel.Click += HandleClickEvent;
            TimeSinceLastLoginLabel.Click += HandleClickEvent;

            // subscribe to animation events
            this.MouseEnter += HandleMouseOverEvent;
            this.MouseLeave += HandleMouseLeaveEvent;

            // subscribe to click events
            this.Click += HandleClickEvent;

            Controls.Add(PlayerLevelLabel);
            Controls.Add(PlayerNameLabel);
            Controls.Add(LastLoginLabel);
            Controls.Add(LastJobLabel);
            Controls.Add(TimeSinceLastLoginLabel);
            Controls.Add(ClearSaveButton);

        }
    }
}
