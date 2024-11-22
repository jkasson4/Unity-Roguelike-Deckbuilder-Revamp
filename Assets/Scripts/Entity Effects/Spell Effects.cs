using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffects : MonoBehaviour
{
    // Dependancies
    public PlayerManager playerManager;

    // Applying Damage To Targets

    public void DealDamage(GameObject target, int damage)
    {
        if(target != null)
        {
            target.GetComponent<IDamageable>().DealDamage(damage + playerManager.GetSpellBuff());
        }
    }

    public void DealDamage(List<GameObject> targets, int damage)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            DealDamage(targets[i], damage);
        }
    }

    // Buffing Targets

    public void Buff(GameObject target, int health, int defense, int attack)
    {
        if (target != null)
        {
            target.GetComponent<IBuffable>().IncreaseHealth(health + playerManager.GetSpellBuff() * (health > 0 ? 1 : 0));
            target.GetComponent<IBuffable>().IncreaseDefense(defense + playerManager.GetSpellBuff() * (defense > 0 ? 1 : 0));
            target.GetComponent<IBuffable>().IncreaseAttack(attack + playerManager.GetSpellBuff() * (attack > 0 ? 1 : 0));
        }
    }

    public void Buff(List<GameObject> targets, int health, int defense, int attack)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Buff(targets[i], health, defense, attack);
        }
    }

    // Buffing Player

    public void BuffPlayer(int health, int defense, int attack)
    {
        playerManager.IncreaseHealth(health + playerManager.GetSpellBuff() * (health > 0 ? 1 : 0));
        playerManager.IncreaseDefense(defense + playerManager.GetSpellBuff() * (defense > 0 ? 1 : 0));
        playerManager.IncreaseAttack(attack + playerManager.GetSpellBuff() * (attack > 0 ? 1 : 0));
    }

    // Getters

    // Applies player's spell buff to card damage.
    public int GetDamage(int damage)
    {
        return damage + playerManager.GetSpellBuff();
    }
}
