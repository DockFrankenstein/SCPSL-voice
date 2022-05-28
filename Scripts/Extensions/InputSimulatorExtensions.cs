using WindowsInput;
using WindowsInput.Native;

namespace SLVoiceController.VoiceCommands
{
    public static class InputSimulatorExtensions
    {
        public static void ToggleKey(this InputSimulator simulator, VirtualKeyCode key)
        {
            bool isDown = simulator.InputDeviceState.IsKeyDown(key);
            switch (isDown)
            {
                case true:
                    key.KeyUp();
                    break;
                case false:
                    key.KeyDown();
                    break;
            }
        }
    }
}
