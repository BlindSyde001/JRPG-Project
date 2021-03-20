using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Mechanics
/// 
/// Basic Attacks
/// 
public class ES_Rebel : BaseEnemy
{
    private void Start()
    {
        CharacterName = "Rebel";
        _ActionBarRechargeAmount = 10;

        level = 5;
        currentXP = 30;

        maxHP = 200;
        maxMP = 0;
        currentHP = 200;
        currentMP = 0;

        attackPower = 5;
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
