using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnClick : MonoBehaviour
{
    public CardManager cardManager;

    void OnMouseDown()
    {
        cardManager.DeckToHand();
    }
}
