using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "character Data", menuName = "Characters")]
public class StatsDataAsset : ScriptableObject
{
    public string character;

    public int level;
    public float baseHP;
    public float baseMP;

    public float baseStr;
    public float baseInt;
    public float basePty;

    public float baseVit;
    public float baseSpr;

    public float baseAcc;
    public float baseEva;
    public float baseAgi;
    public float baseLck;

    float growthRateHyper = 0.5f;
    float growthRateStrong = 0.3f;
    float growthRateAverage = 0.2f;
    float growthRateWeak = 0.1f;

    int dataLevel;
    int dataCurrentXP;
    int dataNextLevelXP;
    
    int dataStrength;
    int dataIntellect;
    int dataPiety;
    
    int dataMaxHP;
    int dataMaxMP;
    int dataCurrentHP;
    int dataCurrentMP;
    int dataVitality;
    int dataSpirit;
    
    int dataAccuracy;
    int dataEvasion;
    int dataAgility;
    int dataLuck;

    private void Awake()
    {
        dataLevel = level;

        dataStrength = (int)( (baseStr * (1 + growthRateStrong) * level) );
        dataIntellect = (int)( (baseInt * (1 + growthRateWeak) * level) );
        dataPiety = (int)( (basePty * (1 + growthRateAverage) * level) );
        dataVitality = (int)( (baseVit * (1 + growthRateHyper) * level) );
        dataSpirit = (int)( (baseSpr * (1 + growthRateAverage) * level) );
        dataAccuracy = (int)( (baseAcc * (1 + growthRateAverage) * level) );
        dataEvasion = (int)( (baseEva * (1 + growthRateWeak) * level) );
        dataAgility = (int)( (baseAgi * (1 + growthRateWeak) * level) );
        dataLuck = (int)( (baseLck * (1 + growthRateAverage) * level) );
        dataMaxHP = (int)( (baseHP * (1 + growthRateHyper) * level) + (dataStrength / 8 + dataVitality) * level );
        dataMaxMP = (int)( (baseMP * (1 + growthRateWeak) * level) + (dataIntellect / 8 + dataSpirit) * level );
    }
}
