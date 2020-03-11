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
    public string _SpellDescription;

    public void ModulatedSpell(BaseStats target)
    {
        if(_SpellType == SpellType.Attack)
        {
            int damageAmount;

            damageAmount = (_SpellPower * 10);
            target.TakeDamage(damageAmount, true);
        }
        if(_SpellType == SpellType.Heal)
        {
            Debug.Log("Heal from " + _SpellName);
        }
        if(_SpellType == SpellType.Other)
        {
            Debug.Log("?? from " + _SpellName);
        }
    }
}
