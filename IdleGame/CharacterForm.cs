using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.Services;
using IdleGame.UI;
using MaterialSkin;
using MaterialSkin.Controls;

namespace IdleGame
{
    public partial class CharacterForm : MaterialForm
    {
        // Event for Character Selected
        public Player SelectedPlayer = null;
        public event EventHandler CharacterSelected;
        private SettingsCard SettingsCard;

        public CharacterForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey700, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Red700, TextShade.WHITE);
        }

        private void CharacterForm_Load(object sender, EventArgs e)
        {
            // Load all saved characters
            var characters = LoadSavedCharacters();
            SettingsCard = new SettingsCard();

            // Add SavedCharacterCards to the SavedCharacterFlowPanel
            SavedCharacterFlowPanel.Controls.Clear();
            foreach (var character in characters)
            {
                var savedCharacterCard = new SavedCharacterCard(character);
                savedCharacterCard.CharacterCardSelected += SavedCharacterCard_CharacterSelected;
                savedCharacterCard.ClearSaveFile += SavedCharacterCard_ClearSaveFile;

                SavedCharacterFlowPanel.Controls.Add(savedCharacterCard);
            }

            // add empty savedcharactercard for new save slot
            var emptySavedCharacterCard = new SavedCharacterCard();
            emptySavedCharacterCard.CreateSaveFile += SavedCharacterCard_CreateCharacter;
            SavedCharacterFlowPanel.Controls.Add(emptySavedCharacterCard);

            DeleteProtectionSwitch_CheckedChanged(sender, e);
        }

        private void SavedCharacterCard_ClearSaveFile(object sender, EventArgs e)
        {
            var savedCharacterCard = (SavedCharacterCard)sender;
            SavedDataService.DeleteData(savedCharacterCard.Player);
            SavedCharacterFlowPanel.Controls.Remove(savedCharacterCard);
        }

        private void SavedCharacterCard_CreateCharacter(object sender, EventArgs e) 
        {
            // create dialog for player name
            var dialog = new MaterialCard()
            {
                Size = new System.Drawing.Size(300, 200),
                Location = new System.Drawing.Point(50, 50),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Bounds = new System.Drawing.Rectangle(50, 50, 300, 200),
                Name = "CreateCharacterDialog"
            };

            var dialogLabel = new MaterialLabel()
            {
                Text = "Enter Player Name",
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(200, 50)
            };

            var dialogTextBox = new MaterialTextBox()
            {
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(200, 50),
                MaxLength = 40,
            };

            var dialogButton = new MaterialButton()
            {
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(200, 50),
                Text = "Create"
            };

            var cancelButton = new MaterialButton()
            {
                Location = new System.Drawing.Point(150, 150),
                Size = new System.Drawing.Size(200, 50),
                Text = "Cancel",
                UseAccentColor = true,
            };

            dialogButton.Click += (sender1, e1) =>
            {
                var characterName = dialogTextBox.Text;



                if (Validator.IsCharacterNameValid(characterName))
                {
                    CreateNewCharacter(characterName);
                    dialog.Dispose();
                }
            };

            cancelButton.Click += (sender1, e1) =>
            {
                dialog.Dispose();
            };

            dialog.Controls.Add(dialogLabel);
            dialog.Controls.Add(dialogTextBox);
            dialog.Controls.Add(dialogButton);
            dialog.Controls.Add(cancelButton);
            Controls.Add(dialog);

            dialog.BringToFront();
        }

        private void CreateNewCharacter(string characterName)
        {
            var character = new Player();
            character.Name = characterName;
            SavedDataService.SaveData(character);
            CharacterForm_Load(this, new EventArgs());
        }

        private async void SavedCharacterCard_CharacterSelected(object sender, EventArgs e)
        {
            var savedCharacterCard = (SavedCharacterCard)sender;
            SelectedPlayer = savedCharacterCard.Player;
            CharacterSelected?.Invoke(this, e);
        }

        private List<Player> LoadSavedCharacters()
        {
            // Load saved characters from file
            // test DATA
            var saves = SavedDataService.GetSavedDataList();

            // sort list by last save
            saves.Sort((y, x) => DateTime.Compare(x.LastSave, y.LastSave));


            return saves;
        }

        private void DeleteProtectionSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteProtectionSwitch.Checked)
            {
                // enable all delete buttons
                var savedCharacterCards = SavedCharacterFlowPanel.Controls;
                foreach (var card in savedCharacterCards)
                {
                    if (card is SavedCharacterCard savedCharacterCard)
                    {
                        if (((SavedCharacterCard)card).Player != null)  savedCharacterCard.ClearSaveButton.Enabled = true;
                    }
                }
                
            }
            else
            {
                // disable all delete buttons
                var savedCharacterCards = SavedCharacterFlowPanel.Controls;
                foreach (var card in savedCharacterCards)
                {
                    if (card is SavedCharacterCard savedCharacterCard)
                    {
                        if (((SavedCharacterCard)card).Player != null) savedCharacterCard.ClearSaveButton.Enabled = false;
                    }
                }

            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (SettingsCard.Visible)
            {
                SettingsCard.Hide();
            }
            else
            {
                SettingsCard.Show();
            }
        }
    }
}
