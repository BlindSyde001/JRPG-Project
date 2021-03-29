using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Gamestate { Battle, World, Title}
public class Menu : MonoBehaviour
{
    // VARIABLES
    public Gamestate _Gamestate;
    [HideInInspector]
    public GameObject _WorldUI;                                     // UI Menu for when you're not fighting 
    [HideInInspector]
    public GameObject _BattleUI;                                    // UI Menu for Battle only

    private bool isBattle;
    [HideInInspector]
    public GameObject itemButtonContainerWorld;                     // Contains all item buttons for UI Menu
    [HideInInspector]
    public GameObject itemButtonContainerBattle;                    // Contains all item buttons for Battle Menu

    public List<ConsumableInfo> _ConsumablesInBag;                  // The items currently in inventory
    public List<EquipmentInfo> _EquipmentInBag;                     // The equipment currently in inventory
    public List<BaseItemInfo> _KeyItemsInBag;                       // Key items obtained for story progression
    [HideInInspector]
    public List<BaseItemInfo> tempItemList;                         // Used as generic list for item type
    [HideInInspector]
    public List<GameObject> tempList;                               // Used for Button Navigation
    
    [HideInInspector]
    public Button _ThisButton;                                      // Button that has been pressed and will be returned to in inventory
    public Button _FirstSelectedWorld;
    public Button _FirstSelectedBattle;
    // UPDATES
    private void Start()
    {
        #region Change UI Base on Gamestate
        if (_Gamestate == Gamestate.Battle)
        {
            _WorldUI.SetActive(false);
            _BattleUI.SetActive(true);
            isBattle = true;
        }
        else if (_Gamestate == Gamestate.World)
        {
            _WorldUI.SetActive(true);
            _BattleUI.SetActive(false);
            isBattle = false;
        }
        else if (_Gamestate == Gamestate.Title)
        {
            _WorldUI.SetActive(false);
            _BattleUI.SetActive(false);
            isBattle = false;
        }
        #endregion
        DontDestroyOnLoad(this.gameObject);
    }

    // METHODS

    // Put in Container and items to put into buttons
    public void InitiateInventory(int y)
    {
        tempList.Clear();
        tempItemList.Clear();
        // To identify which item type is to be shown in the inventory. Upcast elements from list into tempitemlist.
        if (y == 0)
        {
            foreach (ConsumableInfo child in _ConsumablesInBag)
            {
                BaseItemInfo parent = child;
                tempItemList.Add(parent);
            }
        }
        else if (y == 1)
        {
            foreach (EquipmentInfo child in _EquipmentInBag)
            {
                BaseItemInfo parent = child;
                tempItemList.Add(child);
            }
        }
        else if (y == 2)
        {
            foreach (BaseItemInfo child in _KeyItemsInBag)
            {
                BaseItemInfo parent = child;
                tempItemList.Add(child);
            }
        }

        GameObject Container = isBattle ? itemButtonContainerBattle : itemButtonContainerWorld;

        // Reset Item List
        foreach (Transform a in Container.transform)
            a.gameObject.SetActive(false);
        if (tempItemList.Count != 0)
        {
            for (int i = 0; i < tempItemList.Count; i++)
            {
                Container.transform.GetChild(i).gameObject.SetActive(true);
                // Identify item in the templist
                Container.transform.GetChild(i).GetComponent<ItemButton>().thisItem = tempItemList[i];
                // Sets the item into the UI Button
                Container.transform.GetChild(i).GetComponent<ItemButton>().
                    SetButton(Container.transform.GetChild(i).GetComponent<ItemButton>().thisItem);
                // Adds button to navigation list
                tempList.Add(Container.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList.Count != 0 || tempList.Count != 1)
                {
                    if (i == 0) // For first in the list, UP, goes to the last item in list
                    {
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[tempList.Count - 1].gameObject.GetComponent<Button>();
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                    }
                    else if (i == tempList.Count - 1) // For last in the list, DOWN, goes to first item in list
                    {
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[0].gameObject.GetComponent<Button>();
                    }
                    else if (i > 0 || i < tempList.Count - 1) // For all other items
                    {
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnUp = tempList[i - 1].gameObject.GetComponent<Button>();
                        tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnDown = tempList[i + 1].gameObject.GetComponent<Button>();
                    }
                    tempList[i].gameObject.GetComponent<ItemButton>().newNav.selectOnLeft = _ThisButton;
                    tempList[i].gameObject.GetComponent<ItemButton>().SetNavMode();
                }
            }       // Setting the buttons' Up/Down/Left Navigation
            if (Container.transform.GetChild(0) != null)
            {
                Container.transform.GetChild(0).GetComponent<Button>().Select();
            }   // If you have items, when you open UI have 1st selected
        }
        else if (tempItemList.Count == 0)
        {
            _ThisButton.Select();
        }
    }
    public void SetReturnButton(Button pressedButton)
    {
        _ThisButton = pressedButton;
    }

    #region Event Subscription
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Battle Scene")
        {

        }
        if (scene.name == "Battle Scene")
        {
        }
    }
    void OnBattleStart()
    {
        _Gamestate = Gamestate.Battle;
    }
    void OnBattleEnd()
    {

    }
    #endregion
    #region OnEnable/Disable
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
        EventManager.BattleStarted += OnBattleStart;
        EventManager.BattleEnded += OnBattleEnd;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
        EventManager.BattleStarted -= OnBattleStart;
        EventManager.BattleEnded -= OnBattleEnd;
    }
    #endregion
}
