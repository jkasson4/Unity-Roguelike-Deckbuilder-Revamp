using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour, IUpkeepable
{
    // Dependancies
    public CardManager cardManager;
    public PlayerManager playerManager;
    public SelectionManager selectionManager;

    // Abstract Variables - Child Needs To Set
    protected abstract string card_name { get; }
    protected abstract int card_cost { get; }
    protected abstract string card_type { get; }

    // Virtual Variables - Child May Optionally To Set
    protected virtual string card_description { get { return ""; } }
    protected virtual string card_flavor { get { return ""; } }
    protected virtual bool remove_on_play { get { return false; } }

    // Initiate Casting

    public void Cast(List<GameObject> targets)
    {
        playerManager.IncreaseMana(-card_cost);
        CastingEffects(targets);
        cardManager.HandToGrave(this);
    }
    
    // Casting Targets And Effects - Child Must Set Casting Effects

    protected virtual List<int> CastingTargets()
    {
        return new List<int>() { };
    }

    protected abstract void CastingEffects(List<GameObject> targets);

    // Upkeep Targets, Effects, and Getters - Child May Add Additional Effects

    public List<int> GetUpkeepTargets()
    {
        return UpkeepTargets();
    }

    protected virtual List<int> UpkeepTargets()
    {
        return new List<int>();
    }

    public void UpkeepEffect(List<GameObject> targets)
    {
        AddUpkeepEffect(targets);
    }

    protected virtual void AddUpkeepEffect(List<GameObject> targets)
    {

    }

    // Update Card Text

    public void UpdateText()
    {
        CardText cardText = GetComponent<CardText>();

        cardText.SetCardName(card_name);
        cardText.SetCardCost(card_cost.ToString());
        cardText.SetCardType(TypeText() + " - " + card_type);
        cardText.SetCardText(card_description, card_flavor);
        cardText.SetCardStats(StatsText());
    }

    public void UpdateStats()
    {
        CardText cardText = GetComponent<CardText>();

        cardText.SetCardStats(StatsText());
    }

    // Getters

    public bool GetCardRemove()
    {
        return remove_on_play;
    }

    public int GetManaCost()
    {
        return card_cost;
    }

    public List<int> GetCastTargets()
    {
        return CastingTargets();
    }

    protected abstract string TypeText();

    protected abstract string StatsText();

    // Setters

    public void SetCardManager(CardManager cardManager)
    {
        this.cardManager = cardManager;
    }

    public void SetPlayerManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public void SetSelectionManager(SelectionManager selectionManager)
    {
        this.selectionManager = selectionManager;
    }
}
