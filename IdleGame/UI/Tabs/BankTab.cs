using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using IdleGame.Models;
using IdleGame.Services;

namespace IdleGame.UI.Tabs
{
    public class BankTab
    {
        public Item SelectedItem { get; set; }
        public int CurrentTab { get; set; }

        public BankTab(MainForm form)
        {
            SelectedItem = new Item();
            CurrentTab = 1;

            form.SellItemCountSlider.Click += SellItemCountSlider_Click;
            form.SellItemButton.Click += SellItemButton_Click;
        }

        public void Update(MainForm form)
        {
            ClearBankView(form);

            // Add items to view and subscribe to events
            foreach (var item in form.thisPlayer.IBanker.BankTab1)
            {
                var itemCard = new ItemCard();
                itemCard.Image = ImageService.GetItemImage(item.ImageName);
                itemCard.Item = item;

                itemCard.ItemCardClicked += OnItemCardClicked;
                form.BankTabFlowPanel.Controls.Add(itemCard);
            }

            // update selected item
            if (SelectedItem != null && SelectedItem.Name != null)
            {
                var bankItem = form.thisPlayer.IBanker.GetItem(SelectedItem.Name);
                
                if (bankItem != null)
                {
                    Console.WriteLine("Item found in bank");
                    SelectedItem = bankItem;
                }
                else
                {
                    Console.WriteLine("Item not found in bank");
                    SelectedItem = null;
                }
            }

            SetSelectedItem(form);
        }

        private void OnItemCardClicked(object sender, EventArgs e)
        {
            var clickedItemCard = (ItemCard)sender;
            var form = clickedItemCard.FindForm() as MainForm;

            if (SelectedItem == clickedItemCard.Item)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = clickedItemCard.Item;
            }
            
            // Update UI
            SetSelectedItem(form);
        }

        private void SellItemCountSlider_Click(object sender, EventArgs e)
        {
            var form = (MainForm)((System.Windows.Forms.TrackBar)sender).FindForm();
            form.SellSliderItemCountLabel.Text = form.SellItemCountSlider.Value.ToString();
            form.SellSliderValueLabel.Text = (form.SellItemCountSlider.Value * SelectedItem.Value).ToString();
        }

        private void SellItemButton_Click(object sender, EventArgs e)
        {
            var form = (MainForm)((System.Windows.Forms.Button)sender).FindForm();
            var player = form.thisPlayer;

            if (SelectedItem == null)
            {
                return;
            }

            var itemCount = form.SellItemCountSlider.Value;
            var itemValue = SelectedItem.Value;

            var totalValue = itemCount * itemValue;

            player.Coins += totalValue;
            player.IBanker.Withdraw(SelectedItem, itemCount);

            // Notify the player
            var message = $"Sold {itemCount} {SelectedItem.Name}(s) for {totalValue} coins";
            Notifyer.Notify(message);

            // Update UI
            Update(form);
        }

        public void ClearBankView(MainForm form)
        {
            // unsubscribe from events
            foreach (ItemCard itemCard in form.BankTabFlowPanel.Controls)
            {
                itemCard.ItemCardClicked -= OnItemCardClicked;
                //ItemCard.Dispose(); TODO: Implement dispose
            }

            // clear view
            form.BankTabFlowPanel.Controls.Clear();
        }

        public void SetSelectedItem(MainForm form)
        {
            if (SelectedItem == null)
            {
                ClearSelectedItem(form);
                return;
            }

            form.SelectedItemCard.Item = SelectedItem;
            form.SelectedItemCard._label.Text = "";

            // format selected item message
            var suf = SelectedItem.Count > 1 ? "s" : "";
            var coinSuf = SelectedItem.Value > 1 ? "s" : "";
            var itemNameMessage = $"{SelectedItem.Count} {SelectedItem.Name}{suf}";
            var itemValueMessage = $"{SelectedItem.Value} coin({coinSuf})";


            form.ItemNameLabel.Text = itemNameMessage;
            form.ItemValueLabel.Text = itemValueMessage;

            // format Sell Item Slider
            form.SellItemCountSlider.Value = SelectedItem.Count;
            form.SellSliderItemCountLabel.Text = itemNameMessage;
        }

        public void ClearSelectedItem(MainForm form)
        {
            form.SelectedItemCard.Item = null;
            form.ItemNameLabel.Text = "";
            //MainForm.Instance().ItemDescriptionLabel.Text = "";
            form.ItemValueLabel.Text = "";
            form.ItemRarityLable.Text = "";
        }
    }
}
