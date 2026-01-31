using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<Card> _currentInventory;
    public List<Card> CurrentInventory
    {
        get => _currentInventory;
        set
        {
            _currentInventory = value;
        }
    }

    private void Start()
    {
        _currentInventory = new List<Card>();
    }
}
