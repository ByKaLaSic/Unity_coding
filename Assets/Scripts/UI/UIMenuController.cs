using UnityEngine;

public sealed class UIMenuController : MonoBehaviour
{
    [SerializeField] private UIWinScreenManager _uIWinScreenManager;
    [SerializeField] private DeadZombieChecker _deadZombieChecker;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _WinMenu;
    [SerializeField] private UIMenuManager _uIMenuManager;

    private UIMenu _uIMainMenu;
    private UIMenu _uIWinScreen;

    private void Start()
    {
        _uIMainMenu = new UIMenu(_settingsMenu, _uIMenuManager);
        _uIWinScreen = new UIMenu(_WinMenu, _uIMenuManager);
    }

    private void OnEnable()
    {
        _deadZombieChecker.AllZombieDead += ShowWinScreen;
        _uIWinScreenManager.ClosedWinScreen += CloseWinScreen;
    }

    private void OnDisable()
    {
        _deadZombieChecker.AllZombieDead -= ShowWinScreen;
        _uIWinScreenManager.ClosedWinScreen -= CloseWinScreen;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uIMainMenu.ShowOrCloseMenu();
        }
    }

    private void ShowWinScreen()
    {
        _uIWinScreen.ShowOrCloseMenu();
    }

    private void CloseWinScreen()
    {
        _uIWinScreen.ShowOrCloseMenu();
    }
}
