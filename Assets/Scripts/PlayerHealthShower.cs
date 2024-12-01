using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerHealthShower
{
    private Slider _healthBar;

    public PlayerHealthShower(Slider healthBar, float health)
    {
        _healthBar = healthBar;
        _healthBar.maxValue = health;
        _healthBar.value = health;
    }

    public void ChangeHealth(float health)
    {
        _healthBar.value = health;
    }
}
