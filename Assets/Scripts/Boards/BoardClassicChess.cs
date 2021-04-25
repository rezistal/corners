using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardClassicChess : IBoard
{
    //private ChainedParameters<Color> colors;
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public int Size { get; }

    public BoardClassicChess()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Cell");
        StartCondition = new SCBoard8x8().GetConditions();
        Size = (int)Mathf.Sqrt(StartCondition.Count);
        /*
        List<Color> colorsList = new List<Color>()
        {
            Color.grey,
            Color.white
        };
        colors = new ChainedParameters<Color>(colorsList);
        */
    }

    public Color GetCellColor(int x, int y)
    {
        if ((x + y) % 2 == 0)
        {
            return Color.grey;
        }
        return Color.white;
    }
}