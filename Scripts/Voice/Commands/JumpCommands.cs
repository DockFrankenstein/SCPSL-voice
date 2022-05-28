using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class JumpCommands
    {
        [VoiceCommand("jump_jump", "jump")]
        public static void Jump() =>
            SLKeys.current.jump.KeyPress();
    }
}