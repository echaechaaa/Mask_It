using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int levelID;

    public int SideSlotsCount;
    public List <Card> startingInventory;

    //Solution display and mask
    public List<Card> solutionShapes;
    public List <Card> solutionMasks;
}
