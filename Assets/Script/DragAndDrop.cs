using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas _canvas; // Reference to the parent canvas necessary to drag correctly with delta

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [HideInInspector] public DropArea _lastDroppedArea;
    public float maxSize;
    private float initialsize;
    public float smoothDamp;
    private float targetSize;
    float currentSize;
    float refvel;
    private void Update()
    {
        currentSize = Mathf.SmoothDamp(currentSize, targetSize, ref refvel, smoothDamp);
        transform.localScale = currentSize * Vector3.one;
    }
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
        initialsize = transform.localScale.x;
        targetSize = initialsize;
        currentSize = initialsize;
    }

    public void OnPointerDown(PointerEventData eventData) { }//Necessary to implement to detect begin drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(_canvas.transform); //Set parent to canvas to be on top of other elements while dragging

        if (_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = false;
            //Do something when dragging here like animation, sound event etc
            _canvasGroup.alpha = 0.6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor; //To move correctly with canvas scale according to delta
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = true;
            //Undo something when stop dragging here
            _canvasGroup.alpha = 1f;

            bool isDroppedOnDropArea = false;
            foreach (GameObject Go in eventData.hovered)
            {
                if (Go.TryGetComponent<DropArea>(out DropArea dropArea))
                {
                    if (IsDropAllowed() == false) //doesnt work properly yet needs to be called elsewhere
                    {
                        break;
                    }

                    //Set parent to last dropped area if dropped on a drop area
                    gameObject.transform.SetParent(dropArea.transform);
                    isDroppedOnDropArea = true;
                    break;
                }
            }
            if (!isDroppedOnDropArea)
            {
                //Return to last dropped area if not dropped on a drop area
                if (_lastDroppedArea != null)
                {
                    transform.SetParent(_lastDroppedArea.transform);
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetSize = maxSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetSize = initialsize;
    }
}
