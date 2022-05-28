using ConsoleSystem.Commands;

using static ConsoleSystem.ConsoleLogger;
using static ConsoleSystem.Logic.ConsoleArgumentSorter;

namespace ConsoleSystem
{
    public static class ConsoleController
    {
        public static void Initialize()
        {
            Log("Initializing console...", ConsoleColor.Magenta);

            consoleThread = new Thread(() =>
            {
                while (!_isQuitting)
                    HandleInput(GetInput());
            });

            ConsoleCommandList.Initialize();
            Log("Console has been initialized", ConsoleColor.Blue);
        }

        static bool _isQuitting;
        static Thread? consoleThread;

        public static void Start()
        {
            if (consoleThread == null)
            {
                LogError("Console has not been initialized!");
            }

            consoleThread.Start();
            Log("Console started. Use 'help' for more information", ConsoleColor.Green);
        }

        private static void HandleInput(string? cmd)
        {
            List<string> args = SortCommand(cmd);
            if (args.Count < 1) return;

            if(!ConsoleCommandList.TryGettingConsoleCommand(args[0], out ConsoleCommand? command))
            {
                Log($"Command '{args[0]}' does not exist!", ConsoleColor.Red);
                return;
            }

            command?.Run(args);
        }

        public static void Exit()
        {
            _isQuitting = true;
            Environment.Exit(0);
        }
    }
}