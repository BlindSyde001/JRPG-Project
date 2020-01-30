using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //VARIABLES
    #region Reference Variables
    private GameManager _gameManager;
    #endregion
    #region Positions
    public List<GameObject> heroPos;
    public List<GameObject> enemyPos;
    #endregion
    public GameObject capsule;

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
    }

    //METHODS
    private void SpawnCharacters()
    {
        //instantiate selected heroes in party lineup
        for(int i = 0; i < _gameManager.partyLineup.Count; i++)
        {
            if(_gameManager.partyLineup[i] != "")
            {
                Debug.Log(_gameManager.partyLineup[i] + " " + heroPos[i]);
                Instantiate(Resources.Load(_gameManager.partyLineup[i]),      //HERO
                    new Vector3(heroPos[i].transform.position.x,              //POSITION
                    heroPos[i].transform.position.y + 0.5f, 
                    heroPos[i].transform.position.z), 
                    heroPos[i].transform.rotation);                           //ROTATION
            }
        }

        for(int i = 0; i < _gameManager.enemyLineup.Count; i++)
        {
            if(_gameManager.enemyLineup[i] != "")
            {
                Debug.Log(_gameManager.enemyLineup[i] + " " + enemyPos[i]);
                Instantiate(Resources.Load(_gameManager.enemyLineup[i]),     //ENEMY
                    new Vector3(enemyPos[i].transform.position.x,            //POSITION
                    enemyPos[i].transform.position.y + 0.5f, 
                    enemyPos[i].transform.position.z),
                    enemyPos[i].transform.rotation);                         //ROTATION
            }
        }
    }
}
