using WindowsInput.Native;
using System.Reflection;
using ConsoleSystem;

namespace SLVoiceController.Config
{
    [System.Serializable]
    public class SLKeys
    {
        public static readonly SLKeys defaultLayout = new SLKeys()
        {
            jump = VirtualKeyCode.SPACE,
            run = VirtualKeyCode.LEFT,
            sneak = VirtualKeyCode.VK_C,

            forward = VirtualKeyCode.VK_W,
            backward = VirtualKeyCode.VK_S,
            left = VirtualKeyCode.VK_A,
            right = VirtualKeyCode.VK_D,

            voiceChat = VirtualKeyCode.VK_Q,
            altVoice = VirtualKeyCode.VK_V,

            interact = VirtualKeyCode.VK_E,
            inventory = VirtualKeyCode.TAB,

            playerList = VirtualKeyCode.VK_N,
            chatacterInfo = VirtualKeyCode.F1,

            throwItem = VirtualKeyCode.VK_T,
            shoot = VirtualKeyCode.LBUTTON,
            zoom = VirtualKeyCode.RBUTTON,
            reload = VirtualKeyCode.VK_R,
            flashlight = VirtualKeyCode.VK_F,
            inspect = VirtualKeyCode.VK_I,
            cockRevolver = VirtualKeyCode.MBUTTON,

            keycardHotkey = VirtualKeyCode.LCONTROL,
            weaponHotkey = VirtualKeyCode.VK_1,
            weaponHotkey2 = VirtualKeyCode.VK_2,
            medicalHotkey = VirtualKeyCode.VK_X,
            grenadeHotkey = VirtualKeyCode.VK_G,

            remoteAdmin = VirtualKeyCode.VK_M,
            noclip = VirtualKeyCode.MENU,
            toggleNoclipFog = VirtualKeyCode.VK_O,
            gameConsole = VirtualKeyCode.OEM_8,
            hideGUI = VirtualKeyCode.VK_P,
        };

        public static SLKeys current = new SLKeys(defaultLayout);

        public SLKeys() { }

        public SLKeys(SLKeys other)
        {
            jump = other.jump;
            run = other.run;
            sneak = other.sneak;
            forward = other.forward;
            backward = other.backward;
            left = other.left;
            right = other.right;
            voiceChat = other.voiceChat;
            altVoice = other.altVoice;
            interact = other.interact;
            inventory = other.inventory;
            playerList = other.playerList;
            chatacterInfo = other.chatacterInfo;
            throwItem = other.throwItem;
            shoot = other.shoot;
            zoom = other.zoom;
            reload = other.reload;
            flashlight = other.flashlight;
            inspect = other.inspect;
            cockRevolver = other.cockRevolver;
            keycardHotkey = other.keycardHotkey;
            weaponHotkey = other.weaponHotkey;
            weaponHotkey2 = other.weaponHotkey2;
            medicalHotkey = other.medicalHotkey;
            grenadeHotkey = other.grenadeHotkey;
            remoteAdmin = other.remoteAdmin;
            noclip = other.noclip;
            toggleNoclipFog = other.toggleNoclipFog;
            gameConsole = other.gameConsole;
            hideGUI = other.hideGUI;
        }

        public VirtualKeyCode jump;
        public VirtualKeyCode run;
        public VirtualKeyCode sneak;

        public VirtualKeyCode forward;
        public VirtualKeyCode backward;
        public VirtualKeyCode left;
        public VirtualKeyCode right;

        public VirtualKeyCode voiceChat;
        public VirtualKeyCode altVoice;

        public VirtualKeyCode interact;
        public VirtualKeyCode inventory;

        public VirtualKeyCode playerList;
        public VirtualKeyCode chatacterInfo;

        public VirtualKeyCode throwItem;
        public VirtualKeyCode shoot;
        public VirtualKeyCode zoom;
        public VirtualKeyCode reload;
        public VirtualKeyCode flashlight;
        public VirtualKeyCode inspect;
        public VirtualKeyCode cockRevolver;

        public VirtualKeyCode keycardHotkey;
        public VirtualKeyCode weaponHotkey;
        public VirtualKeyCode weaponHotkey2;
        public VirtualKeyCode medicalHotkey;
        public VirtualKeyCode grenadeHotkey;

        public VirtualKeyCode remoteAdmin;
        public VirtualKeyCode noclip;
        public VirtualKeyCode toggleNoclipFog;
        public VirtualKeyCode gameConsole;
        public VirtualKeyCode hideGUI;

        /// <returns>Returns true if the rebind was successfull</returns>
        public bool RebindKey(string keyName, VirtualKeyCode key)
        {
            keyName = keyName.ToLower();

            List<FieldInfo> targetFields = GetKeyFields()
                .Where(x => x.Name.ToLower() == keyName)
                .ToList();

            if (targetFields.Count != 1)
            {
                ConsoleLogger.LogError($"Couldn't rebind key {keyName}: key name doesn't exist");
                return false;
            }
            
            targetFields[0].SetValue(this, key);

            return true;
        }

        public static IEnumerable<FieldInfo> GetKeyFields() =>
            typeof(SLKeys)
            .GetFields()
            .Where(x => x.FieldType == typeof(VirtualKeyCode));

        public static IEnumerable<FieldInfo> GetKeyFieldsList() =>
            GetKeyFields()
            .ToList();

        public string[] GetKeyNames() =>
            GetKeyFields()
            .Select(x => x.Name.ToLower())
            .ToArray();
    }
}