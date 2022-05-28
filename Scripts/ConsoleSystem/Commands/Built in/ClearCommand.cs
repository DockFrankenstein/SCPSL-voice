namespace ConsoleSystem.Commands.Builtin
{
    public class ClearCommand : ConsoleCommand
    {
        public override string CommandName { get => "clear"; }
        public override string Description { get => "clears all logs"; }
        public override string[] Aliases { get => new string[] { "cls" }; }

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;
            ConsoleLogger.Clear();
        }
    }
}