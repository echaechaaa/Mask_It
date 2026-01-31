using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SolutionDisplayer : MonoBehaviour
{
    public float CardSize;
    public List<CardUI> DisplayedCards;
    public List<CardUI> MaskedCards;

    List<GameObject> soluce;
    public void GenerateSoluce(LevelData data)
    {
        if(soluce != null)
        {
            foreach (GameObject gameObj in soluce)
            {
                Destroy(gameObj);
                Debug.Log("destroy");
            }
        }
        
        DisplayedCards = data.solutionShapes;
        MaskedCards = data.solutionMasks;

        soluce = new();
        foreach (CardUI card in DisplayedCards)
        {
            Card cardObj = Instantiate(card.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;

            cardObj.Showcard();
            soluce.Add(cardObj.gameObject);
        }

        foreach (CardUI card in MaskedCards)
        {
            Card cardObj = Instantiate(card.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;

            cardObj.MaskCard();
            soluce.Add(cardObj.gameObject);

        }
    }

    
}
