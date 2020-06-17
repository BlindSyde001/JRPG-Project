using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Interaction : MonoBehaviour
{
    //VARIABLES
    private bool talkingRange = false;               //The NPCs Chat Cycle
    public NPCGeneric NPC;                           //The NPC in Chat Range
    public bool convoCont;
    public Animator msgBoxAnim;

    //UPDATES
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && talkingRange && !convoCont)
        {
            msgBoxAnim.SetBool("IsOpen", true);
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
            msgBoxAnim.SetBool("IsOpen", false);
        }
    }
    #region Level Changing
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Battle Scene")
        {
            msgBoxAnim = GameObject.Find("Message Box").GetComponent<Animator>();
        }
    }
    #endregion
}
