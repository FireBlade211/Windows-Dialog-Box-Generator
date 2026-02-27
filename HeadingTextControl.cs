using FireBlade.WinInteropUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Dialog_Box_Generator
{
    public partial class HeadingTextControl : Control
    {
        private Window? _wnd;

        public HeadingTextControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _wnd = Window.FromHandle(Handle);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            try
            {
                if (_wnd != null)
                {
                    using (VisualStyle vs = VisualStyle.OpenThemeData(_wnd, "TEXTSTYLE"))
                    {
                        // Part ID 1 = main instruction
                        vs.DrawThemeText(pe.Graphics, 1, 0, Text, -1, ThemeTextOptions.SingleLine | ThemeTextOptions.AlignLeft | ThemeTextOptions.AlignMiddle,
                            new Rectangle(0, 0, Width, Height));
                    }
                }
            }
            catch
            {

            }
        }
    }
}
