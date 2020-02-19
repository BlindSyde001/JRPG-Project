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

    //METHODS
    public override void Die()
    {
        base.Die();
    }
}
