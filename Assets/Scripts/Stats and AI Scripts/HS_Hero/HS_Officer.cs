using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Officer : BasePartyMember
{
    #region Affecting Stats
    private void EquipStats()
    {
        // Reset stats added from equipment to zero
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
        #region Add Equipped Items to a Temporary List
        List<EquipmentInfo> tempEquip = new List<EquipmentInfo>();
        tempEquip.Add(Weapon);
        if (Armour != null)
            tempEquip.Add(Armour);
        if (AccessoryOne != null)
            tempEquip.Add(AccessoryOne);
        if (AccessoryTwo != null)
            tempEquip.Add(AccessoryTwo);
        #endregion
        // Add new stats to the characters from equipped items
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
    private void TotalStats()
    {
        attackPower = (int)thisChara.baseAtkPwr + equipAttackPower;
        magAttackPower = (int)thisChara.baseMagAtkPwr + equipMagAttackPower;
        defense = (int)thisChara.baseDef + equipDefense;
        magDefense = (int)thisChara.baseMagDef + equipMagDefense;

        strength = (int)(thisChara.baseStr * (1 + growthRateStrong) * level) + equipStrength;
        mind = (int)(thisChara.baseMnd * (1 + growthRateWeak) * level) + equipMind;
        vitality = (int)(thisChara.baseVit * (1 + growthRateAverage) * level) + equipVitality;
        spirit = (int)(thisChara.baseSpr * (1 + growthRateAverage) * level) + equipSpirit;

        speed = (int)(thisChara.baseSpd * (1 + growthRateWeak) * level) + equipSpeed;
        luck = (int)(thisChara.baseLck * (1 + growthRateAverage) * level) + equipLuck;

        maxHP = (int)(thisChara.baseHP * (1 + growthRateHyper) * level) + (strength / 2 * level) + (vitality / 4 * level) + equipHP;
        maxMP = (int)(thisChara.baseMP * (1 + growthRateWeak) * level) + (mind) + (spirit / 4 * level) + equipMP;
    }
    public override void NextLevel()
    {
        base.NextLevel();
        GainNewAbility();
    }


    public override void StartOfGameStats()
    {
        EquipStats();

        CharacterName = thisChara.ID;
        level = thisChara.startingLevel;
        currentXP = thisChara.startingXP;

        attackPower = (int)thisChara.baseAtkPwr + equipAttackPower;
        magAttackPower = (int)thisChara.baseMagAtkPwr + equipMagAttackPower;
        defense = (int)thisChara.baseDef + equipDefense;
        magDefense = (int)thisChara.baseMagDef + equipMagDefense;

        strength = (int)(thisChara.baseStr * (1 + growthRateStrong) * level) + equipStrength;
        mind = (int)(thisChara.baseMnd * (1 + growthRateWeak) * level) + equipMind;
        vitality = (int)(thisChara.baseVit * (1 + growthRateAverage) * level) + equipVitality;
        spirit = (int)(thisChara.baseSpr * (1 + growthRateAverage) * level) + equipSpirit;

        speed = (int)(thisChara.baseSpd * (1 + growthRateWeak) * level) + equipSpeed;
        luck = (int)(thisChara.baseLck * (1 + growthRateAverage) * level) + equipLuck;

        maxHP = (int)(thisChara.baseHP * (1 + growthRateHyper) * level) + (strength / 2 * level) + (vitality / 4 * level) + equipHP;
        maxMP = (int)(thisChara.baseMP * (1 + growthRateWeak) * level) + (mind) + (spirit / 4 * level) + equipMP;
    }
    #endregion
    #region Ability Gain
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
    #endregion
    #region Unique Abilities
    private void LimitBreak()
    {
        // Deadeye
        // Executes low HP enemies, and deals heightened critical damage to those it doesn't execute
    }


    private void KrakGrenades()
    {
            bool crit;
            if (Random.Range(0, 101) < luck)
                crit = true;
            else
                crit = false;
        // AOE damage on enemies equal to 80% of basic attack. Pierces defenses
        foreach(BaseEnemy enemy in _BM._ActiveEnemies)
        {
            int damage = (int)(attackPower * Random.Range(1f, 1.5f) * (crit ? 1.75 : 1));
            TakeDamage(damage, false, crit, ActionElement.None, true);
        }
    }
    private void PiercingShot()
    {
        // Damage and reduces target's defense by 20%

    }
    private void RapidFire()
    {
        // Spamming shots at a target
    }
    #endregion
}
