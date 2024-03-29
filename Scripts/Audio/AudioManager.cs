﻿using System.Media;
using ConsoleSystem;

using static ConsoleSystem.ConsoleLogger;

namespace SLVoiceController.Audio
{
    public static class AudioManager
    {
        public static SoundPlayer vineThud = new SoundPlayer("Sfx/vine_thud.wav");
        public static SoundPlayer helicopter = new SoundPlayer("Sfx/helicopter.wav");

        [Initialize]
        public static void Initialize()
        {
            Log("[Audio] Loading sounds...", ConsoleColor.Gray);

            vineThud.Load();
            helicopter.Load();

            Log("[Audio] All sounds loaded", ConsoleColor.Gray);
        }
    }
}