using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnOnClick : MonoBehaviour
{
    public CombatManager combatManager;

    void OnMouseDown()
    {
        combatManager.PlayerEndTurn();
    }
}
