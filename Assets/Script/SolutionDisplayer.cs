using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SolutionDisplayer : MonoBehaviour
{
    public float CardSize;
    public List<SolutionElement> DisplayedCards;
    public List<SolutionElement> MaskedCards;

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
        foreach (SolutionElement solutionElement in DisplayedCards)
        {
            Card cardObj = Instantiate(solutionElement.CardUI.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;
            cardObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, solutionElement.allowedRotation[0]));
            cardObj.Showcard();
            soluce.Add(cardObj.gameObject);
        }

        foreach (SolutionElement solutionElement in MaskedCards)
        {
            Card cardObj = Instantiate(solutionElement.CardUI.Card, transform);
            cardObj.transform.localPosition = Vector3.zero;
            cardObj.transform.localScale = Vector3.one * CardSize;
            cardObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, solutionElement.allowedRotation[0]));

            cardObj.MaskCard();
            soluce.Add(cardObj.gameObject);

        }
    }

    
}
