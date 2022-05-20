namespace VoiceCommands.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VoiceCommandAttribute : Attribute
    {
        string commandName = "";
        string group = "";

        public string CommandName => commandName;
        public string Group => group;

        public VoiceCommandAttribute(string commandName)
        {
            this.commandName = commandName;
        }

        public VoiceCommandAttribute(string commandName, string group)
        {
            this.commandName = commandName;
            this.group = group;
        }
    }
}