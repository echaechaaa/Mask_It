using UnityEngine;
using UnityEngine.UI;

public class SlotColor : MonoBehaviour
{

    [SerializeField] private Image Border;
    [SerializeField]
    private Image _srBackGround;
    public void SetColors(Color border, Color bg)
    {
        Border.color = border;
        _srBackGround.color = bg;
    
    }
}
