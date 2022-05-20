using WindowsInput;
using WindowsInput.Native;

namespace VoiceCommands.Commands
{
    public static class JumpCommands
    {
        [VoiceCommand("Jump")]
        public static void Jump(InputSimulator simulator) =>
            simulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
    }
}