namespace ConsoleSystem.Commands.Builtin
{
    public class EchoCommand : ConsoleCommand
    {
        public override string CommandName => "echo";
        public override string Description => "prints out a log";

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 1)) return;
            Log(args[1]);
        }
    }
}