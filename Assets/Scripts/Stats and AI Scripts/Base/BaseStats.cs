using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class BaseStats : MonoBehaviour
{
    //VARIABLES
    public string CharacterName;
    public List<Spells> availableSpells;
    public bool inBattle;
    public BattleManager _BM;
    public BattleUIController _BUI;
    public GameObject _DPSSpawnPoint;
    public GameObject characterModel;

    private bool _CounterReady;                  // Is character able to counter physical attacks
    private bool _MCounterReady;                 // Is character able to counter magical attacks

    #region ATB Variables
    public float _ActionBarAmount;                 // Current Charge amount of ATB Gauge
    public float _ActionBarRechargeAmount;         // Current recharge amount specific to player
    #endregion
    #region Stats Sheet
    [Header("Level")]
    [Header("MAIN STATS")]
    public int level;                   // Level used to increase stats. range from 1 - 100
    public int totalXP;                 // Amount of XP accumulated
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
    #endregion

    //UPDATES
    public virtual void Awake()
    {
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
        bool successful = (Random.Range(0, 100) < chance);

        if(successful)
        {
            bool crit;
            if (Random.Range(0, 101) < luck)
                crit = true;
            else
                crit = false;
            int damage = (int)(attackPower * Random.Range(1f,1.5f) * (crit ? 1.75 : 1)); // attackPower, randomRoll, crit.
            targetCharacter.TakeDamage(damage, false, crit);
        } else
        {
            targetCharacter.TakeDamage(0, false, false);
        }
        return successful;
    }
    public bool CastMagic(Spells spell, BaseStats targetCharacter)
    {
        bool successful = currentMP >= spell._SpellManaCost;    // Returns if you have enough mana to cast the spell

        if (successful)
        {
            currentMP -= spell._SpellManaCost;                  // Because successful, take away mana cost
            spell.ModulatedSpell(this, targetCharacter);
            _ActionBarAmount = 0;
        }
        else
        {
            _BUI.MessageOnScreen("Not Enough MP!");
        }
        _BM.UpdatePartyVariables();
        return successful;
    }
    public void Defend()
    {
        defense += (int)(defense * .4f);
        Debug.Log("Increased Defence");
    }                                             // Increase in defence for a turn
    public void UseItem(Items item, BaseStats targetCharacter)
    {

    }
    public virtual void Die()
    {

    }                                        // Go into die state
    #endregion
    #region Receiver Methods
    public void TakeDamage(int amount, bool magical, bool wasCritical)
    {
        int damageAmount = (int)(amount - (5 + (magical ? magDefense : defense)) );
        if(damageAmount < 0)
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
    }                // Damage taken by character
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
    #region Debuff
    //public void Petrify()
    //{
    //    //Stops Actions and becomes untargetable by enemy for set period of time. Nothing affects them except for petrify curative effects
    //}
    //public void Taint()
    //{
    //    //All healing becomes damage
    //}
    //public void Slow()
    //{
    //    //Decrease ATB charge rate
    //}
    //public void Stop()
    //{
    //    //atb charge rate becomes 0
    //}
    //public void Frenzy()
    //{
    //    //no longer in control of player. Only uses attack command when atb is full, and attacks random enemy
    //}
    //public void Confusion()
    //{
    //    //no longer in control of player.  
    //}
    //public void Silence()
    //{
    //    //spells are locked
    //}
    //public void Condemn()
    //{
    //    //PROC CHANCE
    //    int strikeChance = 0;
    //    int Proc = Random.Range(0, 100);
    //    int Resist = Random.Range(0, 100);

    //    if(Resist > condemnResist)
    //    {
    //        if(Proc <= strikeChance)
    //        {
    //            condemn = true;
    //        }
    //    }

    //    //EFFECT
    //    float duration = 15f;

    //    if(condemn)
    //    {
    //        duration -= Time.deltaTime;

    //        if (duration <= 0)
    //        {
    //            currentHP = 0;
    //        }
    //    }
    //}
    //public void Poison()
    //{
    //    //PROC CHANCE
    //    int strikeChance = 0;
    //    int Proc = Random.Range(0, 100);
    //    int Resist = Random.Range(0, 100);

    //    if(Resist > poisonResist)
    //    {
    //        if(Proc <= strikeChance)
    //        {
    //            poison = true;
    //        }
    //    }
    //    //EFFECT
    //    if(poison)
    //    {
    //        float timerTick = 0f;
    //        int instanceApplication = 7;

    //        timerTick += Time.deltaTime;

    //        if (timerTick >= instanceApplication)

    //        {
    //            currentHP -= (maxHP * 5 / 100);
    //            timerTick = 0;
    //        }
    //    }
    //}
    //public void Paralyzed()
    //{
        
    //}
    //public void DeathStrike()
    //{
    //    //PROC CHANCE
    //    int strikeChance = 0;
    //    int Proc = Random.Range(0, 100);
    //    int Resist = Random.Range(0, 100);

    //    if (Resist > deathstrikeResist)
    //    {
    //        if(Proc <= strikeChance)
    //        {
    //            deathstrike = true;
    //        }
    //    }

    //    //EFFECT
    //    if(deathstrike)
    //    {
    //        currentHP = 0;
    //        deathstrike = false;
    //    }
    //}
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
    public bool isAlive = true;                        // Check to see if player has not died in battle
    public StatsDataAsset thisChara;                   // The data asset containing information on the character
    public Sprite characterPortrait;                   // Portrait image displayed in UI
    public int currentLimit;                           // Limit amount of charge for special move
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

    //UPDATES
    new void Update()
    {
        if (isAlive)
            base.Update();
    }

    //METHODS
    public void NextLevel()
    {
        nextLevelXP = (int)(15 * Mathf.Pow(level, 2.3f) + (15 * level));
        if (totalXP >= nextLevelXP)
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
    }
}

public class BaseEnemy : BaseStats
{
    public bool isAlive;
    public int x;
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
        isAlive = false;
        int i = _BM._ActiveEnemies.IndexOf(this);
        _BM._ActiveEnemies.Remove(this);       // Remove from targetting list
        _BM._DownedEnemies.Add(this);          // Add to downed list (for XP tally/revives, etc)
        _BUI.SetEnemyTargets();
        FindObjectOfType<BattleManager>().expPool += totalXP; // Add XP amount to total pool
        Destroy(_BM._EnemyModels[i]);
        _BM._EnemyModels.Remove(_BM._EnemyModels[i]);
        if (_BM._ActiveEnemies.Count == 0)   // Win if no more enemies
        {
            _BM.VictoryState();
        }
    }
}

