using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    private Canvas _canvas; // Reference to the parent canvas necessary to drag correctly with delta

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData) { }//Necessary to implement to detect begin drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(_canvas.transform); //Set parent to canvas to be on top of other elements while dragging

        if (_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = false;
            //Do something when dragging here
            _canvasGroup.alpha = 0.6f;
        }
    }
  
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor; //To move correctly with canvas scale according to delta
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = true;
            //Undo something when stop dragging here
            _canvasGroup.alpha = 1f;
        }
    }
}
