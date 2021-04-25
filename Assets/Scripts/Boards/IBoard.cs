using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    List<(int x, int y)> StartCondition { get; }
    GameObject Prefab { get; }
    Color GetCellColor(int x, int y);
    int Size { get; }
}
