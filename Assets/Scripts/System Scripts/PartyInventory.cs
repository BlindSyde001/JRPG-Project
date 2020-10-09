using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyInventory : MonoBehaviour
{
    //VARIABLES
    public GameObject itemButton;                          // Used as base button for items.
    public GameObject itemButtonContainerUI;               // Contains all item buttons for UI Menu.
    public GameObject itemButtonContainerBattle;           // Contains all item buttons for Battle Menu.
    public List<ItemInfo> _Items;                             // The items currently in inventory.

    //METHODS
    #region ITEMS
    public void InitiateInventoryUI()                      // Creates the Inventory list when you open the UI
    {
        // Clear Item List
        foreach (Transform a in itemButtonContainerUI.transform)
        {
            Destroy(a.gameObject);
        }

        // Remake buttons
        for (int i = 0; i < _Items.Count; i++)
        {
            if(_Items[i]._ItemAmount > 0)
            {
                // Create Item button in inventory.
                GameObject x = Instantiate(itemButton, itemButtonContainerUI.transform);
                // Attach Dataset component.
                x.GetComponent<ItemButton>().SetButton(_Items[i]);
                Debug.Log("I Have " + _Items[i]._ItemAmount + " x " + _Items[i] + "!");
            }
            UpdateInventoryUI();
            itemButtonContainerUI.transform.GetChild(0).GetComponent<Button>().Select();
        }
    }
    public void UpdateInventoryUI()                        // Updates the Inventory when any changes occur to amount
    {
        foreach(Transform a in itemButtonContainerUI.transform)
        {
            if(a.GetComponent<ItemInfo>()._ItemAmount <= 0)
            {
                Destroy(a.gameObject);
            }
        }
    }
    public void SortInventoryUI()                          // Sort Items in Inventory according to their item type.
    {

    }
    public void UseItemUI(ItemInfo item, BaseStats target)    // Apply effects of an item that is usable out of Combat
    {

    }

    public void InitiateInventoryBattle()                  // Creates the Item List when you open battle menu
    {
        foreach(Transform a in itemButtonContainerBattle.transform)
        {
            Destroy(a.gameObject);
        }
        for(int i = 0; i < _Items.Count; i++)
        {
            if(_Items[i]._ItemAmount > 0)
            {
                // Create Item button in inventory.
                GameObject x = Instantiate(itemButton, itemButtonContainerBattle.transform);
                // Attach Dataset component.
                x.GetComponent<ItemButton>().SetButton(_Items[i]);
                Debug.Log("I Have " + _Items[i]._ItemAmount + " x " + _Items[i] + "!");
            }
        }
        UpdateInventoryBattle();
    }
    public void UpdateInventoryBattle()                    // Updates the Inventory when any changes occur to amount
    {
        foreach (Transform a in itemButtonContainerBattle.transform)
        {
            if (a.GetComponent<ItemInfo>()._ItemAmount <= 0)
            {
                Destroy(a.gameObject);
            }
        }
    }
    public void UseItemBattle()                            // Use the item
    {

    }
    #endregion
    #region OTHER
    public void ChangeFormation()              // Change position of party member in the lineup.
    {

    }
    #endregion
}
