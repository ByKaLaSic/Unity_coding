using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp = 100;
    [SerializeField] private bool _isDeath = false;

    [SerializeField] private Teams MyTeam;
    [SerializeField] private Material EnemyColor;
    [SerializeField] private Material AllyColor;

    private void Start()
    {
        KillPlayer();
        JoinTeam();
    }

    void KillPlayer()
    {
        int BaseDamage = 2;

        while (_hp > 0)
        {
            _hp -= BaseDamage;
            Debug.Log(_hp);
        }

        _hp = 0;
        _isDeath = true;
        Debug.LogWarning("Игрок мёртв");
    }

    void JoinTeam() 
    {
        MeshRenderer PlayerMesh = GetComponent<MeshRenderer>();
        bool isEnemy;

        if (MyTeam == Teams.Enemy)
        {
            PlayerMesh.material = EnemyColor;
            isEnemy = true;
        }
        else
        {
            PlayerMesh.material = AllyColor;
            isEnemy = false;
        }

        Debug.LogError($"Игрок во вражеской команде: {isEnemy}");
    }
}

enum Teams
{
    Enemy = 0,
    Ally = 1
}