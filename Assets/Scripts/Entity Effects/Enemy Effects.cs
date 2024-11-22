using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    // Applying Damage To Targets

    public void DealDamage(GameObject target, int damage)
    {
        if (target != null)
        {
            target.GetComponent<IDamageable>().DealDamage(damage);
        }
    }
}
