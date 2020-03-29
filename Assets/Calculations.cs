using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calculations : BaseStats
{
    float baseHP;
    float baseMP;

    float baseStr;
    float baseInt;
    float basePty;

    float baseVit;
    float baseSpr;

    float baseAcc;
    float baseEva;
    float baseAgi;
    float baseLck;

    float growthRateHyper = 0.5f;
    float growthRateStrong = 0.3f;
    float growthRateAverage = 0.2f;
    float growthRateWeak = 0.1f;
    
    public TextMeshProUGUI word;
    private void Update()
    {
        if (level > 100)
            level = 100;

        if (level < 1)
            level = 1;
        
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

                baseHP = 50;
                baseMP = .1f;

                baseStr = 1.4f;
                baseInt = 1f;
                basePty = 1f;

                baseVit = 1.4f;
                baseSpr = 1f;

                baseAcc = 1f;
                baseEva = 1f;
                baseAgi = 1f;
                baseLck = 1f;

                strength = (int)(baseStr * (1 + growthRateStrong) * level);
                intellect = (int)(baseInt * (1 + growthRateWeak) * level);
                piety = (int)(basePty * (1 + growthRateAverage) * level);

                vitality = (int)(baseVit * (1 + growthRateHyper) * level);
                spirit = (int)(baseSpr * (1 + growthRateAverage) * level);

                accuracy = (int)(baseAcc * (1 + growthRateAverage) * level);
                evasion = (int)(baseEva * (1 + growthRateWeak) * level);
                agility = (int)(baseAgi * (1 + growthRateWeak) * level);
                luck = (int)(baseLck * (1 + growthRateAverage) * level);

                maxHP = (int)((baseHP * (1 + growthRateHyper) * level) + (strength/8 + vitality) * level);
                maxMP = (int)(baseMP * (1 + growthRateWeak) * level) + (intellect);
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
        word.text = CharacterName
            + "\n\nLevel = " + level
            + "\nHP = " + maxHP
            + "\nMP = " + maxMP
            + "\n\nStr = " + strength
            + "\nInt = " + intellect
            + "\nPty = " + piety
            + "\nVit = " + vitality
            + "\nSpr = " + spirit
            + "\nAcc = " + accuracy
            + "\nEva = " + evasion
            + "\nAgi = " + agility
            + "\nLck = " + luck;
    }
}
