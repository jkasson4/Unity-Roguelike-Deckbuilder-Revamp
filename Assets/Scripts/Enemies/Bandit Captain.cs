using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCaptain : Enemy
{
    protected override string enemy_name { get { return "Bandit Captain"; } }
    protected override string enemy_type { get { return "Human"; } }
    protected override int starting_attack { get { return 3; } }
    protected override int starting_health { get { return 5; } }
    protected override int starting_defense { get { return 0; } }
}
