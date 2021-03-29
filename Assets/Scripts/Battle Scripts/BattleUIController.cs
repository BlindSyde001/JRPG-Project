using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

    #region End of Fight Screen
    public GameObject finishMessageBox;
    public GameObject goldBox;
    public GameObject xpBox;
    public List<GameObject> finishHeroPanel;
    public bool endOfFight;
    private int endOfFightTransition = 0;
    private float t = 0;
    #endregion
    #region Navigate UI Variables
    public List<Button> _CommandPanelButtons;     // Inputs for the player
    public List<GameObject> _InputPanels;         // List of all Panels with input buttons
    public List<GameObject> _DisplayPanels;       // List of default Panels open when battle starts
    public List<GameObject> EnemyTargets;         // List of the buttons used to target enemies in battle
    public BasePartyMember chosenHero;            // Currently selected hero, to control actions
    public int cycleButtonInput;
    public int lastCycleInput;                    // For hero death purposes
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
    #region Updates
    private void Awake()
    {
        _BM = FindObjectOfType<BattleManager>();
        _GM = FindObjectOfType<GameManager>();
        messageText = messageBox.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        messageBox.SetActive(false);                               // turned on only when needed
        finishMessageBox.SetActive(false);
        chosenHero = _BM._ActivePartyMembers[0];                   // Set first hero to control through UI
        _BM._CharacterPanels[0].transform.Find("Selected Panel").gameObject.SetActive(true);
        SetEnemyTargets();                                         // Set UI for enemies you can target
        ToggleCommandButtons(false);
    }
    private void Update()
    {
        if (endOfFight)
        {
            if (_BM._DownedMembers.Count == _BM._PartyMembersInBattle.Count)
                EndOfFightMessaging(false);
            else
                EndOfFightMessaging(true);
        }
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
    #endregion
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
            lastCycleInput = cycleButtonInput;
            chosenHero = _BM._ActivePartyMembers[cycleButtonInput];
            _BM._CharacterPanels[cycleButtonInput].transform.Find("Selected Panel").gameObject.SetActive(true);   // Turn on new select
        }
    }                     // Switch control between active Heroes.
    public void CycleOnDeathHeroes()
    {
        if (_BM._DownedMembers.Contains(chosenHero))
        {
            Debug.Log("Yes");
            _BM._CharacterPanels[lastCycleInput].transform.Find("Selected Panel").gameObject.SetActive(false);  // turn off current select

            lastCycleInput += 1;                                        // find new Select
            if (lastCycleInput < 0)
            {
                lastCycleInput = (_BM._ActivePartyMembers.Count - 1);
            }
            else if (lastCycleInput > (_BM._ActivePartyMembers.Count - 1))
            {
                lastCycleInput = 0;
            }
            chosenHero = _BM._ActivePartyMembers[lastCycleInput];
            _BM._CharacterPanels[lastCycleInput].transform.Find("Selected Panel").gameObject.SetActive(true);
        }
    }                     // Switch control between active Heroes On Death.
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
    private void SetOffAllInputPanels()
    {
        foreach (GameObject a in _InputPanels)
        {
            a.SetActive(false);
        }
    }                  // Turns off all our panels so they don't overlap.
    private void TurnOnDisplayPanels()
    {
        foreach (GameObject a in _DisplayPanels)
        {
            a.SetActive(true);
        }
    }                   // Turn On display panels. Used mainly for start of battle.
    private void OpenTargetList()
    {
        _InputPanels[0].SetActive(true);
    }                        // Open Targetting list (Enemies / Heroes).

    #endregion
    #region Finish Battle Messaging
    private void EndOfFightMessaging(bool win)
    {
        t += Time.deltaTime;
        MessageOnScreen(win? "Victory!" : "Game Over...");

        if(Input.GetButtonDown("Submit"))
        {
            if(win && t > 1f)
            {
                endOfFightTransition++;
                switch (endOfFightTransition)
                {
                    case 2:   // Bring up the EXP Screen for fighting members
                        for (int i = 0; i < _BM._PartyMembersInBattle.Count; i++)
                        {
                            finishMessageBox.SetActive(true);
                            finishHeroPanel[i].GetComponentInChildren<TextMeshProUGUI>().text = _BM._PartyMembersInBattle[i].CharacterName;
                        }
                        break;
                    case 3:  // Tally the Experience and Gold
                        goldBox.SetActive(true);
                        xpBox.SetActive(true);
                        goldBox.GetComponentInChildren<TextMeshProUGUI>().text = _BM.goldPool.ToString() + "G";
                        xpBox.GetComponentInChildren<TextMeshProUGUI>().text = _BM.expPool.ToString() + "EXP";
                        foreach (BasePartyMember a in _BM._ActivePartyMembers)
                        {
                            a.currentXP += _BM.expPool / _BM._ActivePartyMembers.Count;
                            Debug.Log(a.CharacterName + "Has gained " + (int)(_BM.expPool / _BM._ActivePartyMembers.Count) + " XP!");
                            a.NextLevel();
                        }
                        break;
                    case 4:  // Resume Game
                        endOfFightTransition = 0;
                        SceneManager.LoadScene(_BM._gameManager.currentScene);
                        break;
                }
                t = 0;
            }        // Divy up EXP, Gold, Item Drops.
            else if(!win && t > 2f)
            {
                SceneManager.LoadScene("Title Screen");
            }  // Transition to the Title Screen
        }
    }
    #endregion

    public void BattleCommand(ActionInfo action, BaseStats target)
    {
        chosenHero.UseAction(action, target);
    }
    public void MessageOnScreen(string text)
    {
        messageBox.SetActive(true);
        messageText.text = text;
        
    }
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
        if (scene.name == "Battle Scene")
        {
            finishMessageBox.SetActive(false);
            goldBox.SetActive(false);
            xpBox.SetActive(false);
            SetOffAllInputPanels();
            TurnOnDisplayPanels();
            startOfGameSelected.GetComponent<Button>().Select();
        }
    }
    #endregion
}
