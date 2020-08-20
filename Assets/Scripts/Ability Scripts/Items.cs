using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ItemType { Damage, Heal, Other};
[CreateAssetMenu(fileName = "Item", menuName = "Items")]

public class Items : ScriptableObject
{
    //VARIABLES
    public string _ItemName;
    public int _ItemID;
    public int _ItemAmount;
    public ItemType _ItemType;
    public string _ItemDescription;
}
