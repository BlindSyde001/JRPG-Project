using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    /// Mechanics
    /// 
    /// Boss Basic Attacks
    /// Every THIRD Action Alternates betwwen Anti - Personnel Missles and Buster Shot
    /// At 30% HP, Activates Defensive Matrix
    ///
public class ES_XS_Destroyer : BaseEnemy
{
    private bool criticalHP = false;
    private bool alternate = false;
    private int moveCounter = 0;

    void Start()
    {
        CharacterName = "XS - Destroyer";
        _ActionBarRechargeAmount = 10;

        level = 15;
        totalXP = 300;

        maxHP = 2000;
        maxMP = 500;
        currentHP = 2000;
        currentMP = 500;

        attackPower = 30;
    }
    new void Update()
    {
        if (isAlive)
        {
            base.Update();
            if (_ActionBarAmount >= 100)
            {
                EnemyAction();
                moveCounter++;
                _ActionBarAmount = 0;
            }
        }
    }

    private void EnemyAction() // Choose what action enemy takes
    {
        if(currentHP <= (3/10)*(maxHP) && !criticalHP)
        {
            DefensiveMatrix();
            criticalHP = true;
        }
        else if(moveCounter >= 2)
        {
            // Buster Shot or Anti Personnel Missles
            if(alternate)
            {
                AntiPersonnelMissles();
                alternate = !alternate;
            }
            else if(!alternate)
            {
                BusterShot();
                alternate = !alternate;
            }
            moveCounter = 0;
        }
        else
        {
            BasicAttack();
        }
    }

    private void BasicAttack()
    {
        int x = Random.Range(0, _BM._ActivePartyMembers.Count);
        BaseStats targetCharacter = _BM._ActivePartyMembers[x];
        print(CharacterName + " Attacked " + targetCharacter.CharacterName);
        Attack(targetCharacter);
    }
    private void AntiPersonnelMissles() // AOE
    {
        Debug.Log("anti personnel missiles!");
        foreach(BaseStats hero in _BM._ActivePartyMembers.Reverse<BasePartyMember>())
        {
            hero.TakeDamage(hero.maxHP/2, true, false, ActionElement.None, true);
        }
    }
    private void BusterShot() // Tank Buster
    {
        Debug.Log("Buster Shot!");
        attackPower += 100;
        BasicAttack();
        attackPower -= 100;
        // Target tank and deal heavier damage to them
    }
    private void DefensiveMatrix() // Reduced Damage, CounterAttacks
    {
        Debug.Log("SHIELD UP");
        defense += defense;
    }

}
