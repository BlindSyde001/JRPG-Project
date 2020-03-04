using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellManager : MonoBehaviour
{
    public List<Spells> allSpells;       //All spells within the game
    public List<Spells> availableSpells; //All available spells to the player

    private void Start()
    {
        UIManager.instance.CreateSpellButtonPool(allSpells);
    }
    void Update()
    {
        AssignRandomSpells();   //Just to test. Fills avauilable spells with a random number of spells
    }

    void AssignRandomSpells()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            availableSpells.Clear();
            int rnd = Random.Range(1, allSpells.Count+1);
            for(int i = 0; i < rnd; i++)
            {
                availableSpells.Add(allSpells[i]);
            }
            UIManager.instance.SetAvailableSpellButtons(availableSpells);
        }
    }
}
