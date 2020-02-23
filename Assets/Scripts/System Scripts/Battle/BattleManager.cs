using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    //VARIABLES
    #region Reference Variables
    private GameManager _gameManager;
    private BattleUIController _BUI;
    public List<GameObject> _CharacterPanels;      // The UI components showing the stats of party members

    [Header("Reference Variables")]
    public List<PartyMember> _PartyMembersInBattle;   // List of current Party Members who have entered the battle
    public List<Enemy> _EnemiesInBattle;              // List of current Enemies who have entered battle
    [Space]
    public List<PartyMember> _ActivePartyMembers;     // List of Active Members in battle; Targettable
    public List<Enemy> _ActiveEnemies;                // List of Active Enemies in battle; Targettable
    [Space]
    public List<PartyMember> _DownedMembers;          // List of Downed Party Members; Non Targettable except for ressurection
    public List<Enemy> _DownedEnemies;                // List of Downed Enemies; Non Targettable except for ressurection
    #endregion
    #region Positions
    [Header("Positions")]
    public List<GameObject> heroPosFront;
    public List<GameObject> heroPosBack;
    public List<GameObject> enemyPos;
    #endregion

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
            _CharacterPanels[i].transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount 
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
            if (_gameManager.partyLineup[i] != "")                    // Check if there is a party member in that slot
            {                                                        // Check their position in the lineup. Is it front or back
                if (_gameManager.positionFront[i] == true)
                {
                    // Generate Hero Model
                    Instantiate(Resources.Load(_gameManager.partyLineup[i]) as GameObject,
                        new Vector3(heroPosFront[i].transform.position.x,
                        heroPosFront[i].transform.position.y + 0.5f,
                        heroPosFront[i].transform.position.z),
                        heroPosFront[i].transform.rotation);
                }
                else if (_gameManager.positionFront[i] == false)
                {
                    // Generate Hero Model
                    Instantiate(Resources.Load(_gameManager.partyLineup[i]) as GameObject,
                        new Vector3(heroPosBack[i].transform.position.x,
                        heroPosBack[i].transform.position.y + 0.5f,
                        heroPosBack[i].transform.position.z),
                        heroPosBack[i].transform.rotation);
                }
                // Find script with the hero's name and add it to the members in battle list
                for (int j = 0; j < _gameManager._PartyMembers.Count; j++)
                {
                    if (_gameManager._PartyMembers[j].CharacterName == _gameManager.partyLineup[i])
                    {
                        _PartyMembersInBattle.Add(_gameManager._PartyMembers[j]);      // Add to current fighting list

                        // Check status then add to appropriate list
                        if(_gameManager._PartyMembers[j].currentHP == 0)               
                        {
                            _DownedMembers.Add(_gameManager._PartyMembers[j]);
                            break;
                        }
                        else                                                           
                        {
                            _ActivePartyMembers.Add(_gameManager._PartyMembers[j]);
                            break;
                        }
                    }
                }
            }
        }

        // Instantiate Enemies into lineup positions
        for (int i = 0; i < _gameManager.enemyLineup.Count; i++)
        {
            if (_gameManager.enemyLineup[i] != "")
            {
                // Generate Enemy Model
                GameObject instantiatedEnemy = Instantiate(Resources.Load(_gameManager.enemyLineup[i]) as GameObject,
                    new Vector3(enemyPos[i].transform.position.x,
                    enemyPos[i].transform.position.y + 0.5f,
                    enemyPos[i].transform.position.z),
                    enemyPos[i].transform.rotation);

                // Add Enemies to overall List and Active List
                _EnemiesInBattle.Add(instantiatedEnemy.GetComponent<Enemy>());
                _ActiveEnemies.Add(instantiatedEnemy.GetComponent<Enemy>());
            }
        }
    }
    private void InstantiateUI()
    {
        // Set up the UI to represent All the Party Members in the battle Active and Downed
        for (int i = 0; i < _PartyMembersInBattle.Count; i++)                   // Cycle through Party List
        {
            _CharacterPanels[i].SetActive(true); // Turn on UI
            // Character Portrait
            _CharacterPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = _PartyMembersInBattle[i].characterPortrait;
            // Character Name
            _CharacterPanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _PartyMembersInBattle[i].CharacterName;
        }
    }
    private void StartActionBar()
    {
        foreach (PartyMember x in _ActivePartyMembers)
        {
            x._ActionBarAmount = x.agility + x.level;
            x.InitiateATB();
        }
        foreach (Enemy y in _ActiveEnemies)
        {
            y._ActionBarAmount = y.agility + y.level;
            y.InitiateATB();
        }
    }
    #endregion
    #region End of Game States
    public void VictoryState()
    {
        _BUI.MessageOnScreen("Victory!");
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
            _CharacterPanels[i].transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>().text =
                _PartyMembersInBattle[i].currentHP.ToString() + " / " + _PartyMembersInBattle[i].maxHP.ToString();

            // HP Gauge
            _CharacterPanels[i].transform.GetChild(3).GetChild(1).GetComponent<Image>().fillAmount =
              (float)_PartyMembersInBattle[i].currentHP / _PartyMembersInBattle[i].maxHP;
            #endregion
            #region MP UI
            // MP
            _CharacterPanels[i].transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text =
                _PartyMembersInBattle[i].currentMP.ToString() + " / " + _PartyMembersInBattle[i].maxMP.ToString();

            // MP Gauge
            _CharacterPanels[i].transform.GetChild(4).GetChild(1).GetComponent<Image>().fillAmount =
                (float)_PartyMembersInBattle[i].currentMP / _PartyMembersInBattle[i].maxMP;
            #endregion

            // Limit Gauge
            _CharacterPanels[i].transform.GetChild(6).GetChild(1).GetComponent<Image>().fillAmount
                = (float)_PartyMembersInBattle[i].currentLimit / 100;
        }
    }
}
