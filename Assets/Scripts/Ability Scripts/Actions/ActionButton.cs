using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ActionButton : MonoBehaviour
{
    //VARIABLES
    public ActionInfo thisAction;
    public TextMeshProUGUI actionButtonName;
    public TextMeshProUGUI actionName;
    public TextMeshProUGUI actionCost;
    public TextMeshProUGUI actionDescription;

    //METHODS
    private void Start()
    {
        SetButton(thisAction);
    }
    public void SetButton(ActionInfo _action)
    {
        thisAction = _action;
        actionButtonName.text = thisAction._ActionName;

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Submit;
        entry.callback.AddListener((data) => { OnSubmit(data); });
        gameObject.GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public void OnSubmit(BaseEventData data)
    {
        //GameObject.FindObjectOfType<BattleUIController>().SubmitMagic(thisAction._SpellID);
    }

    public void SetDescription()
    {
        actionName.text = thisAction._ActionName;
        actionCost.text = thisAction._ActionCost.ToString();
        actionDescription.text = thisAction._ActionDescription;
    }
}
