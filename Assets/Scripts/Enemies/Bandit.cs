using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    protected override string enemy_name { get { return "Bandit"; } }
    protected override string enemy_type { get { return "Human"; } }
    protected override int starting_attack { get { return 2; } }
    protected override int starting_health { get { return  3; } }
    protected override int starting_defense { get { return 0; } }
}
