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
    public GameObject testSelect;
    public GameObject firstMagicButton;

    public GameObject messageBox;                 // For any messages needed to be written to player i.e victory/defeat, speech, etc
    public TextMeshProUGUI messageText;           // Text component of above

    #region Navigate UI Variables
    public List<Button> _CommandPanelButtons;     // Inputs for the player
    public List<GameObject> EnemyTargets;        // List of the buttons used to target enemies in battle
    public GameObject _EnemyTargetPanel;
    public GameObject _MagicPanel;
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
    }
    public void SetEnemyTargets()
    {
        // Set the buttons of targettable enemies. first turns them all off then cycle through how many and turn on the buttons to match.
        foreach (GameObject a in EnemyTargets)
        {
            a.SetActive(false);
        }
        for (int i = 0; i < _BM._ActiveEnemies.Count; i++)
        {
            EnemyTargets[i].SetActive(true);
            EnemyTargets[i].GetComponentInChildren<TextMeshProUGUI>().text = _BM._ActiveEnemies[i].CharacterName;
        }
    }
    public void SetAvailableSpells()
    {

    }

    public void ToggleCommandButtons(bool isActive)
    {
        canAttack = isActive;   //Sets us to be able to use the attacke button as in the function AttackTest
        _ES.firstSelectedGameObject = testSelect;
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
    }
    public void DoAttack()
    {
        SetOffAllPanels();
        if (canAttack)
        {
            _EnemyTargetPanel.SetActive(true);
            EnemyTargets[0].GetComponent<Button>().Select();
            ActionType("Attack");
        }
    }

    public void DoMagic()
    {
        SetOffAllPanels();
        if (canAttack)
        {
            //This is a bit buggy. Needs to be fixed properly
            _MagicPanel.SetActive(true);
            _ES.firstSelectedGameObject = firstMagicButton;   //Need to change to the magic buttons
            EnemyTargets[0].GetComponent<Button>().Select();
            ActionType("Magic");
        }
    }

    void SetOffAllPanels()      //Turns off all our panels so they don't overlap
    {
        _EnemyTargetPanel.SetActive(false);
        _MagicPanel.SetActive(false);
    }
    #region Test
    public void MessageOnScreen(string text)           // Display a message
    {
        messageBox.SetActive(true);
        messageText.text = text;
    }
    public void ActionType(string inputString)
    {
        action = inputString;
    }
    public void ActionTarget(int enemy)
    {
        targetForAction = _BM._ActiveEnemies[enemy];
        PerformAction(action, targetForAction);
    }
    public void SpellID(int id)
    {
        _SpellID = id;
    }
    public void PerformAction(string action, BaseStats target)
    {
        if(action == "Attack")
        {
            chosenHero.Attack(target);
            chosenHero._ActionBarAmount = 0;
        }
        else if(action == "Cast Magic")
        {
            Debug.Log("Magic Cast!");
            Spells spellToCast = chosenHero.availableSpells[_SpellID];
            chosenHero.CastMagic(spellToCast, target);
            chosenHero._ActionBarAmount = 0;
        }
        else if (action == "Ability")
        {
            chosenHero._ActionBarAmount = 0;
        }
        else if( action == "Item")
        {
            chosenHero._ActionBarAmount = 0;
        }
    }
    #endregion
}
