using TMPro;
using System;
using UnityEngine;

public sealed class UIBulletIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private WeaponController _weaponController;

    private void Start()
    {
        _weaponController.WeaponSelector.WeaponChanged += ShowBulletCount;
    }

    public void ShowBulletCount(Weapon weapon)
    {
        _text.text = $"<sprite=0>{weapon.AmmunitionLeft}";
    }

    private void OnDestroy()
    {
        _weaponController.WeaponSelector.WeaponChanged -= ShowBulletCount;
    }
}
