using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementTurnOnClick : MonoBehaviour
{
    public CombatManager combatManager;

    void OnMouseDown()
    {
        combatManager.TestIncrementTurn();
    }
}
