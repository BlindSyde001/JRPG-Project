using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Submit;
        entry.callback.AddListener((data) => { OnSubmit(data); });
        gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public void OnSubmit(BaseEventData data)
    {
        Debug.Log("Worked");
        GameObject.FindObjectOfType<BattleUIController>().SubmitMagic(thisSpell._SpellID);
    }

    public void SetDescription()
    {
        spellName.text = thisSpell._SpellName;
        spellCost.text = thisSpell._SpellManaCost.ToString();
        spellDescription.text = thisSpell._ActionDescription;
    }
}
