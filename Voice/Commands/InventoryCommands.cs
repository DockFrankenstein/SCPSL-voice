using WindowsInput;
using WindowsInput.Native;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class InventoryCommands
    {
        [VoiceCommand("pp")]
        public static void SelectWeapon(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_1);

        [VoiceCommand("pp 2")]
        public static void SelectSecondWeapon(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_2);

        [VoiceCommand("stash")]
        public static void OpenInventory(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_F);

        [VoiceCommand("keycard")]
        public static void KeyCard(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_3);

        [VoiceCommand("medicalos")]
        public static void Medicals(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_4);

        [VoiceCommand("boom boom")]
        public static void Grenade(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_5);

        [VoiceCommand("pick up")]
        public static void Pickup(InputSimulator simulator)
        {
            new Thread(() =>
            {
                simulator.Keyboard.KeyUp(VirtualKeyCode.VK_E);
                simulator.Keyboard.KeyDown(VirtualKeyCode.VK_E);
                Thread.Sleep(4100);
                simulator.Keyboard.KeyUp(VirtualKeyCode.VK_E);
            }).Start();
        }
    }
}