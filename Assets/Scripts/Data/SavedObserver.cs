using Managers;
using UnityEngine;

namespace Data
{
    public class SavedObserver <T> : SimpleObserver<T>
    {
        private readonly string _id;
        private SaverBase _saver;
        public SavedObserver(string id)
        {
            SimpleServiceLocator.TryGet(out _saver);
            _id = id;
            Debug.Log(GetType().FullName);
            OnChanged += _ => Save();
            TryLoad();
        }

        private void Save()
        {
            _saver.Save(_id, Value);
        }

        private void TryLoad()
        {
            if (_saver.TryLoad<T>(_id, out var value))
            {
                Value = value;
            }
        }
    }
}