using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Templar : BasePartyMember
{
    //private void Start()
    //{
    //    EquipStats();
    //    NextLevel();
    //}
    private void EquipStats()
    {
        #region Reset Equip Stats
        equipAttackPower = 0;
        equipMagAttackPower = 0;
        equipDefense = 0;
        equipMagDefense = 0;

        equipStrength = 0;
        equipMind = 0;
        equipVitality = 0;
        equipSpirit = 0;

        equipSpeed = 0;
        equipLuck = 0;

        equipHP = 0;
        equipMP = 0;
        #endregion
        #region Add Equipped Items to Temporary List
        List<EquipmentInfo> tempEquip = new List<EquipmentInfo>();
        tempEquip.Add(Weapon);
        if (Armour != null)
            tempEquip.Add(Armour);
        if(AccessoryOne != null)
            tempEquip.Add(AccessoryOne);
        if (AccessoryTwo != null)
            tempEquip.Add(AccessoryTwo);
        #endregion

        foreach (EquipmentInfo currentEquip in tempEquip)
        {
            equipAttackPower += currentEquip.attackPower;
            equipMagAttackPower += currentEquip.magAttackPower;
            equipDefense += currentEquip.defense;
            equipMagDefense += currentEquip.magDefense;

            equipStrength += currentEquip.strength;
            equipMind += currentEquip.mind;
            equipVitality += currentEquip.vitality;
            equipSpirit += currentEquip.spirit;

            equipSpeed += currentEquip.speed;
            equipLuck += currentEquip.luck;

            equipHP += currentEquip.hP;
            equipMP += currentEquip.mP;
        }
        TotalStats();
    }
    private void TotalStats()
    {
        CharacterName = thisChara.ID;
        level = thisChara.startingLevel;
        currentXP = thisChara.startingXP;

        attackPower = (int)thisChara.baseAtkPwr + equipAttackPower;
        magAttackPower = (int)thisChara.baseMagAtkPwr + equipMagAttackPower;
        defense = (int)thisChara.baseDef + equipDefense;
        magDefense = (int)thisChara.baseMagDef + equipMagDefense;

        strength = (int)(thisChara.baseStr * (1 + growthRateStrong) * level) + equipStrength;
        mind = (int)(thisChara.baseMnd * (1 + growthRateWeak) * level) + equipMind;
        vitality = (int)(thisChara.baseVit * (1 + growthRateStrong) * level) + equipVitality;
        spirit = (int)(thisChara.baseSpr * (1 + growthRateStrong) * level) + equipSpirit;

        speed = (int)(thisChara.baseSpd * (1 + growthRateWeak) * level) + equipSpeed;
        luck = (int)(thisChara.baseLck * (1 + growthRateAverage) * level) + equipLuck;

        maxHP = (int)(thisChara.baseHP * (1 + growthRateHyper) * level) + (strength / 2 * level) + (vitality / 4 * level) + equipHP;
        maxMP = (int)(thisChara.baseMP * (1 + growthRateWeak) * level) + (mind) + (spirit / 4 * level) + equipMP;
    }

    private void LimitBreak()
    {

    }
    public override void NextLevel()
    {
        base.NextLevel();
        GainNewAbility();
    }
    private void GainNewAbility()
    {
        switch (level)
        {
            case 7:

                break;
        }
    }         // Unlock New Abilities as character levels up
}
