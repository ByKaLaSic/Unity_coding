using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObjectPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        if (_spawnObjectPrefab == null)
        {
            Debug.LogError("The spawn object is not specified!");
            return;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points not specified!");
            return;
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_spawnObjectPrefab, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}
