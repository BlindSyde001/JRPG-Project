using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModulator : MonoBehaviour
{
    //VARIABLES
    public ActionInfo actionInfo;
    public ConsumableInfo itemInfo;
    protected int rawValue;                    // Value of input before defense reduciton
    protected bool crit;

    //METHODS
    public virtual void ModulatedAction(ActionInfo actionUsed, BaseStats Caster, BaseStats target) // Single
    {
        if (Random.Range(0, 101) < Caster.luck)
        { crit = true; }
        else { crit = false; }

        actionInfo = actionUsed;
        switch(actionInfo._ActionEffect)
        {
            case ActionEffect.Damage:
                // For conventional damage attacks
                if (actionUsed._ActionValue == ActionValue.Flat)
                {
                    rawValue = (int)((actionInfo.powerValue * Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                    target.TakeDamage(rawValue, true, crit, actionUsed._ActionElement, actionUsed._ActionPiercing);
                }
                // For perecentage based attacks
                else if(actionUsed._ActionValue == ActionValue.Percent)
                {
                    rawValue = (int)((actionInfo.powerValue / 100) * target.maxHP);
                    target.TakeDamage(rawValue, true, crit, actionUsed._ActionElement, actionUsed._ActionPiercing);
                }
                break;

            case ActionEffect.Heal:

                rawValue = (int)((actionInfo.powerValue* Caster.magAttackPower + Caster.mind) * Random.Range(1, 1.6f) * (crit ? 1.75f : 1));
                target.HealDamage(rawValue, crit);
                break;
        }
        ActionInfo inputAction = actionInfo;
        StatusEffect(target, inputAction);
    }
    public void StatusEffect(BaseStats target, ActionInfo action) // Status Effects Logic: Positive then Negative
    {
        for (int i = 0; i < action.posBuffChances.Count; i++)
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
        for (int i = 0; i < action.negStatusChances.Count; i++)
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
        for (int i = 0; i < action.dispelChances.Count; i++)
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
   
}