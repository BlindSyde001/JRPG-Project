using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //VARIABLES
    public string _SpellName;              // What's it called
    public int _SpellPower;                // Numeric modifier for damage/heal value
    public int _SpellManaCost;             // Resource cost
    public enum SpellType { Attack, Heal}; // Category/type of spell
    public SpellType _SpellType;

    private Vector3 targetPosition;        // Point where the spell anim is cast
    public float _SpellAnimSpeed;
    public float _SpellEndAnimSpeed;

    //UPDATES
    private void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _SpellAnimSpeed);
            if(Vector3.Distance(transform.position, targetPosition) < .25f)
            {
                Destroy(this.gameObject, _SpellEndAnimSpeed);
            }
        } else
        {
            Destroy(this.gameObject);
        }

    }

    //METHODS
    public void Cast(BaseStats target)
    {
        targetPosition = target.transform.position;
        Debug.Log(_SpellName + " Was Cast On " + target.name);
        if(_SpellType == SpellType.Attack)
        {
            target.Hurt(_SpellPower);                          // Damage Character
        }
        else if(_SpellType == SpellType.Heal)
        {
            target.Heal(_SpellPower);                          // Heal Character
        }
    }
}
