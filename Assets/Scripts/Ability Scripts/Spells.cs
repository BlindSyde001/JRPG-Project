using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum SpellType { Attack, Heal, Other };
[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]

public class Spells : ScriptableObject
{
    //VARIABLES
    public string _SpellName;
    public int _SpellID;
    public int _SpellPower;
    public int _SpellManaCost;
    public SpellType _SpellType;
}
