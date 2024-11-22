using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Allows gameObject From MonoBehavior
    GameObject gameObject { get; }

    // Returns Targets List For Damage Effect
    public List<int> GetDamageTargets();

    // Performs Death Effect Using Input List
    public void DamageEffect(List<GameObject> targets);

    // Object Is Dealt Damage
    public void DealDamage(int damage);

    // Check if Oject Active
    public bool GetActive();

    // Reste Object Stats
    public void ResetStats();
}
