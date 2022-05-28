using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class GunCommands
    {
        public static bool Shooting { get; set; }
        public static bool Zoomed { get; set; }

        [VoiceCommand("gun_shoot", "bang")]
        public static void ShootOneShot()
        {
            SLKeys.current.zoom.KeyPress();
        }

        [VoiceCommand("gun_shootall", "shoot auto")]
        public static void ShootAutomatic()
        {
            SLKeys.current.shoot.ChangeKeyState(Shooting = !Shooting);
        }

        [VoiceCommand("gun_zoom", "zoom")]
        public static void Zoom()
        {
            Zoomed = !Zoomed;
            SLKeys.current.shoot.ChangeKeyState(Zoomed = !Zoomed);
        }

        [VoiceStop]
        public static void OnStopRecognition()
        {
            SLKeys.current.shoot.KeyUp();
            SLKeys.current.zoom.KeyUp();
            Shooting = false;
            Zoomed = false;
        }

        [VoiceCommand("gun_flashlight", "flashlight")]
        public static void Flashlight() =>
            SLKeys.current.flashlight.KeyPress();

        [VoiceCommand("gun_reload", "reload")]
        public static void Reload() =>
            SLKeys.current.reload.KeyPress();

        [VoiceCommand("gun_unload", "unload")]
        public static void Unload()
        {
            new Thread(() =>
            {
                SLKeys.current.reload
                    .KeyDown()
                    .Wait(2000)
                    .KeyUp();
            }).Start();
        }

        [VoiceCommand("gun_cock", "cock")]
        public static void CockGun() =>
            SLKeys.current.cockRevolver.KeyPress();
    }
}