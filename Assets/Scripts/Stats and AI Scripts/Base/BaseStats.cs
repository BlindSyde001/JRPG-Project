using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class BaseStats : MonoBehaviour
{
    //VARIABLES
    [HideInInspector]
    public ActionModulator _BS;
    [HideInInspector]
    public BattleManager _BM;
    [HideInInspector]
    public BattleUIController _BUI;
    public string CharacterName;
    public bool inBattle;
    public GameObject _DPSSpawnPoint;
    public GameObject characterModel;

    public bool isAlive;
    public bool attackPiercing;                  // Does the character ignore defense?

    protected float timer = 0;
    protected bool slowLock;
    #region ATB Variables
    public float _ActionBarAmount;                 // Current Charge amount of ATB Gauge
    public float _ActionBarRechargeAmount;         // Current recharge amount specific to player
    #endregion
    #region Stats Sheet
    [Header("Level")]
    [Header("MAIN STATS")]
    public int level;                   // Level used to increase stats. range from 1 - 100
    public int currentXP;               // Amount of XP accumulated
    public int nextLevelXP;             // Amount needed to Level Up

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
    public int maxHP;                   // Determines amount of Damage able to take before being KO'D
    public int maxMP;                   // Determines amount of Spells able to be cast
    public int currentHP;               // Modifiable value for non stats interaction
    public int currentMP;               // Modifiable value for non stats interaction

    [Header("Buffs")]
    public bool zeal;                   // Increase Physical Damage
    public bool faith;                  // Increase Magical Damage
    public bool haste;                  // Increase speed of Action Bar Charge

    [Header("Status Effects")]
    public bool poison;                 // Damage over time
    public bool silence;                // Can't cast spells
    public bool slow;                   // Decrease speed of Action Bar Charge

    [Header("Status Resistances")]
    public int poisonResist;
    public int silenceResist;
    public int slowResist;
    #endregion

    //UPDATES
    public virtual void Awake()
    {
        _BS = FindObjectOfType<ActionModulator>();
        _BM = FindObjectOfType<BattleManager>();
        _BUI = FindObjectOfType<BattleUIController>();
    }
    public virtual void Update()
    {
        if (inBattle && !_BUI.endOfFight)
        {
            _ActionBarAmount += _ActionBarRechargeAmount * Time.deltaTime;          // Recharge your action bar so you can take a turn
            _ActionBarAmount = Mathf.Clamp(_ActionBarAmount, 0, 100);
        }
    }

    //METHODS
    #region Actions
    public bool Attack(BaseStats targetCharacter)
    {
        // Did it hit? Calculate dodge
        int chance;
        chance = 100;
        bool successful = Random.Range(0, 100) < chance;

        if(successful)
        {
            bool crit;
            if (Random.Range(0, 101) < luck)
                crit = true;
            else
                crit = false;
            int damage = (int)(attackPower * Random.Range(1f,1.5f) * (crit ? 1.75 : 1)); // attackPower, randomRoll, crit.
            targetCharacter.TakeDamage(damage, false, crit, ActionElement.None, attackPiercing);
        } else
        {
            targetCharacter.TakeDamage(0, false, false, ActionElement.None, attackPiercing);
        }
        return successful;
    }
    public void UseAction(ActionInfo actionUsed, BaseStats targetCharacter)
    {
        switch (actionUsed._ActionType)
        {
            case ActionType.Spell:
        bool successful = currentMP >= actionUsed._ActionCost;    // Returns if you have enough mana to cast the spell

        if (successful && !silence)
        {
            currentMP -= actionUsed._ActionCost;                  // Because successful, take away mana cost
            // Run the method using the spell inserted.
            if(actionUsed._ActionTarget == ActionTarget.Single)        // Single Target Logic
            {
                _BS.ModulatedAction(actionUsed, this, targetCharacter);
            }
            else if(actionUsed._ActionTarget == ActionTarget.Multi)    // Multi Target Logic
            {
                if(_BM._ActiveEnemies.Contains(targetCharacter))
                {
                    for(int i = 0; i < _BM._ActiveEnemies.Count; i++)    // Upcast and put into placeholders
                    {
                        targetCharacter = (_BM._ActiveEnemies[i]);
                        _BS.ModulatedAction(actionUsed, this, targetCharacter);
                    }
                }
                else if(_BM._ActivePartyMembers.Contains(targetCharacter))
                {
                     for (int i = 0; i < _BM._ActivePartyMembers.Count; i++)
                     {
                        targetCharacter = (_BM._ActivePartyMembers[i]);
                        _BS.ModulatedAction(actionUsed, this, targetCharacter);
                     }
                }
            }
            _ActionBarAmount = 0;
        }
        else if(silence)
        {
            _BUI.MessageOnScreen("Unable to Cast!");
        }
        else
        {
            _BUI.MessageOnScreen("Not Enough MP!");
        }
                break;
            case ActionType.Ability:
                if (actionUsed._ActionTarget == ActionTarget.Single)        // Single Target Logic
                {
                    _BS.ModulatedAction(actionUsed, this, targetCharacter);
                }
                else if (actionUsed._ActionTarget == ActionTarget.Multi)    // Multi Target Logic
                {
                    if (_BM._ActiveEnemies.Contains(targetCharacter))
                    {
                        for (int i = 0; i < _BM._ActiveEnemies.Count; i++)    // Upcast and put into placeholders
                        {
                            targetCharacter = (_BM._ActiveEnemies[i]);
                            _BS.ModulatedAction(actionUsed, this, targetCharacter);
                        }
                    }
                    else if (_BM._ActivePartyMembers.Contains(targetCharacter))
                    {
                        for (int i = 0; i < _BM._ActivePartyMembers.Count; i++)
                        {
                            targetCharacter = (_BM._ActivePartyMembers[i]);
                            _BS.ModulatedAction(actionUsed, this, targetCharacter);
                        }
                    }
                }
                break;
        }
        _BM.UpdatePartyVariables();
    }
    public void UseItem(ConsumableInfo item, BaseStats targetCharacter)
    {

    }
    public virtual void Die()
    {

    }                                        // Go into die state
    #endregion
    #region Receiver Methods
    public void TakeDamage(int amount, bool magical, bool wasCritical, ActionElement element, bool piercing)
    {
        int damageAmount = (int)(amount - (piercing ? (5 + (magical ? magDefense : defense)) : 0) );
        if (damageAmount < 0)
        {
            damageAmount = 0;
        }
        if (damageAmount == 0)
        {
            DamageDisplay(damageAmount, false);
        }
        else
        {
            print(CharacterName + " has taken " + damageAmount + "!");
            currentHP = Mathf.Max(currentHP - damageAmount, 0);              // No going below 0 hp
            _BM.UpdatePartyVariables();
            DamageDisplay(damageAmount, wasCritical);
            if (currentHP == 0)
            {
                Die();
            }
        }
    } // Damage taken by character
    public void HealDamage(int amount, bool wasCritical)
    {
        int healAmount = amount;
        print(amount + " Healed!");
        currentHP = Mathf.Min(currentHP + healAmount, maxHP);   // No overhealing
        print("Added to " + currentHP);
        _BM.UpdatePartyVariables();
    }                              // Healing taken by character
    public void InitiateATB()
    {
        inBattle = true;
    }                                       // For characters in battle, start charging their ATB gauges
    #endregion
    #region UI Display
    private void DamageDisplay(int damageAmount, bool wasCritical)
    {
        if(wasCritical)
        {
            GameObject _damageDisplay = Instantiate(Resources.Load("Prefabs/CriticalDamageText") as GameObject,
                                                   _DPSSpawnPoint.transform.position,
                                                   _DPSSpawnPoint.transform.rotation);
            _damageDisplay.GetComponent<DamageNumbers>().damageToDisplay = damageAmount;
        }
        else if(!wasCritical)
        {
            GameObject _damageDisplay = Instantiate(Resources.Load("Prefabs/DamageText") as GameObject,
                                                _DPSSpawnPoint.transform.position,
                                                _DPSSpawnPoint.transform.rotation);
            _damageDisplay.GetComponent<DamageNumbers>().damageToDisplay = damageAmount;
        }
    }
    #endregion
    #region Status Effects
    public void Condemn()
    {
        // Die after timer countdown
    }
    public void Confusion()
    {
        // No longer in control of player.  
    }
    public void DeathStrike()
    {
        // Chance to instant kill
    }
    public void Frenzy()
    {
        // No longer in control of player. Only uses attack command when atb is full, and attacks random enemy
    }
    public void Petrify()
    {
        // Stops Actions and becomes untargetable by enemy for set period of time. Nothing affects them except for petrify curative effects
    }
    public void Poison()
    {
        // Damage over time
        if(poison)
        {
            timer += Time.deltaTime;
            if(timer >= 10)
            {
                TakeDamage((5 / 100) * maxHP, true, false, ActionElement.None, true);
                timer = 0;
                Debug.Log(CharacterName + " has taken poison Damage!");
            }
        }
    }
    public void Silence()
    {
        // Spells are locked
        // Bool Check in Cast Magic Command
    }
    public void Slow()
    {
        // Decrease ATB charge rate
        if(slow && !slowLock)
        {
            _ActionBarRechargeAmount = 0.75f * _ActionBarRechargeAmount;
            slowLock = true;
        }
    }
    public void Stop()
    {
        // ATB charge rate becomes 0
    }
    public void Taint()
    {
        // All healing becomes damage
    }
    #endregion

    // Used for the UI component to display amount in graphic representation
    #region Level Changing
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
    private void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Battle Scene")
        {
            // Variable declarations
            _BM = FindObjectOfType<BattleManager>();
            _BUI = FindObjectOfType<BattleUIController>();
        }
    }
    #endregion
    public float ActionBarNormalized()
    {
        return _ActionBarAmount / 100;
    }
}
public class BasePartyMember : BaseStats
{
    //VARIABLES
    public StatsDataAsset thisChara;                   // The data asset containing information on the character
    public Sprite characterPortrait;                   // Portrait image displayed in UI
    public int currentLimit;                           // Limit amount of charge for special move
    public List<ActionInfo> availableSpells;
    public List<ActionInfo> availableAbilities;

