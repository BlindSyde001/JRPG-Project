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
                break;

            case 2:
                int x = Random.Range(0, _BM._ActivePartyMembers.Count - 1);
                BaseStats targetCharacter = _BM._ActivePartyMembers[x];

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
        _BM._ActiveEnemies.Remove(this);       // Remove from targetting list
        _BM._DownedEnemies.Add(this);          // Add to downed list (for XP tally/revives, etc)
        _BUI.SetEnemyTargets();
        Destroy(this.gameObject);
        if(_BM._ActiveEnemies.Count == 0)   // Win if no more enemies
        {
            _BM.VictoryState();
        }
    }
}
