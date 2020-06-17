using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    public float slopeForceRayLength = 2f;
    public float slopeForce = 4f;
    #endregion
    #region Movement & Rotation
    [Header("MOVEMENT & ROTATION")]
    [SerializeField]
    private float velocity = 8f;
    [SerializeField]
    private float turnSpeed = 20;

    public Vector3 input;
    private float angle;
    private Quaternion targetRotation;
    private Quaternion refQuat;
    #endregion
    #region Look Variables
    private Quaternion _LookRotation;
    private Vector3 _direction;
    public float _RotationSpeed;
    #endregion

    //UPDATES
    private void Start()
    {
        _systemInput = FindObjectOfType<SystemInput>();
        _cc = this.GetComponent<CharacterController>();
        cam = Camera.main.transform;
        referenceDirection = GameObject.Find("Movement Reference").transform;
    }
    private void FixedUpdate()
    {
        if (FindObjectOfType<Interaction>().convoCont)                            // 1. If talking, don't move
        {
            LookAt();
            return;
        }

        if (_systemInput.isMenuOpen == true)                                      // Also 1. If in menu, don't move
            return;

        if (input.sqrMagnitude >= Mathf.Epsilon)
        {
            calculateDirection();                                                 // 3. Which way is character facing
            rotate();                                                             // 4. Spin to movement direction
        }
        else
        { refQuat = referenceDirection.rotation; }
        Move();
    }

    //METHODS
    #region Rotation & Movement Methods
    void calculateDirection()
    {
        input = refQuat * input;
        input.y = 0F;

        if (input.magnitude > 1F)
            input.Normalize();                 // Normalize so movement in all directions is same speed

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
    private void LookAt()
    {
        _direction = (FindObjectOfType<Interaction>().NPC.transform.position - transform.position).normalized;
        _direction.y = 0;
        _LookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _LookRotation, Time.deltaTime * _RotationSpeed);
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
    //    if(scene.name != "Battle Scene")
    //    {
    //    _systemInput = FindObjectOfType<SystemInput>();
    //    _cc = this.GetComponent<CharacterController>();
    //    cam = Camera.main.transform;
    //    referenceDirection = GameObject.Find("Movement Reference").transform;
    //    }
    //}
    #endregion
}