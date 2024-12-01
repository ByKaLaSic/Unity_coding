using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public sealed class ZombieController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Transform _playerTransform;

    public void Initialize(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void Update()
    {
        if (_playerTransform != null)
        {
            MoveTo(_playerTransform.position);
        }
    }

    private void MoveTo(Vector3 target)
    {
        _agent.SetDestination(target);
    }
}
