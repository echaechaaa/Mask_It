using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardDisplayer : MonoBehaviour
{
    List<Card> _displayedCards; 
    List<Card> _maskedCards;
    private void Awake()
    {
        _displayedCards = new List<Card>();
        _maskedCards = new List<Card>();
    }
    public void AddCardToDisplay(Card card)
    {
        GameObject cardObj = Instantiate(card).gameObject;
        cardObj.transform.position = this.transform.position;
        cardObj.transform.parent = this.transform;
        card.Showcard();
        _displayedCards.Add(card);
    }

    public void AddCardToMask(Card card)
    {
        GameObject cardObj = Instantiate(card).gameObject;
        cardObj.transform.position = this.transform.position;
        cardObj.transform.parent = this.transform;
        card.MaskCard();
        _maskedCards.Add(card);
    }
}
