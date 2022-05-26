using ConsoleSystem.Commands;
using WindowsInput;
using WindowsInput.Native;

namespace SLVoiceController.Commands
{
    public class InputTestCommand : ConsoleCommand
    {
        public override string CommandName => "input_test";

        public override string Description => "tests input simulator";

        public override void Run(List<string> args)
        {
            if (!CheckForArgumentCount(args, 0)) return;

            Log("Running input simulator test...");

            InputSimulator sim = new InputSimulator();

            sim.Keyboard.TextEntry("echo \"hello world!\"");
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        public override void Log(string text) =>
            Log(text, ConsoleColor.Cyan);

        public override void Log(string text, ConsoleColor color) =>
            base.Log($"[Input test] {text}", color);
    }
}