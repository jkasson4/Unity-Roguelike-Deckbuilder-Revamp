using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombatOnClick : MonoBehaviour
{
    // Dependancies
    public GameManager gameManager;

    // Combat To Start
    public int combat_id;

    void OnMouseDown()
    {
        gameManager.BeginCombat(combat_id);
    }
}
