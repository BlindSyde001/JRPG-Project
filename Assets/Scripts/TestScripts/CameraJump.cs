using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJump : MonoBehaviour
{
    public GameObject posChange;
    public Vector3 currentPos;

    [Space]
    public CameraFollow follow;

    private void Start()
    {
        if(posChange.CompareTag("StartCamPos"))
        {
            CutToShot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            Camera.main.GetComponent<RoomOverlapCamera>().currentZone = this.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Camera.main.GetComponent<RoomOverlapCamera>().currentZone == gameObject)
        {
            CutToShot();

            foreach (CameraFollow cf in FindObjectsOfType<CameraFollow>())
                cf.StopFollow();

            if (follow)
                follow.StartFollow();
        }
    }

    public void CutToShot()
    {
        Camera.main.transform.localPosition = posChange.transform.position;
        Camera.main.transform.localRotation = posChange.transform.rotation;
    }
}
