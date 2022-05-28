namespace ConsoleSystem.Commands.Builtin
{
    public class ExitCommand : ConsoleCommand
    {
        public override string CommandName => "exit";

        public override string Description => "closes the application";

        public override void Run(List<string> args)
        {
            Log("Goodbye!");
            ConsoleController.Exit();
        }
    }
}