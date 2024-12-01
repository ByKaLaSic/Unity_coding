using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public sealed class ZombieHealthController : MonoBehaviour
{
    public static event UnityAction ZombieDeadSimple;
    public static event UnityAction<TextMeshProUGUI> ZombieDeadWithText;

    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _deathSoundPrefab;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _dyingTime;


    private int _currentHealth;
    private EntryPoint _entryPoint;
    private ObjectPool<ZombieHealthController> _zombiePool;
    private bool _isDead;

    private void Start()
    {
        _hurtSource.clip = _hurtClip;
        _currentHealth = _health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out BulletBase bullet) == true)
        {
            if (TryGetDamage(bullet.Damage))
            {
                _hurtSource.Play();
                _text.text = $"{_currentHealth}";
            }
            else
            {
                if (_isDead == false)
                {
                    StartCoroutine(Die());
                }
            }
        }
    }

    public bool TryGetDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            return false;
        }

        return true;
    }

    public void Initialize(EntryPoint entryPoint)
    {
        _entryPoint = entryPoint;
        _zombiePool = _entryPoint.GetPool<ZombieHealthController>("ZombiePool");
        Alive();
    }

    public IEnumerator Die()
    {
        _isDead = true;

        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        _animator.enabled = false;

        ZombieDeadWithText?.Invoke(_text);
        ZombieDeadSimple?.Invoke();
        GameObject zombieDeathSound = Instantiate(_deathSoundPrefab, transform.position, Quaternion.identity);
        Destroy(zombieDeathSound, _deathClip.length);

        yield return new WaitForSeconds(_dyingTime);

        gameObject.SetActive(false);
        _zombiePool.ReturnObjectToPool(this);
        RestoreHealth();
    }

    private void Alive()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }

        _animator.enabled = true;
    }

    private void RestoreHealth()
    {
        _currentHealth = _health;
        _isDead = false;
        _text.text = $"{_currentHealth}";
        Alive();
    }
}
