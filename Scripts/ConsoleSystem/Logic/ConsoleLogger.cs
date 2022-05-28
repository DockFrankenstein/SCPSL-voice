using static System.Console;

namespace ConsoleSystem
{
    public class ConsoleLogger
    {
        public static ConsoleColor DefaultColor { get => ConsoleColor.Gray; }

        public static bool EnableClearing { get; set; } = true;

        public static void Log(string? text) =>
            Log(text, DefaultColor);

        public static void LogError(string? text) =>
            Log(text, ConsoleColor.Red);

        public static void Log(string? text, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(text)) return;
            DateTime time = DateTime.Now;
            ForegroundColor = color;

            string timeText = $"[{time:yyyy/MM/dd} {time:HH/mm/ss} {time:zz}]";
            string log = $"{timeText} {text.Replace("\n", $"\n{timeText} ")}";

            WriteLine(log);
            ForegroundColor = DefaultColor;
        }

        public static void Clear()
        {
            if (!EnableClearing) return;
            Console.Clear();
        }

        public static string GetInput()
        {
            Write(">");
            return ReadLine() ?? string.Empty;
        }
    }
}