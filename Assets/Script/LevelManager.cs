using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _canvaTransform; //To put inventory under the canvas
    [SerializeField] private GameObject _emptyInventoryPrefab;
 
    private List<Card> _levelStartingInventory;


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
                Instantiate(_emptyInventoryPrefab, _canvaTransform);
                _levelStartingInventory = new List<Card>(levelData.startingInventory);
            }
        }
    }
}


