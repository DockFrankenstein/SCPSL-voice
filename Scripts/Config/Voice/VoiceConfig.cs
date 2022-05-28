using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SLVoiceController.Config
{
    public class VoiceConfig
    {
        public static VoiceConfig current = new VoiceConfig();

        public VoiceConfig() { }

        public VoiceConfig(VoiceConfig other)
        {
            _items = new Dictionary<string, CommandData>(other._items);
        }

        public List<CommandData> _serializableItems = new List<CommandData>();

        private Dictionary<string, CommandData> _items = new Dictionary<string, CommandData>();

        [Serializable]
        public struct CommandData
        {
            public string key;
            public string command;
            public string language;

            public CommandData(string key)
            {
                this.key = key;
                command = string.Empty;
                language = string.Empty;
            }

            public CommandData(string key, string command)
            {
                this.key = key;
                this.command = command;
                language = string.Empty;
            }
        }

        [OnSerializing]
        private void OnSerializingMethod(StreamingContext context)
        {
            _serializableItems = _items.Select(x => x.Value).ToList();
        }

        [OnDeserialized]
        private void OnDeserializedMethod(StreamingContext context)
        {
            _items = _serializableItems.ToDictionary(x => x.key);
        }

        public void AddItem(CommandData data)
        {
            switch (_items.ContainsKey(data.key))
            {
                case true:
                    _items[data.key] = data;
                    break;
                case false:
                    _items.Add(data.key, data);
                    break;
            }
        }

        public CommandData GetItem(string key, string defaultCommand)
        {
            EnsureItem(key, defaultCommand);
            return _items[key];
        }

        public CommandData[] GetItemsFromCommand(string command) =>
            _items.Where(x => x.Value.command == command)
            .Select(x => x.Value)
            .ToArray();
        
        public void EnsureItem(string key, string defaultCommand)
        {
            if (!_items.ContainsKey(key))
                _items.Add(key, new CommandData(key, defaultCommand));
        }
    }
}