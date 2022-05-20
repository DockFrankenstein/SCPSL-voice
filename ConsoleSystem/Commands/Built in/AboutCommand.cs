namespace ConsoleSystem.Commands.Builtin
{
    public class AboutCommand : ConsoleCommand
    {
        public override string CommandName { get => "about"; }
        public override string Description { get => "displays about information"; }
        public override string[] Aliases { get => new string[] { "info" }; }

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;
            Log("\n" +
                "             ///////////////    /////   qASIC based console v0.0.1\n" +
                "          ///////       ////////////    https://qasictools.com\n" +
                $"        /////               //////      command count: {ConsoleCommandList.Commands.Count}\n" +
                "       ////                   ////      \n" +
                "      ////                     ////     \n" +
                "      ////                     ////     \n" +
                "      ////                     ////     \n" +
                "       ////                   ////      \n" +
                "        /////               /////       \n" +
                "         ////////       ///////         \n" +
                "       /////////////////////            \n" +
                "      /////                             beta version\n", ConsoleColor.Cyan);
        }
    }
}