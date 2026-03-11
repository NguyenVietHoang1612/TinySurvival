using System.IO;
using UnityEngine;

namespace GameRPG
{
    public static class SaveSystem
    {
        public const string SAVE_FOLDER = "/09123938012/";

        public static void SaveJson<T>(T obj, string fileName)
        {
            string fullFolderPath = Application.persistentDataPath + SAVE_FOLDER;
            string filePath = Path.Combine(fullFolderPath, fileName);

            try
            {
                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                    Debug.Log("Created save directory at: " + fullFolderPath);
                }

                string dataToStore = JsonUtility.ToJson(obj, true);

                using (FileStream file = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.Write(dataToStore);
                    }
                }
                Debug.Log($"Successfully saved JSON to: {filePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error saving JSON to {filePath}: {e.Message}");
            }
        }

        public static T LoadJson<T>(string fileName)
        {
            string fullFolderPath = Application.persistentDataPath + SAVE_FOLDER;
            string filePath = Path.Combine(fullFolderPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    T loadedObj = JsonUtility.FromJson<T>(json);
                    Debug.Log($"Successfully loaded JSON from: {filePath}");
                    return loadedObj;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error loading JSON from {filePath}: {e.Message}");
                    return default(T);
                }
            }
            else
            {
                Debug.LogWarning($"JSON save file not found at: {filePath}");
                return default(T);
            }
        }

        public static void LoadJsonOverwrite<T>(T objToOverwrite, string fileName)
        {
            string fullFolderPath = Application.persistentDataPath + SAVE_FOLDER;
            string filePath = Path.Combine(fullFolderPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    JsonUtility.FromJsonOverwrite(json, objToOverwrite);
                    Debug.Log($"Successfully overwrote JSON data to object from: {filePath}");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error overwriting JSON data from {filePath}: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"JSON save file not found for overwrite at: {filePath}");
            }
        }

        public static bool SaveExistsJson(string fileName)
        {
            string fullFolderPath = Application.persistentDataPath + SAVE_FOLDER;
            string filePath = Path.Combine(fullFolderPath, fileName);
            return File.Exists(filePath);
        }
    }
}