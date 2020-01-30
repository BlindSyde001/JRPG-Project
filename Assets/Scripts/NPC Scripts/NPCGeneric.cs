using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCGeneric : MonoBehaviour
{
    //VARIABLES
    [TextArea(1, 2)]
    public List<string> sentences;
    public string _name;

    #region Patrol Variables
    [SerializeField]
    private float waitTime = 0;
    private bool coroutineLoop = false;
    private bool inTalkingRange = false;
    public List<Transform> patrolPoints;
    private int currentPos = 0;
    #endregion

    //UPDATES
    private void FixedUpdate()
    {
        if (FindObjectOfType<Interaction>().convoCont && inTalkingRange)          //1. If talking, don't move
        {
            transform.LookAt(FindObjectOfType<Movement>().transform);
            return;
        }
        if (!coroutineLoop && patrolPoints.Count != 0)
            MovePattern();
    }

    //METHODS
    private void MovePattern()                                             //Moving between one point and another
    {
        coroutineLoop = true;
        if(Vector3.Distance(transform.position, patrolPoints[currentPos].position) < 2)
        {
            currentPos++;
            if (currentPos >= patrolPoints.Count)
            {
                currentPos = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("InteractionZone"))
        {
            inTalkingRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractionZone"))
        {
            inTalkingRange = false;
        }
    }
}
