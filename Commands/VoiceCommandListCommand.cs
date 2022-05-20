using VoiceCommands;

namespace ConsoleSystem.Commands
{
    public class VoiceCommandListCommand : ConsoleCommand
    {
        public override string CommandName => "voice_command_list";
        public override string Description => "displays a list of all avaliable voice command";

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;

            string log = "Avaliable voice commands:";
            for (int i = 0; i < VoiceCommandController.CommandNames.Length; i++)
                log += $"\n{VoiceCommandController.CommandNames[i]}";

            log += "\n";
            Log(log);
        }
    }
}
