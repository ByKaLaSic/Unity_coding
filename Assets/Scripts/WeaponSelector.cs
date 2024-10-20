using UnityEngine;
using UnityEngine.Events;

public sealed class WeaponSelector
{
    public event UnityAction<Weapon> WeaponChanged;

    private int _currentWeaponIndex;
    private Weapon _currentWeapon;

    private readonly Weapon[] _weapons;

    public WeaponSelector(Weapon[] weapons)
    {
        _weapons = weapons;
    }

    public void Fire()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Fire();
            WeaponChanged?.Invoke(_currentWeapon);
        }
    }

    public void Recharge()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Recharge();
            WeaponChanged?.Invoke(_currentWeapon);
        }
    }

    public void Next()
    {
        _currentWeaponIndex++;
        SelectWeapon();
    }

    public void Preview()
    {
        _currentWeaponIndex--;
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
        }

        int index = Mathf.Abs(_currentWeaponIndex % _weapons.Length);
        _currentWeapon = _weapons[index];
        _currentWeapon.gameObject.SetActive(true);
        WeaponChanged?.Invoke(_currentWeapon);
    }
}
