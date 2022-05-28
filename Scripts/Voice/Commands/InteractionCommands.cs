using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class InteractionCommands
    {
        [VoiceCommand("interaction_interact", "interact")]
        public static void Interact() =>
            SLKeys.current.interact.KeyPress();
    }
}