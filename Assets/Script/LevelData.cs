using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int SideSlotsCount;
    public List <Card> startingInventory;

    //Solution display and mask

    [Header("Solution Data")]
    public List<Card> solutionShapes;
    public List <Card> solutionMasks;
}
