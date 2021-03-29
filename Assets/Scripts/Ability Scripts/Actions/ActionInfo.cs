using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ActionType      { Spell, Ability, Item };
    public enum ActionTarget    { Single, Multi };                              // How many characters does this affect
    public enum ActionEffect    { Damage, Heal, Effect };                       // What effect does this have on character
    public enum ActionElement   { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };    // Element for resists
    public enum ActionValue     { Flat, Percent, None };                               // Value variable to calculate
    public enum ActionCombat    { Combat, World, Both };                               // where can you use this action?
[CreateAssetMenu(fileName = "Action Data", menuName = "Actions")]

public class ActionInfo : ScriptableObject
{
    public ActionType _ActionType;
    public ActionTarget _ActionTarget;
    public ActionEffect _ActionEffect;
    public ActionElement _ActionElement;
    public ActionValue _ActionValue;
    public ActionCombat _ActionCombat;

    public string _ActionName;
    public float powerValue;                     // Used for damage calculation
    public int _ActionCost;
    public string _ActionDescription;            // UI: What does action do?

    public List<int> posBuffChances;             // Zeal, Faith, Haste, Revive    // Chances to inflict buff
    public List<int> negStatusChances;           // Poison, Silence, Slow, Death  // Chances to inflict debuff
    public List<int> dispelChances;              // All pos and neg
    public bool _ActionPiercing;                 // Does this go through defences? Calc minusing defense stats or full damage
}