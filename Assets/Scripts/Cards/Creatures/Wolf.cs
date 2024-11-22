using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Creature
{
    protected override string card_name { get { return "Wolf"; } }
    protected override int card_cost { get { return 1; } }
    protected override string card_type { get { return "Beast"; } }

    protected override int starting_attack { get { return 3; } }
    protected override int starting_health { get { return 2; } }
    protected override int starting_defense { get { return 0; } }
}
