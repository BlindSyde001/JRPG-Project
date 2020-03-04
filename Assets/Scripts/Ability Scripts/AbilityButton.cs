using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityButton : MonoBehaviour
{

    public Abilities thisAbility;
    public TextMeshProUGUI abilityName;
    public TextMeshProUGUI abilityPower;
    public TextMeshProUGUI abilityCost;

    //Initializes the button with the ability data
    public void SetButton(Abilities _ability)
    {
        thisAbility = _ability;
        abilityName.text = thisAbility._AbilityName;
        abilityPower.text = thisAbility._AbilityPower.ToString();
        abilityCost.text = thisAbility._AbilityCost.ToString();
    }

    public void CastAbility()
    {
        Debug.Log(thisAbility._AbilityName + " Was Cast");
    }
}
