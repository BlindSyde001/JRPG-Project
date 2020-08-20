using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    //VARIABLES
    #region Reference Variables
    private BattleUIController _BUI;
    public GameManager _gameManager;
    public List<GameObject> _CharacterPanels;             // The UI components showing the stats of party members

    [Header("Reference Variables")]
    public List<BasePartyMember> _PartyMembersInBattle;   // List of current Party Members who have entered the battle
    public List<BaseEnemy> _EnemiesInBattle;              // List of current Enemies who have entered battle
    [Space]
    public List<BasePartyMember> _ActivePartyMembers;     // List of Active Members in battle; Targettable
    public List<BaseEnemy> _ActiveEnemies;                // List of Active Enemies in battle; Targettable
    [Space]
    public List<BasePartyMember> _DownedMembers;          // List of Downed Party Members; Non Targettable except for ressurection
    public List<BaseEnemy> _DownedEnemies;                // List of Downed Enemies; Non Targettable except for ressurection
    [Space]
    public List<GameObject> _PartyMemberModels;           // In game character model
    public List<GameObject> _EnemyModels;                 // In game character model
    #endregion
    #region Positions
    [Header("Positions")]
    public List<GameObject> heroPosFront;
    public List<GameObject> heroPosBack;
    public List<GameObject> enemyPos;
    #endregion
    public int expPool;     // Total XP gained from defeating enemies, to be split between party members

    //UPDATES
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _BUI = FindObjectOfType<BattleUIController>();
    }
    private void Start()
    {
        if (_gameManager == null)
            return;
        SpawnCharacters();
        InstantiateUI();
        StartActionBar();
        UpdatePartyVariables();
    }
    private void Update()
    {
        for (int i = 0; i < _PartyMembersInBattle.Count; i++)
        {
            // ATB Gauge
            _CharacterPanels[i].transform.Find("ATB Bar").Find("ATB Colour").GetComponent<Image>().fillAmount 
                = _PartyMembersInBattle[i].ActionBarNormalized();
        }
    }

    //METHODS
    #region Start Of Game
    private void SpawnCharacters()
    {
        // Instantiate selected heroes in party lineup
        for (int i = 0; i < _gameManager.partyLineup.Count; i++)      // Cycle through party member list
        {
            if (_gameManager.partyLineup[i] != null)                  // Check if there is a party member in that slot
            {                                                         // Check their position in the lineup. Is it front or back
                if (_gameManager.positionFront[i] == true)
                {
                    // Generate Hero Model
                    GameObject instantiatedHero = Instantiate((_gameManager.partyLineup[i].characterModel) as GameObject,
                        new Vector3(heroPosFront[i].transform.position.x,
                        heroPosFront[i].transform.position.y + 0.5f,
                        heroPosFront[i].transform.position.z),
                        heroPosFront[i].transform.rotation);
                    _PartyMemberModels.Add(instantiatedHero); // Add character model
                }
                else if (_gameManager.positionFront[i] == false)
                {
                    // Generate Hero Model
                    GameObject instantiatedHero = Instantiate((_gameManager.partyLineup[i].characterModel) as GameObject,
                        new Vector3(heroPosBack[i].transform.position.x,
                        heroPosBack[i].transform.position.y + 0.5f,
                        heroPosBack[i].transform.position.z),
                        heroPosBack[i].transform.rotation);
                    _PartyMemberModels.Add(instantiatedHero);
                }
                // Find script with the hero's name and add it to the members in battle list
                for (int j = 0; j < _gameManager._AllPartyMembers.Count; j++)
                {
                    if (_gameManager._AllPartyMembers[j].thisChara.ID == _gameManager.partyLineup[i].CharacterName)
                    {
                        _PartyMembersInBattle.Add(_gameManager._AllPartyMembers[j]);      // Add to current fighting list

                        // Check status then add to appropriate list
                        if(_gameManager._AllPartyMembers[j].currentHP == 0)               
                        {
                            _DownedMembers.Add(_gameManager._AllPartyMembers[j]);
                            break;
                        }
                        else                                                           
                        {
                            _ActivePartyMembers.Add(_gameManager._AllPartyMembers[j]);
                            break;
                        }
                    }
                }
            }
        }

        // Instantiate Enemies into lineup positions
        for (int i = 0; i < _gameManager.enemyLineup.Count; i++)
        {
            if (_gameManager.enemyLineup[i] != null)
            {
                // Generate Enemy Model
                GameObject instantiatedEnemy = Instantiate((_gameManager.enemyLineup[i].characterModel) as GameObject,
                    new Vector3(enemyPos[i].transform.position.x,
                    enemyPos[i].transform.position.y + 0.5f,
                    enemyPos[i].transform.position.z),
                    enemyPos[i].transform.rotation);

                // Add Enemies to overall List and Active List
                _EnemiesInBattle.Add(_gameManager.enemyLineup[i]);
                _ActiveEnemies.Add(_gameManager.enemyLineup[i]);
                _EnemyModels.Add(instantiatedEnemy); // Add enemy model
            }
        }
    }
    private void InstantiateUI()
    {
        // Set up the UI to represent All the Party Members in the battle Active and Downed
        for (int i = 0; i < _PartyMembersInBattle.Count; i++)                   // Cycle through Party List
        {
            _PartyMembersInBattle[i]._DPSSpawnPoint =
            _PartyMemberModels[i].transform.GetChild(0).gameObject;
            // Turn on UI
            _CharacterPanels[i].SetActive(true);
            // Character Portrait
            _CharacterPanels[i].transform.Find("Hero Image").Find("Mask").Find("Graphic").GetComponent<Image>().sprite = 
                _PartyMembersInBattle[i].characterPortrait;
            // Character Name
            _CharacterPanels[i].transform.Find("Hero Name").GetComponent<TextMeshProUGUI>().text = 
                _PartyMembersInBattle[i].CharacterName;
        }
        for(int i = 0; i <_EnemyModels.Count; i++)
        {
            _EnemiesInBattle[i]._DPSSpawnPoint =
                _EnemyModels[i].transform.GetChild(0).gameObject;
            // Attach and reset damage display
        }
    }
    private void StartActionBar()
    {
        foreach (BasePartyMember x in _ActivePartyMembers)
        {
            x._ActionBarAmount = x.speed + x.level;
            x.InitiateATB();
        }
        foreach (BaseEnemy y in _ActiveEnemies)
        {
            y._ActionBarAmount = y.speed + y.level;
            y.InitiateATB();
        }
    }
    #endregion
    #region End of Game States
    public void VictoryState()
    {
        _BUI.endOfFight = true;
        Destroy(FindObjectOfType<EnemyInfoScript>().gameObject);
    }
    public void GameOverState()
    {
        _BUI.MessageOnScreen("Game Over!");
    }
    #endregion
    public void UpdatePartyAliveStatus()
    {
        if(_DownedMembers.Count == _PartyMembersInBattle.Count)  // If all the party members have been downed
            GameOverState();
    }
    public void UpdatePartyVariables()
    {
        for (int i = 0; i < _PartyMembersInBattle.Count; i++)
        {
            #region HP UI
            // HP
            _CharacterPanels[i].transform.Find("HP Bar").Find("Current HP Value").GetComponent<TextMeshProUGUI>().text =
                _PartyMembersInBattle[i].currentHP.ToString();
            _CharacterPanels[i].transform.Find("HP Bar").Find("Max HP Value").GetComponent<TextMeshProUGUI>().text = 
               "/" + _PartyMembersInBattle[i].maxHP.ToString();

            // HP Gauge
            _CharacterPanels[i].transform.Find("HP Bar").Find("HP Colour").GetComponent<Image>().fillAmount =
              (float)_PartyMembersInBattle[i].currentHP / _PartyMembersInBattle[i].maxHP;
            #endregion
            #region MP UI
            // MP
            _CharacterPanels[i].transform.Find("MP Bar").Find("MP Value").GetComponent<TextMeshProUGUI>().text =
                _PartyMembersInBattle[i].currentMP.ToString();

            // MP Gauge
            _CharacterPanels[i].transform.Find("MP Bar").Find("MP Colour").GetComponent<Image>().fillAmount =
                (float)_PartyMembersInBattle[i].currentMP / _PartyMembersInBattle[i].maxMP;
            #endregion

            // Limit Gauge
            _CharacterPanels[i].transform.Find("Limit Bar").Find("Limit Colour").GetComponent<Image>().fillAmount
                = (float)_PartyMembersInBattle[i].currentLimit / 100;
        }
    }
}
