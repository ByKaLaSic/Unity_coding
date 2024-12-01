using UnityEngine;

public sealed class Zombie : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;

    public float Damage => _damage;
}
