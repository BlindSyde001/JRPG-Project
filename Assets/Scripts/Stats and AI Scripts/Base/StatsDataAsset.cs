using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum CharacterType { Humanoid, Beast, Machine};
[CreateAssetMenu(fileName = "character Data", menuName = "Characters")]
public class StatsDataAsset : ScriptableObject
{
    public string ID;
    public CharacterType _characterType;

    public int level;                      // Character's starting level
    public int totalXP;                    // Character's starting XP

    public float baseHP;                   // Health Pool
    public float baseMP;                   // Mana Pool

    public float baseAtkPwr;               // Primary physical damage stat
    public float baseMagAtkPwr;            // Primary magical damage stat
    public float baseDef;                  // Primary physical defense stat
    public float baseMagDef;               // Primary magical defense stat

    public float baseStr;                  // Secondary stat affects: HP, AtkPwr
    public float baseMnd;                  // Secondary stat affects: MP, MagAtkPwr
    public float baseVit;                  // Secondary stat affects: HP, Def
    public float baseSpr;                  // Secondary stat affects: MP, MagDef

    public float baseSpd;                  // Tertiary stat affects actionBarRecharge
    public float baseLck;                  // Tertiary stat affects critical hit chance

    public float actionBarRecharge;        // Speed of which actions can be taken
}
