using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //VARIABLES
    public Transform cameraRig;
    public Transform camTransform;
    private Camera cam;

    [SerializeField]
    private float targetDistance = 10f;
    private float currentDistance = 10f;
    public float cameraRelocate = 0.2f;
    private float currentVelocity;

    private float currentX = 0f;
    private float currentY = 0f;
    private const float yAngle = 40f;

    //UPDATES
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        currentY = Mathf.Clamp(currentY, -17, yAngle);
    }
    private void LateUpdate()
    {
        FreeRotateCamera();
        CameraClipping();
    }

    //METHODS
    void FreeRotateCamera()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.rotation = rotation;
    }
    void CameraClipping()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, targetDistance))
        {
            currentDistance = hit.distance;
        }
        else
        {
            currentDistance = targetDistance;
        }
        cam.transform.localPosition = new Vector3(0, 0, -currentDistance + cameraRelocate);
        Debug.DrawRay(transform.position, -transform.forward * targetDistance);
    }
}