    #region Growth Stat Base
    [HideInInspector]
    public float growthRateHyper = 0.5f;                      // Assigned to the Hero's Strongest stat
    [HideInInspector]
    public float growthRateStrong = 0.3f;                     // Assigned to the Hero's Secondary stat
    [HideInInspector]
    public float growthRateAverage = 0.2f;                    // Assigned to the Hero's Averaging stat
    [HideInInspector]
    public float growthRateWeak = 0.1f;                       // Assigned to the Hero's Weakest stat
    #endregion
    #region Equipment Items
    [Space]
    [Header("Equipment")]
    public EquipmentInfo Weapon;                       // Provides Main DMG Stat and a Secondary; May have additional Effects
    public EquipmentInfo Armour;                       // Provides Main Def Stat and a Secondary; May have additional Effects
    public EquipmentInfo AccessoryOne;                 // Provides various effects and/or stats
    public EquipmentInfo AccessoryTwo;                 // Provides various effects and/or stats
    #endregion
    #region Equipment Stats
    [HideInInspector]
    public int equipAttackPower;             // Main Damage Modifier for Physical Attacks
    [HideInInspector]
    public int equipMagAttackPower;          // Main Damage Modifier for Magical Attacks
    [HideInInspector]
    public int equipDefense;                 // Main Damage Reduction Modifier for Physical Attacks
    [HideInInspector]
    public int equipMagDefense;              // Main Damage Reduction Modifier for Magical Attacks

