using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Gun : Weapon
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _countInClip;

    private Transform _bulletRoot;
    private Bullet[] _bullets;

    protected override void Start()
    {
        base.Start();
        _bulletRoot = new GameObject("BulletRoot").transform;
        _bulletRoot.SetParent(gameObject.transform);
        Recharge();
    }

    public override void Fire()
    {
        if (CanShoot == false)
        {
            return;
        }

        if (TryGetBullet(out Bullet bullet))
        {
            bullet.Run(_barrel.forward * Force, _barrel.position);
            _lastShootTime = 0.0f;
        }
    }

    public override void Recharge()
    {
        foreach (Transform bullet in _bulletRoot)
        {
            Destroy(bullet.gameObject);
        }

        _bullets = new Bullet[_countInClip];

        for (int i = 0; i < _countInClip; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
            bullet.Sleep();
            _bullets[i] = bullet;
        }
    }

    private bool TryGetBullet(out Bullet bullet)
    {
        int candidate = -1;

        if (_bullets == null)
        {
            bullet = default;
            return false;
        }

        for (int i = 0; i < _bullets.Length; i++)
        {
            if (_bullets[i] == null)
            {
                continue;
            }

            if (_bullets[i].IsActive == true)
            {
                continue;
            }

            candidate = i;
            break;
        }

        if (candidate == -1)
        {
            bullet = default;
            return false;
        }

        bullet = _bullets[candidate];
        return true;
    }
}
