using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //VARIABLES
    public string _ItemName;
    public int _ItemAmount;
    public enum ItemType { Damage, Heal, Other};
    public ItemType _ItemType;

    //METHODS
    public void UseItem(BaseStats target)
    {
        Debug.Log(_ItemName + " was used on" + target.CharacterName);
        if(_ItemType == ItemType.Damage)
        {

        }
        else if (_ItemType == ItemType.Heal)
        {

        }
        else if (_ItemType == ItemType.Other)
        {

        }
        _ItemAmount--;
    }
}
