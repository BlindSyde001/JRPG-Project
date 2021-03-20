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
    private DialogueManager _dm;
    public string _sentenceForSkip;

    //UPDATES
    private void Start()
    {
        msgBoxAnim = GameObject.Find("Message Box").GetComponent<Animator>();
        _dm = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        // Starts Talking
        if(Input.GetKeyDown(KeyCode.E) && talkingRange && !convoCont)
        {
            msgBoxAnim.SetBool("IsOpen", true);
            _dm.StartDialogue(NPC);
            convoCont = true;
        }
        // If I'm already talking
        else if(Input.GetKeyDown(KeyCode.E) && convoCont)
        {
            // Finish the sentence or start new one
            if (_dm.chatText.text != _sentenceForSkip)
            {
                _dm.StopAllCoroutines();
                _dm.chatText.text = _sentenceForSkip;
            }
            else
                _dm.DisplayNextSentence();
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
    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnNewSceneLoaded;
    //}
    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnNewSceneLoaded;
    //}
    //private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name != "Battle Scene")
    //    {
    //        msgBoxAnim = GameObject.Find("Message Box").GetComponent<Animator>();
    //    }
    //}
    #endregion
}
