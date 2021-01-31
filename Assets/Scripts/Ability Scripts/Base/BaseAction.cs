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
    public virtual void ModulatedSpell(SpellsInfo spellUsed, BaseStats Caster, BaseStats target)         // Single
    {
        spellInfo = spellUsed;
        switch(spellInfo._SpellEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.TakeDamage(rawValue, true, crit, spellInfo._SpellElement, spellInfo._ActionPiercing);
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
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

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.TakeDamage(rawValue, true, crit, spellInfo._SpellElement, spellInfo._ActionPiercing);
                }
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.HealDamage(rawValue, crit);
                }
                break;
        }
        ActionsInfo inputAction = spellInfo;
        StatusEffect(targets, inputAction);
    }

    public virtual void ModulatedAbility(AbilityInfo abilityUsed, bool magical, BaseStats Caster, BaseStats target)
    {
        abilityInfo = abilityUsed;
        switch (abilityInfo._AbilityEffect)
        {
            case ActionEffect.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo._AbilityPower * 
                (magical ? Caster.magAttackPower + Caster.mind : Caster.attackPower + Caster.strength)) 
                * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));

                target.TakeDamage(rawValue, true, crit, abilityInfo._AbilityElement, abilityInfo._ActionPiercing);
                break;

            case ActionEffect.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((abilityInfo._AbilityPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
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

                rawValue = (int)((abilityInfo._AbilityPower *
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

                rawValue = (int)((abilityInfo._AbilityPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.HealDamage(rawValue, crit);
                }
                break;
        }
        ActionsInfo inputAction = abilityInfo;
        StatusEffect(targets, inputAction);
    }

    public void StatusEffect(BaseStats target, ActionsInfo action) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < action.posStatusChances.Length; i++)
        {
            if(action.posStatusChances[i] > 0)
            {
                switch(i)
                {
                    case 0: // Zeal
                        if(Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            target.zeal = true;
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            target.faith = true;
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            target.haste = true;
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
                        if (Random.Range(0, 101 + target.poisonResist) <= action.posStatusChances[i])
                        {
                            target.poison = true;
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101 + target.silenceResist) <= action.posStatusChances[i])
                        {
                            target.silence = true;
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101 + target.slowResist) <= action.posStatusChances[i])
                        {
                            target.slow = true;
                        }
                        break;
                }
            }
        }
    }
    public void StatusEffect(List<BaseStats> targets, ActionsInfo action) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < action.posStatusChances.Length; i++)
        {
            if (spellInfo.posStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Zeal
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            foreach(BaseStats target in targets)
                            {
                                target.zeal = true;
                            }
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.faith = true;
                            }
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.haste = true;
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
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.poison = true;
                            }
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.silence = true;
                            }
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101) <= action.posStatusChances[i])
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

    public void Gravity()
    {
        // rawValue = (int)(target.currentHP * spellinfo.SpellPower);
        // target.TakeDamage(rawValue, true, false, spellinfo.SpellElement);
    }
}