using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInformation : MonoBehaviour
{
    private GameManager _GM;

    public string sceneName;                  // The name of the scene
    public string battleSceneBackground;      // What to use as backdrop for battle scene
    public List<BaseEnemy> enemiesInScene;    // The list of enemies to be used by GM for battle

    public int stageEncounterRate;            // Used as base encounter rate of enemies
    public int enemyEncounterInt;             // Used to see how enemies spawn for battle, refreshed when entering scene
    private int encounterType;                // Different arrangements of enemies for the encounter
    private float t;                          // Artificial tick rate timer
    private bool lockout;                     // Only one instance of enemy encounter loading

    private void Start()
    {
        _GM = FindObjectOfType<GameManager>();
        _GM.currentScene = sceneName;
        _GM.currentBattleSceneBackground = battleSceneBackground;

        enemyEncounterInt = Random.Range(50, 200);
    }
    private void FixedUpdate()
    {
        // If you're moving
        if(FindObjectOfType<Movement>().input.sqrMagnitude > 0)
        {
            // Tick rate, used to indicate per step
            t += Time.deltaTime;
            if(t > .25f)
            {
                enemyEncounterInt -= 5;
                t = 0;
            }
            if(enemyEncounterInt < stageEncounterRate && !lockout)
            {
                EnemyEncounter();
                lockout = !lockout;
            }
        }
        else
        {
            // Reset tick rate
            t = 0;
        }
    }

    private void EnemyEncounter()
    {
        encounterType = Random.Range(5, 5);
        switch(encounterType)
        {
            case 5:
                _GM.enemyLineup.AddRange(enemiesInScene);
                break;

            case 4:
                break;

            case 3:
                break;

            case 2:
                break;

            case 1:
                break;
        }
        _GM.lastKnownPosition = FindObjectOfType<Movement>().transform.position;
        _GM.lastKnownRotation = FindObjectOfType<Movement>().transform.rotation;
        SceneManager.LoadScene("Battle Scene");
    }
}
