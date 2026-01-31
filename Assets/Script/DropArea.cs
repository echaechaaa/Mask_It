using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public enum DropType
{
    mask,
    display,
    inventory
}
public class DropArea : MonoBehaviour, IDropHandler
{
    public UnityEvent<Card> OnCardDropped;
    public DropType dropType;
    public void OnDrop(PointerEventData eventData)
    {
        if (IsDropAllowed() == false)
        {
            return;
        }

        if (eventData.pointerDrag != null)
        {
            // "Snap" the dropped item to the center of the drop area
            GameObject droppedCard = eventData.pointerDrag;
            droppedCard.transform.SetParent(gameObject.transform);
            droppedCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            Card cardDropped = GetComponentInChildren<CardUI>().Card;
            OnCardDropped.Invoke(cardDropped);
            GetComponentInChildren<DragAndDrop>()._lastDroppedArea = this;


            switch (dropType)
            {
                case DropType.mask:
                    CardDisplayer.Instance.AddCardToMask(GetComponentInChildren<CardUI>());
                    break;
                case DropType.display:
                    CardDisplayer.Instance.AddCardToDisplay(GetComponentInChildren<CardUI>());
                    break;
                case DropType.inventory:
                    CardDisplayer.Instance.RemoveCard(GetComponentInChildren<CardUI>());
                    break;
            }
        }
    }

    public bool IsDropAllowed() //To avoid stacking
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.TryGetComponent<DragAndDrop>(out DragAndDrop d))
            {
                return false; // There is at least one child, so drop is not allowed
            }
        }
        return true; // No children found, drop is allowed
    }
}
