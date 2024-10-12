using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class UIMenuManager : MonoBehaviour
{
    public event UnityAction<bool> OnAllMenusClosed;

    private int _activeMenusCount = 0;

    public void MenuOpened()
    {
        _activeMenusCount++;
        CheckMenuState();
    }

    public void MenuClosed()
    {
        _activeMenusCount--;
        CheckMenuState();
    }

    private void CheckMenuState()
    {
        OnAllMenusClosed?.Invoke(_activeMenusCount == 0);
    }
}
