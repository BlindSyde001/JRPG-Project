using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    //VARIABLES
    private BattleManager _BM;
    private GameManager _GM;
    public EventSystem _ES;
    public GameObject startOfGameSelected;
    public GameObject firstMagicButton;
    public GameObject messageBox;                 // For any messages needed to be written to player i.e victory/defeat, speech, etc
    public TextMeshProUGUI messageText;           // Text component of above

    #region Navigate UI Variables
    public List<Button> _CommandPanelButtons;     // Inputs for the player
    public List<GameObject> _InputPanels;         // List of all Panels with input buttons
    public List<GameObject> EnemyTargets;         // List of the buttons used to target enemies in battle
    public BasePartyMember chosenHero;            // Currently selected hero, to control actions
    public int cycleButtonInput;
    bool canAttack = false;

    public string action;
    public int _SpellID;
    public BaseStats targetForAction;

    [Header("Button Colours")]
    public Color normalColorActive;
    public Color pressedColorActive;
    public Color highlightedColorActive;
    public Color normalColorInactive;
    public Color pressedColorInactive;
    public Color highlightedColorInactive;
    #endregion

    //UPDATES
    private void Awake()
    {
        _BM = FindObjectOfType<BattleManager>();
        _GM = FindObjectOfType<GameManager>();
        messageText = messageBox.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        messageBox.SetActive(false);                               // turned on only when needed
        chosenHero = _BM._ActivePartyMembers[0];                   // Set first hero to control through UI
        _BM._CharacterPanels[0].transform.Find("Selected Panel").gameObject.SetActive(true);
        SetEnemyTargets();                                         // Set UI for enemies you can target
        ToggleCommandButtons(false);
    }
    private void Update()
    {
        if (chosenHero._ActionBarAmount >= 100)
        {
            ToggleCommandButtons(true); // Can use UI to input commands
        }
        else
        {
            ToggleCommandButtons(false); // Cannot use UI to input commands
        }
    }
    private void LateUpdate()
    {
        CycleThroughHeroes();    // Press cycle buttons to switch between heroes, so that the UI represents that hero's action pool
    }

    //METHODS
    #region UI Navigation Commands
    public void CycleThroughHeroes()
    {
        if (Input.GetButtonDown("Cycle"))
        {
            _BM._CharacterPanels[cycleButtonInput].transform.Find("Selected Panel").gameObject.SetActive(false);  // turn off current select

            cycleButtonInput += (int)Input.GetAxisRaw("Cycle");                                        // find new Select
            if (cycleButtonInput < 0)
            {
                cycleButtonInput = (_BM._ActivePartyMembers.Count - 1);
            }
            else if (cycleButtonInput > (_BM._ActivePartyMembers.Count - 1))
            {
                cycleButtonInput = 0;
            }
            chosenHero = _BM._ActivePartyMembers[cycleButtonInput];
            _BM._CharacterPanels[cycleButtonInput].transform.Find("Selected Panel").gameObject.SetActive(true);   // Turn on new select
            Debug.Log(chosenHero.CharacterName + " Is Selected");
        }
    }                     // Switch control between active Heroes
    public void CycleThroughTabs()
    {
        if(Input.GetButtonDown("Trigger"))
        {

        }
    }                       // Switch between UI tab groups
    public void SetEnemyTargets()
    {
        foreach (GameObject a in EnemyTargets)
        {
            a.SetActive(false);
        }
        for (int i = 0; i < _BM._ActiveEnemies.Count; i++)
        {
            EnemyTargets[i].SetActive(true);
            EnemyTargets[i].GetComponentInChildren<TextMeshProUGUI>().text = _BM._ActiveEnemies[i].CharacterName;
        }
    }                        // Set the buttons of targettable enemies.
    public void SetAvailableSpells()
    {

    }                     // Turns on only the spells that the player has.
    public void ToggleCommandButtons(bool isActive)
    {
        canAttack = isActive;   //Sets us to be able to use the attacke button as in the function AttackTest
        _ES.firstSelectedGameObject = startOfGameSelected;
        foreach (Button a in _CommandPanelButtons)
        {
            //Rather than using button.inactive, we are just changing the colors of the buttons to mimic being inactive
            if (isActive)
            {
                ColorBlock colors = a.colors;
                colors.normalColor = normalColorActive;
                colors.highlightedColor = highlightedColorActive;
                colors.selectedColor = highlightedColorActive;
                colors.pressedColor = pressedColorActive;
                a.colors = colors;
            }
            else
            {
                ColorBlock colors = a.colors;
                colors.normalColor = normalColorInactive;
                colors.highlightedColor = highlightedColorInactive;
                colors.selectedColor = highlightedColorInactive;
                colors.pressedColor = pressedColorInactive;
                a.colors = colors;
            }
        }
    }      // If ATB is charged, can input commands
    private void SetOffAllPanels()
    {
        foreach (GameObject a in _InputPanels)
        {
            a.SetActive(false);
        }
    }                       // Turns off all our panels so they don't overlap.
    private void OpenTargetList()
    {
        _InputPanels[0].SetActive(true);
    }                        // Open Targetting list (Enemies / Heroes)

    // Event triggers for UI buttons
    // SUBMIT
    public void AccessAttack()
    {
        SetOffAllPanels();
        if (canAttack)
        {
            _InputPanels[0].SetActive(true);
            EnemyTargets[0].GetComponent<Button>().Select();
            ActionType("Attack");
        }
    }
    public void AccessMagic()
    {
        SetOffAllPanels();
        if (canAttack)
        {
            _InputPanels[3].SetActive(true);
            _InputPanels[2].SetActive(true);
            firstMagicButton.GetComponent<Button>().Select();
            ActionType("Magic");
        }
    }
    public void AccessAbilities()
    {
        SetOffAllPanels();
        if (canAttack)
        {

        }
    }
    public void AccessItems()
    {
        SetOffAllPanels();
        if (canAttack)
        {

        }
    }

    public void SubmitMagic(int input)
    {
        if(canAttack)
        {
            _InputPanels[0].SetActive(true);
            EnemyTargets[0].transform.GetComponent<Button>().Select();
            _SpellID = input;
        }
    }
    public void SubmitAbility()
    {

    }
    public void SubmitItem()
    {

    }
    // CANCEL
    public void ReturnToCommandAttack()
    {
        _InputPanels[0].SetActive(false);
        _InputPanels[2].SetActive(false);
        _CommandPanelButtons[0].Select();
    }
    public void ReturnToCommandMagic()
    {
        _InputPanels[3].SetActive(false);
        _InputPanels[2].SetActive(false);
        _CommandPanelButtons[1].Select();
    }
    public void ReturnToCommandAbility()
    {
        _InputPanels[2].SetActive(false);
        _CommandPanelButtons[2].Select();
    }
    public void ReturnToCommandItem()
    {
        _InputPanels[2].SetActive(false);
        _CommandPanelButtons[3].Select();
    }

    public void ReturnToAccessMagic()
    {

    }
    public void ReturnToAccessAbility()
    {

    }
    public void ReturnToAccessItem()
    {

    }
    #endregion
    #region Variables from Inputs
    public void ActionType(string inputString)
    {
        action = inputString;
    }            // Attack, Magic, Ability, Item
    public void SpellNum(int idNum)
    {
        _SpellID = idNum;
    }
    public void ActionTarget(int enemy)
    {
        targetForAction = _BM._ActiveEnemies[enemy];
        PerformAction(action, targetForAction);
    }
    #endregion
    #region Outputs
    public void MessageOnScreen(string text)
    {
        messageBox.SetActive(true);
        messageText.text = text;

        //float t = 0;
        //float cd = 3;
        //t += Time.deltaTime;
        //if(t >= cd)
        //{
        //    messageText.text = "";
            
        //}
    }              // Display a message
    public void PerformAction(string action, BaseStats target)
    {
        if(action == "Attack")
        {
            chosenHero.Attack(target);
            chosenHero._ActionBarAmount = 0;
        }
        else if(action == "Magic")
        {
            Debug.Log(chosenHero.CharacterName + " casted " + _GM._SpellsPool[_SpellID]._SpellName);
            Spells spellToCast = _GM._SpellsPool[_SpellID];   // Define the spell to be cast
            chosenHero.CastMagic(spellToCast, target);        // Send to player info to cast spell with their stats
            //chosenHero._ActionBarAmount = 0;
        }
        else if (action == "Ability")
        {
            chosenHero._ActionBarAmount = 0;
        }
        else if( action == "Item")
        {
            chosenHero._ActionBarAmount = 0;
        }
        SetOffAllPanels();
        startOfGameSelected.GetComponent<Button>().Select();
    }
    #endregion
}
