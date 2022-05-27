using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class VoiceChatCommands
    {
        private static bool _voiceActive = false;
        private static bool _radioActive = false;

        [VoiceCommand("voicechat_voice", "voice chat")]
        public static void VoiceChatCommand() =>
            SLKeys.current.voiceChat.ChangeKeyState(_voiceActive = !_voiceActive);

        [VoiceCommand("voicechat_alt", "alt chat")]
        public static void RadioChatCommand() =>
            SLKeys.current.altVoice.ChangeKeyState(_radioActive = !_radioActive);

        [VoiceStop]
        public static void HandleRecognitionStop()
        {
            _voiceActive = false;
            _radioActive = false;
            SLKeys.current.voiceChat.KeyUp();
            SLKeys.current.altVoice.KeyUp();
        }
    }
}