using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Wolf : BaseEnemy
{
    private void Start()
    {
        CharacterName = "Wolf";
        _ActionBarRechargeAmount = 10;

        level = 10;
        totalXP = 25;

        maxHP = 300;
        maxMP = 50;
        currentHP = 100;
        currentMP = 50;

        attackPower = 10;
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
        int dieRoll = Random.Range(0, 2);
        switch (dieRoll)
        {
            case 0:
                print("Wolf used Sonic Howl!");
                SonicHowl();
                break;

            case 1:
                int x = Random.Range(0, _BM._ActivePartyMembers.Count);
                BaseStats targetCharacter = _BM._ActivePartyMembers[x];

                print(CharacterName + " Attacked " + targetCharacter.CharacterName);
                Attack(targetCharacter);
                break;
        }
    }
    private void SonicHowl()
    {
        int damage = attackPower * 5;
        foreach(BasePartyMember a in _BM._ActivePartyMembers)
        {
            a.TakeDamage(damage, false, false);
        }
    }
}
