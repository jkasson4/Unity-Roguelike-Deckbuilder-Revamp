using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlight : MonoBehaviour
{
    /* ---------------------------------------
     * Wrapper for Card Movement.
     * Enables seperate stored default targets
     * and targets during active mouse-hover.
     * ---------------------------------------
     */

    // Dependancies
    public CardMovement cardMovement;

    // Target Positions
    private Vector3 origin_pos;
    private Vector3 highlight_pos;
    private Quaternion origin_rot;
    private Quaternion highlight_rot;

    // Active Mouse Hover
    private bool mouse_over;

    // Set Targets To Default
    void Start()
    {
        cardMovement.SetTargetPos(origin_pos);
        cardMovement.SetTargetRot(origin_rot);
        mouse_over = false;
    }

    // Sets Targets To Highlighted On Mouse Hover
    void OnMouseEnter()
    {
        cardMovement.SetTargetPos(highlight_pos);
        cardMovement.SetTargetRot(highlight_rot);
        mouse_over = true;
    }

    // Sets Targets To Default On Mouse Hover Stops
    void OnMouseExit()
    {
        cardMovement.SetTargetPos(origin_pos);
        cardMovement.SetTargetRot(origin_rot);
        mouse_over = false;
    }

    // Update Movement Based On Mouse Hover State

    private void UpdateMovement()
    {
        if (mouse_over)
        {
            cardMovement.SetTargetPos(highlight_pos);
            cardMovement.SetTargetRot(highlight_rot);
        }
        else
        {
            cardMovement.SetTargetPos(origin_pos);
            cardMovement.SetTargetRot(origin_rot);
        }
    }

    // Setters

    public void SetOriginPos(Vector3 input)
    {
        origin_pos = input;
        UpdateMovement();
    }

    public void SetHighlightPos(Vector3 input)
    {
        highlight_pos = input;
        UpdateMovement();
    }

    public void SetOriginRot(Quaternion input)
    {
        origin_rot = input;
        UpdateMovement();
    }

    public void SetHighlightRot(Quaternion input)
    {
        highlight_rot = input;
        UpdateMovement();
    }

    public void SetCardMovement(CardMovement cardMovement)
    {
        this.cardMovement = cardMovement;
    }
}
