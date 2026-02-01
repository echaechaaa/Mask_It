using UnityEngine;

public class CardColor : MonoBehaviour
{
    public SpriteRenderer SpriteRendererBG;
    public void SetColor(Color colorBG, Color colorElements)
    {
        SpriteRendererBG.color = colorBG;

        CardElement[] ce = GetComponentsInChildren<CardElement>();

        foreach (CardElement ce2 in ce)
        {
            ce2.GetComponent<SpriteRenderer>().color = colorElements;
        }
    }
}
