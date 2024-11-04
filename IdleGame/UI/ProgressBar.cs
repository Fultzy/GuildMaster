using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace IdleGame.UI
{
    /// <summary>
    /// Material design-like progress bar
    /// </summary>
    public class MaterialProgressBar : ProgressBar, IMaterialControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProgressBar"/> class.
        /// </summary>
        public MaterialProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.Height = 20;
        }

        private MaterialLabel _label = new MaterialLabel();

        /// <summary>
        /// Sets the value deciding if the label should be shown.
        /// </summary>
        /// <value>
        /// bool
        /// </value>
        [Browsable(true)]
        public bool ShowText
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [Browsable(false)]
        public int Depth
        {
            get; set;
        }

        // allows for devs to change the color of the progress bar
        [Browsable(true)]
        public Color ProgressColor
        {
            get; set;
        }

        // allows for devs to change the color of the backing of the progress bar
        [Browsable(true)]
        public Color BackingColor
        {
            get; set;
        }

        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>
        /// The skin manager.
        /// </value>
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        [Browsable(false)]
        public MouseState MouseState
        {
            get; set;
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 15, specified);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // make brushes for progress bar
            var fillBrush = new SolidBrush(ProgressColor);
            var emptyBrush = new SolidBrush(BackingColor);

            var doneProgress = (int)(e.ClipRectangle.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(fillBrush, 0, 0, doneProgress, e.ClipRectangle.Height);
            e.Graphics.FillRectangle(emptyBrush, doneProgress, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);

            // draw the text
            if (ShowText)
            {   
                _label.AutoSize = true;
                _label.Text = this.Text;
                _label.Location = new Point(Width / 2 - _label.Width / 2, (Height / 2 - _label.Height / 2)+2);
                
                this.Controls.Add(_label);
            }
        }
    }
}