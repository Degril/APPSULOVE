using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    public class PrefsSaver : SaverBase
    {
        public override void Save<T>(string id, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(id, json);
        }

        public override bool TryLoad<T>(string id, out T value)
        {
            value = default;
            try
            {
                if (!PlayerPrefs.HasKey(id))
                    return false;
                var json = PlayerPrefs.GetString(id);
                    
                var obj = JsonConvert.DeserializeObject<T>(json);
                value = obj;
                return true;
            }
            catch
            {
                Debug.LogError($"Cannot deserialize \"{id}\" from player prefs");
            }

            return false;
        }
    }
}