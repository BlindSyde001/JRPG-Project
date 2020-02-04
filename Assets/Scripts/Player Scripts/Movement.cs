using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //VARIABLES
    #region Scene References
    [Header("SCENE REFERENCES")]
    [SerializeField]
    private Transform cam;
    [SerializeField]
    public Transform referenceDirection;
    private CharacterController _cc;
    private SystemInput _systemInput;
    public float slopeForceRayLength;
    public float slopeForce;
    #endregion
    #region Movement & Rotation
    [Header("MOVEMENT & ROTATION")]
    [SerializeField]
    private float velocity = 5f;
    [SerializeField]
    private float turnSpeed = 20;

    public Vector3 input;
    private float angle;
    private Quaternion targetRotation;
    private Quaternion refQuat;
    #endregion

    //UPDATES
    private void Start()
    {
        _systemInput = FindObjectOfType<SystemInput>();
        cam = Camera.main.transform;
        _cc = this.GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (FindObjectOfType<Interaction>().convoCont)                            // 1. If talking, don't move
        {
            transform.LookAt(FindObjectOfType<Interaction>().NPC.transform);
            return;
        }

        if (_systemInput.isMenuOpen == true)                                      // Also 1. If in menu, don't move
            return;

        //getInput();                                                             // 2. Which input is being pressed
        if (input.sqrMagnitude >= Mathf.Epsilon)
        {
            calculateDirection();                                                 // 3. Which way is character facing
            rotate();                                                             // 4. Spin to movement direction
            Move();
        }
        else
        {
            refQuat = referenceDirection.rotation;
        }
    }

    //METHODS
    #region Rotation & Movement Methods
    void calculateDirection()
    {
        input = refQuat * input;
        input.y = 0F;

        if (input.magnitude > 1F)
            input.Normalize();                 //Normalize so movement in all directions is same speed

        angle = Mathf.Atan2(input.x, input.z);
        angle = Mathf.Rad2Deg * angle;
    }

    void rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    void Move()
    {
        _cc.Move(new Vector3(input.x * velocity * Time.deltaTime, -1 * velocity * Time.deltaTime, input.z * velocity * Time.deltaTime));
    }
    #endregion
}