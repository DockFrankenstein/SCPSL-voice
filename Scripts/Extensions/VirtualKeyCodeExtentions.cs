﻿using WindowsInput;
using WindowsInput.Native;
using SLVoiceController.VoiceCommands;

namespace SLVoiceController
{
    static class VirtualKeyCodeExtentions
    {
        static InputSimulator simulator =>
            VoiceCommandController.Simulator;

        public static VirtualKeyCode KeyPress(this VirtualKeyCode key)
        {
            switch (key)
            {
                case VirtualKeyCode.RBUTTON:
                    simulator.Mouse.RightButtonClick();
                    break;
                case VirtualKeyCode.LBUTTON:
                    simulator.Mouse.LeftButtonClick();
                    break;
                default:
                    simulator.Keyboard.KeyPress(key);
                    break;
            }

            simulator.Keyboard.KeyPress(key);
            return key;
        }

        public static VirtualKeyCode KeyDown(this VirtualKeyCode key)
        {
            switch (key)
            {
                case VirtualKeyCode.RBUTTON:
                    simulator.Mouse.RightButtonDown();
                    break;
                case VirtualKeyCode.LBUTTON:
                    simulator.Mouse.LeftButtonDown();
                    break;
                default:
                    simulator.Keyboard.KeyDown(key);
                    break;
            }

            return key;
        }

        public static VirtualKeyCode KeyUp(this VirtualKeyCode key)
        {
            switch (key)
            {
                case VirtualKeyCode.RBUTTON:
                    simulator.Mouse.RightButtonUp();
                    break;
                case VirtualKeyCode.LBUTTON:
                    simulator.Mouse.LeftButtonUp();
                    break;
                default:
                    simulator.Keyboard.KeyUp(key);
                    break;
            }

            return key;
        }

        public static bool IsKeyUp(this VirtualKeyCode key) =>
            simulator.InputDeviceState.IsKeyUp(key);

        public static bool IsKeyDown(this VirtualKeyCode key) =>
            simulator.InputDeviceState.IsKeyDown(key);

        public static VirtualKeyCode IsKeyUp(this VirtualKeyCode key, out bool isUp)
        {
            isUp = IsKeyUp(key);
            return key;
        }

        public static VirtualKeyCode IsKeyDown(this VirtualKeyCode key, out bool isUp)
        {
            isUp = IsKeyDown(key);
            return key;
        }

        public static bool IsHardwareKeyUp(this VirtualKeyCode key) =>
            simulator.InputDeviceState.IsHardwareKeyUp(key);

        public static bool IsHardwareKeyDown(this VirtualKeyCode key) =>
            simulator.InputDeviceState.IsHardwareKeyDown(key);

        public static VirtualKeyCode IsHardwareKeyUp(this VirtualKeyCode key, out bool isUp)
        {
            isUp = IsHardwareKeyUp(key);
            return key;
        }

        public static VirtualKeyCode IsHardwareKeyDown(this VirtualKeyCode key, out bool isUp)
        {
            isUp = IsHardwareKeyDown(key);
            return key;
        }

        public static VirtualKeyCode ChangeKeyState(this VirtualKeyCode key, bool state)
        {
            switch (state)
            {
                case true:
                    key.KeyDown();
                    break;
                case false:
                    key.KeyUp();
                    break;
            }

            return key;
        }

        public static VirtualKeyCode ToggleKey(this VirtualKeyCode key)
        {
            simulator.ToggleKey(key);
            return key;
        }

        public static VirtualKeyCode Wait(this VirtualKeyCode key, int milisecondsTimeout)
        {
            Thread.Sleep(milisecondsTimeout);
            return key;
        }
    }
}