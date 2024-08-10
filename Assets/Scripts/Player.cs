using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int _baseDamage = 12;
    [SerializeField] private float _multiplier = 1.6f;
    
    private bool _levelUp = false;
    private int _level = 1;
    private int _experience = 0;
    private int _experienceToNextLevel = 100;

    private void Start()
    {
        ShowInitialData();
    }

    private void Update()
    {
        CheckNextLevel();
    }

    private void OnValidate()
    {
        float damageFloat = _baseDamage * _multiplier;
        Debug.LogWarning($"������� (float) ����: {damageFloat}");
    }

    private void ShowInitialData()
    {
        int damageInt = _baseDamage;
        float damageFloat = _baseDamage * _multiplier;

        Debug.LogWarning($"��������! �������� {damageInt} (int) �����");
        Debug.LogWarning($"��������! �������� {damageFloat} (float) �����");
        Debug.LogWarning($"���������� �� �� �� ��������� �������? {_levelUp}");
    }

    private void CheckNextLevel()
    {
        _experience += 10;

        Debug.Log($"������� ���� {_experience}");

        if (_experience >= _experienceToNextLevel)
        {
            _levelUp = true;
            _level++;
            _experience = 0;

            Debug.LogError($"�� ������� �� ������� {_level}! �����������!");
        }
    }
}
