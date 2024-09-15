using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeaponData), menuName = "Data/Weapon/Data")]
public sealed class WeaponData : ScriptableObject
{
    [SerializeField] private float _force;
    [SerializeField] private float _shootDelay;

    public float Force 
    { 
        get
        {
            return _force;
        } 
    }

    public float ShootDelay
    {
        get
        {
            return _shootDelay;
        }
    }
}
