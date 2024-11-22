using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    // Allows gameObject From MonoBehavior
    GameObject gameObject { get; }

    // Increases Attack Stat
    public void IncreaseAttack(int amount);

    // Increases Health Stat
    public void IncreaseHealth(int amount);

    // Increases Defense Stat
    public void IncreaseDefense(int amount);

    // Check if Oject Active
    public bool GetActive();
}
