using WindowsInput;
using WindowsInput.Native;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class GunCommands
    {
        public static bool Shooting { get; set; }
        public static bool Zoomed { get; set; }

        [VoiceCommand("bang")]
        public static void ShootOneShot(InputSimulator simulator)
        {
            simulator.Mouse.LeftButtonUp();
            simulator.Mouse.LeftButtonClick();
        }

        [VoiceCommand("parabellum")]
        public static void ShootAutomatic(InputSimulator simulator)
        {
            Shooting = !Shooting;
            switch (Shooting)
            {
                case true:
                    simulator.Mouse.LeftButtonDown();
                    break;
                case false:
                    simulator.Mouse.LeftButtonUp();
                    break;
            }
        }

        [VoiceCommand("enhance")]
        public static void Zoom(InputSimulator simulator)
        {
            Zoomed = !Zoomed;
            switch (Zoomed)
            {
                case true:
                    simulator.Mouse.RightButtonDown();
                    break;
                case false:
                    simulator.Mouse.RightButtonUp();
                    break;
            }
        }

        [VoiceStop]
        public static void OnStopRecognition(InputSimulator simulator)
        {
            simulator.Mouse.RightButtonUp();
            simulator.Mouse.LeftButtonUp();
            Shooting = false;
            Zoomed = false;
        }

        [VoiceCommand("flashlight")]
        public static void Flashlight(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_G);

        [VoiceCommand("bullet")]
        public static void Reload(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_R);

        [VoiceCommand("unload")]
        public static void Unload(InputSimulator simulator)
        {
            new Thread(() =>
            {
                simulator.Keyboard.KeyDown(VirtualKeyCode.VK_R);
                Thread.Sleep(2000);
                simulator.Keyboard.KeyUp(VirtualKeyCode.VK_R);
            }).Start();
        }

        [VoiceCommand("cock")]
        public static void CockGun(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.MBUTTON);
    }
}