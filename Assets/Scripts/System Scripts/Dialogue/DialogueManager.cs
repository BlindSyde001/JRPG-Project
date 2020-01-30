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

    //UPDATES
    private void Start()
    {
        sentences = new Queue<string>();
    }
    
    //METHODS
    public void StartDialogue(NPCGeneric NPC)
    {
        nameText.text = NPC._name;
        sentences.Clear();
        foreach (string sentence in NPC.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        chatText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            chatText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        FindObjectOfType<Interaction>().anim.SetBool("IsOpen", false);
        FindObjectOfType<Interaction>().convoCont = false;
    }
}
