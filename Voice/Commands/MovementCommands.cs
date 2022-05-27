using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class MovementCommands
    {
        enum VerticalAxis { none, forward, backward }
        static VerticalAxis verticalAxis;

        [VoiceCommand("forward")]
        public static void WalkForward() =>
            WalkVertical(VerticalAxis.forward);

        [VoiceCommand("control z")]
        public static void Backward() =>
            WalkVertical(VerticalAxis.backward);

        static void WalkVertical(VerticalAxis axis)
        {
            if (verticalAxis == axis)
            {
                SLKeys.current.run.KeyUp();
                SLKeys.current.sneak.KeyUp();
                return;
            }

            verticalAxis = axis;
            switch (axis)
            {
                case VerticalAxis.none:
                    SLKeys.current.forward.KeyUp();
                    SLKeys.current.backward.KeyUp();
                    SLKeys.current.run.KeyUp();
                    SLKeys.current.sneak.KeyUp();
                    break;
                case VerticalAxis.forward:
                    SLKeys.current.forward.KeyDown();
                    SLKeys.current.backward.KeyUp();
                    break;
                case VerticalAxis.backward:
                    SLKeys.current.forward.KeyUp();
                    SLKeys.current.backward.KeyDown();
                    break;
            }
        }

        [VoiceCommand("skidaddle")]
        public static void Run(InputSimulator simulator)
        {
            SLKeys.current.sneak.KeyUp();
            SLKeys.current.run.ToggleKey();
            ForceMovement();
        }

        [VoiceStop]
        [VoiceCommand("stop")]
        public static void Stop(InputSimulator simulator)
        {
            WalkVertical(VerticalAxis.none);
        }

        [VoiceCommand("crouch")]
        public static void Crouch(InputSimulator simulator)
        {
            new Thread(() =>
            {
                SLKeys.current.run.KeyUp();
                SLKeys.current.sneak.ToggleKey();
                Thread.Sleep(50);
                ForceMovement();
            }).Start();
        }

        static void ForceMovement()
        {
            if (verticalAxis == VerticalAxis.none)
                WalkVertical(VerticalAxis.forward);
        }
    }
}