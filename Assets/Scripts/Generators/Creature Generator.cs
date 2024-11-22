using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureGenerator : MonoBehaviour
{
    // Dependancies
    public CreatureEffects creatureEffects;

    public void GenerateCreature(Creature creature)
    {
        // Set Dependancies
        creature.SetCreatureEffects(creatureEffects);

        // Reset Creature Stats
        creature.ResetStats();
    }
}
