using UnityEngine;

public sealed class UIMenu
{
    private GameObject _menu;
    private UIMenuManager _uIMenuManager;
    private bool _isMenuActive = false;

    public UIMenu(GameObject menu, UIMenuManager uIMenuManager)
    {
        _menu = menu;
        _uIMenuManager = uIMenuManager;
    }

    public void ShowOrCloseMenu()
    {
        _isMenuActive = !_isMenuActive;
        _menu.SetActive(_isMenuActive);

        if (_isMenuActive)
        {
            _uIMenuManager.MenuOpened();
        }
        else
        {
            _uIMenuManager.MenuClosed();
        }
    }
}
