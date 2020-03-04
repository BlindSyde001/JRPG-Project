using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;         //Lazy way to create a singleton
    public GameObject spellButtonPrefab;      //The spell prefab button
    public GameObject spellButtonContainer;   //The container we want out buttons to be created in
    public List<GameObject> allSpellButtons;  //List containing all of our spell buttons

    private void Awake()
    {
        instance = this;
    }

    //Creates the spell button pool. Runs once only
    public void CreateSpellButtonPool(List<Spells> _spells)
    {
        foreach(Spells sp in _spells)
        {
            GameObject go = Instantiate(spellButtonPrefab, spellButtonContainer.transform);
            go.GetComponent<SpellButton>().SetButton(sp);
            allSpellButtons.Add(go);
            //go.SetActive(false);
        }
    }

    //Sets the available spell buttons on or off
    public void SetAvailableSpellButtons(List<Spells> _spells)
    {
        foreach (GameObject asb in allSpellButtons)
        {
            //asb.SetActive(false);           //turn all buttons off
            asb.GetComponent<Button>().interactable = false;           //turn all buttons off
            foreach (Spells sp in _spells)   //loop through the spells
            {
                if(asb.GetComponent<SpellButton>().thisSpell == sp) //if our spells match
                    asb.GetComponent<Button>().interactable = true;
                //asb.SetActive(true);    //Set active if possible
            }
        }
    }
}
