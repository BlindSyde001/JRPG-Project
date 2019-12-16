using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
       //LEVEL VARIABLES

    int level;                   //Level used to increase stats. range from 1 - 100
    int currentXP;               //Amount of XP accumulated
    int nextLvlXP;               //Amount needed to Level Up

        //ATTACK VARIABLES
    int strength;                //Increases Physical Damage
    int intellect;               //Increases Magical Damage
    int piety;                   //Increases Healing Potency

       //DEFENCE VARIABLES
    int maxHP;                   //Determines amount of Damage able to take before being KO'D
    int maxMP;                   //Determines amount of Spells able to be cast
    int currentHP;               // Modifiable value for non stats interaction
    int currentMP;               // Modifiable value for non stats interaction
    int vitality;                //Decreases Physical Damage
    int spirit;                  //Decreases Magical Damage

       //OTHER VARIABLES
    int accuracy;                //Increases chance of landing a successful Physical Attack
    int evasion;                 //Decreases chance of being hit by a Physical Attack
    int agility;                 //Increases speed of Action Bar being charged
    int luck;                    //Increases chance of Damaging abilities Critically Striking

       //ELEMENTAL VARIABLES
    bool fire;                   //Changes Damage Type to Fire
    bool ice;                    //Changes Damage Type to Ice
    bool lightning;              //Changes Damage Type to Lightning
    bool water;                  //Changes Damage Type to Water
    bool earth;                  //Changes Damage Type to Earth
    bool wind;                   //Changes Damage Type to Wind
    bool shadow;                 //Changes Damage Type of Shadow
    bool holy;                   //Changes Damage Type to Light

       //ELEMENTAL RESISTANCE VARIABLES
    int fireResistance;          //Decreases All Fire Damage
    int iceResistance;           //Decreases All Ice Damage
    int lightningResistance;     //Decreases All Lightning Damage
    int waterResistance;         //Decreases All Water Damage
    int earthResistance;         //Decreases All Earth Damage
    int windResistance;          //Decreases All Wind Damage
    int shadowResistance;        //Decreases All Shadow Damage
    int lightResistance;         //Decreases All Light Damage

       //STATUS VARIABLES
    bool petrify;                //Unable to perform actions, be damaged or healed
    bool taint;                  //Healing effects to player now deals damage
    bool slow;                   //Action bar takes longer to charge
    bool stop;                   //unable to perform actions. Action bar does not charge
    bool frenzy;                 //Will automatically attack enemies. Unable to take direct action
    bool confusion;              //Will randomly attack anything
    bool silence;                //Unable to cast spells
    bool condemn;                //Will be KO'D after a period of time elapsed
    bool poison;                 //takes damage over time
    bool paralyzed;              //Will sometimes be unable to perform an action
    bool deathstrike;            //Instantly KO's damaged opponent

       //STATUS RESISTANCE VARIABLES
    int petrificationResistance; //Reduces chance of being Petrified
    int taintResistance;         //Reduces chance of being Tainted
    int slowResistance;          //Reduces chance of being Slowed
    int stopResistance;          //Reduces chance of being Stopped
    int frenzyResistance;        //Reduces chance of being Frenzied
    int confusionResistance;     //Reduces chance of being Confused
    int silenceResistance;       //Reduces chance of being Silenced
    int condemnResistance;       //Reduces chance of being Condemned
    int poisonResistance;        //Reduces chance of being Poisoned
    int paralysisResistance;     //Reduces chance of being Paralyzed
    int deathstrikeResistance;   //Reduces chance of being Deathstruck

       //BUFF VARIABLES
    bool zeal;                   //Increases Physical Damage Dealt
    bool mastery;                //Increases Magical Damage and Healing Dealt
    bool haste;                  //Increases speed of Action Bar charge
    int barrier;                 //Gains a Physical Damage Shield
    int mBarrier;                //Gains a Magical Damage Shield
    bool shield;                 //Decreases Physical Damage Taken
    bool dampen;                 //Decreases Magical Damage Taken

    //METHODS
    #region Basic Commands
    public void Attack()
    {

    }

    public void Skill()
    {

    }

    public void Item()
    {

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

        if(Resist > condemnResistance)
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

        if(Resist > poisonResistance)
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

        if (Resist > deathstrikeResistance)
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
}
