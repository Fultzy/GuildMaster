using System;
using MaterialSkin;
using MaterialSkin.Controls;
using IdleGame.Services;
using IdleGame.Models;
using IdleGame.UI;
using IdleGame.Properties;
using System.Windows.Forms;
using System.Drawing;

namespace IdleGame
{
    public partial class MainForm : MaterialForm
    {
        private FlowLayoutPanel NotificationPanel;
        public UIService IUIService;
        public Player thisPlayer;

        public MainForm(Player player)
        {
            InitializeComponent();
            InitializeNotificationPanel();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey700, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Red700, TextShade.WHITE);
            thisPlayer = player;

            this.Text = $"Idle Game - {thisPlayer.Name}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load Tabs
            IUIService = new UIService(this);
            IUIService.Settings = thisPlayer.UISettings;
            IUIService.Load(this);
        }

        private void InitializeNotificationPanel()
        {
            NotificationPanel  = new FlowLayoutPanel
            {
                Width = 300, // Set the desired width
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(0),
                Anchor = AnchorStyles.Bottom,
            };

            // Optional: Add a border for better visibility
            NotificationPanel.BorderStyle = BorderStyle.FixedSingle;

            // Position the notification panel initially (to be updated later)
            NotificationPanel.Location = new Point((this.ClientSize.Width - NotificationPanel.Width) / 2, this.ClientSize.Height - NotificationPanel.Height - 30);

            NotificationPanel.ControlRemoved += CheckHide();
            this.Controls.Add(NotificationPanel);
        }

        private ControlEventHandler CheckHide()
        {
            Console.WriteLine("Checking if notification panel is empty");
            return (sender, e) =>
            {
                if (NotificationPanel.Controls.Count == 0)
                {
                    NotificationPanel.Visible = false;
                }
                else
                {
                    NotificationPanel.Visible = true;
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            if (NotificationPanel == null)
            {
                base.OnResize(e);
                return;
            }

            // Update notification panel position when main form is resized
            NotificationPanel.Location = new Point((this.ClientSize.Width - NotificationPanel.Width) / 2 + 95, this.ClientSize.Height - NotificationPanel.Height - 30);
            base.OnResize(e);
        }

        // Buttons
        private void SaveButton_Click(object sender, EventArgs e)
        {
            thisPlayer.WriteAll();
            SavedDataService.SaveData(thisPlayer);
        }

        private void DarkModeSwitch_Clicked(object sender, EventArgs e)
        {
            IUIService.CheckDarkmode(DarkModeSwitch.Checked, this);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            var settingsCard = new SettingsCard();
            this.Controls.Add(settingsCard);
        }

        private void NotifyMeDadduButton_Click(object sender, EventArgs e)
        {
            var message = "You Clicked a button!";
            var title = "Congrats!";
            Notifyer.Notify(message);
        }

        private void SellItemCountSlider_Click(object sender, EventArgs e)
        {
            SellSliderItemCountLabel.Text = SellItemCountSlider.Value.ToString();
            SellSliderValueLabel.Text = (SellItemCountSlider.Value * this.IUIService.IBankTab.SelectedItem.Value).ToString();
        }

        private void SellItemButton_Click(object sender, EventArgs e)
        {

        }
    }
}
