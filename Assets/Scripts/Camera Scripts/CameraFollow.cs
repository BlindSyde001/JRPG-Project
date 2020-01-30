using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //VARIABLES
    private GameObject player;
    private Camera cam;

    public Vector3 offset;
    public Vector3 min;
    public Vector3 max;

    private bool isFollowing;

    //UPDATES
    private void Start()
    {
        cam = Camera.main;

        player = GameObject.FindGameObjectWithTag("Player");

        min += transform.position;
        max += transform.position;

        if (min.x > max.x)
        {
            float val = min.x;
            min.x = max.x;
            max.x = val;
        }

        if (min.y > max.y)
        {
            float val = min.y;
            min.y = max.y;
            max.y = val;
        }

        if (min.z > max.z)
        {
            float val = min.z;
            min.z = max.z;
            max.z = val;
        }
    }

    private void LateUpdate()
    {
        if (isFollowing)
        {
            Vector3 pos = player.transform.position + offset;
            cam.transform.position = new Vector3(Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y), Mathf.Clamp(pos.z, min.z, max.z));
        }
    }

    //METHODS
    public void StartFollow()
    {
        isFollowing = true;
    }

    public void StopFollow()
    {
        isFollowing = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 min = transform.position + this.min;
        Vector3 max = transform.position + this.max;

        if (UnityEditor.EditorApplication.isPlaying) {
            min = this.min;
            max = this.max;
        }

        Gizmos.DrawWireCube((max + min) / 2F, max - min);
    }
#endif
}
