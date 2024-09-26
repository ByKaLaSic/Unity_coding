using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTriggerScream : MonoBehaviour
{
    [SerializeField] private Renderer _zombieRenderer;
    [SerializeField] private AudioSource _screamSource;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioClip _screamClip;

    private const string _playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _zombieRenderer.material.color = Color.yellow;

            if (_hurtSource.isPlaying == false)
            {
                if (_screamSource.isPlaying == false)
                {
                    _screamSource.PlayOneShot(_screamClip);
                }
            }
            else
            {
                _screamSource.Stop();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _zombieRenderer.material.color = Color.black;
            _screamSource.Stop();
        }
    }
}
