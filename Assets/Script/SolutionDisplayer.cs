using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SolutionDisplayer : MonoBehaviour
{
    public float CardSize;
    public List<CardUI> DisplayedCards;
    public List<CardUI> MaskedCards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (CardUI card in DisplayedCards)
        {
            Card cardObj = Instantiate(card.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;

            cardObj.Showcard();
        }

        foreach (CardUI card in MaskedCards)
        {
            Card cardObj = Instantiate(card.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;

            cardObj.MaskCard();
        }
    }
    public void GenerateSoluce(LevelData data)
    {

    }

    
}
