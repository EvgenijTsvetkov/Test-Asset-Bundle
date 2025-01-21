using System;
using System.IO;
using UnityEngine;

namespace Project.Services
{
    public class SaveSystem
    {
        private SaveData _saveData;

        private string _fullPath;

        public SaveData SaveData => _saveData;

        private const string SaveName = "save.dat";

        public SaveSystem()
        {
            _fullPath = Path.Combine(Application.persistentDataPath, SaveName);
        }
        public void Save()
        {
            try
            {
                File.WriteAllText(_fullPath, JsonUtility.ToJson(_saveData));
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {_fullPath} with exception {e}");
            }
        }

        public void Load()
        {
            try
            {
                _saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_fullPath));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to read from {_fullPath} with exception {e}");
                
                _saveData = new SaveData {Counter = -1};
            }
        }
    }
}