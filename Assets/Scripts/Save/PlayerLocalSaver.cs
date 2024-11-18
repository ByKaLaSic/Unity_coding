using UnityEngine;

public class PlayerLocalSaver
{
    private const string KEY = "PlayerData";
    private Transform _player;
    private string _playerData = "";

    public PlayerLocalSaver(Transform player)
    {
        _player = player;
        string json = PlayerPrefs.GetString(KEY);
        if (string.IsNullOrEmpty(json)) return;
        _playerData = json;
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        _player.position = data.GetPosition();
    }

    public void SavePlayerPosition()
    {
        PlayerData data = new PlayerData(_player.position);
        string json = JsonUtility.ToJson(data);
        _playerData = json;
        PlayerPrefs.SetString(KEY, json);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPosition()
    {
        PlayerData data = JsonUtility.FromJson<PlayerData>(_playerData);
        _player.position = data.GetPosition();
    }
}
