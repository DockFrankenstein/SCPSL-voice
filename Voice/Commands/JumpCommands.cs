using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class JumpCommands
    {
        [VoiceCommand("Jump")]
        public static void Jump(InputSimulator simulator) =>
            SLKeys.current.jump.KeyPress();
    }
}