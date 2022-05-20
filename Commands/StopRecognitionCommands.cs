using ConsoleSystem.Commands;
using VoiceCommands;

namespace Commands
{
    public class StopRecognitionCommands : ConsoleCommand
    {
        public override string CommandName => "stop_speech_recognition";

        public override string Description => "stops speech recognition";

        public override string[] Aliases => new string[] { "stop" };

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;
            VoiceCommandController.Stop();
        }
    }
}