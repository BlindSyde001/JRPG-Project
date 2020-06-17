using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Bandit : BaseEnemy
{
    private void Start()
    {
        CharacterName = "Bandit";
        _ActionBarRechargeAmount = 10;

        level = 10;
        totalXP = 30;

        maxHP = 300;
        maxMP = 50;
        currentHP = 300;
        currentMP = 50;
    }
}
