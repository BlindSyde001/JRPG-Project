using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]

public class AbilityInfo : ActionsInfo
{
    //VARIABLES
    public string _AbilityName;
    public int _AbilityID;
    public int _AbilityPower;
    public int _AbilityCost;

    public enum AbilityProperty { Physical, Magical};

    public ActionTarget _Abilitytarget;
    public ActionEffect _AbilityEffect;
    public ActionElement _AbilityElement;
    public ActionCombat _AbilityCombat;
    public AbilityProperty _AbilityProperty;
}
