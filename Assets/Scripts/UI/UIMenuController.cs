using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private UIMenuManager _uIMenuManager;

    private UIMenu _uIMenu;

    private void Start()
    {
        _uIMenu = new UIMenu(_settingsMenu, _uIMenuManager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uIMenu.ShowOrCloseMenu();
        }
    }
}
