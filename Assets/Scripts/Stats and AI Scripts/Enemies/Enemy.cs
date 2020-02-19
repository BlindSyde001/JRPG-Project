using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseStats
{
    //UPDATES
    new void Update()
    {
       base.Update();
       if(_ActionBarAmount >= 100)
        {
            EnemyAction();
            _ActionBarAmount = 0;
        }
    }

    //METHODS
    public void EnemyAction()        // Choose which action enemy takes
    {
        int dieRoll = Random.Range(0, 3);
        switch(dieRoll)
        {
            case 0:
                Defend();
                print(CharacterName + " Defended!");
                break;

            case 1:
                print(CharacterName + " Casted a spell!");
                //Spells spellToCast = GetRandomSpell();
                //if(spellToCast._SpellType == Spells.SpellType.Heal)
                //{
                //    // Heal friendly enemy
                //}
                //if(!CastMagic(spellToCast, null))
                //{
                //    //attack
                //}
                break;

            case 2:
                int x = Random.Range(0, _BM._MembersInBattle.Count - 1);
                BaseStats targetCharacter = _BM._MembersInBattle[x];

                Attack(targetCharacter);
                print(CharacterName + " Attacked " + targetCharacter.CharacterName);
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
