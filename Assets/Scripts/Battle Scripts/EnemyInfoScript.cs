using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
