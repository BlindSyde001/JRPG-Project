using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Wolf : BaseEnemy
{
    private void Start()
    {
        CharacterName = "Wolf";
        _ActionBarRechargeAmount = 10;

        level = 10;
        totalXP = 25;

        maxHP = 300;
        maxMP = 50;
        currentHP = 300;
        currentMP = 50;
    }
}
