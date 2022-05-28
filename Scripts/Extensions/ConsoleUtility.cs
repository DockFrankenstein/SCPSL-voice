using ConsoleSystem;
using System.Text;

namespace SLVoiceController
{
    public static class ConsoleUtility
    {
        public static ConsoleKey DisplayPrompt(string prompt, params ConsoleKey[] keys) =>
            DisplayPrompt(prompt, null, ConsoleColor.White, null, keys);

        public static ConsoleKey DisplayPrompt(string prompt, ConsoleColor promptColor, params ConsoleKey[] keys) =>
            DisplayPrompt(prompt, null, promptColor, null, keys);

        public static ConsoleKey DisplayPrompt(string prompt, string? wrongPrompt, ConsoleColor promptColor, params ConsoleKey[] keys) =>
            DisplayPrompt(prompt, wrongPrompt, promptColor, null, keys);

        public static ConsoleKey DisplayPrompt(string prompt, string? wrongPrompt, ConsoleColor promptColor, ConsoleColor? wrongPromptColor, params ConsoleKey[] keys)
        {
            ConsoleLogger.Log(prompt, promptColor);

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();

                if (Array.IndexOf(keys, key) != -1)
                    return key;

                ConsoleLogger.Log(wrongPrompt ?? prompt, wrongPromptColor ?? promptColor);
            }
        }

        public static bool DisplayYesNoPrompt(string prompt) =>
            DisplayYesNoPrompt(prompt, null, ConsoleColor.White, null);

        public static bool DisplayYesNoPrompt(string prompt, ConsoleColor promptColor) =>
            DisplayYesNoPrompt(prompt, null, promptColor, null);

        public static bool DisplayYesNoPrompt(string prompt, string? wrongPrompt, ConsoleColor promptColor) =>
            DisplayYesNoPrompt(prompt, wrongPrompt, promptColor, null);

        public static bool DisplayYesNoPrompt(string prompt, string? wrongPrompt, ConsoleColor promptColor, ConsoleColor? wrongPromptColor) =>
            DisplayPrompt(prompt, wrongPrompt, promptColor, wrongPromptColor, ConsoleKey.Y, ConsoleKey.N) == ConsoleKey.Y;

        public static string? ReadLineCancel()
        {
            StringBuilder text = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Write(key.KeyChar);
                text.Append(key.KeyChar);

                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    return null;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return text.ToString();
                }
            }
        }
    }
}
