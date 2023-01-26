using System;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public event Action<GameObject> OnDestroy;

    public void DestroyWithEvent()
    {
        OnDestroy?.Invoke(gameObject);
        Destroy(gameObject);
    }
}