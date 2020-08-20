using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyInventory : MonoBehaviour
{
    //VARIABLES
    public GameObject itemButton;             // Used as base button for items. 
    public GameObject itemButtonContainer;    // Contains all item buttons.
    public List<Items> _Items;                // The items currently in inventory.

    //UPDATES
    private void Start()
    {
        InitiateInventory();
    }

    //METHODS
    public void InitiateInventory()            // Creates the Inventory list when you open the UI
    {
        foreach (Transform a in itemButtonContainer.transform)
        {
            Destroy(a.gameObject);
        }

        for (int i = 0; i < _Items.Count; i++)
        {
            if(_Items[i]._ItemAmount > 0)
            {
                GameObject x = Instantiate(itemButton, itemButtonContainer.transform);
                x.GetComponent<ItemButton>().SetButton(_Items[i]);
                Debug.Log("I Have " + _Items[i]._ItemAmount + " x " + _Items[i] + "!");
                // Create Item button in inventory.
                // Attach Dataset component.
            }
            itemButtonContainer.transform.GetChild(0).GetComponent<Button>().Select();
        }
    }
    public void SortInventory()                // Sort Items in Inventory according to their item type.
    {

    }
    public void ChangeFormation()              // Change position of party member in the lineup.
    {

    }
}
