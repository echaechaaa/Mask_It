using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<Card> _inventoryCards;

    private void Start()
    {
        _inventoryCards = new List<Card>();
    }

    public void AddCard(Card card)
    {
        _inventoryCards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        _inventoryCards.Remove(card);
    }

    public List<Card> GetInventoryCards()
    {
        return _inventoryCards;
    }
}
