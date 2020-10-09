using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    //VARIABLES
    public SpellsInfo spellInfo;
    protected int rawValue;                    // Value of input before defense reduciton
    protected bool crit;

    //METHODS
    public virtual void ModulatedSpell(SpellsInfo spellUsed, BaseStats Caster, BaseStats target)         // Single
    {
        spellInfo = spellUsed;
        switch(spellInfo._SpellType)
        {
            case SpellType.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.TakeDamage(rawValue, true, crit, spellInfo._SpellElement);
                break;

            case SpellType.Heal:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.HealDamage(rawValue, crit);
                break;
        }
        StatusEffect(target);
    }
    public virtual void ModulatedSpell(SpellsInfo spellUsed, BaseStats Caster, List<BaseStats> targets)  // Multi
    {
        Debug.Log("SUCCESS");
        spellInfo = spellUsed;
        switch(spellInfo._SpellType)
        {
            case SpellType.Damage:
                if (Random.Range(0, 101) < Caster.luck)
                { crit = true; }
                else { crit = false; }

                rawValue = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                foreach (BaseStats x in targets)
                {
                    x.TakeDamage(rawValue, true, crit, spellInfo._SpellElement);
                }
                break;

            case SpellType.Heal:
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
        StatusEffect(targets);
    }

    public void StatusEffect(BaseStats target) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < spellInfo.posStatusChances.Length; i++)
        {
            if(spellInfo.posStatusChances[i] > 0)
            {
                switch(i)
                {
                    case 0: // Zeal
                        if(Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            target.zeal = true;
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            target.faith = true;
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            target.haste = true;
                        }
                        break;
                }
            }
        }
        for (int i = 0; i < spellInfo.negStatusChances.Length; i++)
        {
            if (spellInfo.negStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Poison
                        if (Random.Range(0, 101 + target.poisonResist) <= spellInfo.posStatusChances[i])
                        {
                            target.poison = true;
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101 + target.silenceResist) <= spellInfo.posStatusChances[i])
                        {
                            target.silence = true;
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101 + target.slowResist) <= spellInfo.posStatusChances[i])
                        {
                            target.slow = true;
                        }
                        break;
                }
            }
        }
    }
    public void StatusEffect(List<BaseStats> targets) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < spellInfo.posStatusChances.Length; i++)
        {
            if (spellInfo.posStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Zeal
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            foreach(BaseStats target in targets)
                            {
                                target.zeal = true;
                            }
                        }
                        break;

                    case 1: // Faith
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.faith = true;
                            }
                        }
                        break;

                    case 2: // Haste
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
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
        for (int i = 0; i < spellInfo.negStatusChances.Length; i++)
        {
            if (spellInfo.negStatusChances[i] > 0)
            {
                switch (i)
                {
                    case 0: // Poison
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.poison = true;
                            }
                        }
                        break;

                    case 1: // Silence
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
                        {
                            foreach (BaseStats target in targets)
                            {
                                target.silence = true;
                            }
                        }
                        break;

                    case 2: // Slow
                        if (Random.Range(0, 101) <= spellInfo.posStatusChances[i])
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
}