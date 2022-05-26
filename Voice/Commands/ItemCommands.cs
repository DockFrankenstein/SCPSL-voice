using WindowsInput;
using WindowsInput.Native;
using System.Speech.Synthesis;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class ItemCommands
    {
        static bool EnableZipZap { get; set; }

        //Using items can be achieved in GunCommands by saying "bang"

        [VoiceCommand("twitter")]
        public static void CancelUsing(InputSimulator simulator) =>
            simulator.Mouse.RightButtonClick();


        [VoiceCommand("begin the process of zip zaping my medical kit please")]
        public static void ZipZap(InputSimulator simulator)
        {
            EnableZipZap = true;
            new Thread(() =>
            {
                new SpeechSynthesizer().SpeakAsync(new Prompt("Activating fun"));
                while (EnableZipZap)
                {
                    simulator.Mouse.LeftButtonClick();
                    Thread.Sleep(500);
                    simulator.Mouse.RightButtonClick();
                    Thread.Sleep(200);
                }
            }).Start();
        }

        [VoiceStop]
        [VoiceCommand("terminate the process of zip zaping my medical kit please")]
        public static void DisableZipZap(InputSimulator simulator)
        {
            if (!EnableZipZap) return;
            EnableZipZap = false;
            new SpeechSynthesizer().SpeakAsync(new Prompt("Fun successfully disabled"));
        }

        [VoiceCommand("mission commit spectator")]
        public static void DetonateSelf(InputSimulator simulator)
        {
            new Thread(() =>
            {
                simulator.Keyboard.KeyPress(VirtualKeyCode.VK_5);
                simulator.Mouse.MoveMouseBy(0, 1440);
                new SpeechSynthesizer().SpeakAsync(new Prompt("Commencing ultimate bye bye"));
                Thread.Sleep(200);
                simulator.Mouse.LeftButtonClick();
            }).Start();
        }

        [VoiceCommand("throw")]
        public static void Throw(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_T);
    }
}