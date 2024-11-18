using UnityEngine;
using UnityEngine.UI;

public sealed class UISettingsMenuManager : MonoBehaviour
{
    [SerializeField] private Button _musicControllerButton;
    [SerializeField] private Slider _musicControllerSlider;
    [SerializeField] private Image _musicControllerImage;
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;

    private float _musicVolume;

    private void Awake()
    {
        _musicVolume = _musicControllerSlider.value;
    }

    private void OnEnable()
    {
        _musicControllerButton.onClick.AddListener(ButtonClick);
        _musicControllerSlider.onValueChanged.AddListener(SliderValueChanged);
        _saveButton.onClick.AddListener(SaveGame);
        _loadButton.onClick.AddListener(LoadGame);
    }

    private void OnDisable()
    {
        _musicControllerButton.onClick.RemoveListener(ButtonClick);
        _musicControllerSlider.onValueChanged.RemoveListener(SliderValueChanged);
        _saveButton.onClick.RemoveListener(SaveGame);
        _loadButton.onClick.RemoveListener(LoadGame);
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

    private void SaveGame()
    {
        _entryPoint.SavePlayerPosition();
    }

    private void LoadGame()
    {
        _entryPoint.LoadPlayerPosition();
    }
}
