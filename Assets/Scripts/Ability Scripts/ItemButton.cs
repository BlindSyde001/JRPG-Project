using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public Items thisItem;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAmount;

    //Initializes the button with the spell data
    public void SetButton(Items _Item)
    {
        thisItem = _Item;
        ItemName.text = thisItem._ItemName;
        ItemAmount.text = thisItem._ItemAmount.ToString();
    }

    public void UseItem()
    {
        Debug.Log(thisItem._ItemName + " Was Cast");
    }
}
