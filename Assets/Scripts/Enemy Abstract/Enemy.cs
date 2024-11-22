using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IBuffable, IDeathable, IUpkeepable
{
    // Dependancies
    public EnemyManager enemyManager;
    public SelectionManager selectionManager;
    public EnemyEffects enemyEffects;

    // Abstract Variables - Child Needs To Set
    protected abstract string enemy_name { get; }
    protected abstract string enemy_type { get; }
    protected abstract int starting_attack { get; }
    protected abstract int starting_health { get; }

    // Virtual Variables - Child May Optionally To Set
    protected virtual string enemy_description { get { return ""; } }
    protected virtual string enemy_flavor { get { return ""; } }
    protected virtual int starting_defense { get { return 0; } }
    protected virtual int reset_defense { get { return 0; } }

    // Current Enemy Variables
    [SerializeField] private bool active;
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private int defense;
    [SerializeField] private int active_turn;
    [SerializeField] private bool action_flag;

    // Action Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetActionTargets()
    {
        return ActionTargets();
    }

    protected virtual List<int> ActionTargets()
    {
        return new List<int>() { 0 };
    }

    public void ActionEffect(List<GameObject> targets)
    {
        enemyEffects.DealDamage(targets[0], attack);
        AddActionEffects(targets);
    }
    protected virtual void AddActionEffects(List<GameObject> targets)
    {
        
    }

    // Upkeep Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetUpkeepTargets()
    {
        return UpkeepTargets();
    }

    protected virtual List<int> UpkeepTargets()
    {
        return new List<int>();
    }

    public void UpkeepEffect(List<GameObject> targets)
    {
        if (GetActive() == true)
        {
            AddUpkeepEffect(targets);
        }
    }

    protected virtual void AddUpkeepEffect(List<GameObject> targets)
    {

    }

    // Death Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetDeathTargets()
    {
        return DeathTargets();
    }

    protected virtual List<int> DeathTargets()
    {
        return new List<int>();
    }

    public void DeathEffect(List<GameObject> targets)
    {
        ResetStats();
        enemyManager.PlayToGrave(this);
        AddDeathEffect(targets);
    }

    protected virtual void AddDeathEffect(List<GameObject> targets)
    {

    }

    // Initialize Death

    private void OnNoHealth()
    {
        active = false;
        enemyManager.RaiseInPlay(this);
        selectionManager.SendDeath(this);

        // Stop Particles
        GetComponent<CardEmitter>().StopActionsRemaining();
    }

    // Damage Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetDamageTargets()
    {
        return DamageTargets();
    }

    protected virtual List<int> DamageTargets()
    {
        return new List<int>();
    }

    public void DamageEffect(List<GameObject> targets)
    {
        AddDamageEffect(targets);
    }

    protected virtual void AddDamageEffect(List<GameObject> targets)
    {

    }

    // IDamageable

    // Prioritize decreasing defense, select and active damage effect, and send death if health <= 0.
    public void DealDamage(int damage)
    {
        if (damage <= defense)
        {
            defense -= damage;
        }
        else
        {
            if (defense > 0)
            {
                health = health + defense - damage;
            }
            else
            {
                health -= damage;
            }
        }
        UpdateStats();
        selectionManager.SendDamage(this);
        if (health < 1)
        {
            OnNoHealth();
        }
    }

    public bool GetActive()
    {
        return enemyManager.PlayContains(this) && active;
    }

    public void ResetStats()
    {
        active = false;
        health = starting_health;
        defense = starting_defense;
        attack = starting_attack;

        UpdateStats();
    }

    public void ResetDefense()
    {
        defense = reset_defense;

        UpdateStats();
    }

    // IBuffable

    public void IncreaseAttack(int attack)
    {
        this.attack += attack;

        UpdateStats();
    }

    public void IncreaseHealth(int health)
    {
        this.health += health;

        UpdateStats();
    }

    public void IncreaseDefense(int defense)
    {
        this.defense += defense;

        UpdateStats();
    }

    // Active

    public void Activate()
    {
        active = true;
        GetComponent<CardEmitter>().PlayActionsRemaining();
    }

    // Update Enemy Text

    public void UpdateText()
    {
        CardText cardText = GetComponent<CardText>();

        cardText.SetCardName(enemy_name);
        cardText.SetCardCost("");
        cardText.SetCardType("Enemy - " + enemy_type);
        cardText.SetCardText(enemy_description, enemy_flavor);
        if (defense > 0)
        {
            cardText.SetCardStats(attack + " / " + health + " + " + defense);
        }
        else
        {
            cardText.SetCardStats(attack + " / " + health);
        }
    }

    public void UpdateStats()
    {
        CardText cardText = GetComponent<CardText>();

        if (defense > 0)
        {
            cardText.SetCardStats(attack + " / " + health + " + " + defense);
        }
        else
        {
            cardText.SetCardStats(attack + " / " + health);
        }
    }

    // Setters

    public void SetEnemyManager(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
    }

    public void SetSelectionManager(SelectionManager selectionManager)
    {
        this.selectionManager = selectionManager;
    }

    public void SetEnemyEffects(EnemyEffects enemyEffects)
    {
        this.enemyEffects = enemyEffects;
    }

    public void SetActiveTurn(int active_turn)
    {
        this.active_turn = active_turn;
    }

    public void SetActionFlag(bool action_flag)
    {
        this.action_flag = action_flag;

        // Play Particles If Action Remaining
        if (action_flag == true)
        {
            GetComponent<CardEmitter>().PlayActionsRemaining();
        }
        else
        {
            GetComponent<CardEmitter>().StopActionsRemaining();
        }
    }

    // Getters

    public int GetActiveTurn()
    {
        return active_turn;
    }

    public bool GetActionFlag()
    {
        return action_flag;
    }
}
