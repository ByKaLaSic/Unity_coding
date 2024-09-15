using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeaponUpgradeData), menuName = "Data/Weapon/Upgrade")]
public sealed class WeaponUpgradeData : ScriptableObject
{
    [Serializable]
    private class WeaponDataByLevel
    {
        public int Level;
        public WeaponData Data;
    }

    [SerializeField] private WeaponDataByLevel[] _weaponData;
     
    public bool TryGetWeaponData(int level, out WeaponData weaponData)
    {
        for (int i = 0; i < _weaponData.Length; i++)
        {
            WeaponDataByLevel data = _weaponData[i];

            if (data.Level == level)
            {
                weaponData = data.Data;
                return true;
            }
        }

        weaponData = default;
        return false;
    }
}
