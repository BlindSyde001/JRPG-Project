using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Templar : BasePartyMember
{
    private void Start()
    {
        Stats();
        NextLevel();
    }
    private void Stats()
    {
        CharacterName = thisChara.ID;
        level = thisChara.level;
        totalXP = thisChara.totalXP;

        attackPower = (int)thisChara.baseAtkPwr;
        magAttackPower = (int)thisChara.baseMagAtkPwr;
        defense = (int)thisChara.baseDef;
        magDefense = (int)thisChara.baseMagDef;

        strength = (int)(thisChara.baseStr * (1 + growthRateStrong) * level);
        mind = (int)(thisChara.baseMnd * (1 + growthRateWeak) * level);
        vitality = (int)(thisChara.baseVit * (1 + growthRateStrong) * level);
        spirit = (int)(thisChara.baseSpr * (1 + growthRateStrong) * level);

        speed = (int)(thisChara.baseSpd * (1 + growthRateWeak) * level);
        luck = (int)(thisChara.baseLck * (1 + growthRateAverage) * level);

        maxHP = (int)(thisChara.baseHP * (1 + growthRateHyper) * level) + (strength / 2 * level) + (vitality / 4 * level);
        maxMP = (int)(thisChara.baseMP * (1 + growthRateWeak) * level) + (mind) + (spirit / 4 * level);
    }
    private void LimitBreak()
    {

    }
}
