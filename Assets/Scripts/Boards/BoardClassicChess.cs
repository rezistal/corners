using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardClassicChess : IBoard
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public int Size { get; }

    private Color white;
    private Color black;

    public BoardClassicChess()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Cell");
        StartCondition = new SCBoard8x8().GetConditions();
        Size = (int)Mathf.Sqrt(StartCondition.Count);
        black = new Color(117 / 255f, 96 / 255f, 62 / 255f);
        white = new Color(194 / 255f, 166 / 255f, 122 / 255f);
    }

    public Color GetCellColor(int x, int y)
    {
        if ((x + y) % 2 == 0)
        {
            return black;
        }
        return white;
    }
}