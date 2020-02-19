﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStats : MonoBehaviour
{
    //VARIABLES
    public string CharacterName;
    public List<Spells> spells;
    public bool inBattle;
    public BattleManager _BM;

    #region ATB Variables
    public float _ActionBarAmount;                 // Current Charge amount of ATB Gauge
    public float _ActionBarRechargeAmount;         // Current recharge amount specific to player
    #endregion
    #region Stats Sheet
    [Header("Level")]
    [Header("MAIN STATS")]
    public int level;                   //Level used to increase stats. range from 1 - 100
    public int currentXP;               //Amount of XP accumulated
    public int nextLevelXP;             //Amount needed to Level Up

    [Header("Attack")]
    public int strength;                //Increases Physical Damage
    public int intellect;               //Increases Magical Damage
    public int piety;                   //Increases Healing Potency

    [Header("Defence")]
    public int maxHP;                   //Determines amount of Damage able to take before being KO'D
    public int maxMP;                   //Determines amount of Spells able to be cast
    public int currentHP;               // Modifiable value for non stats interaction
    public int currentMP;               // Modifiable value for non stats interaction
    public int vitality;                //Decreases Physical Damage
    public int spirit;                  //Decreases Magical Damage

    [Header("Other")]
    public int accuracy;                //Increases chance of landing a successful Physical Attack
    public int evasion;                 //Decreases chance of being hit by a Physical Attack
    public int agility;                 //Increases speed of Action Bar being charged as well as initial Action Bar charge amount
    public int luck;                    //Increases chance of Physical Attacks & Damage Abilities Critically Striking
    
    [Header("Elemental")]
    [Header("ELEMENTAL STATS")]
    public bool fire;                   //Changes Damage Type to Fire
    public bool ice;                    //Changes Damage Type to Ice
    public bool lightning;              //Changes Damage Type to Lightning
    public bool water;                  //Changes Damage Type to Water
    public bool earth;                  //Changes Damage Type to Earth
    public bool wind;                   //Changes Damage Type to Wind
    public bool shadow;                 //Changes Damage Type of Shadow
    public bool holy;                   //Changes Damage Type to Light
    
    [Header("Elemental Resistance")]
    public int fireResist;              //Decreases All Fire Damage
    public int iceResist;               //Decreases All Ice Damage
    public int lightningResist;         //Decreases All Lightning Damage
    public int waterResist;             //Decreases All Water Damage
    public int earthResist;             //Decreases All Earth Damage
    public int windResist;              //Decreases All Wind Damage
    public int shadowResist;            //Decreases All Shadow Damage
    public int lightResist;             //Decreases All Light Damage

    [Header("Main Status")]
    [Header("STATUS EFFECTS")]
    public bool petrify;                //Unable to perform actions, be damaged or healed
    public bool taint;                  //Healing effects to player now deals damage
    public bool slow;                   //Action bar takes longer to charge
    public bool stop;                   //unable to perform actions. Action bar does not charge
    public bool frenzy;                 //Will automatically attack enemies. Unable to take direct action
    public bool confusion;              //Will randomly attack anything
    public bool silence;                //Unable to cast spells
    public bool condemn;                //Will be KO'D after a period of time elapsed
    public bool poison;                 //takes damage over time
    public bool paralyzed;              //Will sometimes be unable to perform an action
    public bool deathstrike;            //Instantly KO's damaged opponent
    
    [Header("Status Resistance")]
    public int petrificationResist;     //Reduces chance of being Petrified
    public int taintResist;             //Reduces chance of being Tainted
    public int slowResist;              //Reduces chance of being Slowed
    public int stopResist;              //Reduces chance of being Stopped
    public int frenzyResist;            //Reduces chance of being Frenzied
    public int confusionResist;         //Reduces chance of being Confused
    public int silenceResist;           //Reduces chance of being Silenced
    public int condemnResist;           //Reduces chance of being Condemned
    public int poisonResist;            //Reduces chance of being Poisoned
    public int paralysisResist;         //Reduces chance of being Paralyzed
    public int deathstrikeResist;       //Reduces chance of being Deathstruck
    
    [Header("BUFFS")]
    public bool zeal;                   //Increases Physical Damage Dealt
    public bool mastery;                //Increases Magical Damage and Healing Dealt
    public bool haste;                  //Increases speed of Action Bar charge
    public int barrier;                 //Gains a Physical Damage Shield
    public int mBarrier;                //Gains a Magical Damage Shield
    public bool shield;                 //Decreases Physical Damage Taken
    public bool dampen;                 //Decreases Magical Damage Taken
    #endregion

    //UPDATES
    public virtual void Awake()
    {
        _BM = FindObjectOfType<BattleManager>();
    }
    public virtual void Update()
    {
        if (inBattle)
        {
            _ActionBarAmount += _ActionBarRechargeAmount * Time.deltaTime;          // Recharge your action bar so you can take a turn
            _ActionBarAmount = Mathf.Clamp(_ActionBarAmount, 0, 100);
        }
    }

    //METHODS
    #region Actions
    public bool Attack(BaseStats targetCharacter)
    {
        // Did it hit? Calculate dodge
        int chance;
        chance = accuracy - targetCharacter.evasion;
        bool successful = (Random.Range(0, 100) < chance);

        if(successful)
        {
            bool crit;
            if (Random.Range(0, 100) < luck)
                crit = true;
            else
                crit = false;

            int damage = (int)(strength * (zeal ? 1.2 : 1) * (crit ? 1.75 : 1)); // Strength, buff, crit.
            targetCharacter.TakeDamage(damage, false);
            print("Attack connected!");
        } else
        { print("Attack Failed!"); }
        return successful;
    }
    public bool CastMagic(Spells spell, BaseStats targetCharacter)
    {
        bool successful = currentMP >= spell._SpellManaCost;   // Returns if you have enough mana to cast the spell

        if (successful)
        {
            currentMP -= spell._SpellManaCost;               // Because successful, take away mana cost
            spell.Cast(targetCharacter);
        }
        return successful;
    }
    public void Defend()                      // Increase in defence for a turn
    {
        vitality += (int)(vitality * .4f);
        Debug.Log("Increased Defence");
    }
    public void UseItem(Items item, BaseStats targetCharacter)
    {

    }
    public virtual void Die()                 // Go into die state
    {
        //Destroy(this.gameObject);
    }
    #endregion
    #region Receiver Methods
    public void TakeDamage(int amount, bool magical)        //Damage taken by character
    {
        int damageAmount = (int)(amount - (5 * (magical ? spirit : vitality)) );
        if(damageAmount < 0)
        {
            damageAmount = 0;
        }
        print(damageAmount + " Taken!");
        currentHP = Mathf.Max(currentHP - damageAmount, 0);              // No going below 0 hp
        print("Reduced to " + currentHP);
        if(currentHP == 0)
        {
            Die();
        }
    }
    public void HealDamage(int amount)                      // Healing taken by character
    {
        int healAmount = amount;
        print(amount + " Healed!");
        currentHP = Mathf.Min(currentHP + healAmount, maxHP);   // No overhealing
        print("Added to " + currentHP);
    }
    public void InitiateATB()                               // For characters in battle, start charging their ATB gauges
    {
        inBattle = true;
    }
    #endregion
    #region Debuff
    public void Petrify()
    {
        //Stops Actions and becomes untargetable by enemy for set period of time. Nothing affects them except for petrify curative effects
    }
    public void Taint()
    {
        //All healing becomes damage
    }
    public void Slow()
    {
        //Decrease ATB charge rate
    }
    public void Stop()
    {
        //atb charge rate becomes 0
    }
    public void Frenzy()
    {
        //no longer in control of player. Only uses attack command when atb is full, and attacks random enemy
    }
    public void Confusion()
    {
        //no longer in control of player.  
    }
    public void Silence()
    {
        //spells are locked
    }
    public void Condemn()
    {
        //PROC CHANCE
        int strikeChance = 0;
        int Proc = Random.Range(0, 100);
        int Resist = Random.Range(0, 100);

        if(Resist > condemnResist)
        {
            if(Proc <= strikeChance)
            {
                condemn = true;
            }
        }

        //EFFECT
        float duration = 15f;

        if(condemn)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                currentHP = 0;
            }
        }
    }
    public void Poison()
    {
        //PROC CHANCE
        int strikeChance = 0;
        int Proc = Random.Range(0, 100);
        int Resist = Random.Range(0, 100);

        if(Resist > poisonResist)
        {
            if(Proc <= strikeChance)
            {
                poison = true;
            }
        }
        //EFFECT
        if(poison)
        {
            float timerTick = 0f;
            int instanceApplication = 7;

            timerTick += Time.deltaTime;

            if (timerTick >= instanceApplication)

            {
                currentHP -= (maxHP * 5 / 100);
                timerTick = 0;
            }
        }
    }
    public void Paralyzed()
    {
        
    }
    public void DeathStrike()
    {
        //PROC CHANCE
        int strikeChance = 0;
        int Proc = Random.Range(0, 100);
        int Resist = Random.Range(0, 100);

        if (Resist > deathstrikeResist)
        {
            if(Proc <= strikeChance)
            {
                deathstrike = true;
            }
        }

        //EFFECT
        if(deathstrike)
        {
            currentHP = 0;
            deathstrike = false;
        }
    }
    #endregion
    #region Normalized Values
    // Used for the UI component to display amount in graphic representation
    public float ActionBarNormalized()
    {
        return _ActionBarAmount / 100;
    }
    #endregion
}