    [HideInInspector]
    public int equipStrength;                // Secondary Modifier for Attack, HP
    [HideInInspector]
    public int equipMind;                    // Secondary Modifier for magAttack, MP
    [HideInInspector]
    public int equipVitality;                // Secondary Modifier for Defense, HP
    [HideInInspector]
    public int equipSpirit;                  // Secondary Modifier for magDefense, MP

    [HideInInspector]
    public int equipSpeed;                   // Increases speed of Action Bar being charged as well as initial Action Bar charge amount
    [HideInInspector]
    public int equipLuck;                    // Increases chance of Attacks Critically Striking

    [HideInInspector]
    public int equipHP;                      // Determines amount of Damage able to take before being KO'D
    [HideInInspector]
    public int equipMP;                      // Determines amount of Spells able to be cast
    #endregion
    //UPDATES
    private void Start()
    {
        if (currentHP > 0)
            isAlive = true;
    }
    new void Update()
    {
        if (isAlive)
            base.Update();
    }

    //METHODS
    public virtual void StartOfGameStats()
    {

    }
    public virtual void NextLevel()
    {
        nextLevelXP = (int)(15 * Mathf.Pow(level, 2.3f) + (15 * level));
        if (currentXP >= nextLevelXP)
        {
            level++;
        }
    }
    public override void Die()
    {
        isAlive = false;
        _BM._ActivePartyMembers.Remove(this);
        _BM._DownedMembers.Add(this);
        _BM.UpdatePartyAliveStatus();
        if (_BM._ActivePartyMembers.Count != 0)
        {
            Debug.Log("ON DIE CYCLE");
            _BUI.CycleOnDeathHeroes();
        }
    }
}
public class BaseEnemy : BaseStats
{
    public int targetVariable;
    public BaseStats targetCharacter;
    //UPDATES
    new void Update()
    {
        if (isAlive)
        {
            base.Update();
        }
    }

    //METHODS
    public override void Die()
    {
        Debug.Log(CharacterName + " DIED!");
        isAlive = false;
        int i = _BM._ActiveEnemies.IndexOf(this);
        _BM._ActiveEnemies.Remove(this);       // Remove from targetting list
        _BM._DownedEnemies.Add(this);          // Add to downed list (for XP tally/revives, etc)
        _BUI.SetEnemyTargets();
        FindObjectOfType<BattleManager>().expPool += currentXP; // Add XP amount to total pool
        Destroy(_BM._EnemyModels[i]);
        _BM._EnemyModels.Remove(_BM._EnemyModels[i]);
        if (_BM._ActiveEnemies.Count == 0)   // Win if no more enemies
        {
            _BM.EndOfBattleState();
        }
    }
}

