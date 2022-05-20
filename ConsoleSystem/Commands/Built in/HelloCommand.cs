namespace ConsoleSystem.Commands.Builtin
{
    public class HelloCommand : ConsoleCommand
    {
        public override string CommandName { get => "hello"; }
        public override string Description { get => "hello world"; }
        public override string[] Aliases { get => new string[] { "hello_world" }; }

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;
            Log("Hello World!");
        }
    }
}