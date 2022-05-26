using ConsoleSystem.Commands;
using SLVoiceController.VoiceCommands;

namespace SLVoiceController.Commands
{
    public class StartRecognitionCommand : ConsoleCommand
    {
        public override string CommandName => "start_speech_recognition";

        public override string Description => "starts speech recognition";

        public override string[] Aliases => new string[] { "start" };

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;

            VoiceCommandController.Start();
        }
    }
}