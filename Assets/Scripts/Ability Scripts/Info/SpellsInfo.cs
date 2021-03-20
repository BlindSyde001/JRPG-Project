﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]

public class SpellsInfo : ActionsInfo
{
    //VARIABLES
    public string _SpellName;
    public int _SpellID;
    public int _SpellManaCost;

    public ActionTarget _SpellTarget;
    public ActionCombat _SpellCombat;

    public ActionEffect _SpellEffect;
    public ActionElement _SpellElement;
    public ActionValue _SpellValue;
}
