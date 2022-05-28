namespace ConsoleSystem.Commands.Built
{
    public class TestCommand : ConsoleCommand
    {
        public override string CommandName => "consoletest";

        public override string Description => "runs tests for the console";

        public override void Run(List<string> args)
        {
            Log("Initializing command tests...");
            ConsoleLogger.EnableClearing = false;
            int errorCount = 0;

            foreach (ConsoleCommand command in ConsoleCommandList.Commands)
            {
                if (command == this)
                {
                    Log("Skipping test command...", ConsoleColor.DarkGray);
                    continue;
                }

                try
                {
                    command.Run(args);
                }
                catch (Exception e)
                {
                    LogError($"Command '{command.CommandName}' failed to execute: {e}");
                    errorCount++;
                }
            }

            ConsoleLogger.EnableClearing = true;
            Log($"All tests finished, errors: {errorCount}", ConsoleColor.Green);
        }

        public override void Log(string text) =>
            Log(text, ConsoleColor.Cyan);

        public override void Log(string text, ConsoleColor color) =>
            base.Log($"[Test] {text}", color);
    }
}