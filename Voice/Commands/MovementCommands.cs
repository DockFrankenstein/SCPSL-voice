using SLVoiceController.Config;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class MovementCommands
    {
        enum VerticalAxis { none, forward, backward }
        static VerticalAxis verticalAxis;

        [VoiceCommand("movement_forward", "forward")]
        public static void WalkForward() =>
            WalkVertical(VerticalAxis.forward);

        [VoiceCommand("movement_backward", "backward")]
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

        [VoiceCommand("movement_run", "sprint")]
        public static void Run()
        {
            SLKeys.current.sneak.KeyUp();
            SLKeys.current.run.ToggleKey();
            ForceMovement();
        }

        [VoiceStop]
        [VoiceCommand("movement_stop", "stop")]
        public static void Stop()
        {
            WalkVertical(VerticalAxis.none);
        }

        [VoiceCommand("movement_sneak", "sneak")]
        public static void Crouch()
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