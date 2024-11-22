using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGenerator : MonoBehaviour
{
    // Dependancies
    public SpellEffects spellEffects;

    public void GenerateSpell(Spell spell)
    {
        // Set Dependancies
        spell.SetSpellEffects(spellEffects);
    }
}
