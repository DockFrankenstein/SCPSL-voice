using System.Management;
using ConsoleSystem;

namespace SLVoiceController
{
    public static class ApplicationData
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        [Initialize]
        static void Init()
        {
            ManagementObjectSearcher mydisplayResolution = new ManagementObjectSearcher("SELECT CurrentHorizontalResolution, CurrentVerticalResolution FROM Win32_VideoController");
            foreach (ManagementObject record in mydisplayResolution.Get())
            {
                Width = int.Parse(record["CurrentHorizontalResolution"].ToString() ?? string.Empty);
                Height = int.Parse(record["CurrentVerticalResolution"].ToString() ?? string.Empty);
                ConsoleLogger.Log($"[Application] Resolution: {Width}x{Height}");
            }
        }
    }
}
