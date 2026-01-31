using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _canvaTransform; //To put UI Elements under the canvas

    [SerializeField] private GameObject _shapesLayoutPrefab;
    [SerializeField] private GameObject _masksLayoutPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private GameObject _cardSlotPrefab;

    private GameObject _currentInventoryGO = null;
    private GameObject _currentShapesLayoutGO = null;
    private GameObject _currentMasksLayoutGO = null;

    private List<Card> _levelStartingInventory;
    //Next up we could have a levelStartingShapes and levelStartingMasks if needed


    [EasyButtons.Button]
    public void InitLevel(int levelID)
    {
        LevelData levelData = Resources.Load<LevelData>($"Data/Level/SO_Level{levelID}");
        if (levelData == null)
        {
            Debug.LogError($"Level data for level {levelID} not found!");
            return;
        }
        else
        {
            if(_canvaTransform != null)
            {
                //Instantiate UI
                _currentInventoryGO = Instantiate(_inventoryPrefab, _canvaTransform);
                _currentShapesLayoutGO = Instantiate(_shapesLayoutPrefab, _canvaTransform);
                _currentMasksLayoutGO = Instantiate(_masksLayoutPrefab, _canvaTransform);

                //Load level starting data
                _levelStartingInventory = new List<Card>(levelData.startingInventory);

                ////Initialize Data
                foreach (Card card in _levelStartingInventory)
                {
                    //Add cards to inventory
                    Instantiate(_cardSlotPrefab, _currentInventoryGO.transform);
                }

                for(int i = 0; i < levelData.SideSlotsCount; i++)
                {
                    Instantiate(_cardSlotPrefab, _currentShapesLayoutGO.transform);
                    Instantiate(_cardSlotPrefab, _currentMasksLayoutGO.transform);
                }
            }
        }
    }
}


