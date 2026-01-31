using UnityEngine;
using UnityEngine.UI;

public class ConverterCardUI : MonoBehaviour
{
    public Card card;

    [EasyButtons.Button]
    public void ConvertCard()
    {
        GameObject parent = new GameObject(card.name + "UI");
        foreach (CardElement cardEl in card.CardElements)
        {
            GameObject cardElementImage = new GameObject(cardEl.name);
            Image img = cardElementImage.AddComponent<Image>();
            img.sprite = cardEl.SpriteRenderer.sprite;
            cardElementImage.transform.parent = parent.transform;
        }
    }
}
