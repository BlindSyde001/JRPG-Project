using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    //VARIABLES
    public float velocity = 5f;
    public float turnSpeed = 10;
    public float run = 1f;

    private Vector3 input;
    private float angle;
    private Quaternion targetRotation;
    private Transform cam;
    Rigidbody rb;

    private Quaternion cameraQuat;
    private NavMeshAgent nav;

    //UPDATES
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cam = Camera.main.transform;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        getInput();
        Debug.Log(input);

        if (input.sqrMagnitude >= Mathf.Epsilon)
        {
            calculateDirection();
            rotate();
            Move();
        }
        else
        {
            cameraQuat = cam.rotation;
        }
    }

    //METHODS
    void getInput()
    {
        input.x = Input.GetAxisRaw("Horizontal Move");
        input.z = Input.GetAxisRaw("Vertical Move");
    }

    void calculateDirection()
    {
        input = cameraQuat * input;
        input.y = 0F;

        if (input.magnitude > 1F)
            input.Normalize();

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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 3f;
        }
        else
        {
            run = 2f;
        }

        //nav.velocity = input * velocity * run;
        transform.position += input * velocity * run * Time.deltaTime;
    }
}
