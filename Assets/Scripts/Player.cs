using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Analytics;

public sealed class Player : MonoBehaviour
{
    public event Action PlayerDead;

    [SerializeField] private float _health = 100;
    [SerializeField] private Slider _healthBar;

    private PlayerHealthShower _playerHealthBar;
    private float _currentHealth;

    private void Awake()
    {
        _playerHealthBar = new PlayerHealthShower(_healthBar, _health);
        _currentHealth = _health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Zombie zombie))
        {
            _currentHealth -= zombie.Damage;
            _playerHealthBar.ChangeHealth(_currentHealth);

            if (_currentHealth <= 0)
            {
                PlayerDead?.Invoke();
                AnalyticsManager.OnPlayerDead();
            }
        }
    }
}
