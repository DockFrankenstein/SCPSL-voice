using ConsoleSystem.Commands;
using System.Speech.Recognition;

namespace Commands
{
    public class VoiceTestCommand : ConsoleCommand
    {
        public override string CommandName => "voice_test";

        public override string Description => "tests voice recognition";

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;

            Log("Running voice recognition test, press any key to exit...");

            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-UK")))
            {
                recognizer.LoadGrammar(new DictationGrammar());
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(HandleRecognition);

                recognizer.SetInputToDefaultAudioDevice();

                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                Console.ReadKey();
            }
        }

        void HandleRecognition(object? sender, SpeechRecognizedEventArgs e)
        {
            Log($"Result: {e.Result.Text}");
        }

        public override void Log(string text) =>
            Log(text, ConsoleColor.Cyan);

        public override void Log(string text, ConsoleColor color) =>
            base.Log($"[Voice test] {text}", color);
    }
}