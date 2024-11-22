using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDictionary : MonoBehaviour
{
    // Id-Class Dictionary And Inverse Dictionary
    Dictionary<int, System.Type> enemyIds = new Dictionary<int, System.Type>();
    Dictionary<System.Type, int> enemyTypes;

    // Initialize Dictionary With Enemies
    // Also initializes inverse dictionary for O(1) inverse requests.
    void Awake()
    {
        enemyIds.Add(0, typeof(Bandit));
        enemyIds.Add(1, typeof(BanditCaptain));

        enemyTypes = enemyIds.ToDictionary(x => x.Value, x => x.Key);
    }

    // Getters

    public System.Type GetType(int id)
    {
        return enemyIds[id];
    }

    public int GetID(System.Type id)
    {
        return enemyTypes[id];
    }
}
