using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class UIManager : MonoBehaviour
{
    [SerializeField] private Button _musicControllerButton;
    [SerializeField] private Slider _musicControllerSlider;
    [SerializeField] private Image _musicControllerImage;
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private Sprite _musicOnSprite;

    private float _musicVolume;

    private void Awake()
    {
        _musicVolume = _musicControllerSlider.value;
    }

    private void OnEnable()
    {
        _musicControllerButton.onClick.AddListener(ButtonClick);
        _musicControllerSlider.onValueChanged.AddListener(SliderValueChanged);
    }

    private void OnDisable()
    {
        _musicControllerButton.onClick.RemoveListener(ButtonClick);
        _musicControllerSlider.onValueChanged.RemoveListener(SliderValueChanged);
    }

    private void ButtonClick()
    {
        if (Mathf.Abs(_backgroundMusic.volume) > 0.01f)
        {
            _musicVolume = _backgroundMusic.volume;
            _backgroundMusic.volume = 0f;
            _musicControllerSlider.value = 0f;
            _musicControllerImage.sprite = _musicOffSprite;
        }
        else
        {
            _backgroundMusic.volume = _musicVolume;
            _musicControllerSlider.value = _musicVolume;
            _musicControllerImage.sprite = _musicOnSprite;
        }
    }

    private void SliderValueChanged(float value)
    {
        _backgroundMusic.volume = value;

        bool isMuted = Mathf.Abs(_backgroundMusic.volume) < 0.01f;

        if (isMuted && _musicControllerImage.sprite != _musicOffSprite)
        {
            _musicControllerImage.sprite = _musicOffSprite;
        }
        else if (!isMuted && _musicControllerImage.sprite != _musicOnSprite)
        {
            _musicControllerImage.sprite = _musicOnSprite;
        }
    }
}
