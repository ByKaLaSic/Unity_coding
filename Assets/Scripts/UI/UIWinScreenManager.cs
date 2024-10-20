using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public sealed class UIWinScreenManager : MonoBehaviour
{
    public event UnityAction ClosedWinScreen;

    [Header("BackgroundImage")]
    [SerializeField] private Image _backgroundImage;
    [Header("WinTitle")]
    [SerializeField] private CanvasGroup _victoryTitle;
    [Header("Awards")]
    [SerializeField] private RectTransform _cupsAward;
    [SerializeField] private RectTransform _coinsAward;
    [Header("Buttons")]
    [SerializeField] private CanvasGroup _collectButtonGroup;
    [SerializeField] private CanvasGroup _adButtonGroup;
    [Header("Sounds")]
    [SerializeField] private AudioSource _winSource;
    [SerializeField] private AudioClip _winScreenSound;
    [SerializeField] private AudioClip _winTitleSound;
    [SerializeField] private AudioClip _winHardStamp;
    [SerializeField] private AudioClip _winButtonsSound;
    [Space]
    [SerializeField] private DeadZombieChecker _deadZombieChecker;
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private float _fadeAlpha = 0.7f;

    private readonly Vector3 _awardStartScale = new Vector3(40, 40, 40);
    private readonly Color _transparentColor = new Color(0, 0, 0, 0);
    private int _awardCount = 0;
    private float _callDelay = 0.5f;
    private Button _collectButton;
    private Button _adButton;
    private TextMeshProUGUI _cupsAwardText;
    private TextMeshProUGUI _coinsAwardText;
    private Sequence _sequence;

    private void Awake()
    {
        _collectButton = _collectButtonGroup.GetComponent<Button>();
        _adButton = _adButtonGroup.GetComponent<Button>();
        _cupsAwardText = _cupsAward.GetComponentInChildren<TextMeshProUGUI>();
        _coinsAwardText = _coinsAward.GetComponentInChildren<TextMeshProUGUI>();

        _deadZombieChecker.AllZombieDead += ShowWinAnimation;
        _deadZombieChecker.ZombieDead += AddAward;
        _collectButton.onClick.AddListener(CloseWinScreen);
        _adButton.onClick.AddListener(ShowAd);
    }

    private void OnDestroy()
    {
        _deadZombieChecker.AllZombieDead -= ShowWinAnimation;
        _deadZombieChecker.ZombieDead -= AddAward;
        _collectButton.onClick.RemoveListener(CloseWinScreen);
        _adButton.onClick.RemoveListener(ShowAd);
        _sequence?.Kill();
    }

    private void ShowWinAnimation()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _backgroundImage.color = _transparentColor;
        _victoryTitle.alpha = 0f;
        _cupsAward.localScale = _awardStartScale;
        _coinsAward.localScale = _awardStartScale;
        _collectButtonGroup.alpha = 0f;
        _adButtonGroup.alpha = 0f;
        _collectButtonGroup.transform.localScale = Vector3.zero;
        _adButtonGroup.transform.localScale = Vector3.zero;
        _cupsAwardText.text = $"{_awardCount}";
        _coinsAwardText.text = $"{_awardCount}";

        _sequence.Append(_backgroundImage.DOFade(_fadeAlpha, _fadeDuration)).OnStart(() => _winSource.PlayOneShot(_winScreenSound));
        _sequence.AppendCallback(() => _winSource.PlayOneShot(_winTitleSound));
        _sequence.Append(_victoryTitle.DOFade(1f, _fadeDuration));
        _sequence.AppendCallback(() => DOVirtual.DelayedCall(_callDelay, () => _winSource.PlayOneShot(_winHardStamp)));
        _sequence.Append(_cupsAward.DOScale(Vector3.one, _fadeDuration / 2));
        _sequence.AppendCallback(() => DOVirtual.DelayedCall(_callDelay, () => _winSource.PlayOneShot(_winHardStamp)));
        _sequence.Append(_coinsAward.DOScale(Vector3.one, _fadeDuration / 2));
        _sequence.AppendCallback(() => _winSource.PlayOneShot(_winButtonsSound));
        _sequence.Append(_collectButtonGroup.DOFade(1f, _fadeDuration));
        _sequence.Join(_adButtonGroup.DOFade(1f, _fadeDuration));
        _sequence.Join(_collectButtonGroup.transform.DOScale(Vector3.one, _fadeDuration));
        _sequence.Join(_adButtonGroup.transform.DOScale(Vector3.one, _fadeDuration)).OnComplete(EnableButtons);
    }

    private void ShowAd()
    {
        Debug.Log("»дЄт реклама...");
    }

    private void EnableButtons()
    {
        _collectButtonGroup.interactable = true;
        _adButtonGroup.interactable = true;
    }

    private void CloseWinScreen()
    {
        ClosedWinScreen?.Invoke();
        _collectButtonGroup.interactable = false;
        _adButtonGroup.interactable = false;
        ResetAward();
    }

    private void AddAward()
    {
        _awardCount++;
    }

    private void ResetAward()
    {
        _awardCount = 0;
    }
}
