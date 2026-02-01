using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI.Table;

public class CardDisplayer : MonoBehaviour
{
    public static CardDisplayer Instance;
    List<Card> _displayedCards; 
    List<Card> _maskedCards;
    public float cardScale;
    public UnityEvent OnLevelSuccess;
    [SerializeField] private LevelManager _levelmanager;
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
        cardObj.CardUI = cardUI;
        cardObj.transform.rotation = cardUI.transform.rotation;
        cardObj.currentRot = cardUI.currentRot;
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
        cardObj.CardUI = cardUI;
        cardObj.transform.localPosition = Vector3.zero;
        cardObj.transform.localScale = Vector3.one * cardScale;
        cardObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, cardUI.currentRot));
        cardObj.currentRot = cardUI.currentRot;
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

            Destroy(found.gameObject);
            return;
        }

        found = _maskedCards
            .Find(c => c.PrefabSource == cardUI.Card);

        if (found != null)
        {
            _maskedCards.Remove(found);
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
    [EasyButtons.Button]
    public void ChecklevelSuccess()
    {
        LevelData level = _levelmanager.GetCurrentlevel();

        if(level.solutionMasks.Count != _maskedCards.Count)
        {
            return;
        }

        if (level.solutionShapes.Count != _displayedCards.Count)
        {
            return;
        }

        bool sameDisplayCards = true;
        foreach(Card card in _displayedCards)
        {
            bool found = level.solutionShapes.Exists(
            s => s.CardUI != null && s.CardUI.Card == card.PrefabSource && CheckRotationAllowed(card.currentRot, s.allowedRotation)
            );

            if (!found)
            {
                sameDisplayCards = false;
                break;
            }
        }
        if (!sameDisplayCards)
        {
            return;
        }
        bool sameMaskCards = true;
        foreach (Card card in _maskedCards)
        {
            bool found = level.solutionMasks.Exists(
            s => s.CardUI != null && s.CardUI.Card == card.PrefabSource && CheckRotationAllowed(card.currentRot, s.allowedRotation)
            );

            if (!found)
            {
                sameMaskCards = false;
                break;
            }
        }
        if(!sameMaskCards)
        {
            return;
        }
        Debug.Log("WIIIIN");
        OnLevelSuccess?.Invoke();
    }
    public bool CheckRotationAllowed(int rot, List<int> sol)
    {
        bool allow = false;
        foreach (int element in sol)
        {
            Debug.Log(element + " " + rot);
            if (
                element == rot)
            {
                allow = true; break;
            }
        }
            return allow;
    }

}
