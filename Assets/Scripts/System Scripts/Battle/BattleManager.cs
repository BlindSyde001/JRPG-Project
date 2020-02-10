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
    public List<GameObject> _CharacterPanels;
    public List<GameObject> _Members;
    #endregion
    #region Positions
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
    }

    //METHODS
    #region Start Of Game
    private void SpawnCharacters()
    {
        // Instantiate selected heroes in party lineup
        for(int i = 0; i < _gameManager.partyLineup.Count; i++)      // Cycle through party member list
        {
            if(_gameManager.partyLineup[i] != "")                    // Check if there is a party member in that slot
            {   
                if(_gameManager.positionFront[i] == true)
                {
                    GameObject _Hero = Instantiate(Resources.Load(_gameManager.partyLineup[i]) as GameObject,      //HERO
                        new Vector3(heroPosFront[i].transform.position.x,         //POSITION
                        heroPosFront[i].transform.position.y + 0.5f,
                        heroPosFront[i].transform.position.z),
                        heroPosFront[i].transform.rotation);                      //ROTATION
                    _Members.Add(_Hero);
                }
                else if(_gameManager.positionFront[i] == false)
                {
                    GameObject _Hero = Instantiate(Resources.Load(_gameManager.partyLineup[i]) as GameObject,      //HERO
                        new Vector3(heroPosBack[i].transform.position.x,          //POSITION
                        heroPosBack[i].transform.position.y + 0.5f,
                        heroPosBack[i].transform.position.z),
                        heroPosBack[i].transform.rotation);                       //ROTATION
                    _Members.Add(_Hero);
                }
            }
        }

        // Instantiate Enemies into lineup positions
        for(int i = 0; i < _gameManager.enemyLineup.Count; i++)
        {
            if(_gameManager.enemyLineup[i] != "")
            {
                Instantiate(Resources.Load(_gameManager.enemyLineup[i]),     //ENEMY
                    new Vector3(enemyPos[i].transform.position.x,            //POSITION
                    enemyPos[i].transform.position.y + 0.5f, 
                    enemyPos[i].transform.position.z),
                    enemyPos[i].transform.rotation);                         //ROTATION
            }
        }
    }
    private void InstantiateUI()
    {
        for(int i = 0; i < _gameManager.partyLineup.Count; i++)         // Cycle trhough Party List
        {
            if(_gameManager.partyLineup[i] != "")                       // If there is a party member
            {
                // Set Name CharacterPanels[i].Find("Hero Image").Sprite = 
                // Set Image
                // Set HP UI text and visual
                // Set MP Ui Text and visual
                // Set Limit visual
            }
        }
    }
    #endregion
}
