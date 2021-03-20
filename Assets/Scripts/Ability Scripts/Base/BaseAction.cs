using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    //VARIABLES
    public SpellsInfo spellInfo;
    public AbilityInfo abilityInfo;
    public ConsumableInfo itemInfo;
    protected int rawValue;                    // Value of input before defense reduciton
    protected bool crit;

    //METHODS
    #region Spell
    public virtual void ModulatedSpell(SpellsInfo spellUsed, BaseStats Caster, BaseStats target)         // Single
    {
        spellInfo = spellUsed;
        switch(spellInfo._SpellEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                // For conventional damage attacks
                if (spellUsed._SpellValue == ActionValue.Flat)
                {
                    rawValue = (int)((spellInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                    target.TakeDamage(rawValue, true, crit, spellUsed._SpellElement, spellUsed._ActionPiercing);
                }
                // For perecnetage based attacks
                else if(spellUsed._SpellValue == ActionValue.Percent)
                {
                    rawValue = (int)((spellInfo.powerValue / 100) * target.maxHP);
                    target.TakeDamage(rawValue, true, crit, spellUsed._SpellElement, spellUsed._ActionPiercing);
                }
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo.powerValue* Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.HealDamage(rawValue, crit);
                break;
        }
        ActionsInfo inputAction = spellInfo;
        StatusEffect(target, inputAction);
    }
    public virtual void ModulatedSpell(SpellsInfo spellUsed, BaseStats Caster, List<BaseStats> targets)  // Multi
    {
        spellInfo = spellUsed;
        switch(spellInfo._SpellEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.TakeDamage(rawValue, true, crit, spellInfo._SpellElement, spellInfo._ActionPiercing);
                }
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.HealDamage(rawValue, crit);
                }
                break;
        }
        ActionsInfo inputAction = spellInfo;
        StatusEffect(targets, inputAction);
    }
    #endregion
    #region Ability
    public virtual void ModulatedAbility(AbilityInfo abilityUsed, bool magical, BaseStats Caster, BaseStats target)
    {
        abilityInfo = abilityUsed;
        switch (abilityInfo._AbilityEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo.powerValue * 
                (magical ? Caster.magAttackPower + Caster.mind : Caster.attackPower + Caster.strength)) 
                * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));

                target.TakeDamage(rawValue, true, crit, abilityInfo._AbilityElement, abilityInfo._ActionPiercing);
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.HealDamage(rawValue, crit);
                break;
        }
        ActionsInfo inputAction = abilityInfo;
        StatusEffect(target, inputAction);
    }
    public virtual void ModulatedAbility(AbilityInfo abilityUsed, bool magical, BaseStats Caster, List<BaseStats> targets)
    {
        abilityInfo = abilityUsed;
        switch (abilityInfo._AbilityEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo.powerValue *
                (magical ? Caster.magAttackPower + Caster.mind : Caster.attackPower + Caster.strength))
                * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));

                foreach (BaseStats x in targets)
                {
                    x.TakeDamage(rawValue, true, crit, abilityInfo._AbilityElement, abilityInfo._ActionPiercing);
                }
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.HealDamage(rawValue, crit);
                }
                break;
        }
        ActionsInfo inputAction = abilityInfo;
        StatusEffect(targets, inputAction);
    }
    #endregion
    #region Status
    public void StatusEffect(BaseStats target, ActionsInfo action) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < action.posBuffChances.Length; i++)
        {
            if(action.posBuffChances[i] > 0)
            {
                switch(i)
                {
                    case 0: // Zeal
                        if(Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            target.zeal = true;
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            target.faith = true;
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            target.haste = true;
                        }
                        break;

                    case 3: // Revive
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            target.isAlive = true;
                            target._BM._ActivePartyMembers.Add((BasePartyMember)target);
                            target._BM._DownedMembers.Remove((BasePartyMember)target);
                            target._BM.UpdatePartyAliveStatus();
                        }
                        break;
                }
            }
        }
        for (int i = 0; i < action.negStatusChances.Length; i++)
        {
            if (action.negStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Poison
                        if (Random.Range(0, 101 + target.poisonResist) <= action.posBuffChances[i])
                        {
                            target.poison = true;
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101 + target.silenceResist) <= action.posBuffChances[i])
                        {
                            target.silence = true;
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101 + target.slowResist) <= action.posBuffChances[i])
                        {
                            target.slow = true;
                        }
                        break;
                }
            }
        }
        for (int i = 0; i < action.dispelChances.Length; i++)
        {
            if (action.dispelChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Zeal
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                                target.zeal = true;
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                                target.faith = true;
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                                target.haste = true;
                        }
                        break;

                    case 3: // Poison
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                                target.poison = true;
                        }
                        break;

                    case 4: // Silence
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                                target.silence = true;
                        }
                        break;

                    case 5: // Slow
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                                target.slow = true;
                        }
                        break;
                }
            }
        }
    }
    public void StatusEffect(List<BaseStats> targets, ActionsInfo action) // Status Effects Logic: Positive, Negative, then Dispel(Pos/Neg)
    {
        for (int i = 0; i < action.posBuffChances.Length; i++)
        {
            if (spellInfo.posBuffChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Zeal
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach(BaseStats target in targets)
                            {
                                target.zeal = true;
                            }
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.faith = true;
                            }
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.haste = true;
                            }
                        }
                        break;

                    case 3: // Revive
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.isAlive = true;
                                target._BM._ActivePartyMembers.Add((BasePartyMember)target);
                                target._BM._DownedMembers.Remove((BasePartyMember)target);
                                target._BM.UpdatePartyAliveStatus();
                            }
                        }
                        break;
                }
            }
        }
        for (int i = 0; i < action.negStatusChances.Length; i++)
        {
            if (action.negStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Poison
                        if (Random.Range(0, 101) <= action.negStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.poison = true;
                            }
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101) <= action.negStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.silence = true;
                            }
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101) <= action.negStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.slow = true;
                            }
                        }
                        break;
                }
            }
        }
        for (int i = 0; i < action.dispelChances.Length; i++)
        {
            if (action.dispelChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Zeal
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.zeal = true;
                            }
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.faith = true;
                            }
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posBuffChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.haste = true;
                            }
                        }
                        break;

                    case 3: // Poison
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.poison = true;
                            }
                        }
                        break;

                    case 4: // Silence
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.silence = true;
                            }
                        }
                        break;

                    case 5: // Slow
                        if (Random.Range(0, 101) <= action.dispelChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.slow = true;
                            }
                        }
                        break;
                }
            }
        }
    }
    #endregion
}