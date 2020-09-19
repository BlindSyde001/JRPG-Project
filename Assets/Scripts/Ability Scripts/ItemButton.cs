using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    //VARIABLES
    public Items thisItem;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAmount;
    
    //METHODS
    public void SetButton(Items _Item)
    {
        thisItem = _Item;
        ItemName.text = thisItem._ItemName;
        ItemAmount.text = thisItem._ItemAmount.ToString();
    }
}
