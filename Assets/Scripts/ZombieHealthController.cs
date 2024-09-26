using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthController : MonoBehaviour
{
    [SerializeField] private GameObject _deathSoundPrefab;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private int _health;

    private void Start()
    {
        _hurtSource.clip = _hurtClip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out BulletBase bullet) == true)
        {
            bullet = collision.gameObject.GetComponent<BulletBase>();

            if (TryGetDamage(bullet.Damage))
            {
                _hurtSource.Play();
            }
            else
            {
                Die();
            }
        }
    }

    private bool TryGetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            return false;
        }

        return true;
    }

    void Die()
    {
        Destroy(gameObject);
        GameObject zombieDeathSound = Instantiate(_deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(zombieDeathSound, _deathClip.length);
    }
}
