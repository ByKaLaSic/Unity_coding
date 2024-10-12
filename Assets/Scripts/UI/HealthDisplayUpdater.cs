using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public sealed class HealthDisplayUpdater : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _controller;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        _controller.PlayerMove += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        _controller.PlayerMove -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(GameObject mainCamera)
    {
        _healthText.transform.rotation = Quaternion.LookRotation(_healthText.transform.position - mainCamera.transform.position);
    }
}
