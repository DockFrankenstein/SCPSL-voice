using System.Text;
using System.Text.Json;

namespace SLVoiceController.File
{
    public static class FileManager
    {
        #region Folder Path
        public static string GetCustomFolderPath(Environment.SpecialFolder type) { return Environment.GetFolderPath(type); }
        #endregion

        #region Trim
        /// <summary>Trims paths end by specified amount of folders</summary>
        /// <param name="amount">Amount of folders to trim</param>
        public static string TrimPathEnd(string path, int amount)
        {
            path = path.Replace('\\', '/');
            amount = Math.Abs(amount);
            string[] folders = path.Split('/');

            string trimmedPath = string.Empty;
            for (int i = 0; i < folders.Length - amount; i++) trimmedPath += $"{folders[i]}/";
            return trimmedPath.TrimEnd('/');
        }

        /// <summary>Trims paths start by specified amount of folders</summary>
        /// <param name="amount">Amount of folders to trim</param>
        public static string TrimPathStart(string path, int amount)
        {
            path = path.Replace('\\', '/');
            amount = Math.Abs(amount);
            string[] folders = path.Split('/');

            string trimmedPath = string.Empty;
            for (int i = 0; i < folders.Length - amount; i++)
            {
                trimmedPath += $"{folders[i + amount]}/";
            }
            return trimmedPath.TrimEnd('/');
        }
        #endregion

        #region Writer
        public static void SaveWriter(string path, string data)
        {
            string directory = TrimPathEnd(path, 1);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            StreamWriter writer = new StreamWriter(path);
            writer.Write(data);
            writer.Flush();
            writer.Close();
        }

        public static string LoadWriter(string path)
        {
            StreamReader reader = new StreamReader(path);
            string data = reader.ReadToEnd();
            reader.Close();
            if (data == null) data = string.Empty;
            return data;
        }

        public static bool TrySaveFileWriter(string path, string data)
        {
            try
            {
                SaveWriter(path, data);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool TryLoadFileWriter(string path, out string data)
        {
            data = string.Empty;
            try
            {
                data = LoadWriter(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region JSON
        public static void SaveJSON<T>(string path, T? data)
        {
            SaveWriter(path, JsonSerializer.Serialize(data, new JsonSerializerOptions() { IncludeFields = true, WriteIndented = true }));
        }

        public static T? LoadJSON<T>(string path)
        {
            string textData = LoadWriter(path);
            object? data = JsonSerializer.Deserialize<T>(textData);
            if (data == null)
                throw new Exception($"Couldn't read JSON data from {textData}");

            return (T?)data;
        }

        public static bool TrySaveFileJSON(string path, object data)
        {
            try
            {
                SaveJSON(path, data);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool TryReadFileJSON<T>(string path, out T? data)
        {
            data = default;
            try
            {
                data = LoadJSON<T>(path);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
