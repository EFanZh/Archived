using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImgProc.MyControls
{
    internal class MyGrayLine : Control
    {
        static Color defaultBackColor = SystemColors.ControlDarkDark;

        public MyGrayLine()
        {
            ResetBackColor();
            TabStop = false;
        }

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

        #region TabStop property

        [DefaultValue(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        #endregion TabStop property
    }
}
