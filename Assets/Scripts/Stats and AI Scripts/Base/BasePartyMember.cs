using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasePartyMember : BaseStats
{
    //VARIABLES
    public bool isAlive = true;                        // Check to see if player has not died in battle
    public StatsDataAsset thisChara;                   // The data asset containing information on the character
    public Sprite characterPortrait;                   // Portrait image displayed in UI
    public int currentLimit;                           // Limit amount of charge for special move
    #region Growth Stat Base
    [HideInInspector]
    public float growthRateHyper = 0.5f;                      // Assigned to the Hero's Strongest stat
    [HideInInspector]
    public float growthRateStrong = 0.3f;                     // Assigned to the Hero's Secondary stat
    [HideInInspector]
    public float growthRateAverage = 0.2f;                    // Assigned to the Hero's Averaging stat
    [HideInInspector]
    public float growthRateWeak = 0.1f;                       // Assigned to the Hero's Weakest stat
    #endregion

    //UPDATES
    new void Update()
    {
        if(isAlive)
        base.Update();
    }

    //METHODS
    public void NextLevel()
    {
        nextLevelXP = (int)(15 * Mathf.Pow(level, 2.3f) + (15 * level));
        if (totalXP >= nextLevelXP)
        {
            level++;
        }
    }
    public override void Die()
    {
        isAlive = false;
        _BM._ActivePartyMembers.Remove(this);
        _BM._DownedMembers.Add(this);
        _BM.UpdatePartyAliveStatus();
    }
}
