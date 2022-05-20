using CommandList = ConsoleSystem.Commands.ConsoleCommandList;

namespace ConsoleSystem.Commands.Builtin
{
    public class HelpCommand : ConsoleCommand
    {
        public override string CommandName { get => "help"; }
        public override string Description { get => "displays help"; }

        public static int CommandLimit { get; set; } = 16;

        int pageCount = 0;

        public override void Run(List<string> args)
        {
            pageCount = CommandList.Commands.Count / CommandLimit;

            switch (pageCount)
            {
                case 1:
                    if (CheckForArgumentCount(args, 0)) break;
                    return;
                default:
                    if (CheckForArgumentCount(args, 0, 1)) break;
                    return;
            }

            int page = 0;

            if (args.Count == 2 && !int.TryParse(args[1], out page))
            {
                DisplayDetailedDescription(args[1]);
                return;
            }

            string s = pageCount == 1 ? "Displaying help:" : $"Displaying help page {page} out of {pageCount}";

            for (int i = page * CommandLimit; i < Math.Min(CommandList.Commands.Count, (page + 1) * CommandLimit); i++)
                s += $"\n- {CommandList.Commands[i].CommandName} - {CommandList.Commands[i].Description}";

            Log(s);
        }

        void DisplayDetailedDescription(string commandName)
        {
            if (!CommandList.TryGettingConsoleCommand(commandName, out ConsoleCommand? command))
            {
                LogError($"Command '{commandName.ToLower()}' does not exist!");
                return;
            }

            Log($"Help for command '{(command.CommandName)}' {(string.IsNullOrWhiteSpace(command.DetailedDescription) ? command.Description : command.DetailedDescription)}");
        }
    }
}