using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private PlayerLocalSaver _playerLocalSaver;

    //public PlayerLocalSaver PlayerLocalSaver => _playerLocalSaver;

    private void Awake()
    {
        _playerLocalSaver = new PlayerLocalSaver(_player);
    }

    public void SavePlayerPosition()
    {
        _playerLocalSaver.SavePlayerPosition();
    }

    public void LoadPlayerPosition()
    {
        _playerLocalSaver.LoadPlayerPosition();
    }
}
