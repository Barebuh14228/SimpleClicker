using System.IO;
using Components;
using Leopotam.Ecs;
using Save;
using UnityEngine;

namespace Systems
{
    public class LoadSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        public void Init()
        {
            if (TryLoad(out var saveData))
            {
                _world.NewEntity().Replace(new LoadedData()
                {
                    SaveData = saveData
                });
            }
            else
            {
                Debug.Log($"<color=yellow>Save not found</color>");
            }
        }
        
        private static bool TryLoad(out SaveData result)
        {
            result = null;
            
            if (!File.Exists(App.SavePath))
                return false;
            
            var json = File.ReadAllText(App.SavePath);
            
            result = JsonUtility.FromJson<SaveData>(json);
            
            return true;
        }
    }
}