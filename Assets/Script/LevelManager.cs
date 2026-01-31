using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
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
            Instantiate(_emptyInventoryPrefab);
            _levelStartingInventory = new List<Card>(levelData.startingInventory);
        }
    }
}


