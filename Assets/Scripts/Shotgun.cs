using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private int _countInShell;

    private Shell[] _shells;

    public override void Fire()
    {
        if (_shells != null)
        {
            AudioSource.PlayOneShot(ShotClip);

            for (int i = 0; i < _countInShell; i++)
            {
                _shells[i].Run(_barrel.forward * Force);
            }

            _shells = null;
        }
    }

    public override void Recharge()
    {
        if (_shells != null)
        {
            return;
        }

        _shells = new Shell[_countInShell];

        for (int i = 0; i < _countInShell; i++)
        {
            Shell shell = Instantiate(_shellPrefab, _barrel);
            shell.Sleep();
            _shells[i] = shell;
        }
    }
}
