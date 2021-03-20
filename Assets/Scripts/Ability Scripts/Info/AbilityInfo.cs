using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]

public class AbilityInfo : ActionsInfo
{
    //VARIABLES
    public string _AbilityName;
    public int _AbilityID;
    public int _AbilityCost;

    public enum AbilityProperty { Physical, Magical};

    public ActionTarget _AbilityTarget;
    public ActionCombat _AbilityCombat;

    public ActionEffect _AbilityEffect;
    public ActionElement _AbilityElement;
    public ActionValue _AbilityValue;

    public AbilityProperty _AbilityProperty;
}
