using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImgProc.MyControls
{
    [ToolboxItem(true)]
    internal class MyGradientTitleBar : Label
    {
        static Color defaultGradientBeginColor = SystemColors.ActiveCaption;
        static Color defaultGradientEndColor = SystemColors.GradientActiveCaption;
        static Color defaultBackColor = SystemColors.ActiveCaption;
        static Color defaultForeColor = SystemColors.ActiveCaptionText;
        static Font defaultFont = SystemFonts.CaptionFont;
        Color gradientBeginColor;
        Color gradientEndColor;
        const DockStyle defaultDock = DockStyle.Top;
        const ContentAlignment defaultTextAlign = ContentAlignment.MiddleLeft;

        public MyGradientTitleBar()
        {
            ResetGradientBeginColor();
            ResetGradientEndColor();
            ResetBackColor();
            ResetForeColor();
            ResetFont();
            TextAlign = defaultTextAlign;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Brush b = null;
            try
            {
                b = new LinearGradientBrush(new Point(0, 0), new Point(this.ClientRectangle.Width, 0), GradientBeginColor, GradientEndColor);
                pevent.Graphics.FillRectangle(b, this.ClientRectangle);
            }
            finally
            {
                if (b != null)
                {
                    b.Dispose();
                }
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();

            Dock = defaultDock;
        }

        #region GradientBeginColor property

        public Color GradientBeginColor
        {
            get
            {
                return gradientBeginColor;
            }
            set
            {
                gradientBeginColor = value;
            }
        }

        public void ResetGradientBeginColor()
        {
            GradientBeginColor = defaultGradientBeginColor;
        }

        public bool ShouldSerializeGradientBeginColor()
        {
            return GradientBeginColor != defaultGradientBeginColor;
        }

        #endregion GradientBeginColor property

        #region GradientEndColor property

        public Color GradientEndColor
        {
            get
            {
                return gradientEndColor;
            }
            set
            {
                gradientEndColor = value;
            }
        }

        public void ResetGradientEndColor()
        {
            GradientEndColor = defaultGradientEndColor;
        }

        public bool ShouldSerializeGradientEndColor()
        {
            return GradientEndColor != defaultGradientEndColor;
        }

        #endregion GradientEndColor property

        #region BackColor property

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        public override void ResetBackColor()
        {
            BackColor = defaultBackColor;
        }

        public bool ShouldSerializeBackColor()
        {
            return BackColor != defaultBackColor;
        }

        #endregion BackColor property

        #region ForeColor property

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public override void ResetForeColor()
        {
            ForeColor = defaultForeColor;
        }

        public bool ShouldSerializeForeColor()
        {
            return ForeColor != defaultForeColor;
        }

        #endregion ForeColor property

        #region Font property

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public override void ResetFont()
        {
            Font = defaultFont;
        }

        public bool ShouldSerializeFont()
        {
            return Font != defaultFont;
        }

        #endregion Font property

        #region Dock property

        [DefaultValue(DockStyle.Top)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        #endregion Dock property

        #region TextAlign property

        [DefaultValue(defaultTextAlign)]
        public override ContentAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                base.TextAlign = value;
            }
        }

        #endregion TextAlign property

        public void Activate()
        {
            GradientBeginColor = SystemColors.ActiveCaption;
            GradientEndColor = SystemColors.GradientActiveCaption;
            ForeColor = SystemColors.ActiveCaptionText;
            this.Invalidate();
        }

        public void Deactivate()
        {
            GradientBeginColor = SystemColors.InactiveCaption;
            GradientEndColor = SystemColors.GradientInactiveCaption;
            ForeColor = SystemColors.InactiveCaptionText;
            this.Invalidate();
        }
    }
}
