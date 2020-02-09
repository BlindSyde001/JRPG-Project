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
    public List<GameObject> _CharacterPanel;
    #endregion
    #region Positions
    public List<GameObject> heroPos;
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
    private void SpawnCharacters()
    {
        // Instantiate selected heroes in party lineup
        for(int i = 0; i < _gameManager.partyLineup.Count; i++)      // Cycle through party member list
        {
            if(_gameManager.partyLineup[i] != "")                    // Check if there is a party member in that slot
            {
                if(_gameManager.positionFront[i] == true)
                {
                    Instantiate(Resources.Load(_gameManager.partyLineup[i + (i - 1)]),    //HERO
                        new Vector3(heroPos[i + (i - 1)].transform.position.x,            //POSITION
                        heroPos[i + (i - 1)].transform.position.y + 0.5f,
                        heroPos[i + (i - 1)].transform.position.z),
                        heroPos[i + (i - 1)].transform.rotation);                         //ROTATION
                }
                else if(_gameManager.positionFront[i] == false)
                {
                    Instantiate(Resources.Load(_gameManager.partyLineup[2 * i]),      //HERO
                        new Vector3(heroPos[2 * i].transform.position.x,              //POSITION
                        heroPos[2 * i].transform.position.y + 0.5f,
                        heroPos[2 * i].transform.position.z),
                        heroPos[2 * i].transform.rotation);                           //ROTATION
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
        for(int i = 0; i < _gameManager.partyLineup.Count; i++)
        {
            if(_gameManager.partyLineup[i] != "")
            {

            }
        }
    }
}
