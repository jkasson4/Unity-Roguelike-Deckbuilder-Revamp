using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Card, IDamageable, IBuffable, IDeathable
{
    // Dependancies
    public CreatureEffects creatureEffects;

    // Abstract Variables - Child Needs To Set
    protected abstract int starting_attack { get; }
    protected abstract int starting_health { get; }

    // Virtual Variables - Child May Optionally To Set
    protected virtual int starting_defense { get { return 0; } }
    protected virtual int reset_defense { get { return 0; } }
    protected virtual int starting_actions { get { return 0; } }
    protected virtual int max_actions { get { return 1; } }

    // Current Creature Variables
    [SerializeField] private bool active;
    [SerializeField] protected int attack;
    [SerializeField] private int health;
    [SerializeField] private int defense;
    [SerializeField] private int actions;

    // Casting Effects - Child May Add Additional Effects

    protected override void CastingEffects(List<GameObject> targets)
    {
        AddCastingEffects(targets);
        cardManager.HandToPlay(this);
        active = true;

        // Play Particles If Action Remaining
        if (actions > 0)
        {
            GetComponent<CardEmitter>().PlayActionsRemaining();
        }
    }

    protected virtual void AddCastingEffects(List<GameObject> targets)
    {

    }

    // Attacking Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetAttackTargets()
    {
        return AttackTargets();
    }

    protected virtual List<int> AttackTargets()
    {
        return new List<int>() { 0 };
    }

    public void AttackEffect(List<GameObject> targets)
    {
        actions -= 1;
        creatureEffects.DealDamage(targets[0], attack);
        AddAttackEffect(targets);

        // Stop Particles If No Action Remaining
        if (actions < 1)
        {
            GetComponent<CardEmitter>().StopActionsRemaining();
        }
    }

    protected virtual void AddAttackEffect(List<GameObject> targets)
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
        cardManager.PlayToGrave(this);
        AddDeathEffect(targets);
    }

    protected virtual void AddDeathEffect(List<GameObject> targets)
    {

    }

    // Initiate Death

    private void OnNoHealth()
    {
        active = false;
        cardManager.RaiseInPlay(this);
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
        return cardManager.PlayContains(this) && active;
    }

    public void ResetStats()
    {
        active = false;
        health = starting_health;
        defense = starting_defense;
        attack = starting_attack;
        actions = starting_actions;

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

    // Reset

    public void ResetDefense()
    {
        defense = reset_defense;

        UpdateStats();
    }

    public void ResetActions()
    {
        actions = max_actions;

        // Play Particles If Action Remaining
        if (actions > 0)
        {
            GetComponent<CardEmitter>().PlayActionsRemaining();
        }
    }

    // Card Text

    protected override string TypeText()
    {
        return "Creature";
    }

    protected override string StatsText()
    {
        if (defense > 0)
        {
            return attack + " / " + health + " + " + defense;
        }
        else
        {
            return attack + " / " + health;
        }
    }

    // Setters

    public void SetCreatureEffects(CreatureEffects creatureEffects)
    {
        this.creatureEffects = creatureEffects;
    }

    // Getters

    public int GetActions()
    {
        return actions;
    }
}
