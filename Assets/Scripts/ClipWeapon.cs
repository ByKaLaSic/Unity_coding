using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ClipWeapon<T> : Weapon where T : ClipBullet
{
    [SerializeField] private T _bulletPrefab;
    [SerializeField] private int _countInClip;

    private Transform _bulletRoot;
    private Queue<T> _bullets;

    protected override void Start()
    {
        base.Start();
        _bullets = new Queue<T>(_countInClip);
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

        if (_bullets.TryDequeue(out T bullet))
        {
            AudioSource.PlayOneShot(ShotClip);
            bullet.Run(_barrel.forward * Force, _barrel.position);
            _lastShootTime = 0.0f;
        }
    }

    public override void Recharge()
    {
        if (_bulletRoot.childCount == _countInClip)
        {
            return;
        }

        if (_bulletRoot.childCount > 0)
        {
            foreach (Transform bullet in _bulletRoot)
            {
                Destroy(bullet.gameObject);
                _bullets.Clear();
            }
        }

        for (int i = 0; i < _countInClip; i++)
        {
            T bullet = Instantiate(_bulletPrefab, _bulletRoot);
            bullet.Sleep();
            _bullets.Enqueue(bullet);
        }
    }
}
