using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Xml;

namespace Windows_Dialog_Box_Generator
{
    public static class UIHelper
    {
        public static RadioButton? GetCheckedRadioButton(Control container)
        {
            foreach (var c in container.Controls)
            {
                if (c is RadioButton radio)
                {
                    if (radio.Checked)
                    {
                        return radio;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Automatically sets a radio button to be checked with exclusive behavior.
        /// </summary>
        /// <remarks>
        /// Unlike setting <see cref="RadioButton.Checked"/> directly, this method automatically unchecks the other RadioButtons in the parent container. This overload auto-detects the container from the parent of the RadioButton to be checked.
        /// </remarks>
        /// <param name="target">The target radio button to be checked.</param>
        public static void SetCheckedRadioButton(RadioButton target)
        {
            if (target.Parent == null) return;

            foreach (var rb in target.Parent.Controls.OfType<RadioButton>())
            {
                rb.Checked = rb == target;
            }
        }

        /// <summary>
        /// Automatically sets a radio button to be checked with exclusive behavior.
        /// </summary>
        /// <remarks>
        /// Unlike setting <see cref="RadioButton.Checked"/> directly, this method automatically unchecks the other RadioButtons in the container.
        /// </remarks>
        /// <param name="container">The container to look for <see cref="RadioButton"/>s in.</param>
        /// <param name="target">The target radio button to be checked.</param>
        public static void SetCheckedRadioButton(Control container, RadioButton target)
        {
            foreach (var rb in container.Controls.OfType<RadioButton>())
            {
                rb.Checked = rb == target;
            }
        }

        /// <summary>
        /// Automatically sets a radio button to be checked with exclusive behavior. This overload allows multiple containers.
        /// </summary>
        /// <remarks>
        /// Unlike setting <see cref="RadioButton.Checked"/> directly, this method automatically unchecks the other RadioButtons in the containers.
        /// </remarks>
        /// <param name="target">The target radio button to be checked.</param>
        /// <param name="containers">The containers to look for <see cref="RadioButton"/>s in.</param>
        public static void SetCheckedRadioButton(RadioButton target, params Control[] containers)
        {
            foreach (var rb in containers.SelectMany(c => c.Controls.OfType<RadioButton>()))
            {
                rb.Checked = rb == target;
            }
        }
    }

    public static class DialogHelper
    {
        [DllImport("shell32.dll", EntryPoint = "PickIconDlg", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PickIconDlg(nint hWnd, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszIconPath, uint cchIconPath, ref int piIconIndex);

        // The Windows API's PickIconDlg allows passing null for the hWnd

        /// <summary>
        /// Declares the <c>MAX_PATH</c> environment variable from the Windows API.
        /// </summary>
        public const int MAX_PATH = 260;

        /// <summary>
        /// Displays a dialog box that allows the user to choose an icon from the selection available embedded in a resource such as an executable or DLL file.
        /// </summary>
        /// <param name="hWnd">The handle of the parent window.</param>
        /// <param name="defIconPath">A string that contains the fully qualified path of the default resource that contains the icons.
        /// You should verify that the path is valid before using it.</param>
        /// <param name="defIconIndex">An integer that specifies the index of the initial selection.</param>
        /// <param name="iconPath">If the user chooses a different resource in the dialog, contains the path of that file when the function returns; otherwise, it contains
        /// the path stored in <paramref name="defIconPath"/>.</param>
        /// <param name="iconIndex">When this function returns successfully, receives the index of the icon that was selected.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        [SupportedOSPlatform("windows5.1")] // Windows XP
        public static bool ShowPickIconDialog(nint hWnd, string defIconPath, int defIconIndex, out string iconPath, out int iconIndex)
        {
            var sb = new StringBuilder(defIconPath, MAX_PATH);
            iconIndex = defIconIndex;

            var result = PickIconDlg(hWnd, sb, (uint)sb.Capacity, ref iconIndex);
            iconPath = sb.ToString();
            return result;
        }
    }

    public enum TDM : uint
    {
        NAVIGATE_PAGE = WM.USER + 101,
        /// <summary>
        ///   "wParam = Button ID"
        /// </summary>
        CLICK_BUTTON = WM.USER + 102,
        /// <summary>
        ///   "wParam = 0 (nonMarque) wParam != 0 (Marquee)"
        /// </summary>
        SET_MARQUEE_PROGRESS_BAR = WM.USER + 103,
        /// <summary>
        ///   "wParam = new progress state"
        /// </summary>
        SET_PROGRESS_BAR_STATE = WM.USER + 104,
        /// <summary>
        ///   "lParam = MAKELPARAM(nMinRange, nMaxRange)"
        /// </summary>
        SET_PROGRESS_BAR_RANGE = WM.USER + 105,
        /// <summary>
        ///   "wParam = new position"
        /// </summary>
        SET_PROGRESS_BAR_POS = WM.USER + 106,
        /// <summary>
        ///   "wParam = 0 (stop marquee), wParam != 0 (start marquee),
        ///   lparam = speed (milliseconds between repaints)"
        /// </summary>
        SET_PROGRESS_BAR_MARQUEE = WM.USER + 107,
        /// <summary>
        ///   "wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)"
        /// </summary>
        SET_ELEMENT_TEXT = WM.USER + 108,
        /// <summary>
        ///   "wParam = Radio Button ID"
        /// </summary>
        CLICK_RADIO_BUTTON = WM.USER + 110,
        /// <summary>
        ///   "lParam = 0 (disable), lParam != 0 (enable), wParam = Button ID"
        /// </summary>
        ENABLE_BUTTON = WM.USER + 111,
        /// <summary>
        ///   "lParam = 0 (disable), lParam != 0 (enable), wParam = Radio Button ID"
        /// </summary>
        ENABLE_RADIO_BUTTON = WM.USER + 112,
        /// <summary>
        ///   "wParam = 0 (unchecked), 1 (checked), lParam = 1 (set key focus)"
        /// </summary>
        CLICK_VERIFICATION = WM.USER + 113,
        /// <summary>
        ///   "wParam = element (TASKDIALOG_ELEMENTS), lParam = new element text (LPCWSTR)"
        /// </summary>
        UPDATE_ELEMENT_TEXT = WM.USER + 114,
        /// <summary>
        ///   "wParam = Button ID, lParam = 0 (elevation not required),
        ///   lParam != 0 (elevation required)"
        /// </summary>
        SET_BUTTON_ELEVATION_REQUIRED_STATE = WM.USER + 115,
        /// <summary>
        ///   "wParam = icon element (TASKDIALOG_ICON_ELEMENTS), lParam = new icon
        ///   (hIcon if TDF_USE_HICON_* was set, PCWSTR otherwise)"
        /// </summary>
        UPDATE_ICON = WM.USER + 116,
    }

    public enum WM : uint
    {
        USER = 0x0400,
        WM_SETICON = 0x0080,
    }

    public static class ObjectExtensions
    {
        // Warning: may throw an exception if the object doesn't have a parameterless constructor
        public static object? Clone(this object obj)
        {
            var t = obj.GetType();
            var n = t.GetConstructor(Type.EmptyTypes)?.Invoke(null);

            foreach (var prop in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.CanRead && prop.CanWrite)
                    prop.SetValue(n, prop.GetValue(obj));
            }

            return n;
        }
    }

    /// <summary>
    /// Maps RID (Resource IDs) to IID (Icon IDs) and allows you to easily convert between them.
    /// </summary>
    public partial class IconResourceMapper
    {
        private readonly List<ushort> _resourceIds = new();

        /// <summary>
        /// Creates a new instance of the <see cref="IconResourceMapper"/> class.
        /// </summary>
        /// <param name="dllPath">The path to the DLL (Dynamic Link Library) to load and map.</param>
        /// <exception cref="Win32Exception">An error occured loading the DLL library.</exception>
        /// <exception cref="NotSupportedException">Could not map resource IDs because non-integer resource names are not supported.</exception>
        public IconResourceMapper(string dllPath)
        {
            IntPtr hModule = LoadLibraryExW(dllPath, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
            if (hModule == IntPtr.Zero)
                throw new Win32Exception();

            try
            {
                EnumResourceNamesW(hModule, RT_GROUP_ICON, (h, t, name, l) =>
                {
                    // Resource IDs can be either integer or string
                    if (IsIntResource(name, out ushort id))
                        _resourceIds.Add(id);
                    else
                        throw new NotSupportedException("Non-integer resource names are not supported.");
                    return true;
                }, IntPtr.Zero);
            }
            finally
            {
                FreeLibrary(hModule);
            }
        }

        /// <summary>
        /// Returns the icon index corresponding to the resource ID.
        /// </summary>
        public int GetIconIndex(ushort resourceId)
        {
            int index = _resourceIds.IndexOf(resourceId);
            if (index == -1)
                throw new ArgumentException($"Resource ID {resourceId} not found in this DLL.");
            return index;
        }

        /// <summary>
        /// Returns the resource ID corresponding to the icon index.
        /// </summary>
        public ushort GetResourceId(int iconIndex)
        {
            if (iconIndex < 0 || iconIndex >= _resourceIds.Count)
                throw new ArgumentOutOfRangeException(nameof(iconIndex));
            return _resourceIds[iconIndex];
        }

        public int Count => _resourceIds.Count;

        #region Win32 API

        private const uint RT_GROUP_ICON = 14;

        private delegate bool EnumResNameProc(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        private static partial IntPtr LoadLibraryExW(string lpFileName, IntPtr hFile, LoadLibraryFlags dwFlags);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeLibrary(IntPtr hModule);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumResourceNamesW(IntPtr hModule, uint lpszType, EnumResNameProc lpEnumFunc, IntPtr lParam);

        private static bool IsIntResource(IntPtr name, out ushort id)
        {
            long val = name.ToInt64();
            if ((val >> 16) == 0)
            {
                id = (ushort)val;
                return true;
            }
            id = 0;
            return false;
        }

        [Flags]
        private enum LoadLibraryFlags : uint
        {
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002
        }

        #endregion
    }

    /// <summary>
    /// Converts an icon index returned by PickIconDlg to an actual resource ID.
    /// </summary>
    public partial class IconIndexMapper
    {
        private readonly List<int> _resourceIds = new();

        public IconIndexMapper(string dllPath)
        {
            // Enumerate icon resource IDs
            EnumResourceNames(new IntPtr(-1), dllPath);
        }

        public int GetResourceIdFromPickIconIndex(int pickIndex)
        {
            if (pickIndex < 0 || pickIndex >= _resourceIds.Count)
                throw new ArgumentOutOfRangeException(nameof(pickIndex));

            return _resourceIds[pickIndex];
        }

        #region Win32

        private delegate bool EnumResNameProc(IntPtr hModule, int lpszType, IntPtr lpszName, IntPtr lParam);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        private static partial IntPtr LoadLibraryExW(string lpFileName, IntPtr hFile, int dwFlags);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeLibrary(IntPtr hModule);

        [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumResourceNamesW(IntPtr hModule, int lpszType, EnumResNameProc lpEnumFunc, IntPtr lParam);

        private const int RT_GROUP_ICON = 14;
        private const int LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

        private void EnumResourceNames(IntPtr dummy, string dllPath)
        {
            IntPtr hModule = LoadLibraryExW(dllPath, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
            if (hModule == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            try
            {
                EnumResourceNamesW(hModule, RT_GROUP_ICON, (h, t, name, l) =>
                {
                    // Resource IDs can be int or string (if MAKEINTRESOURCE not used)
                    if (((long)name >> 16) == 0) // integer ID
                    {
                        _resourceIds.Add(name.ToInt32());
                    }
                    return true;
                }, IntPtr.Zero);
            }
            finally
            {
                FreeLibrary(hModule);
            }
        }

        #endregion
    }

    public static partial class IconHelper
    {
        private const uint RT_GROUP_ICON = 14;
        private const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;

        private delegate bool EnumResNameProc(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);

        [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
        private static partial IntPtr LoadLibraryExW(string lpFileName, IntPtr hFile, uint dwFlags);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeLibrary(IntPtr hModule);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumResourceNamesW(IntPtr hModule, uint lpszType, EnumResNameProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Returns the number of icons contained in the specified file.
        /// </summary>
        public static int CountIcons(string filePath)
        {
            IntPtr hModule = LoadLibraryExW(filePath, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
            if (hModule == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            int count = 0;

            try
            {
                EnumResourceNamesW(hModule, RT_GROUP_ICON, (h, t, name, l) =>
                {
                    count++;
                    return true;
                }, IntPtr.Zero);
            }
            finally
            {
                FreeLibrary(hModule);
            }

            return count;
        }
    }
}
