using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum ItemTarget { Single, Multi, Other }
    public enum ItemType { Damage, Heal, Shield, Other };
    public enum ItemElement { Fire, Ice, Lightning, Water, Wind, Earth, Light, Dark, None };
    public enum ItemCombat { Combat, World, Both };
[CreateAssetMenu(fileName = "New Item", menuName = "Items")]

public class Items : ScriptableObject
{
    //VARIABLES
    public string _ItemName;
    public int _ItemID;
    public int _ItemAmount;
    public string _ItemDescription;

    public ItemTarget _Itemtarget;
    public ItemType _ItemType;
    public ItemElement _ItemElement;
    public ItemCombat _ItemCombat;

    //METHODS
    public void ModulatedItem()
    {

    }
}
