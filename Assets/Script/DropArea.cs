using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public enum DropType
{
    MASK,
    DISPLAY,
    INVENTORY
}
public class DropArea : MonoBehaviour, IDropHandler
{
    public UnityEvent<Card> OnCardDropped;
    public DropType dropType; //Type of object being dropped ?
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) //if something is being dropped
        {
            // "Snap" the dropped item to the center of the drop area
            GameObject droppedGO = eventData.pointerDrag;
            droppedGO.transform.SetParent(gameObject.transform);
            droppedGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Card droppedCard = GetComponentInChildren<CardUI>().Card;
            OnCardDropped.Invoke(droppedCard);
            GetComponentInChildren<DragAndDrop>()._lastDroppedArea = this;

            //Display the card according to drop area type
            switch (dropType)
            {
                case DropType.MASK:
                    CardDisplayer.Instance.AddCardToMask(GetComponentInChildren<CardUI>());
                    break;
                case DropType.DISPLAY:
                    CardDisplayer.Instance.AddCardToDisplay(GetComponentInChildren<CardUI>());
                    break;
                case DropType.INVENTORY:
                    CardDisplayer.Instance.RemoveCard(GetComponentInChildren<CardUI>());
                    break;
            }
        }
    }

   
}
