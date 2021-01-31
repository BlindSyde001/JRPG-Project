using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Officer : BasePartyMember
{
    private void Start()
    {
        Stats();
        NextLevel();
    }
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
        List<EquipmentInfo> tempEquip = new List<EquipmentInfo>();
        tempEquip.Add(Weapon);
        if (Armour != null)
            tempEquip.Add(Armour);
        if (AccessoryOne != null)
            tempEquip.Add(AccessoryOne);
        if (AccessoryTwo != null)
            tempEquip.Add(AccessoryTwo);

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
        vitality = (int)(thisChara.baseVit * (1 + growthRateAverage) * level);
        spirit = (int)(thisChara.baseSpr * (1 + growthRateAverage) * level);

        speed = (int)(thisChara.baseSpd * (1 + growthRateWeak) * level);
        luck = (int)(thisChara.baseLck * (1 + growthRateAverage) * level);

        maxHP = (int)(thisChara.baseHP * (1 + growthRateHyper) * level) + (strength / 2 * level) + (vitality / 4 * level);
        maxMP = (int)(thisChara.baseMP * (1 + growthRateWeak) * level) + (mind) + (spirit / 4 * level);
    }

    private void LimitBreak()
    {
        // Deadeye
        // Executes low HP enemies, and deals heightened critical damage to those it doesn't execute
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
                // Krak Grenades
                break;
            case 10:
                // Piercing Shot
                break;
        }
    }

    #region Unique Abilities
    private void KrakGrenades()
    {
        // AOE damage on enemies
    }
    private void PiercingShot()
    {
        // damage and reduce armor of target enemy
    }
    private void BulletTime()
    {
        // Spamming shots at a target
    }
    #endregion
}
