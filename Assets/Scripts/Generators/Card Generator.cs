using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    // Dependancies
    public SpellGenerator spellGenerator;
    public CreatureGenerator creatureGenerator;
    public CardDictionary cardDictionary;
    public CardManager cardManager;
    public PlayerManager playerManager;
    public SelectionManager selectionManager;
    public GameObject baseCard;

    // Starting Position And Rotation
    private Vector3 starting_pos = new Vector3(8, 0, -1);
    private Quaternion starting_rot = Quaternion.Euler(-90, 0, 0);

    public Card GenerateCard(int id)
    {
        // Instantiate Card From Base Card
        GameObject newCard = GameObject.Instantiate(baseCard);

        // Set Position and Rotation
        newCard.transform.position = starting_pos;
        newCard.transform.rotation = starting_rot;

        // Apply Card Class From Id
        newCard.AddComponent(cardDictionary.GetType(id));

        // Set Dependancies
        newCard.GetComponent<Card>().SetCardManager(cardManager);
        newCard.GetComponent<Card>().SetPlayerManager(playerManager);
        newCard.GetComponent<Card>().SetSelectionManager(selectionManager);

        // Apply Movement Class
        newCard.AddComponent(typeof(CardMovement));
        newCard.AddComponent(typeof(CardHighlight));
        newCard.GetComponent<CardHighlight>().SetCardMovement(newCard.GetComponent<CardMovement>());

        // Generate Sub-Type
        if (newCard.GetComponent<Spell>() != null)
        {
            spellGenerator.GenerateSpell(newCard.GetComponent<Spell>());
        }
        else
        {
            creatureGenerator.GenerateCreature(newCard.GetComponent<Creature>());
        }

        // Update Text
        newCard.GetComponent<Card>().UpdateText();

        return newCard.GetComponent<Card>();
    }
}
