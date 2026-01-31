using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Level List")]
    [SerializeField] private List<LevelData> _levelsInOrder;

    [Header("References and Prefabs")]
    [SerializeField] private Transform _canvaTransform; //To put UI Elements under the canvas
    [SerializeField] private GameObject _shapesLayoutPrefab;
    [SerializeField] private GameObject _masksLayoutPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private GameObject _cardSlotPrefab;

    [SerializeField] private SolutionDisplayer _solutionDisplayer;

    private int _currentLevelID = 0;

    private GameObject _currentInventoryGO = null;
    private GameObject _currentShapesLayoutGO = null;
    private GameObject _currentMasksLayoutGO = null;

    private List<CardUI> _levelStartingInventory;
    //Next up we could have a levelStartingShapes and levelStartingMasks if needed
    private void Start()
    {
        _currentLevelID = 0;
        InitLevel(); //Start at level 1
    }

    [EasyButtons.Button]
    public void InitLevel()
    {
        ClearLevel();

        LevelData currentLevelData = _levelsInOrder[_currentLevelID];

        _solutionDisplayer.GenerateSoluce(currentLevelData);
        if (currentLevelData == null)
        {
            Debug.LogError($"Level data for level {_currentLevelID} not found!");
            return;
        }
        else
        {
            if (_canvaTransform != null)
            {
                //Instantiate UI
                _currentInventoryGO = Instantiate(_inventoryPrefab, _canvaTransform);
                _currentShapesLayoutGO = Instantiate(_shapesLayoutPrefab, _canvaTransform);
                _currentMasksLayoutGO = Instantiate(_masksLayoutPrefab, _canvaTransform);

                _levelStartingInventory = new List<CardUI>(currentLevelData.startingInventory);

                ////Initialize Data
                foreach (CardUI cardUI in _levelStartingInventory)
                {
                    //Add cards to inventory
                    GameObject slot = Instantiate(_cardSlotPrefab, _currentInventoryGO.transform);
                    slot.GetComponent<DropArea>().dropType = DropType.INVENTORY;

                    CardUI card = Instantiate(cardUI, slot.transform);
                    card.transform.SetParent(slot.transform);
                    card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    card.GetComponent<DragAndDrop>()._lastDroppedArea = slot.GetComponent<DropArea>(); //Probs doesnt work
                }

                for (int i = 0; i < currentLevelData.SideSlotsCount; i++)
                {
                    GameObject slotShape = Instantiate(_cardSlotPrefab, _currentShapesLayoutGO.transform);
                    slotShape.GetComponent<DropArea>().dropType = DropType.DISPLAY;

                    GameObject slotMask = Instantiate(_cardSlotPrefab, _currentMasksLayoutGO.transform);
                    slotMask.GetComponent<DropArea>().dropType = DropType.MASK;

                }
            }
        }
    }
    public void ClearLevel()
    {
        if (_currentInventoryGO != null)
        {
            Destroy(_currentInventoryGO);
        }
        if (_currentShapesLayoutGO != null)
        {
            Destroy(_currentShapesLayoutGO);
        }
        if (_currentMasksLayoutGO != null)
        {
            Destroy(_currentMasksLayoutGO);
        }
    }

    /*[EasyButtons.Button]
    public void GoToPreviousLevel()
    {
        _currentLevelID -=1;
        _currentLevelID %= _levelsInOrder.Count;
        InitLevel();
    }*/

    [EasyButtons.Button]
    public void GoToNextLevel()
    {
        _currentLevelID +=1;
        _currentLevelID %= _levelsInOrder.Count;
        InitLevel();
    }
}


