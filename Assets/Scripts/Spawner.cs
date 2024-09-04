using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        if (_spawnObject == null)
        {
            Debug.LogError("Не указан объект для спавна!");
            return;
        }

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("Не указаны точки для спавна!");
            return;
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_spawnObject, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}
