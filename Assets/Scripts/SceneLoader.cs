using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneLoader : MonoBehaviour
{
    [SerializeField] private Player _player;

    private readonly string _mainSceneName = "Main";

    private void OnEnable()
    {
        _player.PlayerDead += ResetScene;
    }

    private void OnDisable()
    {
        _player.PlayerDead -= ResetScene;
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(_mainSceneName);
    }
}
