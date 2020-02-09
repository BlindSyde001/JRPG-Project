using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawnPoint : MonoBehaviour
{
    //METHODS
    public BaseStats Spawn(BaseStats character)
    {
        BaseStats characterToSpawn = Instantiate<BaseStats>(character, this.transform); // Spawn the specified character at this point and parent it
        return characterToSpawn; // Mark instantiated character
    }
}
