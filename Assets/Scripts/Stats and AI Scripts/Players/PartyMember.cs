using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMember : BaseStats
{
    //VARIABLES
    #region In Battle UI Variables 
    public Sprite characterPortrait;                   // Portrait image displayed in UI
    public int currentLimit;                           // Limit amount of charge for special move
    #endregion
    public bool isAlive = true;                        // Check to see if player has not died in battle

    //UPDATES
    new void Update()
    {
        if(isAlive)
        base.Update();
    }

    //METHODS
    public override void Die()
    {
        isAlive = false;
        _BM._ActivePartyMembers.Remove(this);
        _BM._DownedMembers.Add(this);
        _BM.UpdatePartyAliveStatus();
    }
}
