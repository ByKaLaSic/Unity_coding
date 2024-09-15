using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponUpgradeData _upgradeData;
    [SerializeField] protected Transform _barrel;

    protected float _lastShootTime;
    protected float Force { get; private set; }
    protected bool CanShoot { get; private set; }

    private float _shootDelay;
    private int _level = 1;

    protected virtual void Start()
    {
        if (_upgradeData.TryGetWeaponData(_level, out WeaponData data))
        {
            _shootDelay = data.ShootDelay;
            Force = data.Force;
        }
    }

    private void Update()
    {
        CanShoot = _shootDelay <= _lastShootTime;

        if (CanShoot)
        {
            return;
        }

        _lastShootTime += Time.deltaTime;
    }

    public abstract void Fire();
    public abstract void Recharge();
}
