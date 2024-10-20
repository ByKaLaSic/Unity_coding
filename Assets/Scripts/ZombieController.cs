using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public sealed class ZombieController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    
    private Transform _player;

    private void Start()
    {
        ThirdPersonController playerController = FindObjectOfType<ThirdPersonController>();

        if (playerController != null)
        {
            _player = playerController.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            MoveTo(_player.position);
        }
    }

    private void MoveTo(Vector3 target)
    {
        _agent.SetDestination(target);
    }
}
