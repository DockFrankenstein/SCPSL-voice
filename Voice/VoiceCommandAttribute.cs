namespace SLVoiceController.VoiceCommands.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VoiceCommandAttribute : Attribute
    {
        string key = string.Empty;
        string defaultCommand = string.Empty;
        public string group = string.Empty;

        public string Key => key;
        public string DefaultCommand => defaultCommand;
        public string Group => group;

        public VoiceCommandAttribute(string key)
        {
            this.key = key;
        }

        public VoiceCommandAttribute(string key, string defaultCommand)
        {
            this.key = key;
            this.defaultCommand = defaultCommand;
        }
    }
}