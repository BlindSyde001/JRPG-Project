using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMember : BaseStats
{
    //VARIABLES

    // What will be displayed in the Battle UI
    #region In Battle UI Variables 
    public Sprite characterPortrait;        // Portrait image displayed in UI
    public int currentLimit;               // Limit amount of charge for special move
    private int maxLimit;                  // Amount needed to use Limit. Will be 1 and any charge variable is normalized
    #endregion

    //METHODS

    public void Defend()                      // Increase in defence for a turn
    {
        vitality += (int)(vitality * .4f);
        Debug.Log("Increased Defence");
    }
    public override void Die()
    {
        base.Die();
    }
}
