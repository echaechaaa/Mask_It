using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CardDisplayer : MonoBehaviour
{
    public static CardDisplayer Instance;
    List<Card> _displayedCards; 
    List<Card> _maskedCards;
    public float cardScale;
    private void Awake()
    {
        Instance = this;
        _displayedCards = new List<Card>();
        _maskedCards = new List<Card>();
    }
    public void AddCardToDisplay(CardUI cardUI)
    {
        RemoveCard(cardUI);

        Card cardObj = Instantiate(cardUI.Card, transform);
        cardObj.transform.rotation = cardUI.transform.rotation;
        cardObj.transform.localPosition = Vector3.zero;
        cardObj.transform.localScale = Vector3.one * cardScale;

        cardObj.PrefabSource = cardUI.Card;
        cardObj.Showcard();

        cardUI.Cardobj = cardObj.gameObject;
        _displayedCards.Add(cardObj); 
    }


    public void AddCardToMask(CardUI cardUI)
    {
        RemoveCard(cardUI);

        Card cardObj = Instantiate(cardUI.Card, transform);
        cardObj.transform.localPosition = Vector3.zero;
        cardObj.transform.localScale = Vector3.one * cardScale;

        cardObj.PrefabSource = cardUI.Card;
        cardObj.MaskCard();

        cardUI.Cardobj = cardObj.gameObject;
        _maskedCards.Add(cardObj); 
    }

    public void RemoveCard(CardUI cardUI)
    {
        Card found = _displayedCards
            .Find(c => c.PrefabSource == cardUI.Card);

        if (found != null)
        {
            _displayedCards.Remove(found);
            Debug.Log("Find display remove");

            Destroy(found.gameObject);
            return;
        }

        found = _maskedCards
            .Find(c => c.PrefabSource == cardUI.Card);

        if (found != null)
        {
            _maskedCards.Remove(found);
            Debug.Log("Find Mask remove");
            Destroy(found.gameObject);
        }
    }
    public void RemoveAllCards()
    {
        if(_maskedCards != null )
        {
            // Remove masked cards
            for (int i = _maskedCards.Count - 1; i >= 0; i--)
            {
                if (_maskedCards[i] != null)
                    Destroy(_maskedCards[i].gameObject);
            }
            _maskedCards.Clear();
        }
        if(_displayedCards != null )
        {
            // Remove displayed cards
            for (int i = _displayedCards.Count - 1; i >= 0; i--)
            {
                if (_displayedCards[i] != null)
                    Destroy(_displayedCards[i].gameObject);
            }
            _displayedCards.Clear();
        }
        
    }

}
