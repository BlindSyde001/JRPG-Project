using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    //VARIABLES
    private bool talkingRange = false;               //The NPCs Chat Cycle
    public NPCGeneric NPC;                           //The NPC in Chat Range
    public bool convoCont;
    public Animator anim;

    //UPDATES
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && talkingRange && !convoCont)
        {
            anim.SetBool("IsOpen", true);
            FindObjectOfType<DialogueManager>().StartDialogue(NPC);
            convoCont = true;
        } else if(Input.GetKeyDown(KeyCode.E) && convoCont)
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    //METHODS

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NPC"))
        {
            talkingRange = true;
            convoCont = false;
            NPC = other.GetComponent<NPCGeneric>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            talkingRange = false;
            convoCont = false;
            NPC = null;
            anim.SetBool("IsOpen", false);
        }
    }
}
