using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyInventory : MonoBehaviour
{
    //VARIABLES
    public GameObject itemButton;                                   // Used as base button for items.
    public GameObject itemButtonContainerUI;                        // Contains all item buttons for UI Menu.
    public GameObject itemButtonContainerBattle;                    // Contains all item buttons for Battle Menu.
    public List<ConsumableInfo> _Items;                             // The items currently in inventory.
    public List<GameObject> tempList;                               // Used for button Navigation

    //METHODS
    #region ITEMS
    public void InitiateInventoryUI()                      // Creates the Inventory list when you open the UI
    {
        // Reset Item List
        foreach (Transform a in itemButtonContainerUI.transform)
        {
            a.GetComponent<ItemButton>().SetButton(a.GetComponent<ItemButton>().thisItem); // Sets the item into the UI Button

            if(a.GetComponent<ItemButton>().thisItem._ItemAmount == 0)                     // Turns off button if you don't have any of the item
            {
                a.gameObject.SetActive(false);
            }
            else
            {
                a.gameObject.SetActive(true);
                tempList.Add(a.gameObject);

                //Debug.Log("I Have " + a.GetComponent<ItemButton>().thisItem._ItemAmount + " x " + a.GetComponent<ItemButton>().thisItem + "!");
            }
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            if (tempList.Count != 0 || tempList.Count != 1)
            {
                if (i == 0)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[tempList.Count - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                }
                else if (i == tempList.Count - 1)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[0].gameObject.GetComponent<Button>();
                }
                else if (i > 0 || i < tempList.Count - 1)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                }
                tempList[i].gameObject.GetComponent<ItemButton>().SetNavMode();
            }
        }       // Setting the buttons' Up/Down Navigation
        if (itemButtonContainerUI.transform.GetChild(0) != null)                          // If you have items, when you open UI have 1st selected
        {
            itemButtonContainerUI.transform.GetChild(0).GetComponent<Button>().Select();
        }
    }
    public void UpdateInventoryUI()                        // Updates the Inventory when any changes occur to amount
    {
        foreach(Transform a in itemButtonContainerUI.transform)
        {
            if(a.GetComponent<ItemButton>().thisItem._ItemAmount <= 0)
            {
                tempList.Remove(a.gameObject);
                a.gameObject.SetActive(false);
            }
        }
    }
    public void SortInventoryUI()                          // Sort Items in Inventory according to their item type.
    {

    }
    public void UseItemUI(ConsumableInfo item, BaseStats target)    // Apply effects of an item that is usable out of Combat
    {

    }

    public void InitiateInventoryBattle()                  // Creates the Item List when you open battle menu
    {
        // Reset Item List
        foreach (Transform a in itemButtonContainerBattle.transform)
        {
            a.GetComponent<ItemButton>().SetButton(a.GetComponent<ItemButton>().thisItem); // Sets the item into the UI Button

            if (a.GetComponent<ItemButton>().thisItem._ItemAmount == 0)                    // Turns off button if you don't have any of the item
            {
                a.gameObject.SetActive(false);
            }
            else
            {
                a.gameObject.SetActive(true);
                tempList.Add(a.gameObject);

                Debug.Log("I Have " + a.GetComponent<ItemButton>().thisItem._ItemAmount + " x " + a.GetComponent<ItemButton>().thisItem + "!");
            }
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            if (tempList.Count != 0 || tempList.Count != 1)
            {
                if (i == 0)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[tempList.Count - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                }
                else if (i == tempList.Count - 1)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[0].gameObject.GetComponent<Button>();
                }
                else if (i > 0 || i < tempList.Count - 1)
                {
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                }
                tempList[i].gameObject.GetComponent<ItemButton>().SetNavMode();
            }
        }       // Setting the buttons' Up/Down Navigation
        if (itemButtonContainerBattle.transform.GetChild(0) != null)                          // If you have items, when you open UI have 1st selected
        {
            itemButtonContainerBattle.transform.GetChild(0).GetComponent<Button>().Select();
        }
    }
    public void UpdateInventoryBattle()                    // Updates the Inventory when any changes occur to amount
    {
        foreach (Transform a in itemButtonContainerBattle.transform)
        {
            if (a.GetComponent<ConsumableInfo>()._ItemAmount <= 0)
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
    #region Level Changing
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Battle Scene")
        {
            itemButtonContainerUI = GameObject.Find("itemButtonContainerUI");
            InitiateInventoryUI();
        }
        else if(scene.name == "Battle Scene")
        {
            //itemButtonContainerBattle = GameObject.Find("itemButtonContainerBattle");
            //InitiateInventoryBattle();
        }
    }
    #endregion
}
