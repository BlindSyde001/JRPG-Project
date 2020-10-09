using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellButton : MonoBehaviour
{
    //VARIABLES
    public SpellsInfo thisSpell;
    public TextMeshProUGUI spellButtonName;
    public TextMeshProUGUI spellName;
    public TextMeshProUGUI spellCost;
    public TextMeshProUGUI spellDescription;

    //METHODS
    private void Start()
    {
        SetButton(thisSpell);
    }
    public void SetButton(SpellsInfo _spell)
    {
        thisSpell = _spell;
        spellButtonName.text = thisSpell._SpellName;
    }
    public void SetDescription()
    {
        spellName.text = thisSpell._SpellName;
        spellCost.text = thisSpell._SpellManaCost.ToString();
        spellDescription.text = thisSpell._SpellDescription;
    }
}
