using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum SpellTarget    { Single, Multi, Other}
    public enum SpellType      { Damage, Heal, Shield, Other };
    public enum SpellElement   { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };
    public enum SpellCombat    { Combat, World, Both };
[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]

public class Spells : ScriptableObject
{
    //VARIABLES
    public string _SpellName;
    public int _SpellID;
    public int _SpellPower;
    public int _SpellManaCost;
    public string _SpellDescription;

    public SpellTarget _SpellTarget;
    public SpellType _SpellType;
    public SpellElement _SpellElement;
    public SpellCombat _SpellCombat;
}