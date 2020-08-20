using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum AbilityType { Damage, Heal, Other };
[CreateAssetMenu(fileName = "Ability", menuName = "Abilities")]

public class Abilities : ScriptableObject
{
    //VARIABLES
    public string _AbilityName;
    public int _AbilityID;
    public int _AbilityPower;
    public int _AbilityCost;
    public AbilityType _AbilityType;
}
