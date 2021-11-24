using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Console_Web_Store_For_EPAM
{
    /// <summary>
    /// Helps with sending/receiving data from JSON files.
    /// </summary>
    public static class JsonHelpers
    {
        /// <summary>
        /// Get data from json file. Create a new file if it cants found.
        /// </summary>
        /// <typeparam name="T">Data model.</typeparam>
        /// <returns>List of the data.</returns>
        public static List<T> GetData<T>()
        {
            string fileName = GetFileName<T>();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string data = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(data))
                    {
                        return JsonSerializer.Deserialize<List<T>>(data);
                    }

                    return new List<T>();
                }
            }
        }

        /// <summary>
        /// Record data to the file. Create a new file if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">Data model.</typeparam>
        /// <param name="data">Data for record.</param>
        public static void SaveData<T>(List<T> data)
        {
            string fileName = GetFileName<T>();
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    string jsonString = JsonSerializer.Serialize(data);
                    writer.Write(jsonString);
                }
            }
        }

        /// <summary>
        /// Get file name for data storage.
        /// It's located in the main app folder and named with the data model name.
        /// </summary>
        /// <typeparam name="T">Data model.</typeparam>
        /// <returns>File name like "*DataModelName*.json".</returns>
        private static string GetFileName<T>()
        {
            return typeof(T).Name + ".json";
        }
    }
}