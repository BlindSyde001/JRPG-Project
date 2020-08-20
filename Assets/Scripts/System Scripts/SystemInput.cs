using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemInput : MonoBehaviour
{
    //VARIABLES
    private GameManager _GM;
    public Animator anim;
    public bool isMenuOpen;
    private Movement _PlayerMovement;
    public Button _MenuStartButton;

    #region UI Display Variables
    public List<UIComponents> displayPanel;
    #endregion
    //UPDATES
    private void Start()
    {
        _GM = FindObjectOfType<GameManager>();
        _PlayerMovement = FindObjectOfType<Movement>();
        isMenuOpen = false;                             // Changes Inputs When Menu is Open/Closed
    }
    private void FixedUpdate()
    {
        DirectionalButtons();                           //Player Movement
    }
    private void Update()
    {
        StartButton();
        CancelButton();
    }

    //METHODS
    private void DirectionalButtons()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _PlayerMovement.input.x = Input.GetAxisRaw("Horizontal");
            _PlayerMovement.input.z = Input.GetAxisRaw("Vertical");
        }
        else
        {
            _PlayerMovement.input.x = 0;
            _PlayerMovement.input.z = 0;
        }
    }                
    private void StartButton()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (anim.GetBool("IsOpen") == true)
                return;
            // Opens Menu.
            anim.SetBool("IsOpen", true);
            isMenuOpen = true;
            _MenuStartButton.Select();
            _MenuStartButton.OnSelect(null);
            HeroDisplay();
        }
    }
    private void CancelButton()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (anim.GetBool("IsOpen") == false)
                return;
            // Closes menu.
            anim.SetBool("IsOpen", false);
            isMenuOpen = false;
        }
    }

    // Attach to button > Plays animation, opening up specific Menu Item
    public void MenuItemAnim(string animBool)
    {
        anim.SetBool(animBool, true);
    }
    public void MenuItemFirstButton(Button firstButton)
    {
        firstButton.Select();
    }
    public void HeroDisplay()
    {
        foreach(UIComponents a in displayPanel)
        {
            a.gameObject.SetActive(false);
        }
        for(int i = 0; i < _GM.partyLineup.Count; i++)
        {
            displayPanel[i].gameObject.SetActive(true);
            displayPanel[i]._Image.sprite = _GM.partyLineup[i].characterPortrait;
            displayPanel[i]._Name.text = _GM.partyLineup[i].CharacterName;
            displayPanel[i]._Level.text = "Level: " + _GM.partyLineup[i].level.ToString();
            displayPanel[i]._CurrentHPAmount.text = _GM.partyLineup[i].currentHP.ToString();
            displayPanel[i]._CurrentMPAmount.text = _GM.partyLineup[i].currentMP.ToString();
            displayPanel[i]._MaxHPAmount.text = _GM.partyLineup[i].maxHP.ToString();
            displayPanel[i]._MaxMPAmount.text = _GM.partyLineup[i].maxMP.ToString();
        }
    }
}
