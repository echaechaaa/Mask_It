using UnityEngine;
using UnityEngine.Events;
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


    public UnityEvent OnDrop;
    public UnityEvent OnGrab;
    public UnityEvent OnPointerEnter1;
    public UnityEvent OnPointerExit1;



    private void Update()
    {
        // Scale
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
        lastrelease = Time.time;
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnGrab?.Invoke();
    }//Necessary to implement to detect begin drag
    public void OnBeginDrag(PointerEventData eventData)
    {

        transform.SetParent(_canvas.transform);

        if (_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0.6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        targetSize = maxSize;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;

            bool isDroppedOnDropArea = false;

            foreach (GameObject Go in eventData.hovered)
            {
                if (Go.TryGetComponent<DropArea>(out DropArea dropArea))
                {
                    if (!IsDropAllowed())
                        break;

                    transform.SetParent(dropArea.transform);
                    _lastDroppedArea = dropArea;
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    isDroppedOnDropArea = true;
                    break;
                }
            }

            if (!isDroppedOnDropArea && _lastDroppedArea != null)
            {
                transform.SetParent(_lastDroppedArea.transform);
                GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            targetSize = initialsize;
            lastrelease = Time.time;
            OnDrop?.Invoke();
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
        if( Time.time - lastrelease < 0.2f)
        {
            return;
        }
        targetSize = maxSize;
        OnPointerEnter1?.Invoke();

    }
    float lastrelease = 0f;
    public void OnPointerExit(PointerEventData eventData)
    {
        targetSize = initialsize;
        OnPointerExit1?.Invoke();
    }
}
