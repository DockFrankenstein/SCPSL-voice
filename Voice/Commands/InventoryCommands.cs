using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class InventoryCommands
    {
        [VoiceCommand("inventory_weapon", "weapon")]
        public static void SelectWeapon() =>
            SLKeys.current.weaponHotkey.KeyPress();

        [VoiceCommand("inventory_weapon2", "weapon 2")]
        public static void SelectSecondWeapon() =>
            SLKeys.current.weaponHotkey2.KeyPress();

        [VoiceCommand("inventory_inventory", "inventory")]
        public static void OpenInventory() =>
            SLKeys.current.inventory.KeyPress();

        [VoiceCommand("inventory_keycard", "keycard")]
        public static void KeyCard() =>
            SLKeys.current.keycardHotkey.KeyPress();

        [VoiceCommand("inventory_medical", "medicals")]
        public static void Medicals() =>
            SLKeys.current.medicalHotkey.KeyPress();

        [VoiceCommand("inventory_grenade", "grenade")]
        public static void Grenade() =>
            SLKeys.current.grenadeHotkey.KeyPress();

        [VoiceCommand("inventory_pickup", "pick up")]
        public static void Pickup()
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