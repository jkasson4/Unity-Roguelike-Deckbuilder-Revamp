using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardText : MonoBehaviour
{
    // Dependancies
    public TMP_Text card_name;
    public TMP_Text card_cost;
    public TMP_Text card_type;
    public TMP_Text card_text;
    public TMP_Text card_stats;

    // Setters

    public void SetCardName(string name)
    {
        card_name.text = name;
    }

    public void SetCardCost(string cost)
    {
        card_cost.text = cost;
    }

    public void SetCardType(string type)
    {
        card_type.text = type;
    }

    public void SetCardText(string description, string flavor)
    {
        if(description != string.Empty)
        {
            card_text.text = new string("<size=0.65>" + description + "<size=0.2>\n\n<size=0.55>" + flavor);
        }
        else
        {
            card_text.text = new string("<size=0.55>" + flavor);
        }
    }

    public void SetCardStats(string text)
    {
        card_stats.text = text;
    }
}
