using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpkeepable
{
    // Allows gameObject From MonoBehavior
    GameObject gameObject { get; }

    // Returns Targets List For Upkeep Effect
    public List<int> GetUpkeepTargets();

    // Performs Death Effect Using Input List
    public void UpkeepEffect(List<GameObject> targets);
}
