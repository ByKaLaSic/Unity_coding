using StarterAssets;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public sealed class HealthDisplayUpdater : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _controller;
    [SerializeField] private ZombieSpawner _zombieSpawner;

    private List<TextMeshProUGUI> _healthTextsBars;

    private void Awake()
    {
        _healthTextsBars = new List<TextMeshProUGUI>(_zombieSpawner.ZombieToWin);
    }

    private void OnEnable()
    {
        _controller.PlayerMove += UpdateHealthDisplay;
        _zombieSpawner.ZombieCreated += AddHealthBar;
        ZombieHealthController.ZombieDeadWithText += RemoveHealthBar;
    }

    private void OnDisable()
    {
        _controller.PlayerMove -= UpdateHealthDisplay;
        _zombieSpawner.ZombieCreated -= AddHealthBar;
        ZombieHealthController.ZombieDeadWithText -= RemoveHealthBar;
    }

    private void AddHealthBar(TextMeshProUGUI healthText)
    {
        _healthTextsBars.Add(healthText);
    }

    private void RemoveHealthBar(TextMeshProUGUI healthText)
    {
        if (_healthTextsBars.Contains(healthText))
        {
            _healthTextsBars.Remove(healthText);
        }
    }

    private void UpdateHealthDisplay(GameObject mainCamera)
    {
        for (int i = 0; i < _healthTextsBars.Count; i++)
        {
            _healthTextsBars[i].transform.rotation = Quaternion.LookRotation(_healthTextsBars[i].transform.position - mainCamera.transform.position);
        }
    }
}
