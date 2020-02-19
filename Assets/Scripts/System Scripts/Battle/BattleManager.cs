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
    [Header("Reference Variables")]
    public List<GameObject> _CharacterPanels;      // The UI components showing the stats of party members
    public List<PartyMember> _MembersInBattle;     // Stats scripts of whoever is currently in the battle
    public List<Enemy> _EnemiesInBattle;           // Stats Scripts of The enemies currently in the battle
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
    }
    private void Start()
    {
        if (_gameManager == null)
            return;
        SpawnCharacters();
        InstantiateUI();
        StartActionBar();
    }
    private void Update()
    {
        for (int i = 0; i < _MembersInBattle.Count; i++)
        {
            #region HP UI
            // HP
            _CharacterPanels[i].transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>().text =
                _MembersInBattle[i].currentHP.ToString() + " / " + _MembersInBattle[i].maxHP.ToString();

            // HP Gauge
            _CharacterPanels[i].transform.GetChild(3).GetChild(1).GetComponent<Image>().fillAmount =
              (float)_MembersInBattle[i].currentHP / _MembersInBattle[i].maxHP;
            #endregion
            #region MP UI
            // MP
            _CharacterPanels[i].transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text =
                _MembersInBattle[i].currentMP.ToString() + " / " + _MembersInBattle[i].maxMP.ToString();

            // MP Gauge
            _CharacterPanels[i].transform.GetChild(4).GetChild(1).GetComponent<Image>().fillAmount =
                (float)_MembersInBattle[i].currentMP / _MembersInBattle[i].maxMP;
            #endregion
            #region Limit & ATB UI
            // ATB Gauge
            _CharacterPanels[i].transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount = _MembersInBattle[i].ActionBarNormalized();

            // Limit Gauge
            _CharacterPanels[i].transform.GetChild(6).GetChild(1).GetComponent<Image>().fillAmount
                = (float)_MembersInBattle[i].currentLimit / 100;
            #endregion
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
                        _MembersInBattle.Add(_gameManager._PartyMembers[j]);
                        //print(_gameManager._PartyMembers[j].CharacterName + " added!");
                        break;
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

                // Add Enemies to active battle list
                _EnemiesInBattle.Add(instantiatedEnemy.GetComponent<Enemy>());
                //print(_gameManager.enemyLineup[i] + " added!");
            }
        }
    }
    private void InstantiateUI()
    {
        // Set up the UI to represent the Active Party Members in the battle
        for (int i = 0; i < _MembersInBattle.Count; i++)                   // Cycle through Party List
        {
            _CharacterPanels[i].SetActive(true); // Turn on UI
            // Character Portrait
            _CharacterPanels[i].transform.GetChild(1).GetComponent<Image>().sprite = _MembersInBattle[i].characterPortrait;
            // Character Name
            _CharacterPanels[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _MembersInBattle[i].CharacterName;
        }
    }
    private void StartActionBar()
    {
        foreach (PartyMember x in _MembersInBattle)
        {
            x._ActionBarAmount = x.agility + x.level;
            x.InitiateATB();
        }
        foreach (Enemy y in _EnemiesInBattle)
        {
            y._ActionBarAmount = y.agility + y.level;
            y.InitiateATB();
        }
    }
    #endregion
    #region Update UI from Damage/Heal
    public void HPUpdate(int damageAmount)
    {
        // if currentHP != dmg amount
        // float b = currenthp
        // b -= time.deltatime
        // currenthp = (int)b
        //panelui = currenthp.tostring
    }
    #endregion
}
