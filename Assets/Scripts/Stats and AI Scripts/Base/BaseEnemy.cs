using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseStats
{
    public bool isAlive;
    public int x;
    public BaseStats targetCharacter;
    //UPDATES
    new void Update()
    {
        if(isAlive)
        {
            base.Update();
        }
    }

    //METHODS
    public override void Die()
    {
        isAlive = false;
        int i = _BM._ActiveEnemies.IndexOf(this);
        _BM._ActiveEnemies.Remove(this);       // Remove from targetting list
        _BM._DownedEnemies.Add(this);          // Add to downed list (for XP tally/revives, etc)
        _BUI.SetEnemyTargets();
        FindObjectOfType<BattleManager>().expPool += totalXP; // Add XP amount to total pool
        Destroy(_BM._EnemyModels[i]);
        _BM._EnemyModels.Remove(_BM._EnemyModels[i]);
        if(_BM._ActiveEnemies.Count == 0)   // Win if no more enemies
        {
            _BM.VictoryState();
        }
    }
}
