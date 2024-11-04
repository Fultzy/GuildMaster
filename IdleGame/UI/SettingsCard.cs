using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace IdleGame.UI
{
    public partial class SettingsCard : Panel
    {
        private MaterialButton Card;
        private MaterialButton CloseButton;
        private MaterialButton SaveButton;
        private MaterialLabel TitleLabel;
        private MaterialLabel DescriptionLabel;


        public SettingsCard()
        {
            this.Dock = DockStyle.Fill;
            this.Click += CloseSettingsCard;

            Card = new MaterialButton();
            Card.Text = "Settings";
            Card.Size = new System.Drawing.Size(200, 50);
            Card.Location = new System.Drawing.Point(50, 50);
            Card.Click += CloseSettingsCard;

            CloseButton = new MaterialButton();
            CloseButton.Text = "Close";
            CloseButton.Size = new System.Drawing.Size(200, 50);

            SaveButton = new MaterialButton();
            SaveButton.Text = "Save";
            SaveButton.Size = new System.Drawing.Size(200, 50);
            
            TitleLabel = new MaterialLabel();
            DescriptionLabel = new MaterialLabel();
            
            InitializeComponent();
        }

        public void SettingsCard_Load(object sender, EventArgs e)
        {
           

        }

        private void CloseSettingsCard(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
