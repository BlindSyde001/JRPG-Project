using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Bandit : BaseEnemy
{
    private int potionCount = 3;

    private void Start()
    {
        CharacterName = "Bandit";
        _ActionBarRechargeAmount = 10;

        level = 5;
        totalXP = 30;

        maxHP = 150;
        maxMP = 50;
        currentHP = 150;
        currentMP = 50;

        attackPower = 5;
        luck = 25;
    }
    new void Update()
    {
        base.Update();
        if (_ActionBarAmount >= 100)
        {
            EnemyAction();
            _ActionBarAmount = 0;
        }
    }

    private void EnemyAction()        // Choose which action enemy takes
    {
        if(currentHP/maxHP <= 1/4)
        {
            Potion();
        }
        else
        {
            x = Random.Range(0, _BM._ActivePartyMembers.Count);
            targetCharacter = _BM._ActivePartyMembers[x];
            Stab();
        }
    }

    private void Stab()
    {
        int damage = attackPower * 20;
        targetCharacter.TakeDamage(damage, false, false);
    }
    private void Potion()
    {
        if (potionCount > 0)
        {
            for (int i = 0; i < _BM._ActiveEnemies.Count; i++)                        // cycle through enemies
            {
                if(_BM._ActiveEnemies[0])                                             // In case of already target being a hero
                {
                    targetCharacter = _BM._ActiveEnemies[i];
                }
                else if (_BM._ActiveEnemies[i].currentHP / _BM._ActiveEnemies[i].maxHP < targetCharacter.currentHP / targetCharacter.maxHP) // Target is the lowest HP Enemy
                     {
                        targetCharacter = _BM._ActiveEnemies[i];
                     }
            }
            targetCharacter.HealDamage(50, false);                                          // Heal Enemy
            potionCount--;
        }
        else
        {
            Stab();
        }
    }
}
