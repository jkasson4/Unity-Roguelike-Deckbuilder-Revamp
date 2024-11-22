using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Spell
{
    protected override string card_name { get { return "Fireball"; } }
    protected override int card_cost { get { return 1; } }
    protected override string card_type { get { return "Fire"; } }
    protected override string card_description { get { return "Deal " + spellEffects.GetDamage(base_damage) + " damage to any target."; } }

    private int base_damage = 3;

    protected override List<int> CastingTargets()
    {
        return new List<int>() { 0 };
    }

    protected override void AddCastingEffects(List<GameObject> targets)
    {
        spellEffects.DealDamage(targets, base_damage);
    }
}
