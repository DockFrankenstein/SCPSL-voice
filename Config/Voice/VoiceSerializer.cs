using SLVoiceController.VoiceCommands.Commands;
using SLVoiceController.VoiceCommands;
using SLVoiceController.File;
using ConsoleSystem;

namespace SLVoiceController.Config
{
    public static class VoiceSerializer
    {
        public static string VoicePath =>
            $"{AppDomain.CurrentDomain.BaseDirectory}/voice commands.txt";

        internal static void SetupVoiceConfig()
        {
            LoadVoiceConfig();

            foreach (var command in VoiceCommandController.Commands)
            {
                if (!VoiceCommandController.TryGetAttribute(command, out VoiceCommandAttribute? attr) || attr == null)
                    continue;

                VoiceConfig.current.EnsureItem(attr.Key, attr.DefaultCommand);
            }

            SaveVoiceConfig();
        }

        public static void LoadVoiceConfig()
        {
            if (FileManager.TryReadFileJSON(VoicePath, out VoiceConfig? config) && config != null)
            {
                VoiceConfig.current = config;
                return;
            }

            ConsoleLogger.Log("[Voice Serializer] Voice config could not be loaded");
        }

        public static void SaveVoiceConfig()
        {
            FileManager.SaveJSON(VoicePath, VoiceConfig.current);
            ConsoleLogger.Log($"[Voice Serializer] Saving keybinds in {VoicePath}");
        }
    }
}