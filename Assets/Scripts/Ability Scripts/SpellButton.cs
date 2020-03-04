using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellButton : MonoBehaviour
{
    public Spells thisSpell;
    public TextMeshProUGUI spellName;
    public TextMeshProUGUI spellManaCost;
    public TextMeshProUGUI spellPower;
    
    //Initializes the button with the spell data
    public void SetButton(Spells _spell)
    {
        thisSpell = _spell;
        spellName.text = thisSpell._SpellName;
        //spellManaCost.text = thisSpell._SpellManaCost.ToString();
        //spellPower.text = thisSpell._SpellPower.ToString();
    }

    public void CastSpell()
    {
        Debug.Log(thisSpell._SpellName + " Was Cast");
    }
}
