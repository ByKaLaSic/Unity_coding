//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Gun : MonoBehaviour
//{
//    [SerializeField] private Transform _barrel;
//    [SerializeField] private Bullet _bulletPrefab;
//    [SerializeField] private int _countInClip;
//    [SerializeField] private float _force;
//    [SerializeField] private float _shotDelay;

//    private Transform _bulletRoot;
//    private Bullet[] _bullets;
//    private bool _canShoot;
//    private float _lastShootTime;

//    private void Start()
//    {
//        _bulletRoot = new GameObject("BulletRoot").transform;
//        Recharge();
//    }

//    private void Update()
//    {
//        _canShoot = _shotDelay <= _lastShootTime;

//        if (_canShoot)
//        {
//            return;
//        }

//        _lastShootTime += Time.deltaTime;
//    }

//    public void Fire()
//    {
//        if (_canShoot == false)
//        {
//            return;
//        }

//        if (TryGetBullet(out Bullet bullet))
//        {
//            bullet.Run(_barrel.forward * _force, _barrel.position);
//            _lastShootTime = 0.0f;
//        }
//    }

//    public void Recharge()
//    {
//        foreach (Transform bullet in _bulletRoot)
//        {
//            Destroy(bullet.gameObject);
//        }

//        _bullets = new Bullet[_countInClip];

//        for (int i = 0; i < _countInClip; i++)
//        {
//            Bullet bullet = Instantiate(_bulletPrefab, _bulletRoot);
//            bullet.Sleep();
//            _bullets[i] = bullet;
//        }
//    }

//    private bool TryGetBullet(out Bullet bullet)
//    {
//        int candidate = -1;

//        if (_bullets == null)
//        {
//            bullet = default;
//            return false;
//        }

//        for (int i = 0; i < _bullets.Length; i++)
//        {
//            if (_bullets[i] == null)
//            {
//                continue;
//            }

//            if (_bullets[i].IsActive == true)
//            {
//                continue;
//            }

//            candidate = i;
//            break;
//        }

//        if (candidate == -1)
//        {
//            bullet = default;
//            return false;
//        }

//        bullet = _bullets[candidate];
//        return true;
//    }
//}
