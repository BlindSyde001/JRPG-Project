using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellButton : MonoBehaviour
{
    public Spells thisSpell;
    public TextMeshProUGUI spellButtonName;
    public TextMeshProUGUI spellName;
    public TextMeshProUGUI spellCost;
    public TextMeshProUGUI spellDescription;
    public int spellID;

    private void Start()
    {
        SetButton(thisSpell);
    }
    //Initializes the button with the spell data
    public void SetButton(Spells _spell)
    {
        thisSpell = _spell;
        spellButtonName.text = thisSpell._SpellName;
    }
    public void SetDescription()
    {
        spellName.text = thisSpell._SpellName;
        spellCost.text = thisSpell._SpellManaCost.ToString();
        spellDescription.text = thisSpell._SpellDescription;
        spellID = thisSpell._SpellID;
    }
}
