using System.Speech.Recognition;
using WindowsInput;
using System.Reflection;
using SLVoiceController.VoiceCommands.Commands;
using ConsoleSystem.Logic;
using SLVoiceController.Config;

using static ConsoleSystem.ConsoleLogger;

namespace SLVoiceController.VoiceCommands
{
    public static class VoiceCommandController
    {
        static SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

        public static InputSimulator Simulator { get; private set; } = new InputSimulator();
        public static string[] CommandKeys { get; private set; } = new string[0];
        public static string[] CommandNames { get; private set; } = new string[0];

        public static List<MethodInfo>? Commands { get; private set; }
        public static List<MethodInfo>? StopMethods { get; private set; }

        public static bool Pause { get; set; } = false;

        [ConsoleSystem.Initialize]
        public static void Initialize()
        {
            if (Commands == null)
                CreateCommandList();

            VoiceSerializer.SetupVoiceConfig();

            GenerateCommandNames();
            Simulator = new InputSimulator();

            Choices commands = new Choices(CommandNames.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());

            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(commands)));

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(HandleRecognition);
        }

        public static void Start()
        {
            Pause = false;

            new Thread(() =>
            {
                recognizer.SetInputToDefaultAudioDevice();
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }).Start();

            Log($"Started speech recognition.", ConsoleColor.Green);
        }

        public static void Stop()
        {
            recognizer.RecognizeAsyncStop();

            for (int i = 0; i < StopMethods.Count; i++)
            {
                if (!CommandHasCorrectParameters(StopMethods[i], new Type[0], new Type[] { typeof(InputSimulator) }, out int parameterCount)) continue;
                StopMethods[i].Invoke(null, parameterCount == 0 ? new object[0] : new object[] { Simulator });
            }

            Pause = false;
            Log($"Speech recognition has been disabled.", ConsoleColor.Magenta);
        }

        static void CreateCommandList()
        {
            Log("Creating command list, this could take some time...", ConsoleColor.DarkYellow);

            var attributes = TypeFinder.FindAttributes<VoiceCommandAttribute>();
            var stopAttributes = TypeFinder.FindAttributes<VoiceStopAttribute>();

            Log("Reflections finished, creating list...", ConsoleColor.DarkYellow);

            Commands = attributes.ToList();
            StopMethods = stopAttributes.ToList();
        }

        static void GenerateCommandNames()
        {
            if (Commands == null)
                return;

            CommandNames = new string[Commands.Count];
            CommandKeys = new string[Commands.Count];
            for (int i = 0; i < Commands.Count; i++)
            {
                if (!TryGetAttribute(Commands[i], out VoiceCommandAttribute? attribute)) continue;
                VoiceConfig.CommandData data = VoiceConfig.current.GetItem(attribute.Key, attribute.DefaultCommand);
                CommandKeys[i] = data.key;
                CommandNames[i] = data.command;
            }
        }

        internal static bool TryGetAttribute(MethodInfo method, out VoiceCommandAttribute? attribute)
        {
            attribute = null;

            VoiceCommandAttribute[] attrs = (VoiceCommandAttribute[])method.GetCustomAttributes(typeof(VoiceCommandAttribute), true);
            if (attrs.Length != 1) return false;

            attribute = attrs[0];
            return true;
        }

        static bool CommandHasCorrectParameters(MethodInfo method, Type[] args, Type[] optionalArgs, bool log = true) =>
            CommandHasCorrectParameters(method, args, optionalArgs, out _, log);

        static bool CommandHasCorrectParameters(MethodInfo method, Type[] args, Type[] optionalArgs, out int parameterCount, bool log = true)
        {
            ParameterInfo[] methodArgs = method.GetParameters();
            parameterCount = methodArgs.Length;

            Type[] requiredArgs = args.Concat(optionalArgs).ToArray();

            if (methodArgs.Length >= args.Length)
            {
                for (int i = 0; i < methodArgs.Length; i++)
                {
                    if (requiredArgs[i] == methodArgs[i].ParameterType) continue;

                    if (log)
                        LogError($"Method '{method.Name}' contains invalid parameter(s)!");

                    return false;
                }

                return true;
            }

            if (log)
                LogError($"Method '{method.Name}' parameter count is our of range!");

            return false;
        }

        static void HandleRecognition(object? sender, SpeechRecognizedEventArgs args)
        {
            if (Pause) return;

            VoiceConfig.CommandData[] commandData = VoiceConfig.current.GetItemsFromCommand(args.Result.Text);
            foreach (var command in commandData)
            {
                Log($"[Voice command] Recognized command '{command.key}': {command.command}", ConsoleColor.DarkGreen);
                ExecuteCommand(command);
            }
        }

        static void ExecuteCommand(VoiceConfig.CommandData command)
        {
            for (int i = 0; i < CommandNames.Length; i++)
            {
                if (CommandKeys[i] != command.key) continue;
                if (!CommandHasCorrectParameters(Commands[i], new Type[0], new Type[] { typeof(InputSimulator) }, out int count)) continue;

                Commands[i].Invoke(null, count == 0 ? new object[0] : new object[] { Simulator });
            }
        }

        [VoiceCommand("util_shutdown", "shutdown speech recognition")]
        public static void StopCommand()
        {
            Stop();
        }
    }
}