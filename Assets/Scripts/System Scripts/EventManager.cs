using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    //EVENTS

    // When using load functions to affect data
    public delegate void LoadAction();
    public static event LoadAction StartedGame;
    public static event LoadAction LoadedGame;

    // When events occur during the battle phase of game
    public delegate void BattleAction();
    public static event BattleAction BattleStarted;
    public static event BattleAction BattleEnded;

    #region Event Subscriptions
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title Screen")
            StartedGame();
        if (scene.name == "Battle Screen")
            BattleStarted();
    }
    #endregion
    #region OnEnable/Disable
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
    #endregion
}
