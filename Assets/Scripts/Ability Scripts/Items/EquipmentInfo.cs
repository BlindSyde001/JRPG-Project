using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]

public class EquipmentInfo : BaseItemInfo
{
    public enum EquipSlot { Weapon, Armour, Accessory}
    public enum EquipType { Pistol, SwordAndShield }

    [Space]
    public EquipSlot _EquipSlot;
    public EquipType _EquipType;

    [Header("Primary")]
    public int attackPower;             // Main Damage Modifier for Physical Attacks
    public int magAttackPower;          // Main Damage Modifier for Magical Attacks
    public int defense;                 // Main Damage Reduction Modifier for Physical Attacks
    public int magDefense;              // Main Damage Reduction Modifier for Magical Attacks

    [Header("Secondary")]
    public int strength;                // Secondary Modifier for Attack, HP
    public int mind;                    // Secondary Modifier for magAttack, MP
    public int vitality;                // Secondary Modifier for Defense, HP
    public int spirit;                  // Secondary Modifier for magDefense, MP

    [Header("Tertiary")]
    public int speed;                   // Increases speed of Action Bar being charged as well as initial Action Bar charge amount
    public int luck;                    // Increases chance of Attacks Critically Striking

    [Header("Resource")]
    public int hP;                   // Determines amount of Damage able to take before being KO'D
    public int mP;                   // Determines amount of Spells able to be cast


    [Header("Buffs")]
    public bool zeal;                   // Increase Physical Damage
    public bool faith;                  // Increase Magical Damage
    public bool haste;                  // Increase speed of Action Bar Charge
}
