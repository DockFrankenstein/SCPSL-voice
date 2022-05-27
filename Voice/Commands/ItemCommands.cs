using WindowsInput;
using WindowsInput.Native;
using System.Speech.Synthesis;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class ItemCommands
    {
        static bool EnableZipZap { get; set; }

        //Using items can be achieved in GunCommands by saying "bang"

        [VoiceCommand("twitter")]
        public static void CancelUsing(InputSimulator simulator) =>
            SLKeys.current.zoom.KeyPress();


        [VoiceCommand("begin the process of zip zaping my medical kit please")]
        public static void ZipZap(InputSimulator simulator)
        {
            EnableZipZap = true;
            new Thread(() =>
            {
                new SpeechSynthesizer().SpeakAsync(new Prompt("Activating fun"));
                while (EnableZipZap)
                {
                    SLKeys.current.shoot
                        .KeyPress()
                        .Wait(500);
                    SLKeys.current.zoom
                        .KeyPress()
                        .Wait(500);
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
                SLKeys.current.grenadeHotkey.KeyPress();
                simulator.Mouse.MoveMouseBy(0, 4000);

                new SpeechSynthesizer().SpeakAsync(new Prompt("Commencing ultimate bye bye"));
                Thread.Sleep(200);
                SLKeys.current.shoot.KeyPress();
            }).Start();
        }

        [VoiceCommand("throw")]
        public static void Throw(InputSimulator simulator) =>
            SLKeys.current.throwItem.KeyPress();
    }
}