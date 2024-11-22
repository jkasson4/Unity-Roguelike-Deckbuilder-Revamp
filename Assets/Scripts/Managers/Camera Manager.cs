using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Dependancies
    public GameObject combat_default;
    public GameObject combat_creatures;
    private CardMovement cardMovement;

    // Camera Positions
    private int[] combat_default_pos = new int[] { 0, 0 };
    private int[] combat_creatures_pos = new int[] { 0, 1 };

    // Current Position
    private int[] current_position = new int[2];

    // Initialize Starting Position And Movement Settings

    void Awake()
    {
        cardMovement = GetComponent<CardMovement>();
        cardMovement.SetSettings(2.5f, 3.2f, 0f, 0f);
    }

    void Start()
    {
        ToDefault();
    }

    // Move Camera Between Positions On Button Press

    void Update()
    {
        if (Input.GetKeyDown("w") == true)
        {
            if (current_position == combat_default_pos)
            {
                ToCreatures();
            }
        }
        else if (Input.GetKeyDown("s") == true)
        {
            if (current_position == combat_creatures_pos)
            {
                ToDefault();
            }
        }
    }

    // Set Position On Combat Start And End

    public void StartCombat()
    {
        ToDefault();
    }

    public void EndCombat()
    {
        ToDefault();
    }

    // Set Target Position And Rotation For Position

    private void ToDefault()
    {
        current_position = combat_default_pos;
        cardMovement.SetTargetPos(combat_default.transform.position);
        cardMovement.SetTargetRot(combat_default.transform.rotation);
    }

    private void ToCreatures()
    {
        current_position = combat_creatures_pos;
        cardMovement.SetTargetPos(combat_creatures.transform.position);
        cardMovement.SetTargetRot(combat_creatures.transform.rotation);
    }
}
