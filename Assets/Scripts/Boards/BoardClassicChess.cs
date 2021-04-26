using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardClassicChess : IBoard
{
    public List<(int x, int y)> StartCondition { get; }
    public int Size { get; }

    public BoardClassicChess()
    {
        StartCondition = new SCBoard8x8().GetConditions();
        Size = (int)Mathf.Sqrt(StartCondition.Count);
    }
}