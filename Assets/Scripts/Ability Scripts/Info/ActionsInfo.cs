using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ActionType      { Spell, Ability, Item };
    public enum ActionTarget    { Single, Multi };                              // How many characters does this affect
    public enum ActionEffect    { Damage, Heal, Effect };                       // What effect does this have on character
    public enum ActionElement   { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };    // Element for resists
    public enum ActionValue     { Flat, Percent, None };                               // Value variable to calculate
    public enum ValueVariable   { STR, MND, VIT, SPR, SPD, LCK}
    public enum ActionCombat    { Combat, World, Both };                               // where can you use this action?

public class ActionsInfo : ScriptableObject
{
    public ActionType _ActionType;

    public int[] posBuffChances = new int[4];    // Zeal, Faith, Haste, Revive    // Chances to inflict buff
    public int[] negStatusChances = new int[4];  // Poison, Silence, Slow, Death  // Chances to inflict debuff
    public int[] dispelChances = new int[8];     // All pos and neg
    public bool _ActionPiercing;                 // Does this go through defences? Calc minusing defense stats or full damage

    public float powerValue;                     // Used for damage calculation
    public string _ActionDescription;            // UI: What does action do?
}