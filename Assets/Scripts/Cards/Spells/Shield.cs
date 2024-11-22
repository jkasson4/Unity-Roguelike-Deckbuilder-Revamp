using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Spell
{
    protected override string card_name { get { return "Shield"; } }
    protected override int card_cost { get { return 1; } }
    protected override string card_type { get { return "Arcane"; } }
    protected override string card_description { get { return "Give " + spellEffects.GetDamage(base_defense) + " defense to any target.\n" +
                                                              "Gain " + spellEffects.GetDamage(base_spell_buff) + " spell buff."; } }

    private int base_defense = 3;
    private int base_spell_buff = 1;

    protected override List<int> CastingTargets()
    {
        return new List<int>() { 1 };
    }

    protected override void AddCastingEffects(List<GameObject> targets)
    {
        spellEffects.Buff(targets, 0, base_defense, 0);
        spellEffects.BuffPlayer(0, 0, base_spell_buff);
    }
}
