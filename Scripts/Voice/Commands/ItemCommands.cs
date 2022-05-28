using WindowsInput;
using System.Speech.Synthesis;
using SLVoiceController.Config;
using System.Diagnostics;
using WindowsInput.Native;
using System.Management;

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
                SLKeys.current.shoot.KeyUp();
                SLKeys.current.zoom.KeyUp();
                SLKeys.current.grenadeHotkey.KeyPress();

                new SpeechSynthesizer().SpeakAsync(new Prompt("Commencing ultimate bye bye"));
                Thread.Sleep(200);
                SLKeys.current.shoot.KeyPress();
                Thread.Sleep(1200);
                simulator.Mouse.MoveMouseBy(0, ApplicationData.Height);
            }).Start();

            new Thread(() =>
            {
                VirtualKeyCode forward = SLKeys.current.forward;
                VirtualKeyCode backward = SLKeys.current.backward;
                VirtualKeyCode left = SLKeys.current.left;
                VirtualKeyCode right = SLKeys.current.right;

                Stopwatch s = new Stopwatch();
                s.Start();

                while (s.Elapsed < TimeSpan.FromSeconds(10.2))
                {
                    GoBackToPosition(forward, backward);
                    GoBackToPosition(right, left);
                    Thread.Sleep(10);
                }

                forward.KeyUp();
                backward.KeyUp();
                left.KeyUp();
                right.KeyUp();

                void GoBackToPosition(VirtualKeyCode positive, VirtualKeyCode negative)
                {
                    positive.ChangeKeyState(negative.IsKeyDown());
                    negative.ChangeKeyState(positive.IsKeyDown());
                }
            }).Start();
        }

        [VoiceCommand("item_throw", "throw")]
        public static void Throw() =>
            SLKeys.current.throwItem.KeyPress();
    }
}