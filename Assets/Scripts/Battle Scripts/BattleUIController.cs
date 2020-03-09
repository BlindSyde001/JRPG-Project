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
    
    public GameObject messageBox;                 // For any messages needed to be written to player i.e victory/defeat, speech, etc
    public TextMeshProUGUI messageText;           // Text component of above

    #region Navigate UI Variables
    public GameObject _CommandPanel;              // Panel which contains all the player input commands
    public List<GameObject> EnemyUIButton;        // List of the buttons used to target enemies in battle
    public BasePartyMember chosenHero;            // Currently selected hero, to control actions
    public int cycleButtonInput;

    public string action;
    public int _SpellID;
    public BaseStats targetForAction;
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
    }
    private void Update()
    {
        if(chosenHero._ActionBarAmount >= 100)
        {
            // Can use UI to input commands
        } else
        {
            // Cannot use UI to input commands
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
        foreach(GameObject a in EnemyUIButton)
        {
            a.SetActive(false);
        }
        for (int i = 0; i < _BM._ActiveEnemies.Count; i++)
        {
            EnemyUIButton[i].SetActive(true);
            EnemyUIButton[i].GetComponentInChildren<TextMeshProUGUI>().text = _BM._ActiveEnemies[i].CharacterName;
        }
    }
    public void SetAvailableSpells()
    {

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
