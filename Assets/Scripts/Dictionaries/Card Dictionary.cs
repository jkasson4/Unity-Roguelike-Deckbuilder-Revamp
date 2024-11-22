using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDictionary : MonoBehaviour
{
    // Id-Class Dictionary And Inverse Dictionary 
    Dictionary<int, System.Type> cardIds = new Dictionary<int, System.Type>();
    Dictionary<System.Type, int> cardTypes;

    // Initialize Dictionary With Cards
    // Also initializes inverse dictionary for O(1) inverse requests.
    void Awake()
    {
        cardIds.Add(0, typeof(Fireball));
        cardIds.Add(1, typeof(Shield));
        cardIds.Add(50, typeof(Wolf));
        cardIds.Add(51, typeof(LocalGuardsman));

        cardTypes = cardIds.ToDictionary(x => x.Value, x => x.Key);
    }

    // Getters

    public System.Type GetType(int id)
    {
        return cardIds[id];
    }

    public int GetID (System.Type id)
    {
        return cardTypes[id];
    }
}
