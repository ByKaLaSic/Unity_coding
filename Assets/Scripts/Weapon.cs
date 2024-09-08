using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _shotDelay;
    [SerializeField] protected Transform _barrel;

    protected float _lastShootTime;
    protected bool CanShoot { get; private set; }

    protected float Force
    {
        get
        {
            return _force;
        }
    }

    private void Update()
    {
        CanShoot = _shotDelay <= _lastShootTime;

        if (CanShoot)
        {
            return;
        }

        _lastShootTime += Time.deltaTime;
    }

    public abstract void Fire();
    public abstract void Recharge();
}
