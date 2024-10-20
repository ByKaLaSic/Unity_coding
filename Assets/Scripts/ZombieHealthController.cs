using UnityEngine;
using UnityEngine.Events;
using TMPro;

public sealed class ZombieHealthController : MonoBehaviour
{
    public static event UnityAction ZombieDeadSimple;
    public static event UnityAction<TextMeshProUGUI> ZombieDeadWithText; 

    [SerializeField] private GameObject _deathSoundPrefab;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _text;

    private HealthDisplayer _zombieHealthDisplayer;
    private bool _isDead;

    private void Start()
    {
        _hurtSource.clip = _hurtClip;
        _zombieHealthDisplayer = new HealthDisplayer(_text, _health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out BulletBase bullet) == true)
        {
            if (TryGetDamage(bullet.Damage))
            {
                _zombieHealthDisplayer.ChangeHealth(bullet.Damage);
                _hurtSource.Play();
            }
            else
            {
                if (_isDead == false)
                {
                    Die();
                }
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
        _isDead = true;
        ZombieDeadWithText?.Invoke(GetComponentInChildren<TextMeshProUGUI>());
        ZombieDeadSimple?.Invoke();
        Destroy(gameObject);
        GameObject zombieDeathSound = Instantiate(_deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(zombieDeathSound, _deathClip.length);
    }
}
