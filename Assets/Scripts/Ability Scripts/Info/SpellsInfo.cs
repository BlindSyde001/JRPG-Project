using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum SpellTarget    { Single, Multi };
    public enum SpellType      { Damage, Heal, Effect };
    public enum SpellElement   { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };
    public enum SpellCombat    { Combat, World, Both };
[CreateAssetMenu(fileName = "New Spell", menuName = "SpellsInfo")]

public class SpellsInfo : ScriptableObject
{
    //VARIABLES
    public string _SpellName;
    public int _SpellID;
    public int _SpellPower;
    public int _SpellManaCost;
    public string _SpellDescription;
    public int[] posStatusChances = new int[3];  // Zeal, Faith, Haste
    public int[] negStatusChances = new int[3];  // Poison, Silence, Slow

    public SpellTarget _SpellTarget;
    public SpellType _SpellType;
    public SpellElement _SpellElement;
    public SpellCombat _SpellCombat;

}