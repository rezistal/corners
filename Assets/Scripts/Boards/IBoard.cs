using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    List<(int x, int y)> StartCondition { get; }
    int Size { get; }
}
