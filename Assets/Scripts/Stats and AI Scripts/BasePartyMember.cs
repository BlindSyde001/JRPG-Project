﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasePartyMember : BaseStats
{
    //VARIABLES
    #region In Battle UI Variables 
    public Sprite characterPortrait;                   // Portrait image displayed in UI
    public int currentLimit;                           // Limit amount of charge for special move
    #endregion
    public bool isAlive = true;                        // Check to see if player has not died in battle
    
    public int baseHP = 50;
    public int baseMP = 20;

    public int baseStr = 5;
    public int baseInt = 5;
    public int basePty = 5;

    public int baseVit = 5;
    public int baseSpr = 5;

    public int baseAcc = 5;
    public int baseEva = 5;
    public int baseAgi = 5;
    public int baseLck = 5;

    float growthRateHyper = 4.5f;                      // Assigned to the Hero's Strongest stat
    float growthRateStrong = 0.3f;                     // Assigned to the Hero's Secondary stat
    float growthRateAverage = 0.2f;                    // Assigned to the Hero's Averaging stat
    float growthRateWeak = 0.1f;                       // Assigned to the Hero's Weakest stat

    //UPDATES
    private void Start()
    {
        #region Stat Growth Per Character
        switch (CharacterName)
        {
            #region Officer
            case "Hero1":
                strength = (int)(baseStr * (1 + growthRateStrong) * level);
                intellect = (int)(baseInt * (1 + growthRateAverage) * level);
                piety = (int)(basePty * (1 + growthRateAverage) * level);

                vitality = (int)(baseVit * (1 + growthRateStrong) * level);
                spirit = (int)(baseSpr * (1 + growthRateStrong) * level);

                accuracy = (int)(baseAcc * (1 + growthRateStrong) * level);
                evasion = (int)(baseEva * (1 + growthRateAverage) * level);
                agility = (int)(baseAgi * (1 + growthRateAverage) * level);
                luck = (int)(baseLck * (1 + growthRateAverage) * level);

                maxHP = (int)(baseHP * (1 + growthRateStrong) * level);
                maxMP = (int)(baseMP * (1 + growthRateAverage) * level);
                break;
            #endregion
            #region Templar
            case "Hero2":
                strength = (int)(baseStr * (1 + growthRateStrong) * level);
                intellect = (int)(baseInt * (1 + growthRateWeak) * level);
                piety = (int)(basePty * (1 + growthRateAverage) * level);

                vitality = (int)(baseVit * (1 + growthRateHyper) * level);
                spirit = (int)(baseSpr * (1 + growthRateAverage) * level);

                accuracy = (int)(baseAcc * (1 + growthRateAverage) * level);
                evasion = (int)(baseEva * (1 + growthRateWeak) * level);
                agility = (int)(baseAgi * (1 + growthRateWeak) * level);
                luck = (int)(baseLck * (1 + growthRateAverage) * level);

                maxHP = (int)(baseHP * (1 + growthRateHyper) * level) + (strength * level);
                maxMP = (int)(baseMP * (1 + growthRateWeak) * level) + (intellect * level);
                break;
            #endregion
            #region Shaman
            case "Hero3":
                strength = (int)(baseStr * (1 + growthRateWeak) * level);
                intellect = (int)(baseInt * (1 + growthRateStrong) * level);
                piety = (int)(basePty * (1 + growthRateAverage) * level);

                vitality = (int)(baseVit * (1 + growthRateWeak) * level);
                spirit = (int)(baseSpr * (1 + growthRateStrong) * level);

                accuracy = (int)(baseAcc * (1 + growthRateWeak) * level);
                evasion = (int)(baseEva * (1 + growthRateStrong) * level);
                agility = (int)(baseAgi * (1 + growthRateAverage) * level);
                luck = (int)(baseLck * (1 + growthRateAverage) * level);

                maxHP = (int)(baseHP * (1 + growthRateWeak) * level) + (strength * level);
                maxMP = (int)(baseMP * (1 + growthRateStrong) * level) + (intellect * level);
                break;
            #endregion
            #region Mechanomancer
            case "Hero4":
                strength = (int)(baseStr * (1 + growthRateHyper) * level);
                intellect = (int)(baseInt * (1 + growthRateAverage) * level);
                piety = (int)(basePty * (1 + growthRateWeak) * level);

                vitality = (int)(baseVit * (1 + growthRateStrong) * level);
                spirit = (int)(baseSpr * (1 + growthRateWeak) * level);

                accuracy = (int)(baseAcc * (1 + growthRateStrong) * level);
                evasion = (int)(baseEva * (1 + growthRateWeak) * level);
                agility = (int)(baseAgi * (1 + growthRateWeak) * level);
                luck = (int)(baseLck * (1 + growthRateStrong) * level);

                maxHP = (int)(baseHP * (1 + growthRateStrong) * level);
                maxMP = (int)(baseMP * (1 + growthRateAverage) * level);
                break;
            #endregion
            #region Thaumaturge
            case "Hero5":
                strength = (int)(baseStr * (1 + growthRateWeak) * level);
                intellect = (int)(baseInt * (1 + growthRateHyper) * level);
                piety = (int)(basePty * (1 + growthRateWeak) * level);

                vitality = (int)(baseVit * (1 + growthRateWeak) * level);
                spirit = (int)(baseSpr * (1 + growthRateAverage) * level);

                accuracy = (int)(baseAcc * (1 + growthRateWeak) * level);
                evasion = (int)(baseEva * (1 + growthRateWeak) * level);
                agility = (int)(baseAgi * (1 + growthRateAverage) * level);
                luck = (int)(baseLck * (1 + growthRateStrong) * level);

                maxHP = (int)(baseHP * (1 + growthRateWeak) * level);
                maxMP = (int)(baseMP * (1 + growthRateHyper) * level);
                break;
            #endregion
            #region Bounty Hunter
            case "Hero6":
                strength = (int)(baseStr * (1 + growthRateStrong) * level);
                intellect = (int)(baseInt * (1 + growthRateWeak) * level);
                piety = (int)(basePty * (1 + growthRateWeak) * level);

                vitality = (int)(baseVit * (1 + growthRateAverage) * level);
                spirit = (int)(baseSpr * (1 + growthRateWeak) * level);

                accuracy = (int)(baseAcc * (1 + growthRateStrong) * level);
                evasion = (int)(baseEva * (1 + growthRateHyper) * level);
                agility = (int)(baseAgi * (1 + growthRateHyper) * level);
                luck = (int)(baseLck * (1 + growthRateAverage) * level);

                maxHP = (int)(baseHP * (1 + growthRateWeak) * level);
                maxMP = (int)(baseMP * (1 + growthRateWeak) * level);
                break;
            #endregion
            #region Spiritualist
            case "Hero7":
                strength = (int)(baseStr * (1 + growthRateAverage) * level);
                intellect = (int)(baseInt * (1 + growthRateWeak) * level);
                piety = (int)(basePty * (1 + growthRateAverage) * level);

                vitality = (int)(baseVit * (1 + growthRateWeak) * level);
                spirit = (int)(baseSpr * (1 + growthRateWeak) * level);

                accuracy = (int)(baseAcc * (1 + growthRateStrong) * level);
                evasion = (int)(baseEva * (1 + growthRateStrong) * level);
                agility = (int)(baseAgi * (1 + growthRateStrong) * level);
                luck = (int)(baseLck * (1 + growthRateWeak) * level);

                maxHP = (int)(baseHP * (1 + growthRateAverage) * level);
                maxMP = (int)(baseMP * (1 + growthRateAverage) * level);
                break;
            #endregion
            #region Machinist
            case "Hero8":
                strength = (int)(baseStr * (1 + growthRateStrong) * level);
                intellect = (int)(baseInt * (1 + growthRateWeak) * level);
                piety = (int)(basePty * (1 + growthRateWeak) * level);

                vitality = (int)(baseVit * (1 + growthRateWeak) * level);
                spirit = (int)(baseSpr * (1 + growthRateWeak) * level);

                accuracy = (int)(baseAcc * (1 + growthRateHyper) * level);
                evasion = (int)(baseEva * (1 + growthRateAverage) * level);
                agility = (int)(baseAgi * (1 + growthRateAverage) * level);
                luck = (int)(baseLck * (1 + growthRateHyper) * level);

                maxHP = (int)(baseHP * (1 + growthRateStrong) * level);
                maxMP = (int)(baseMP * (1 + growthRateWeak) * level);
                break;
                #endregion
        }
        #endregion
    }
    new void Update()
    {
        if(isAlive)
        base.Update();
    }

    //METHODS
    public override void Die()
    {
        isAlive = false;
        _BM._ActivePartyMembers.Remove(this);
        _BM._DownedMembers.Add(this);
        _BM.UpdatePartyAliveStatus();
    }
}
