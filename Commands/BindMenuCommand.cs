using ConsoleSystem.Commands;
using SLVoiceController.Config;

namespace SLVoiceController.Commands
{
    public class BindMenuCommand : ConsoleCommand
    {
        public override string CommandName => "binds_menu";
        public override string Description => "Displays bind menu";

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;
            KeySerializer.DisplayBindMenu();
        }
    }
}