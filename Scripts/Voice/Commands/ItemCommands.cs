using WindowsInput;
using System.Speech.Synthesis;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class ItemCommands
    {
        static bool EnableZipZap { get; set; }

        //Using items can be achieved in GunCommands by saying "bang"

        [VoiceCommand("item_cancel", "cancel")]
        public static void CancelUsing() =>
            SLKeys.current.zoom.KeyPress();


        [VoiceCommand("item_medkitzip_start", "begin the process of zip zaping my medical kit please")]
        public static void ZipZap()
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
        [VoiceCommand("item_medkitzip_end", "terminate the process of zip zaping my medical kit please")]
        public static void DisableZipZap()
        {
            if (!EnableZipZap) return;
            EnableZipZap = false;
            new SpeechSynthesizer().SpeakAsync(new Prompt("Fun successfully disabled"));
        }

        [VoiceCommand("item_detonate", "mission commit spectator")]
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

        [VoiceCommand("item_throw", "throw")]
        public static void Throw() =>
            SLKeys.current.throwItem.KeyPress();
    }
}