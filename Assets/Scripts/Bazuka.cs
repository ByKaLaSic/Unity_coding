using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Bazuka : Weapon
{
    [SerializeField] private Rocket _rocketPrefab;

    private Rocket _instantiateRocket;

    public override void Fire()
    {
        if (_instantiateRocket != null)
        {
            _instantiateRocket.Run(_barrel.forward * Force);
            _instantiateRocket = null;
        }
    }

    public override void Recharge()
    {
        if (_instantiateRocket != null)
        {
            return;
        }

        _instantiateRocket = Instantiate(_rocketPrefab, _barrel);
        _instantiateRocket.Sleep(_barrel.position);
    }
}
