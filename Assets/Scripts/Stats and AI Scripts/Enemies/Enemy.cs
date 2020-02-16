using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseStats
{

    //METHODS
    public void EnemyAction()        // Choose which action enemy takes
    {
        int dieRoll = Random.Range(0, 2);
        switch(dieRoll)
        {
            case 0:
                //Defend();
                break;

            case 1:
                Spells spellToCast = GetRandomSpell();
                if(spellToCast._SpellType == Spells.SpellType.Heal)
                {
                    // Heal friendly enemy
                }
                if(!CastMagic(spellToCast, null))
                {
                    //attack
                }
                break;

            case 2:
                // Attack
                break;
        }
    }
    Spells GetRandomSpell()
    {
        return spells[Random.Range(0, spells.Count - 1)];
    }
    public override void Die()
    {
        base.Die();
    }
}
