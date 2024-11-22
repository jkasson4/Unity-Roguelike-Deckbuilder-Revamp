using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGuardsman : Creature
{
    protected override string card_name { get { return "Local Guardsman"; } }
    protected override int card_cost { get { return 2; } }
    protected override string card_type { get { return "Human"; } }
    protected override string card_description { get { return "Upkeep: Local Guardsman and another target both gain 3 defense"; } }

    protected override int starting_attack { get { return 2; } }
    protected override int starting_health { get { return 2; } }
    protected override int starting_defense { get { return 3; } }

    protected override List<int> UpkeepTargets()
    {
        return new List<int>() { 0 };
    }

    protected override void AddUpkeepEffect(List<GameObject> targets)
    {
        creatureEffects.Buff(targets, 0, 3, 0);
        creatureEffects.Buff(this.gameObject, 0, 3, 0);
    }
}
