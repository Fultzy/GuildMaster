using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleGame.Models;
using IdleGame.Properties;
using IdleGame.Services;
using MaterialSkin.Controls;

namespace IdleGame.UI
{
    public class ItemCard : Panel
    {
        public MaterialLabel _label;
        public PictureBox _pictureBox;
        public Item _item;
        private bool _isLarge;

        public ItemCard()
        {
            this.Size = new System.Drawing.Size(64, 80);

            // setup picture box
            PictureBox pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(0, 0),
                Size = new Size(64, 64),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            _pictureBox = pictureBox;
            this.Controls.Add(pictureBox);

            // setup label
            _label = new MaterialLabel
            {
                FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Size = new Size(50, 20),
                TextAlign = ContentAlignment.TopCenter,
                RightToLeft = RightToLeft.No,
                Location = new Point(10, 61),
                Dock = DockStyle.Bottom,
                AutoEllipsis = true,

            };

            _label.Text = Count;
            this.Controls.Add(_label);

            // bring label to the front
            _label.BringToFront();

            // create click event
            this.Click += ItemCard_Click;
            this._label.Click += ItemCard_Click;
            this._pictureBox.Click += ItemCard_Click;
        }


        public delegate void ItemCardClickedHandler(object sender, EventArgs e);
        public event ItemCardClickedHandler ItemCardClicked;
        
        private void ItemCard_Click(object sender, EventArgs e)
        {
            ItemCardClicked?.Invoke(this, e);
        }

        [Browsable(true)]
        public string Count
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
                _label.Invalidate();
            }
        }

        [Browsable(true)]
        public bool ShowCount
        {
            get
            {
                return _label.Visible;
            }
            set
            {
                _label.Visible = value;
                _label.Invalidate();
            }
        }

        [Browsable(true)]
        public bool IsLarge
        {
            get
            {
                return _isLarge;
            }
            set
            {
                _isLarge = value;
                if (_isLarge)
                {
                    this.Size = new Size(128, 160);
                    _pictureBox.Size = new Size(128, 128);
                    _label.Size = new Size(100, 20);
                    _label.Location = new Point(14, 131);
                    _label.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
                    if (_item == null) Image = null;
                }
                else
                {
                    this.Size = new Size(64, 80);
                    _pictureBox.Size = new Size(64, 64);
                    _label.Size = new Size(50, 20);
                    _label.Location = new Point(10, 61);
                    _label.FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis;
                }
            }

            
        }

        [Browsable(true)]
        public Image Image
        {
            get
            {
                return _pictureBox.Image;
            }
            set
            {
                _pictureBox.Image = value;
                _pictureBox.Invalidate();
            }
        }

        [Browsable(false)]
        public Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                
                if (_item == null)
                {
                    Image = null;
                    Count = "";
                    return;
                }
                else
                {
                    // get image
                    Image = ImageService.GetItemImage(_item.ImageName);
                    Count = _item.Count.ToString("n0");
                }
            }
        }

        public void Invalidate()
        {
            _label.Invalidate();
            _pictureBox.Invalidate();
            base.Invalidate();
        }
    }
}
