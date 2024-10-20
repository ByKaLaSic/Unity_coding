using StarterAssets;
using UnityEngine;

public sealed class GameStateManager : MonoBehaviour
{
    [SerializeField] private UIMenuManager _uIMenuManager;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private ThirdPersonController _thirdPersonController;

    private void OnEnable()
    {
        _uIMenuManager.OnAllMenusClosed += ToggleStateMenu;
    }

    private void OnDisable()
    {
        _uIMenuManager.OnAllMenusClosed -= ToggleStateMenu;
    }

    private void ToggleStateMenu(bool allMenuClosed)
    {
        Cursor.visible = !allMenuClosed;
        _weaponController.enabled = allMenuClosed;
        _thirdPersonController.enabled = allMenuClosed;

        if (allMenuClosed == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } 
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
