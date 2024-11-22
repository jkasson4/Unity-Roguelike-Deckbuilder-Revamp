using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable, IBuffable
{
    // Dependancies
    public CardManager cardManager;
    public TMP_Text mana_text;
    public TMP_Text health_text;
    public TMP_Text spell_buff_text;

    // Draw Variables
    private int starting_draw = 3;
    private int normal_draw = 2;

    // Player Max Stats
    private int max_health = 5;
    private int reset_defense = 0;
    private int max_mana = 3;
    private int starting_spell_buff = 0;

    // Player Current Stats
    private int health;
    private int defense;
    private int mana;
    private int spell_buff;

    // Reset Stats

    public void ResetHealth()
    {
        health = max_health;
        UpdateHealthText();
    }

    public void ResetDefense()
    {
        defense = reset_defense;
        UpdateHealthText();
    }

    public void ResetMana()
    {
        mana = max_mana;
        UpdateManaText();
    }

    public void ResetSpellBuff()
    {
        spell_buff = starting_spell_buff;
        UpdateSpellBuffText();
    }

    // Update Text

    private void UpdateHealthText()
    {
        if (defense > 0)
        {
            health_text.text = "Health:\n" + health.ToString() + " + " + defense.ToString();
        }
        else
        {
            health_text.text = "Health:\n" + health.ToString();
        }
    }

    private void UpdateManaText()
    {
        mana_text.text = "Mana:\n" + mana.ToString();
    }

    private void UpdateSpellBuffText()
    {
        spell_buff_text.text = "Spell Buff:\n" + spell_buff.ToString();
    }

    // IDamageable

    public List<int> GetDamageTargets()
    {
        return new List<int>();
    }

    public void DamageEffect(List<GameObject> targets)
    {

    }

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
        UpdateHealthText();
    }

    public bool GetActive()
    {
        return health > 0;
    }

    public void ResetStats()
    {
        ResetHealth();
        ResetMana();
        ResetSpellBuff();
    }

    // IBuffable

    public void IncreaseAttack(int buff)
    {
        spell_buff += buff;
        cardManager.UpdateAllCardTexts();
        UpdateSpellBuffText();
    }

    public void IncreaseHealth(int health)
    {
        if (health + this.health > max_health)
        {
            health = max_health;
        }
        else
        {
            this.health += health;
        }

        UpdateHealthText();
    }

    public void IncreaseDefense(int defense)
    {
        this.defense += defense;

        UpdateHealthText();
    }

    // Setters

    public void IncreaseMana(int mana)
    {
        this.mana += mana;
        UpdateManaText();
    }

    public void IncreaseSpellBuff(int spell_buff)
    {
        this.spell_buff += spell_buff;
        cardManager.UpdateAllCardTexts();
        UpdateSpellBuffText();
    }

    // Getters

    public int GetMana()
    {
        return mana;
    }

    public int GetSpellBuff()
    {
        return spell_buff;
    }

    public int GetStartingDraw()
    {
        return starting_draw;
    }

    public int GetNormalDraw()
    {
        return normal_draw;
    }
}
