using System.Runtime.InteropServices;

namespace Windows_Dialog_Box_Generator
{
    /// <summary>
    /// Wraps SHGetStockIconInfo. Unlike <see cref="SystemIcons"/>, this allows you to pass any icon ID you want, so it's not limited to only the <see cref="StockIconId"/> enum.
    /// </summary>
    public class StockIconHelper
    {
        const uint SHGSI_ICON = 0x000000100;
        const uint SHGSI_SMALLICON = 0x000000001;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysImageIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        [DllImport("shell32.dll", SetLastError = false)]
        static extern int SHGetStockIconInfo(
            int siid,
            uint uFlags,
            ref SHSTOCKICONINFO psii);

        public static Icon GetIcon(int stockIconId, int size = 16)
        {
            var info = new SHSTOCKICONINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);

            uint flags = SHGSI_ICON | SHGSI_SMALLICON;
            if (size > 16)
                flags = SHGSI_ICON; // will get large icon

            int hr = SHGetStockIconInfo(stockIconId, flags, ref info);
            if (hr != 0)
                Marshal.ThrowExceptionForHR(hr);

            Icon icon = (Icon)Icon.FromHandle(info.hIcon).Clone();
            DestroyIcon(info.hIcon);
            return icon;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);
    }
}
