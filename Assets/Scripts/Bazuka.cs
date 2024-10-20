using UnityEngine;

public sealed class Bazuka : Weapon
{
    [SerializeField] private Rocket _rocketPrefab;

    private Rocket _instantiateRocket;
    private int _ammunition = 0;

    public override int AmmunitionLeft => _ammunition;

    public override void Fire()
    {
        if (_instantiateRocket != null)
        {
            AudioSource.PlayOneShot(ShotClip);
            _instantiateRocket.Run(_barrel.forward * Force);
            _instantiateRocket = null;
            _ammunition--;
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
        _ammunition++;
    }
}
