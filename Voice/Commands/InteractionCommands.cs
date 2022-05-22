using WindowsInput;
using WindowsInput.Native;

namespace VoiceCommands.Commands
{
    public static class InteractionCommands
    {
        [VoiceCommand("interact")]
        public static void Interact(InputSimulator simulator)
        {
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_E);
        }
    }
}