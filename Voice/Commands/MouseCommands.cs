using WindowsInput;
using System.Media;

namespace SLVoiceController.VoiceCommands.Commands
{
    public static class MouseCommands
    {
        public const int MoveAmmount = 600;
        public const int SmallMoveAmmount = 100;

        public const int MouseMoveInterval = 20;
        public const int MouseSpeed = 600;

        static bool allowRotating = true;

        //[VoiceCommand("look up")]
        //public static void RotateUp(InputSimulator simulator) =>
        //    StartRotating(simulator, 0, -1);

        //[VoiceCommand("look down")]
        //public static void RotateDown(InputSimulator simulator) =>
        //    StartRotating(simulator, 0, 1);

        //[VoiceCommand("look left")]
        //public static void RotateLeft(InputSimulator simulator) =>
        //    StartRotating(simulator, -1, 0);

        //[VoiceCommand("look right")]
        //public static void RotateRight(InputSimulator simulator) =>
        //    StartRotating(simulator, 1, 0);

        [VoiceStop]
        [VoiceCommand("stop looking")]
        public static void StopRotation(InputSimulator simulator)
        {
            allowRotating = false;
        }


        //[VoiceCommand("Quick up")]
        //public static void SmallUp(InputSimulator simulator) =>
        //    simulator.Mouse.MoveMouseBy(0, -SmallMoveAmmount);

        //[VoiceCommand("Quick down")]
        //public static void SmallDown(InputSimulator simulator) =>
        //    simulator.Mouse.MoveMouseBy(0, SmallMoveAmmount);

        //[VoiceCommand("Quick right")]
        //public static void SmallRight(InputSimulator simulator) =>
        //    simulator.Mouse.MoveMouseBy(-SmallMoveAmmount, 0);

        //[VoiceCommand("Quick left")]
        //public static void SmallLeft(InputSimulator simulator) =>
        //    simulator.Mouse.MoveMouseBy(SmallMoveAmmount, 0);

        [VoiceCommand("helicopter")]
        public static void Helicopter(InputSimulator simulator) =>
            StartRotating(simulator, 30, 0);

        public static void StartRotating(InputSimulator simulator, int xMultiplier, int yMultiplier)
        {
            allowRotating = true;
            new Thread(() =>
            {
                Audio.AudioManager.helicopter.Play();
                while (allowRotating)
                {
                    simulator.Mouse.MoveMouseBy(MouseSpeed * MouseMoveInterval * xMultiplier / 1000, MouseSpeed * MouseMoveInterval * yMultiplier / 1000);
                    Thread.Sleep(MouseMoveInterval);
                }
            }).Start();
        }
    }
}