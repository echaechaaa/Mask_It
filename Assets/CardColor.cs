using UnityEngine;
using UnityEngine.UI;

public class CardColor : MonoBehaviour
{
    public Image SpriteRendererBG;
    public void SetColor(Color colorBG, Color colorElements)
    {
        SpriteRendererBG.color = colorBG;

        Image[] ce = GetComponentsInChildren<Image>();

        foreach (Image ce2 in ce)
        {
            ce2.GetComponent<Image>().color = colorElements;
        }
    }
}
