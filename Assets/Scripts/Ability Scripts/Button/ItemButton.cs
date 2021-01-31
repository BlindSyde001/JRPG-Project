using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    //VARIABLES
    public ConsumableInfo thisItem;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemAmount;
    public Navigation newNav =  new Navigation();
    //METHODS
    public void SetButton(ConsumableInfo _Item)
    {
        thisItem = _Item;
        ItemName.text = thisItem._ItemName;
        ItemAmount.text = thisItem._ItemAmount.ToString();
        newNav.mode = Navigation.Mode.Explicit;
        
    }
    
    public void SetNavMode()
    {
        gameObject.GetComponent<Button>().navigation = newNav;
    }
}
