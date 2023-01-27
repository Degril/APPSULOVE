using System;

namespace Data
{
    public class SimpleObserver<T>
    {
        public SimpleObserver(T value)
        {
            _value = value;
        }

        public SimpleObserver()
        {
            
        }

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