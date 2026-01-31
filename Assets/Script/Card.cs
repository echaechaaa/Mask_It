using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardElement[] _cardElements;

    [EasyButtons.Button]
    public void MaskCard()
    {
        _cardElements = GetComponentsInChildren<CardElement>();
        foreach (var card in _cardElements)
        {
            card.Mask.enabled = true;
            card.SpriteRenderer.enabled = false;
        }
    }
    
    [EasyButtons.Button]
    public void Showcard()
    {
        _cardElements = GetComponentsInChildren<CardElement>();
        foreach (var card in _cardElements)
        {
            card.Mask.enabled = false;
            card.SpriteRenderer.enabled = true;
        }
    }
}
