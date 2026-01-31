using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int SideSlotsCount;
    public List <CardUI> startingInventory;

    //Solution display and mask

    [Header("Solution Data")]
    public List<CardUI> solutionShapes;
    public List <CardUI> solutionMasks;
}
