using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class GunCommands
    {
        public static bool Shooting { get; set; }
        public static bool Zoomed { get; set; }

        [VoiceCommand("bang")]
        public static void ShootOneShot(InputSimulator simulator)
        {
            SLKeys.current.zoom.KeyPress();
        }

        [VoiceCommand("parabellum")]
        public static void ShootAutomatic(InputSimulator simulator)
        {
            SLKeys.current.shoot.ChangeKeyState(Shooting = !Shooting);
        }

        [VoiceCommand("enhance")]
        public static void Zoom(InputSimulator simulator)
        {
            Zoomed = !Zoomed;
            SLKeys.current.shoot.ChangeKeyState(Zoomed = !Zoomed);
        }

        [VoiceStop]
        public static void OnStopRecognition(InputSimulator simulator)
        {
            SLKeys.current.shoot.KeyUp();
            SLKeys.current.zoom.KeyUp();
            Shooting = false;
            Zoomed = false;
        }

        [VoiceCommand("flashlight")]
        public static void Flashlight(InputSimulator simulator) =>
            SLKeys.current.flashlight.KeyPress();

        [VoiceCommand("bullet")]
        public static void Reload(InputSimulator simulator) =>
            SLKeys.current.reload.KeyPress();

        [VoiceCommand("unload")]
        public static void Unload(InputSimulator simulator)
        {
            new Thread(() =>
            {
                SLKeys.current.reload
                    .KeyDown()
                    .Wait(2000)
                    .KeyUp();
            }).Start();
        }

        [VoiceCommand("cock")]
        public static void CockGun(InputSimulator simulator) =>
            SLKeys.current.cockRevolver.KeyPress();
    }
}