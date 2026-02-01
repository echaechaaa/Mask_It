using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CustomCursor : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite clickSprite;

    private Image image;
    private RectTransform rectTransform;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        rectTransform.position = Input.mousePosition;

        image.sprite = Input.GetMouseButton(0)
            ? clickSprite
            : idleSprite;
    }
}
