using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEffects : MonoBehaviour
{
    // Applying Damage To Targets

    public void DealDamage(GameObject target, int damage)
    {
        if (target != null)
        {
            target.GetComponent<IDamageable>().DealDamage(damage);
        }
    }

    // Buffing Targets

    public void Buff(GameObject target, int health, int defense, int attack)
    {
        if (target != null)
        {
            target.GetComponent<IBuffable>().IncreaseHealth(health);
            target.GetComponent<IBuffable>().IncreaseDefense(defense);
            target.GetComponent<IBuffable>().IncreaseAttack(attack);
        }
    }

    public void Buff(List<GameObject> targets, int health, int defense, int attack)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Buff(targets[i], health, defense, attack);
        }
    }
}
