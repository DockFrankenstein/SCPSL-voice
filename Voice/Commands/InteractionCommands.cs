using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class InteractionCommands
    {
        [VoiceCommand("interact")]
        public static void Interact(InputSimulator simulator) =>
            SLKeys.current.interact.KeyPress();
    }
}