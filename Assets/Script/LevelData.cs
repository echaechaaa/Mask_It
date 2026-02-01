using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int LevelID;
    public int SideSlotsCount;
    public List <CardInventory> startingInventory;

    //Solution display and mask

    [Header("Solution Data")]
    public List<SolutionElement> solutionShapes;
    public List <SolutionElement> solutionMasks;
}
[Serializable]
public class CardInventory
{
    public CardUI Card;
    public int startRot= 0;
}

[Serializable]
public class SolutionElement
{
    public CardUI CardUI;
    public List<int> allowedRotation;
}
