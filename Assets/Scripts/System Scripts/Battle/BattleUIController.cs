using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    //VARIABLES
    public BattleManager _BM;
    public Transform panel;                       // Currently opened panel to create the appropriate buttons
    public GameObject button;                     // button prefab to be instantiated per UI
    public PartyMember chosenHero;                // Currently selected hero, to control actions
    public int cycleButtonInput;

    //UPDATES
    private void Awake()
    {
        _BM = FindObjectOfType<BattleManager>();
    }
    private void Start()
    {
        chosenHero = _BM._MembersInBattle[0];
        _BM._CharacterPanels[0].transform.GetChild(0).gameObject.SetActive(true);
    }
    private void LateUpdate()
    {
        CycleThroughHeroes();    // Press cycle buttons to switch between heroes, so that the UI represents that hero's action pool
    }

    //METHODS
    public void CreateButtons()
    {
        foreach (Spells spell in _BM._MembersInBattle[0].spells)
        {
            Instantiate(button, panel);
        }
    }
    public void CycleThroughHeroes()
    {
        if (Input.GetButtonDown("Cycle"))
        {
            _BM._CharacterPanels[cycleButtonInput].transform.GetChild(0).gameObject.SetActive(false);  // turn off current select

            cycleButtonInput += (int)Input.GetAxisRaw("Cycle");                                        // find new Select
            if (cycleButtonInput < 0)
            {
                cycleButtonInput = (_BM._MembersInBattle.Count - 1);
            }
            else if (cycleButtonInput > (_BM._MembersInBattle.Count - 1))
            {
                cycleButtonInput = 0;
            }
            chosenHero = _BM._MembersInBattle[cycleButtonInput];
            _BM._CharacterPanels[cycleButtonInput].transform.GetChild(0).gameObject.SetActive(true);   // Turn on new select
            Debug.Log(chosenHero.CharacterName + " Is Selected");
        }
    }
}
