using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.Services;

namespace IdleGame
{
    internal static class Program
    {
        public static List<Player> Players { get; set; } // thinking about muli window support

        [STAThread]
        static void Main()
        {
            // idk dude
            Environment.SetEnvironmentVariable("IsTesting", "false");
            Environment.SetEnvironmentVariable("IsDebug", "false");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var characterForm = new CharacterForm();
            characterForm.CharacterSelected += (sender, e) =>
            {
                var character = ((CharacterForm)sender).SelectedPlayer;
                characterForm.Close();
            };

            characterForm.ShowDialog();

            if (characterForm.SelectedPlayer != null)
            {
                var player = SavedDataService.LoadData(characterForm.SelectedPlayer);
                var mainForm = new MainForm(player);

                //save data every 60 seconds
                Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(60000);
                        SavedDataService.SaveData(mainForm.thisPlayer);
                    }
                });

                mainForm.ShowDialog();
            }
        }
    }
}
