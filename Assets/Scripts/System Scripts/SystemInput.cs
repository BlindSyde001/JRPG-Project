using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInput : MonoBehaviour
{
    //VARIABLES
    public Animator anim;
    public bool isMenuOpen;
    public AnimationClip _Menu_Open;
    public AnimationClip _Menu_Close;

    //UPDATES
    private void Start()
    {
        isMenuOpen = false;
    }
    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Start"))
        {
            if (anim.GetBool("IsOpen") == false)
            {
                anim.SetBool("IsOpen", true);
                isMenuOpen = true;
            }
            else
            {
                anim.SetBool("IsOpen", false);
                isMenuOpen = false;
            }
        }
    }

    //METHODS
}
