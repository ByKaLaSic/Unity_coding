using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClipWeapon : Weapon
{
    [SerializeField] private ClipBullet _bulletPrefab;
    [SerializeField] private int _countInClip;

    private Transform _bulletRoot;
    private Queue<ClipBullet> _bullets;

    public override int AmmunitionLeft => _bullets.Count;

    private void Awake()
    {
        _bullets = new Queue<ClipBullet>(_countInClip);
        _bulletRoot = new GameObject("BulletRoot").transform;
        _bulletRoot.SetParent(gameObject.transform);
        Recharge();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire()
    {
        if (CanShoot == false)
        {
            return;
        }

        if (_bullets.TryDequeue(out ClipBullet bullet))
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
            ClipBullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
            bullet.Sleep();
            _bullets.Enqueue(bullet);
        }
    }
}
