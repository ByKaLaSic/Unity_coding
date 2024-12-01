using UnityEngine;
using System.Collections.Generic;

public sealed class ObjectPool<T> where T : Component
{
    private T _prefab;
    private int _poolSize;

    private Queue<T> objectPool;
    private GameObject _root;

    public ObjectPool(T prefab, int poolSize)
    {
        _prefab = prefab;
        _poolSize = poolSize;

        _root = new GameObject($"{_prefab.name}PoolRoot");
        objectPool = new Queue<T>();

        for (int i = 0; i < _poolSize; i++)
        {
            T obj = Object.Instantiate(_prefab, _root.transform);
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public T GetObjectFromPool()
    {
        if (objectPool.Count > 0)
        {
            T obj = objectPool.Dequeue();
            return obj;
        }
        else
        {
            T obj = Object.Instantiate(_prefab, _root.transform);
            return obj;
        }
    }

    public void ReturnObjectToPool(T obj)
    {
        objectPool.Enqueue(obj);
    }
}
