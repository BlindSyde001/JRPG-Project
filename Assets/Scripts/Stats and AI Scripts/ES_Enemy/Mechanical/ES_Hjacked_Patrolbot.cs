using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Mechanics
/// 
/// 
public class ES_Hjacked_Patrolbot : BaseEnemy
{
    private void Start()
    {
        CharacterName = "Hijacked Patrolbot";
        _ActionBarRechargeAmount = 10;

        level = 7;
        totalXP = 40;

        maxHP = 280;
        maxMP = 0;
        currentHP = 280;
        currentMP = 0;

        attackPower = 9;
    }
    new void Update()
    {
        if (isAlive)
        {
            base.Update();
            if (_ActionBarAmount >= 100)
            {
                EnemyAction();
                _ActionBarAmount = 0;
            }
        }
    }

    private void EnemyAction()
    {
        x = Random.Range(0, _BM._ActivePartyMembers.Count);
        targetCharacter = _BM._ActivePartyMembers[x];
        Attack(targetCharacter);
    }
}
