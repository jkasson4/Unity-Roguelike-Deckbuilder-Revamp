using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectionAI : MonoBehaviour
{
    // Dependancies
    public CardManager cardManager;
    public EnemyManager enemyManager;
    public PlayerManager playerManager;

    public List<GameObject> GetTargets(List<int> in_targets)
    {
        List<GameObject> out_targets = new List<GameObject>();

        // Read selection type, get and shuffle list of valid targets
        for (int i = 0; i < in_targets.Count; i++)
        {
            switch (in_targets[i])
            {
                // Active IDamageable
                // Checks for random valid creature. Targets player if no valid target found.
                case 0:
                    List<Card> play_cards = cardManager.GetPlayCards();
                    Shuffle<Card>(play_cards);
                    bool found = false;
                    for (int j = 0; j < play_cards.Count; j++)
                    {
                        if (play_cards[j].GetComponent<IDamageable>() != null && play_cards[j].GetComponent<IDamageable>().GetActive() == true
                            && out_targets.Contains(play_cards[j].gameObject) == false)
                        {
                            out_targets.Add(play_cards[j].gameObject);
                            break;
                        }
                    }
                    if (found == false)
                    {
                        out_targets.Add(playerManager.gameObject);
                    }
                    break;
                // Card In Hand
                // Adds random card in hand if a card is present.
                case 3:
                    List<Card> hand_cards = cardManager.GetHandCards();
                    Shuffle<Card>(hand_cards);
                    if (hand_cards.Count > 0)
                    {
                        out_targets.Add(hand_cards[0].gameObject);
                    }
                    break;
                default:
                    Debug.LogError("No matching selection type for type " + in_targets[0]);
                    break;
            }
        }
        return out_targets;
    }

    // Shuffles Generic List
    // Allows for random selection target, parsing over all options until valid target is found.

    private List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int random = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[random];
            list[random] = temp;
        }

        return list;
    }
}
