using UnityEngine;

public class CardUI : MonoBehaviour
{
    public Card Card;
    [HideInInspector] public GameObject Cardobj;
    public int currentRot = 0;
    public void Rotate()
    {
        Debug.Log("Rotate");
        currentRot += 90;
        currentRot %= 360;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,currentRot));
        if(Cardobj != null )
        {
            Cardobj.transform.rotation = transform.rotation;
        }
    }
}
