using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    //VARIABLES
    public BattleManager _BM;
    public GameObject panel;

    //UPDATES
    private void Awake()
    {
        _BM = FindObjectOfType<BattleManager>();
    }

    private void Start()
    {
        foreach (Spells spell in _BM._MembersInBattle[0].spells)
        {
            
        }
    }
}
