using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleServiceLocator : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> instancesToInitialize = new ();

    private static readonly Dictionary<Type, MonoBehaviour> _objects = new();
    
    public static bool TryGet<T>(out T value) where T : MonoBehaviour
    {
        if (_objects.TryGetValue(typeof(T), out var instance))
        {
            value = (T)instance;
            return true;
        }

        value = null;
        return false;
    }

    private void Awake()
    {
        foreach (var monoBehaviour in instancesToInitialize)
        {
            int i = 0;
            var type = monoBehaviour.GetType();
            do
            {
                _objects.Add(type, monoBehaviour);
                type = type.BaseType;
            } while (type != typeof(object) && type!= typeof(MonoBehaviour));
        }
    }
}
