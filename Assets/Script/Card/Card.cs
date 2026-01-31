using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [HideInInspector] public CardElement[] CardElements;

    [EasyButtons.Button]
    public void MaskCard()
    {
        Debug.Log("MaskCard");

        CardElements = GetComponentsInChildren<CardElement>();
        foreach (var card in CardElements)
        {
            card.Mask.enabled = true;
            card.SpriteRenderer.enabled = false;
        }
    }
    
    [EasyButtons.Button]
    public void Showcard()
    {
        Debug.Log("ShowCard");
        CardElements = GetComponentsInChildren<CardElement>();
        foreach (var card in CardElements)
        {
            card.Mask.enabled = false;
            card.SpriteRenderer.enabled = true;
        }
    }
}
