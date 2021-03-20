using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumable")]

public class ConsumableInfo : BaseItemInfo
{
    public ActionTarget _ItemTarget;
    public ActionEffect _ItemEffect;
    public ActionElement _ItemElement;
    public ActionCombat _ItemCombat;
}
