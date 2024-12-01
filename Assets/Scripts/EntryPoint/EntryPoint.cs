using UnityEngine;
using System.Collections.Generic;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private ZombieHealthController _enemyPrefab;
    [SerializeField] private int _enemyPoolSize = 15;

    private Dictionary<string, object> _pools;
    private PlayerLocalSaver _playerLocalSaver;


    private void Awake()
    {
        _pools = new Dictionary<string, object>();
        CreatePool("ZombiePool", _enemyPrefab, _enemyPoolSize);
        _playerLocalSaver = new PlayerLocalSaver(_player);
        AnalyticsManager.OnLevelStarted();
    }

    public void SavePlayerPosition()
    {
        _playerLocalSaver.SavePlayerPosition();
    }

    public void LoadPlayerPosition()
    {
        _playerLocalSaver.LoadPlayerPosition();
    }

    private void CreatePool<T>(string key, T prefab, int size) where T : Component
    {
        ObjectPool<T> pool = new ObjectPool<T>(prefab, size);
        _pools[key] = pool;
    }

    public ObjectPool<T> GetPool<T>(string key) where T : Component
    {
        if (_pools.TryGetValue(key, out object pool))
        {
            return pool as ObjectPool<T>;
        }

        return null;
    }
}
