using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpellBuffOnClick : MonoBehaviour
{
    public PlayerManager playerManager;

    void OnMouseDown()
    {
        playerManager.IncreaseSpellBuff(1);
    }
}
