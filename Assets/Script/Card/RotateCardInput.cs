using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateCardInput : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CheckUI();
        }
    }

    void CheckUI()
    {
        // Création des données de l'événement souris
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

        // Liste des résultats
        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast UI
        graphicRaycaster.Raycast(pointerData, results);

        if (results.Count == 0)
            return;

        // On prend le premier élément touché (le plus proche)
        GameObject hitObject = results[0].gameObject;

        // Exemple : vérifier si un composant est présent
        if (hitObject.TryGetComponent<CardUI>(out CardUI cardUI))
        {
            Debug.Log("CardUI trouvé sur : " + hitObject.name);
            cardUI.Rotate();
        }
        else if(hitObject.transform.parent.TryGetComponent<CardUI>(out CardUI cardUI2))
        {
            Debug.Log("CardUI trouvé sur : " + hitObject.name);
            cardUI2.Rotate();
        }
        else
        {

        }
    }
}
