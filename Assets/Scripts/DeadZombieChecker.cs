using UnityEngine;
using UnityEngine.Events;

public sealed class DeadZombieChecker : MonoBehaviour
{
    public event UnityAction ZombieDead;
    public event UnityAction AllZombieDead;

    [SerializeField] private ZombieSpawner _zombieSpawner;

    private int _zombieCount;

    private void Start()
    {
        _zombieCount = _zombieSpawner.ZombieToWin;
    }

    private void OnEnable()
    {
        ZombieHealthController.ZombieDeadSimple += ReportDeadZombie;
    }

    private void OnDisable()
    {
        ZombieHealthController.ZombieDeadSimple -= ReportDeadZombie;
    }

    private void ReportDeadZombie()
    {
        _zombieCount--;
        ZombieDead?.Invoke();

        if (_zombieCount == 0)
        {
            AllZombieDead?.Invoke();
        }
    }
}
