using TMPro;

public sealed class HealthDisplayer
{
    private TextMeshProUGUI _text;
    private int _health;

    public HealthDisplayer(TextMeshProUGUI text, int health)
    {
        _text = text;
        _health = health;
    }

    public void ChangeHealth(int damage)
    {
        _health -= damage;
        _text.text = $"{_health}";
    }
}
