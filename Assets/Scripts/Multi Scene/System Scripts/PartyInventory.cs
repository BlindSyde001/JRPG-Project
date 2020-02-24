using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInventory : MonoBehaviour
{
    //VARIABLES
    public List<string> _PartyMembers;         // All members currently in party.
    public List<string> _PartyLineUp;          // Allocated members in their formation slot [0 - 7] according to their front/back position.
    public List<string> _Items;                // The items currently in inventory.

    //UPDATES

    
    //METHODS
    public void SortInventory()                // Sort Items in Inventory according to their item type.
    {

    }

    public void ChangeFormation()              // Change position of party member in the lineup.
    {

    }
}
