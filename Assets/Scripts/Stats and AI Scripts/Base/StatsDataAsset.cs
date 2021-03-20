using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum CharacterType { Humanoid, Beast, Machine};
[CreateAssetMenu(fileName = "Character Data", menuName = "Characters")]
public class StatsDataAsset : ScriptableObject
{
    public string ID;
    public CharacterType _characterType;

    public int startingLevel;              // Character's starting level
    public int startingXP;                 // Character's starting XP

    public float baseHP;                   // Health Pool
    public float baseMP;                   // Mana Pool

    public float baseAtkPwr;               // Primary Physical Damage stat
    public float baseMagAtkPwr;            // Primary Magical Damage stat
    public float baseDef;                  // Primary Physical Defense stat
    public float baseMagDef;               // Primary Magical Defense stat

    public float baseStr;                  // Secondary stat affects: HP, AtkPwr
    public float baseMnd;                  // Secondary stat affects: MP, MagAtkPwr
    public float baseVit;                  // Secondary stat affects: HP, Def
    public float baseSpr;                  // Secondary stat affects: MP, MagDef

    public float baseSpd;                  // Tertiary stat affects Action Bar Recharge
    public float baseLck;                  // Tertiary stat affects Critical Hit Chance

    public float actionBarRecharge;        // Speed of which actions can be taken
}
