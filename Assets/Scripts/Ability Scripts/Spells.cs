using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //VARIABLES
    public string _SpellName;              // What's it called
    public int _SpellPower;                // Numeric modifier for damage/heal value
    public int _SpellManaCost;             // Resource cost
    public enum SpellType { Attack, Heal}; // Category/type of spell
    public SpellType _SpellType;

    //METHODS
    public void Cast(BaseStats target)
    {
        Debug.Log(_SpellName + " Was Cast On " + target.CharacterName);
        if(_SpellType == SpellType.Attack)
        {
            target.TakeDamage(_SpellPower, true);                    // Damage Character  of type magical(true)
        }
        else if(_SpellType == SpellType.Heal)
        {
            target.HealDamage(_SpellPower);                          // Heal Character
        }
    }
}
