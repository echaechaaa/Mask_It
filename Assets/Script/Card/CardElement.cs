using UnityEngine;

public class CardElement : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public SpriteMask Mask;

    [EasyButtons.Button]
    public void SetupMask()
    {
        SpriteMask mask;
        if (Mask == null)
        {
            mask = SpriteRenderer.gameObject.AddComponent<SpriteMask>();
        }
        else
        {
            mask = SpriteRenderer.GetComponent<SpriteMask>();
        }
        Mask = mask;
        Mask.sprite = SpriteRenderer.sprite;
        Mask.enabled = false;
    }
}
