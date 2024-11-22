using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Dependancies
    public CardGenerator cardGenerator;
    public SelectionManager selectionManager;

    // Markers
    public GameObject list_marker;
    public GameObject deck_marker;
    public GameObject hand_marker;
    public GameObject play_marker;
    public GameObject grave_marker;

    // Card Zone Lists
    private List<Card> list = new List<Card>();
    private List<Card> deck = new List<Card>();
    private List<Card> hand = new List<Card>();
    private List<Card> play = new List<Card>();
    private List<Card> grave = new List<Card>();

    // Zone-Specific Card Spacing and Rotation
    private float card_height = 0.015f;
    private float grave_distance = 0.3f;
    private int hand_distance = 3;
    private int play_distance = 6;
    private float death_rise = 0.3f;
    private Vector3 hand_highlight_move = new Vector3(0, 0.8f, -0.2f);
    private Quaternion deck_rot = Quaternion.Euler(-90, 0, 0);
    private Quaternion hand_origin_rot = Quaternion.Euler(15, 0, 0);
    private Quaternion hand_highlight_rot = Quaternion.Euler(25, 0, 0);
    private Quaternion grave_rot = Quaternion.Euler(90, 0, 0);

    // Testing Deck Initialization

    public void LoadList(List<int> card_ids)
    {
        for (int i = 0; i < card_ids.Count; i++)
        {
            AddListCard(card_ids[i]);
        }
    }

    public void ResetList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i].gameObject);
        }
        list.Clear();
    }

    // Adding Cards To Zones

    public void AddListCard(Card card)
    {
        list.Add(card);
        AdjustList();
    }

    public void AddListCard(int id)
    {
        AddListCard(cardGenerator.GenerateCard(id));
    }

    public void AddDeckCard(Card card)
    {
        deck.Add(card);
        AdjustDeck();
    }

    public void AddDeckCard(int id)
    {
        AddDeckCard(cardGenerator.GenerateCard(id));
    }

    public void AddHandCard(Card card)
    {
        hand.Add(card);
        AdjustHand();
    }

    public void AddHandCard(int id)
    {
        AddHandCard(cardGenerator.GenerateCard(id));
    }

    // Transferring Cards Between Zones

    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DeckToHand();
        }
    }

    public void DeckToHand()
    {
        if (deck.Count > 0)
        {
            hand.Add(deck[deck.Count - 1]);
            deck.RemoveAt(deck.Count - 1);
            AdjustDeck();
            AdjustHand();
        }
    }

    public void HandToGrave(int pos)
    {
        if (hand.Count > pos && pos > -1)
        {
            grave.Add(hand[pos]);
            hand.RemoveAt(pos);
            AdjustHand();
            AdjustGrave();
        }
    }

    public void HandToGrave(Card card)
    {
        if (hand.Contains(card))
        {
            HandToGrave(hand.FindIndex(a => a == card));
        }
    }

    public void HandToPlay(int pos)
    {
        if (hand.Count > pos && pos > -1)
        {
            play.Add(hand[pos]);
            hand.RemoveAt(pos);
            AdjustHand();
            AdjustPlay();
        }
    }

    public void HandToPlay(Card card)
    {
        if (hand.Contains(card))
        {
            HandToPlay(hand.FindIndex(a => a == card));
        }
    }

    public void PlayToGrave(int pos)
    {
        if(play.Count > pos && pos > -1)
        {
            grave.Add(play[pos]);
            play.RemoveAt(pos);
            AdjustPlay();
            AdjustGrave();
        }
    }

    public void PlayToGrave(Card card)
    {
        if (play.Contains(card))
        {
            PlayToGrave(play.FindIndex(a => a == card));
        }
    }

    // Reset Zones

    public void Reset()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Destroy(deck[i].gameObject);
        }
        deck.Clear();
        for (int i = 0; i < hand.Count; i++)
        {
            Destroy(hand[i].gameObject);
        }
        hand.Clear();
        for (int i = 0; i < play.Count; i++)
        {
            Destroy(play[i].gameObject);
        }
        play.Clear();
        for (int i = 0; i < grave.Count; i++)
        {
            Destroy(grave[i].gameObject);
        }
        grave.Clear();

        // Testing With List Changes
        ResetList();
    }

    // Shuffle Deck

    public void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int random = UnityEngine.Random.Range(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[random];
            deck[random] = temp;
        }

        AdjustDeck();
    }

    // Generate Deck From List

    public void GenerateDeck()
    {
        deck = new List<Card>(list);

        for (int i = 0; i < list.Count; i++)
        {
            deck[i] = GameObject.Instantiate(list[i].gameObject).GetComponent<Card>();
            list[i].gameObject.SetActive(false);
        }

        ShuffleDeck();
    }

    public void ResetDefense()
    {
        for (int i = 0; i < play.Count; i++)
        {
            if (play[i].GetComponent<Creature>() != null && play[i].GetComponent<Creature>().GetActive())
            {
                play[i].GetComponent<Creature>().ResetDefense();
            }
        }
    }

    // Adjust Card Positions In Zones
    // Run every time a card is added or removed from a zone.

    private void AdjustList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            CardHighlight highlight = list[i].GetComponent<CardHighlight>();
            Vector3 pos = list_marker.transform.position + new Vector3(0, card_height, 0) * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(deck_rot);
            highlight.SetHighlightRot(deck_rot);
        }
    }

    private void AdjustDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardHighlight highlight = deck[i].GetComponent<CardHighlight>();
            Vector3 pos = deck_marker.transform.position + new Vector3(0, card_height, 0) * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(deck_rot);
            highlight.SetHighlightRot(deck_rot);
        }
    }

    private void AdjustHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            CardHighlight highlight = hand[i].GetComponent<CardHighlight>();
            Vector3 pos = hand_marker.transform.position - hand_marker.transform.right * hand_distance * 0.5f
                + hand_marker.transform.right * hand_distance * (i + 1) / (hand.Count + 1) + card_height * hand_marker.transform.forward * i;
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos + hand_highlight_move);
            highlight.SetOriginRot(hand_origin_rot);
            highlight.SetHighlightRot(hand_highlight_rot);
        }
    }

    private void AdjustPlay()
    {
        for (int i = 0; i < play.Count; i++)
        {
            CardHighlight highlight = play[i].GetComponent<CardHighlight>();
            Vector3 pos = play_marker.transform.position - play_marker.transform.right * play_distance * 0.5f
                + play_marker.transform.right * play_distance * (i + 1) / (play.Count + 1) + card_height * play_marker.transform.up * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(grave_rot);
            highlight.SetHighlightRot(grave_rot);
        }
    }

    private void AdjustGrave()
    {
        for (int i = 0; i < grave.Count; i++)
        {
            CardHighlight highlight = grave[i].GetComponent<CardHighlight>();
            Vector3 pos = grave_marker.transform.position + new Vector3(0, card_height, -grave_distance) * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(grave_rot);
            highlight.SetHighlightRot(grave_rot);
        }
    }

    // Send Upkeep Effects Of All Cards

    public void SendUpkeepEffects()
    {
        for (int i = play.Count - 1; i > -1; i--)
        {
            if (play[i].GetComponent<Creature>() != null && play[i].GetComponent<Creature>().GetActive() == true)
            {
                play[i].GetComponent<Creature>().ResetDefense();
                selectionManager.SendUpkeep(play[i]);
            }
        }
    }

    // Position Adjustments

    public void RaiseInPlay(Card card)
    {
        if (play.Contains(card))
        {
            CardHighlight highlight = play[play.FindIndex(a => a == card)].GetComponent<CardHighlight>();
            Vector3 pos = card.transform.position + new Vector3(0, death_rise, 0);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
        }
    }

    // Update Actions Of All Cards In Play

    public void ResetPlayCardActions()
    {
        for (int i = 0; i < play.Count; i++)
        {
            if (play[i].GetComponent<Creature>() != null)
            {
                play[i].GetComponent<Creature>().ResetActions();
            }
        }
    }

    // Update Card Texts

    public void UpdateAllCardTexts()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            deck[i].UpdateText();
        }
        for (int i = 0; i < hand.Count; i++)
        {
            hand[i].UpdateText();
        }
        for (int i = 0; i < play.Count; i++)
        {
            play[i].UpdateText();
        }
        for (int i = 0; i < grave.Count; i++)
        {
            grave[i].UpdateText();
        }
    }

    // Getters

    public bool HandContains(Card card)
    {
        return hand.Contains(card);
    }

    public bool PlayContains(Card card)
    {
        return play.Contains(card);
    }

    public List<Card> GetHandCards()
    {
        return hand;
    }

    public List<Card> GetPlayCards()
    {
        return play;
    }
}
