using ConsoleSystem;
using SLVoiceController.File;
using WindowsInput.Native;
using WindowsInput;

namespace SLVoiceController.Config
{
    public static class KeySerializer
    {
        static InputSimulator simulator = new InputSimulator();

        static string KeybindPath =>
            $"{AppDomain.CurrentDomain.BaseDirectory}/keybinds.txt";

        [Initialize]
        static void Initialize()
        {
            ConsoleLogger.Log("[Key Serializer] Loading keybinds...", ConsoleColor.Gray);

            if (!FileManager.TryReadFileJSON(KeybindPath, out SLKeys? userKeys))
            {
                FirstTimeSetup();
                return;
            }

            SLKeys.current = userKeys ?? SLKeys.current;
            ConsoleLogger.Log("[Key Serializer] Keybinds successfully loaded", ConsoleColor.Gray);
        }

        static void FirstTimeSetup()
        {
            if (ConsoleUtility.DisplayYesNoPrompt("[Key Serializer] No keybinds detected. Do you want to use the default keybinds? [Y / N]"))
            {
                SaveKeys(SLKeys.current);
                ConsoleLogger.Log($"[Key Serializer] Saving keybinds in {KeybindPath}");
                return;
            }

            ConsoleLogger.Log("[Key Serializer] TODO: Keybind config", ConsoleColor.Yellow);
            DisplayBindMenu();
        }

        public static void DisplayBindMenu()
        {
            while (true)
            {
                ConsoleKey key = ConsoleUtility.DisplayPrompt("Binding menu:\ndisplay key list: L   " +
                    "rebind specific key: B   " +
                    "rebind all keys: F   " +
                    "exit: E", 
                    ConsoleKey.L, ConsoleKey.B, ConsoleKey.F, ConsoleKey.E);

                switch (key)
                {
                    //Display key list
                    case ConsoleKey.L:
                        DisplayKeyList();
                        break;
                    //Rebind specific key
                    case ConsoleKey.B:
                        SelectAndRebindKey();
                        break;
                    //Rebind all keys
                    case ConsoleKey.F:
                        RebindAllKeys();
                        break;
                    //Exit
                    case ConsoleKey.E:
                        SaveKeys(SLKeys.current);
                        return;
                }
            }
        }

        public static void SaveKeys(SLKeys keys)
        {
            FileManager.SaveJSON(KeybindPath, keys);
        }

        public static void DisplayKeyList()
        {
            IEnumerable<string> lines = SLKeys.GetKeyFields()
                .Select(x => $" -{x.Name}: {x.GetValue(SLKeys.current)}");

            ConsoleLogger.Log($"Key list:\n{string.Join("\n", lines)}");
        }

        public static void RebindAllKeys()
        {
            string[] keyNames = SLKeys.current.GetKeyNames();
            foreach (string keyName in keyNames)
            {
                Console.Write($"Press any key for {keyName}: ");
                ListenForKey(keyName, false);
            }
        }

        public static void SelectAndRebindKey()
        {
            Console.Write("Key name: ");
            string? keyName = Console.ReadLine();

            Console.Write("Press any key: ");

            ListenForKey(keyName ?? string.Empty);
        }

        public static void ListenForKey(string keyName, bool log = true)
        {
            List<VirtualKeyCode> keysToIgnore = new List<VirtualKeyCode>();

            foreach (VirtualKeyCode key in Enum.GetValues(typeof(VirtualKeyCode)))
                if (simulator.InputDeviceState.IsHardwareKeyDown(key))
                    keysToIgnore.Add(key);

            while (true)
            {
                List<VirtualKeyCode> keysToIgnoreCopy = new List<VirtualKeyCode>(keysToIgnore);
                foreach (VirtualKeyCode key in keysToIgnoreCopy)
                    if (simulator.InputDeviceState.IsHardwareKeyUp(key))
                        keysToIgnore.Remove(key);

                foreach (VirtualKeyCode key in Enum.GetValues(typeof(VirtualKeyCode)))
                {
                    if (!simulator.InputDeviceState.IsHardwareKeyDown(key) || 
                        keysToIgnore.Contains(key)) continue;

                    if (Console.KeyAvailable)
                        Console.ReadKey(true);

                    Console.WriteLine(key.ToString());

                    if (SLKeys.current.RebindKey(keyName, key) && log)
                        ConsoleLogger.Log($"Successfully rebinded key {keyName} to {key}");

                    return;
                }
            }
        }
    }
}