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
        Debug.LogWarning($"Текущий (float) урон: {damageFloat}");
    }

    private void ShowInitialData()
    {
        int damageInt = _baseDamage;
        float damageFloat = _baseDamage * _multiplier;

        Debug.LogWarning($"Внимание! Нанесено {damageInt} (int) урона");
        Debug.LogWarning($"Внимание! Нанесено {damageFloat} (float) урона");
        Debug.LogWarning($"Переходите ли вы на следующий уровень? {_levelUp}");
    }

    private void CheckNextLevel()
    {
        _experience += 10;

        Debug.Log($"Текущий опыт {_experience}");

        if (_experience >= _experienceToNextLevel)
        {
            _levelUp = true;
            _level++;
            _experience = 0;

            Debug.LogError($"Вы перешли на уровень {_level}! Поздравляем!");
        }
    }
}
