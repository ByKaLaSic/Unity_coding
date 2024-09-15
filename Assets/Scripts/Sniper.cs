using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    [SerializeField] private SniperBullet _sniperBulletPrefab;
    [SerializeField] private int _countInClip;

    private SniperBullet[] _sniperBullets;
    private Transform _sniperBulletRoot;

    protected override void Start()
    {
        base.Start();
        _sniperBulletRoot = new GameObject("SniperBulletRoot").transform;
        _sniperBulletRoot.SetParent(gameObject.transform);
        Recharge();
    }

    public override void Fire()
    {
        if (CanShoot == false)
        {
            return;
        }

        if (TryGetBullet(out SniperBullet sniperBullet))
        {
            sniperBullet.Run(_barrel.forward * Force, _barrel.position);
            _lastShootTime = 0.0f;
        }
    }

    public override void Recharge()
    {
        foreach (Transform bullet in _sniperBulletRoot)
        {
            Destroy(bullet.gameObject);
        }

        _sniperBullets = new SniperBullet[_countInClip];

        for (int i = 0; i < _countInClip; i++)
        {
            SniperBullet sniperBullet = Instantiate(_sniperBulletPrefab, _sniperBulletRoot);
            sniperBullet.Sleep();
            _sniperBullets[i] = sniperBullet;
        }
    }

    private bool TryGetBullet(out SniperBullet sniperBullet)
    {
        int candidate = -1;

        if (_sniperBullets == null)
        {
            sniperBullet = default;
            return false;
        }

        for (int i = 0; i < _sniperBullets.Length; i++)
        {
            if (_sniperBullets[i] == null)
            {
                continue;
            }

            if (_sniperBullets[i].IsActive == true)
            {
                continue;
            }

            candidate = i;
            break;
        }

        if (candidate == -1)
        {
            sniperBullet = default;
            return false;
        }

        sniperBullet = _sniperBullets[candidate];
        return true;
    }
}
