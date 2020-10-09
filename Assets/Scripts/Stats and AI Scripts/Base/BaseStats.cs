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
    public BaseSpell _BS;
    public BattleManager _BM;
    public BattleUIController _BUI;
    public string CharacterName;
    public List<SpellsInfo> availableSpells;
    public bool inBattle;
    public GameObject _DPSSpawnPoint;
    public GameObject characterModel;

    private bool _CounterReady;                  // Is character able to counter physical attacks
    private bool _MCounterReady;                 // Is character able to counter magical attacks

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

    [Header("Buffs")]
    public bool zeal;                   // Increase Physical Damage
    public bool faith;                  // Increase Magical Damage
    public bool haste;                  // Increase speed of Action Bar Charge

    [Header("Status Effects")]
    public bool poison;
    public bool silence;
    public bool slow;

    [Header("Status Resistances")]
    public int poisonResist;
    public int silenceResist;
    public int slowResist;
    #endregion

    //UPDATES
    public virtual void Awake()
    {
        _BS = FindObjectOfType<BaseSpell>();
        _BM = FindObjectOfType<BattleManager>();
        _BUI = FindObjectOfType<BattleUIController>();
    }
    public virtual void Update()
    {
        if (inBattle && !_BUI.endOfFight)
        {
            _ActionBarAmount += _ActionBarRechargeAmount * Time.deltaTime;          // Recharge your action bar so you can take a turn
            _ActionBarAmount = Mathf.Clamp(_ActionBarAmount, 0, 100);

            Poison();
            Slow();
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
            targetCharacter.TakeDamage(damage, false, crit, SpellElement.None);
        } else
        {
            targetCharacter.TakeDamage(0, false, false, SpellElement.None);
        }
        return successful;
    }
    public bool CastMagic(SpellsInfo spell, BaseStats targetCharacter)
    {
        bool successful = currentMP >= spell._SpellManaCost;    // Returns if you have enough mana to cast the spell

        if (successful && !silence)
        {
            currentMP -= spell._SpellManaCost;                  // Because successful, take away mana cost
            // Insert your chosen spellinfo into basespell script.
            // Run the method using the spellinfo inserted.
            if(spell._SpellTarget == SpellTarget.Single)        // Single Target Logic
            {
                _BS.ModulatedSpell(spell, this, targetCharacter);
            }
            else if(spell._SpellTarget == SpellTarget.Multi)    // Multi Target Logic
            {
                if(_BM._ActiveEnemies.Contains(targetCharacter))
                {
                    List<BaseStats> tempList = new List<BaseStats>();    // Temporary placeholders for upcasting
                    BaseStats tempContain;
                    for(int i = 0; i < _BM._ActiveEnemies.Count; i++)    // Upcast and put into placeholders
                    {
                        tempContain = (_BM._ActiveEnemies[i]);
                        tempList.Add(tempContain);
                    }
                    _BS.ModulatedSpell(spell, this, tempList);           // Send Polymorphed list to be used
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
        _BM.UpdatePartyVariables();
        return successful;
    }
    public void Defend()
    {
        defense += (int)(defense * .4f);
        Debug.Log("Increased Defence");
    }                                             // Increase in defence for a turn
    public void UseItem(ItemInfo item, BaseStats targetCharacter)
    {

    }
    public virtual void Die()
    {

    }                                        // Go into die state
    #endregion
    #region Receiver Methods
    public void TakeDamage(int amount, bool magical, bool wasCritical, SpellElement element)
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
                TakeDamage((5 / 100) * maxHP, true, false, SpellElement.None);
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
    [HideInInspector]
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
        Debug.Log(CharacterName + " DIED!");
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

