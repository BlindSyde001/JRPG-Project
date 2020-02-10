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
    private bool inTalkingRange = false;
    public List<Transform> patrolPoints;
    private int currentPos = 0;
    public float speed;
    #endregion
    #region Look Variables
    private Quaternion _LookRotation;
    private Vector3 _direction;
    public float _RotationSpeed;
    #endregion

    //UPDATES
    private void FixedUpdate()
    {
        if (FindObjectOfType<Interaction>().convoCont && inTalkingRange)          //1. If talking, don't move
        {
            LookAt();
            return;
        }
        //if (patrolPoints.Count != 0)
        //    MovePattern();
    }

    //METHODS
    private void MovePattern()                                             //Moving between one point and another
    {
        if(Vector3.Distance(transform.position, patrolPoints[currentPos].position) < 2)
        {
            currentPos++;
            if (currentPos >= patrolPoints.Count)
            {
                currentPos = 0;
            }
        }
        Vector3 _Pdirection = transform.position - patrolPoints[currentPos].position;
        _Pdirection.y = 0;
        Quaternion _PLookRotation = Quaternion.LookRotation(_Pdirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, _PLookRotation, Time.deltaTime * _RotationSpeed);
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void LookAt()
    {
        _direction = (FindObjectOfType<Movement>().transform.position - transform.position).normalized;
        _direction.y = 0;
        _LookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _LookRotation, Time.deltaTime * _RotationSpeed);
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
