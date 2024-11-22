using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Dependancies
    public EnemyDictionary enemyDictionary;
    public EnemyManager enemyManager;
    public SelectionManager selectionManager;
    public EnemyEffects enemyEffects;
    public GameObject baseCard;

    // Starting Position And Rotation
    private Vector3 starting_pos = new Vector3(8, 0, -1);
    private Quaternion starting_rot = Quaternion.Euler(-90, 0, 0);

    public Enemy GenerateEnemy(int id, int active_turn)
    {
        // Instantiate Card From Base Card
        GameObject newCard = GameObject.Instantiate(baseCard);

        // Set Position And Rotation
        newCard.transform.position = starting_pos;
        newCard.transform.rotation = starting_rot;

        // Apply Enemy Class From Id
        newCard.AddComponent(enemyDictionary.GetType(id));

        // Set Dependancies
        newCard.GetComponent<Enemy>().SetEnemyManager(enemyManager);
        newCard.GetComponent<Enemy>().SetSelectionManager(selectionManager);
        newCard.GetComponent<Enemy>().SetEnemyEffects(enemyEffects);

        // Apply Movement Class
        newCard.AddComponent(typeof(CardMovement));
        newCard.AddComponent(typeof(CardHighlight));
        newCard.GetComponent<CardHighlight>().SetCardMovement(newCard.GetComponent<CardMovement>());

        // Set Enemy Active Turn
        newCard.GetComponent<Enemy>().SetActiveTurn(active_turn);

        // Reset Stats And Action Flag
        newCard.GetComponent<Enemy>().ResetStats();
        newCard.GetComponent<Enemy>().SetActionFlag(false);

        // Update Text
        newCard.GetComponent<Enemy>().UpdateText();

        return newCard.GetComponent<Enemy>();
    }
}
