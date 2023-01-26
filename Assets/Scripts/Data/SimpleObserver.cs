using System;
using UnityEngine.Events;

namespace Data
{
    public class SimpleObserver<T>
    {
        public event Action<T> OnChanged; 
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }
    }
}