using WindowsInput;
using WindowsInput.Native;

namespace VoiceCommands
{
    public static class InputSimulatorExtensions
    {
        public static void ToggleKey(this InputSimulator simulator, VirtualKeyCode key)
        {
            bool isDown = simulator.InputDeviceState.IsKeyDown(key);
            switch (isDown)
            {
                case true:
                    simulator.Keyboard.KeyUp(key);
                    break;
                case false:
                    simulator.Keyboard.KeyDown(key);
                    break;
            }
        }
    }
}
