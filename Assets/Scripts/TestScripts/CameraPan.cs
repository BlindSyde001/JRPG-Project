using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan: MonoBehaviour
{
    public Vector3 currentPos;
    public GameObject player;
    private bool inZone;
    Camera main;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentPos = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if (inZone && Camera.main.GetComponent<RoomOverlapCamera>().currentZone == gameObject)
        {
            Camera.main.transform.LookAt(player.transform);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inZone = false;
    }
}
