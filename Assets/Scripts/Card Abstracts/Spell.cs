using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : Card
{
    // Dependancies
    public SpellEffects spellEffects;

    // Casting

    protected override void CastingEffects(List<GameObject> targets)
    {
        AddCastingEffects(targets);
        cardManager.HandToGrave(this);
    }

    protected virtual void AddCastingEffects(List<GameObject> targets)
    {

    }

    // Set Type And Stats Text

    protected override string TypeText()
    {
        return "Spell";
    }

    protected override string StatsText()
    {
        return "";
    }

    // Setters

    public void SetSpellEffects(SpellEffects spellEffects)
    {
        this.spellEffects = spellEffects;
    }
}
