using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathable
{
    // Allows gameObject From MonoBehavior
    GameObject gameObject { get; }

    // Returns Targets List For Death Effect
    public List<int> GetDeathTargets();

    // Performs Death Effect Using Input List
    public void DeathEffect(List<GameObject> targets);

    // Check If Object Active
    public bool GetActive();
}
