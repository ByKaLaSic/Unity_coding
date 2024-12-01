using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public event UnityAction<TextMeshProUGUI> ZombieCreated;

    [SerializeField] private DeadZombieChecker _deadZombieChecker;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _zombieToWin;
    [SerializeField] private int _maxZombie;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private EntryPoint _entryPoint;

    private ObjectPool<ZombieHealthController> _zombiePool;
    private int _generalDeadZombie = 0;
    private float _spawnInterval = 0f;
    private int _currentCountZombie;

    public int ZombieToWin => _zombieToWin;

    private void OnEnable()
    {
        _deadZombieChecker.ZombieDead += DecrementZombieCount;
    }

    private void OnDisable()
    {
        _deadZombieChecker.ZombieDead -= DecrementZombieCount;
    }

    private void Start()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points not specified!");
            return;
        }

        _zombiePool = _entryPoint.GetPool<ZombieHealthController>("ZombiePool");

        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        while(true)
        {
            if (_currentCountZombie < _maxZombie)
            {
                if (_generalDeadZombie + _currentCountZombie < _zombieToWin)
                {
                    SpawnZombie();
                    _spawnInterval = Random.Range(_minSpawnDelay, _maxSpawnDelay);
                }
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void DecrementZombieCount()
    {
        _generalDeadZombie++;
        _currentCountZombie--;
    }

    private void IncrementZombieCount()
    {
        _currentCountZombie++;
    }

    private void SpawnZombie()
    {
        IncrementZombieCount();

        int randomPoint = Random.Range(0, _spawnPoints.Length);
        ZombieHealthController zombie = _zombiePool.GetObjectFromPool();
        zombie.Initialize(_entryPoint);
        zombie.transform.position = _spawnPoints[randomPoint].position;
        zombie.gameObject.SetActive(true);
        zombie.GetComponent<ZombieController>().Initialize(_playerTransform);
        ZombieCreated?.Invoke(zombie.gameObject.GetComponentInChildren<TextMeshProUGUI>());
    }
}
