using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemInput : MonoBehaviour
{
    //VARIABLES
    public Animator anim;
    public bool isMenuOpen;
    private Movement _PlayerMovement;
    public Button _MenuStartButton;

    //UPDATES
    private void Start()
    {
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
        }
    }
    private void CancelButton()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (anim.GetBool("IsOpen") == false)
            {
                return;
            }
            // Closes menu.
            anim.SetBool("IsOpen", false);
            isMenuOpen = false;
        }
    }
}
