using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Dependancies
    public CombatManager combatManager;
    public GameObject combatUI;
    public GameObject combatButtons;
    public GameObject defaultUI;
    public GameObject defaultButtons;

    // Initialize UI

    public void Awake()
    {
        Default();
    }

    // Check For Exit And Skip Presses

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("return"))
        {
            EndCombat();
        }
    }

    // Initiate/End Combat And Set UI

    public void BeginCombat(int id)
    {
        combatManager.InitiateCombat(id);
        Combat();
    }

    public void EndCombat()
    {
        combatManager.ResetCombat();
        Default();
    }

    // Adjust UI

    private void Combat()
    {
        combatUI.SetActive(true);
        combatButtons.SetActive(true);
        defaultUI.SetActive(false);
        defaultButtons.SetActive(false);
    }

    private void Default()
    {
        combatUI.SetActive(false);
        combatButtons.SetActive(false);
        defaultUI.SetActive(true);
        defaultButtons.SetActive(true);
    }
}
