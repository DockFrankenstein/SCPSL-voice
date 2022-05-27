using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class InventoryCommands
    {
        [VoiceCommand("pp")]
        public static void SelectWeapon(InputSimulator simulator) =>
            SLKeys.current.weaponHotkey.KeyPress();

        [VoiceCommand("pp 2")]
        public static void SelectSecondWeapon(InputSimulator simulator) =>
            SLKeys.current.weaponHotkey2.KeyPress();

        [VoiceCommand("stash")]
        public static void OpenInventory(InputSimulator simulator) =>
            SLKeys.current.inventory.KeyPress();

        [VoiceCommand("keycard")]
        public static void KeyCard(InputSimulator simulator) =>
            SLKeys.current.keycardHotkey.KeyPress();

        [VoiceCommand("medicalos")]
        public static void Medicals(InputSimulator simulator) =>
            SLKeys.current.medicalHotkey.KeyPress();

        [VoiceCommand("boom boom")]
        public static void Grenade(InputSimulator simulator) =>
            SLKeys.current.grenadeHotkey.KeyPress();

        [VoiceCommand("pick up")]
        public static void Pickup(InputSimulator simulator)
        {
            new Thread(() =>
            {
                SLKeys.current.interact
                    .KeyUp()
                    .KeyDown()
                    .Wait(4100)
                    .KeyUp();
            }).Start();
        }
    }
}