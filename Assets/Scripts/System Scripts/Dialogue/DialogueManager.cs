using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //VARIABLES
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI chatText;
    public Queue<string> sentences;
    public bool _TurnToFace = false;

    //UPDATES
    private void Start()
    {
        sentences = new Queue<string>();
    }
    
    //METHODS
    public void StartDialogue(NPCGeneric NPC)
    {
        if(_TurnToFace)
        { }
        nameText.text = NPC._name;                          // Set the name.
        sentences.Clear();                                  // Clear all sentence fields.
        foreach (string sentence in NPC.sentences)
        {
            sentences.Enqueue(sentence);                    // Set the sentences in order.
        }
        DisplayNextSentence();                             // Start the Convo.
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)                            // No sentences then closes message box.
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();             // Return sentence at top of queue then gets rid of it.
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));            // Type out the returned sentence.
    }
    IEnumerator TypeSentence (string sentence)
    {
        chatText.text = "";                               // Reset message box text.
        foreach(char letter in sentence.ToCharArray())
        {
            chatText.text += letter;                      // Typing message.
            yield return null;
        }
    }
    public void EndDialogue()
    {
        FindObjectOfType<Interaction>().anim.SetBool("IsOpen", false);        // Play Message box close Anim.
        FindObjectOfType<Interaction>().convoCont = false;                    // Reset bool for next use.
    }

    public void FacePlayer()
    {

    }
}
