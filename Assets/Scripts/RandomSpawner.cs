using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private int objectsMaxCount;
    [SerializeField] private DestroyableObject objectType;
    [SerializeField] private Transform area;
    [SerializeField] private float spawnCooldown;

    private int _objectsCount = 0;
    private float _timeWhenSpawnNextObject;
    
    public event Action<DestroyableObject> OnObjectCreated;

    private void FixedUpdate()
    {
        TrySpawnObject();
    }
    
    private void TrySpawnObject()
    {
        if(_timeWhenSpawnNextObject > Time.time || _objectsCount >= objectsMaxCount)
            return;
        
        SpawnObject();
    }

    private void SpawnObject()
    {
        _timeWhenSpawnNextObject = Time.time + spawnCooldown;
        _objectsCount++;
        var lossyScale = area.lossyScale;
        var x = Random.Range(-lossyScale.x * 0.5f, lossyScale.x * 0.5f);
        var y = Random.Range(-lossyScale.y * 0.5f, lossyScale.y * 0.5f);
        var z = Random.Range(-lossyScale.z * 0.5f, lossyScale.z * 0.5f);
        var randomPosition = new Vector3(x, y, z) + area.transform.position;
        var obj = Instantiate(objectType, randomPosition, objectType.transform.rotation);
        obj.transform.localScale = objectType.transform.localScale;
        obj.transform.parent = area;
        obj.OnDestroy += UpdateObjectsCount;
        OnObjectCreated?.Invoke(obj);
    }

    private void UpdateObjectsCount(GameObject destroyableObject)
    {
        _objectsCount--;
    }

}