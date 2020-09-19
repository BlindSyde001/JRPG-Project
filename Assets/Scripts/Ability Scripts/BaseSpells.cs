using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpells : MonoBehaviour
{
    //VARIABLES
    public Spells spellInfo;

    //METHODS
    public virtual void ModulatedSpell(BaseStats Caster, BaseStats target)  // Single
    {

    }
    public virtual void ModulatedSpell(BaseStats Caster, List<BaseStats> targets) // Multi
    {

    }
}

public class DamageSpells : BaseSpells
{
    public override void ModulatedSpell(BaseStats Caster, BaseStats target)
    {
        int damageAmount;

        bool crit;
        if (Random.Range(0, 101) < Caster.luck)
            crit = true;
        else
            crit = false;

        damageAmount = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
        target.TakeDamage(damageAmount, true, crit);
    }
    public override void ModulatedSpell(BaseStats Caster, List<BaseStats> targets)
    {
        int damageAmount;

        bool crit;
        if (Random.Range(0, 101) < Caster.luck)
            crit = true;
        else
            crit = false;

        damageAmount = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
        foreach (BaseStats x in targets)
        {
            x.TakeDamage(damageAmount, true, crit);
        }
    }
}
public class HealingSpells : BaseSpells
{
    public override void ModulatedSpell(BaseStats Caster, BaseStats target)
    {
        int healAmount;

        bool crit;
        if (Random.Range(0, 101) < Caster.luck)
            crit = true;
        else
            crit = false;

        healAmount = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
        target.HealDamage(healAmount, crit);
    }
    public override void ModulatedSpell(BaseStats Caster, List<BaseStats> targets)
    {
        int healAmount;

        bool crit;
        if (Random.Range(0, 101) < Caster.luck)
            crit = true;
        else
            crit = false;

        healAmount = (int)((spellInfo._SpellPower * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
        foreach (BaseStats x in targets)
        {
            x.HealDamage(healAmount, crit);
        }
    }
}
public class OtherSpells : BaseSpells
{

}