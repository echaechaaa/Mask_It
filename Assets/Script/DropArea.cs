using UnityEngine;
using UnityEngine.EventSystems;
public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // "Snap" the dropped item to the center of the drop area
            GameObject droppedCard = eventData.pointerDrag;
            droppedCard.transform.SetParent(gameObject.transform);
            droppedCard.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
