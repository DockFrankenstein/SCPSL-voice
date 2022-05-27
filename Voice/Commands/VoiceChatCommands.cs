using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class VoiceChatCommands
    {
        private static bool _voiceActive = false;
        private static bool _radioActive = false;

        [VoiceCommand("voice chat")]
        public static void VoiceChatCommand(InputSimulator simulator) =>
            SLKeys.current.voiceChat.ChangeKeyState(_voiceActive = !_voiceActive);

        [VoiceCommand("radio")]
        public static void RadioChatCommand(InputSimulator simulator) =>
            SLKeys.current.altVoice.ChangeKeyState(_radioActive = !_radioActive);

        [VoiceStop]
        public static void HandleRecognitionStop(InputSimulator simulator)
        {
            _voiceActive = false;
            _radioActive = false;
            SLKeys.current.voiceChat.KeyUp();
            SLKeys.current.altVoice.KeyUp();
        }
    }
}