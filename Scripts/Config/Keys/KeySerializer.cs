using ConsoleSystem;
using SLVoiceController.File;
using WindowsInput.Native;
using WindowsInput;

namespace SLVoiceController.Config
{
    public static class KeySerializer
    {
        static InputSimulator simulator = new InputSimulator();

        public static string KeybindPath =>
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
            if (ConsoleUtility.DisplayYesNoPrompt("[Key Serializer] No keybinds detected. Do you want to use the default keybinds? [Y/N]"))
            {
                SaveKeys(SLKeys.current);
                return;
            }

            DisplayBindMenu();
        }

        public static void DisplayBindMenu()
        {
            while (true)
            {
                ConsoleKey key = ConsoleUtility.DisplayPrompt("Binding menu:\ndisplay key list: 1   " +
                    "rebind specific key: 2   " +
                    "rebind all keys: 3   " +
                    "exit: 4", false, 
                    ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4);

                switch (key)
                {
                    //Display key list
                    case ConsoleKey.D1:
                        DisplayKeyList();
                        break;
                    //Rebind specific key
                    case ConsoleKey.D2:
                        SelectAndRebindKey();
                        break;
                    //Rebind all keys
                    case ConsoleKey.D3:
                        RebindAllKeys();
                        break;
                    //Exit
                    case ConsoleKey.D4:
                        SaveKeys(SLKeys.current);
                        return;
                }
            }
        }

        public static void SaveKeys(SLKeys keys)
        {
            FileManager.SaveJSON(KeybindPath, keys);
            ConsoleLogger.Log($"[Key Serializer] Saving keybinds in {KeybindPath}");
        }

        public static void DisplayKeyList()
        {
            IEnumerable<string> lines = SLKeys.GetKeyFields()
                .Select(x => $" -{x.Name}: {x.GetValue(SLKeys.current)}");

            ConsoleLogger.Log($"Key list:\n{string.Join("\n", lines)}");
        }

        public static void RebindAllKeys()
        {
            ConsoleLogger.Log("Press RETURN to start rebinding");
            while (true)
            {
                VirtualKeyCode? key = ListenForKey(false, true);

                if (key == null)
                    return;

                if (key == VirtualKeyCode.RETURN)
                    break;
            }


            string[] keyNames = SLKeys.current.GetKeyNames();
            foreach (string keyName in keyNames)
            {
                Console.Write($"Press any key for {keyName}: ");
                RebindKey(keyName, false);
                Console.WriteLine();
            }
        }

        public static void SelectAndRebindKey()
        {
            Console.Write("Key name: ");
            string? keyName = ConsoleUtility.ReadLineCancel();

            if (keyName == null)
                return;

            Console.Write("Press any key: ");

            RebindKey(keyName);
        }

        public static void RebindKey(string keyName, bool log = true)
        {
            VirtualKeyCode? key = ListenForKey();
            Console.WriteLine();
            if (SLKeys.current.RebindKey(keyName, key ?? default) && log)
                ConsoleLogger.Log($"Successfully rebinded key {keyName} to {key}");
        }

        public static VirtualKeyCode? ListenForKey(bool writeKey = true, bool cancellable = false)
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

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape && cancellable)
                    return null;

                foreach (VirtualKeyCode key in Enum.GetValues(typeof(VirtualKeyCode)))
                {

                    if (!simulator.InputDeviceState.IsHardwareKeyDown(key) ||
                        keysToIgnore.Contains(key)) continue;

                    if (writeKey)
                        Console.Write(key.ToString());

                    return key;
                }
            }
        }
    }
}