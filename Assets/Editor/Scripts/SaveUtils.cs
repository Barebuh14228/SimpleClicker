using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Scripts
{
    public static class SaveUtils
    {
        [MenuItem("Save Data/Clear")]
        public static void DeleteSave()
        {
            if (!File.Exists(App.SavePath))
                return;
            
            File.Delete(App.SavePath);
            
            Debug.Log($"<color=orange>Save data removed</color>");
        }
    }
}