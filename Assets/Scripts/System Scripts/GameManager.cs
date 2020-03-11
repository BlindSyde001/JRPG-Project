using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //VARIABLES
    [Header("FOR BATTLE")]
    public List<string> partyLineup;               // Stored variable on who is in the party to start the battle
    public List<string> enemyLineup;               // Stored variable on which enemy is in the battle
    public List<bool> positionFront;               // For positions of party members
    public List<BasePartyMember> _AllPartyMembers; // Stored variables of all Playable Characters

    [Header("DATABASE")]
    public List<Spells> _SpellsPool;               // All spells in the Game for the player
}
