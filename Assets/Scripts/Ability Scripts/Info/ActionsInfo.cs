using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ActionTarget    { Single, Multi };
    public enum ActionEffect    { Damage, Heal, Effect };
    public enum ActionElement   { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };
    public enum ActionCombat    { Combat, World, Both };

public class ActionsInfo : ScriptableObject
{
    public int[] posStatusChances = new int[3];  // Zeal, Faith, Haste
    public int[] negStatusChances = new int[3];  // Poison, Silence, Slow
    public bool _ActionPiercing;
    public string _ActionDescription;
}