
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class planelayer : MonoBehaviour
{
    [EasyButtons.Button]
    void bjk()
    {
        Renderer r = GetComponent<Renderer>();

        r.sortingLayerName = "Default";
        r.sortingOrder = -1; // derrière Default (0)
    }
}
