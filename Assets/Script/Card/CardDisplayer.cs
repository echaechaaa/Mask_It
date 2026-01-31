using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardDisplayer : MonoBehaviour
{
    List<Card> _displayedCards; 
    List<Card> _maskedCards;
    public float cardScale;
    private void Awake()
    {
        _displayedCards = new List<Card>();
        _maskedCards = new List<Card>();
    }
    public void AddCardToDisplay(Card card)
    {
        Card cardObj = Instantiate(card);
        cardObj.transform.position = this.transform.position;
        cardObj.transform.localScale = Vector3.one * cardScale;
        cardObj.transform.parent = this.transform;
        cardObj.Showcard();
        _displayedCards.Add(card);
    }

    public void AddCardToMask(Card card)
    {
        Card cardObj = Instantiate(card);
        cardObj.transform.position = this.transform.position;
        cardObj.transform.localScale = Vector3.one * cardScale;
        cardObj.transform.parent = this.transform;
        cardObj.MaskCard();
        _maskedCards.Add(card);
    }
}
