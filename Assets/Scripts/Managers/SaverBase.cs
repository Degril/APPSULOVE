using UnityEngine;

namespace Managers
{
    public abstract class SaverBase : MonoBehaviour
    {
        public abstract void Save<T>(string id, T value);
        public abstract bool TryLoad<T>(string id, out T value);
    }
}