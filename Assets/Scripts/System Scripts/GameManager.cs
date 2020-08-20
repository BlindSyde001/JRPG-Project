using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //VARIABLES
    public static GameManager instance;

    [Header("FOR BATTLE")]
    public List<BasePartyMember> partyLineup;      // Stored variable on who is in the party to start the battle
    public List<BaseEnemy> enemyLineup;            // Stored variable on which enemy is in the battle
    public List<bool> positionFront;               // For positions of party members
    public List<BasePartyMember> _AllPartyMembers; // Stored variables of all Playable Characters

    [Header("DATABASE")]
    public List<Spells> _SpellsPool;               // All spells in the Game for the player

    [Header("Stored Data")]
    public string currentScene;                    // The current Scene the game is in.
    public string currentBattleSceneBackground;    // Stored background for battle scene per area.
    public Vector3 lastKnownPosition;              // To spawn player character after battle has occured
    public Quaternion lastKnownRotation;           // To spawn player character after battle has occured
    public GameObject playerCharacter;             // Moving model for open world

    private GameObject _EnemyInfo;                 // Gameobject with enemy scripts for battle scene
    public float _InGameTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else if(instance != null)
        {
            Destroy(this.gameObject);
        }
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
        if (scene.name != "Battle Scene")
        {
            // create new player object at last position
            if(lastKnownPosition != new Vector3(0, 0, 0) && lastKnownRotation != new Quaternion(0, 0, 0, 0))
            {
                Destroy(FindObjectOfType<Movement>().gameObject);                   // Get rid of any that comes with scene
                Instantiate(playerCharacter, lastKnownPosition, lastKnownRotation); // respawn player in same spot
                lastKnownPosition = new Vector3(0, 0, 0);
                lastKnownRotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    #endregion
}
