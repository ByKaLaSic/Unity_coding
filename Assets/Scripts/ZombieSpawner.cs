using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public event UnityAction<TextMeshProUGUI> ZombieCreated;

    [SerializeField] private DeadZombieChecker _deadZombieChecker;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _zombieToWin;
    [SerializeField] private int _maxZombie;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;

    private int _generalDeadZombie = 0;
    private Transform _zombieRoot;
    private float _spawnInterval = 0f;
    private int _currentCountZombie;

    public int ZombieToWin => _zombieToWin;

    private void OnEnable()
    {
        _deadZombieChecker.ZombieDead += UpdateZombieCount;
    }

    private void OnDisable()
    {
        _deadZombieChecker.ZombieDead -= UpdateZombieCount;
    }

    private void Start()
    {
        if (_zombiePrefab == null)
        {
            Debug.LogError("The spawn object is not specified!");
            return;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points not specified!");
            return;
        }

        _zombieRoot = new GameObject("ZombieRoot").transform;
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

    private void UpdateZombieCount()
    {
        _generalDeadZombie++;
        _currentCountZombie--;
    }

    private void SpawnZombie()
    {
        _currentCountZombie++;

        int randomPoint = Random.Range(0, _spawnPoints.Length);
        GameObject zombie = Instantiate(_zombiePrefab, _spawnPoints[randomPoint].position, Quaternion.identity, _zombieRoot);
        ZombieCreated?.Invoke(zombie.GetComponentInChildren<TextMeshProUGUI>());
    }
}
