using System.Reflection;

using static ConsoleSystem.ConsoleLogger;

namespace ConsoleSystem.Commands
{
    public static class ConsoleCommandList
    {
        static List<ConsoleCommand> commands = new List<ConsoleCommand>();

        public static List<ConsoleCommand> Commands { get => commands; }

        public static void Initialize()
        {
            Log("Creating command list...", ConsoleColor.Magenta);
            List<Type> types = GetTypes();
            commands = new List<ConsoleCommand>();

            for (int i = 0; i < types.Count; i++)
            {
                ConstructorInfo? constructor = types[i].GetConstructor(Type.EmptyTypes);
                if (constructor == null) return;

                ConsoleCommand command = (ConsoleCommand)constructor.Invoke(null);

                if (command.Active)
                    commands.Add(command);
            }

            Log($"Generated command list, command count: {commands.Count}", ConsoleColor.Blue);
        }

        static List<Type> GetTypes()
        {
            var type = typeof(ConsoleCommand);
            Assembly? assembly = Assembly.GetAssembly(type);
            if (assembly == null) return new List<Type>();
            return assembly.GetTypes().Where(t => t != type && type.IsAssignableFrom(t)).ToList();
        }

        public static bool TryGettingConsoleCommand(string cmd, out ConsoleCommand? command)
        {
            command = null;
            for (int i = 0; i < Commands.Count; i++)
            {
                if (!AliasExists(Commands[i], cmd) || !Commands[i].Active) continue;

                command = Commands[i];
                return true;
            }

            return false;
        }

        static bool AliasExists(ConsoleCommand command, string targetName)
        {
            if (command.CommandName == targetName) return true;
            if (command.Aliases == null) return false;
            for (int i = 0; i < command.Aliases.Length; i++)
                if (command.Aliases[i] == targetName)
                    return true;
            return false;
        }
    }
}