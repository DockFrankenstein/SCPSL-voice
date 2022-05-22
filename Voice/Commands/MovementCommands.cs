using WindowsInput;
using WindowsInput.Native;

namespace VoiceCommands.Commands
{
    public static class MovementCommands
    {
        enum VerticalAxis { none, forward, backward }
        static VerticalAxis verticalAxis;

        [VoiceCommand("forward")]
        public static void WalkForward(InputSimulator simulator) =>
            WalkVertical(simulator, VerticalAxis.forward);

        [VoiceCommand("control z")]
        public static void Backward(InputSimulator simulator) =>
            WalkVertical(simulator, VerticalAxis.backward);

        static void WalkVertical(InputSimulator simulator, VerticalAxis axis)
        {
            if (verticalAxis == axis)
            {
                simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                return;
            }

            verticalAxis = axis;
            switch (axis)
            {
                case VerticalAxis.none:
                    simulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                    simulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
                    simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                    simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                    break;
                case VerticalAxis.forward:
                    simulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
                    simulator.Keyboard.KeyDown(VirtualKeyCode.VK_W);
                    break;
                case VerticalAxis.backward:
                    simulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
                    simulator.Keyboard.KeyDown(VirtualKeyCode.VK_S);
                    break;
            }
        }

        [VoiceCommand("skidaddle")]
        public static void Run(InputSimulator simulator)
        {
            simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            simulator.ToggleKey(VirtualKeyCode.SHIFT);
            ForceMovement(simulator);
        }

        [VoiceStop]
        [VoiceCommand("stop")]
        public static void Stop(InputSimulator simulator)
        {
            WalkVertical(simulator, VerticalAxis.none);
            simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
            simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
        }

        [VoiceCommand("crouch")]
        public static void Crouch(InputSimulator simulator)
        {
            new Thread(() =>
            {
                simulator.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                simulator.ToggleKey(VirtualKeyCode.LCONTROL);
                Thread.Sleep(50);
                ForceMovement(simulator);
            }).Start();
        }

        static void ForceMovement(InputSimulator simulator)
        {
            if (verticalAxis == VerticalAxis.none)
                WalkVertical(simulator, VerticalAxis.forward);
        }
    }
}