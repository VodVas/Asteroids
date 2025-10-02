using System;

namespace AsteroidsClone
{
    public sealed class ReactiveProperty<T>
    {
        private T _value;

        public event Action<T> OnChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnChanged?.Invoke(_value);
                }
            }
        }
    }
}