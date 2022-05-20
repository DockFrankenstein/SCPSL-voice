using WindowsInput;
using WindowsInput.Native;

namespace VoiceCommands.Commands
{
    public static class MovementCommands
    {
        [VoiceCommand("forward")]
        public static void WalkForward(InputSimulator simulator)
        {
            simulator.Keyboard.KeyDown(VirtualKeyCode.VK_W);
            simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
        }

        //[VoiceCommand("back")]
        //public static void WalkBackward(InputSimulator simulator) =>
        //    simulator.Keyboard.KeyDown(VirtualKeyCode.VK_S);

        //[VoiceCommand("left")]
        //public static void WalkLeft(InputSimulator simulator) =>
        //    simulator.Keyboard.KeyDown(VirtualKeyCode.VK_A);

        //[VoiceCommand("right")]
        //public static void WalkRight(InputSimulator simulator) =>
        //    simulator.Keyboard.KeyDown(VirtualKeyCode.VK_D);

        [VoiceCommand("skidaddle")]
        public static void Run(InputSimulator simulator)
        {
            WalkForward(simulator);
            simulator.Keyboard.KeyDown(VirtualKeyCode.SHIFT);
        }

        [VoiceStop]
        [VoiceCommand("stop")]
        public static void Stop(InputSimulator simulator)
        {
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_A);
            simulator.Keyboard.KeyUp(VirtualKeyCode.VK_D);
            simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
        }

        [VoiceCommand("crouch")]
        public static void Crouch(InputSimulator simulator)
        {
            bool isCrouching = simulator.InputDeviceState.IsKeyUp(VirtualKeyCode.LCONTROL);
            WalkForward(simulator);

            if (isCrouching)
                simulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
        }
    }
}