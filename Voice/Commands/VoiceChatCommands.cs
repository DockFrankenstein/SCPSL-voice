using WindowsInput;
using WindowsInput.Native;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class VoiceChatCommands
    {
        private static bool _voiceActive = false;
        private static bool _radioActive = false;

        [VoiceCommand("voice chat")]
        public static void VoiceChatCommand(InputSimulator simulator) =>
            ToggleChat(simulator, VirtualKeyCode.VK_Q, ref _voiceActive);

        [VoiceCommand("radio")]
        public static void RadioChatCommand(InputSimulator simulator) =>
            ToggleChat(simulator, VirtualKeyCode.VK_V, ref _radioActive);

        static void ToggleChat(InputSimulator simulator, VirtualKeyCode key, ref bool state)
        {
            state = !state;
            switch (state)
            {
                case true:
                    simulator.Keyboard.KeyDown(key);
                    break;
                case false:
                    simulator.Keyboard.KeyUp(key);
                    break;
            }
        }

        [VoiceStop]
        public static void HandleRecognitionStop(InputSimulator simulator)
        {
            _voiceActive = false;
            _radioActive = false;
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_Q);
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_V);
        }
    }
}