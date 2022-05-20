using System.Media;
using ConsoleSystem;

using static ConsoleSystem.ConsoleLogger;

namespace Audio
{
    public static class AudioManager
    {
        public static SoundPlayer vineThud = new SoundPlayer("vine_thud.wav");
        public static SoundPlayer helicopter = new SoundPlayer("helicopter.wav");

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