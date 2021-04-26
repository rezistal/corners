using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBoardOldColored : IBuilderBoardCells
{
    private GameObject Prefab;
    private Color white;
    private Color black;

    public BuilderBoardOldColored()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Cell");
        black = Color.gray;  //new Color(117 / 255f, 96 / 255f, 62 / 255f);
        white = Color.white; //new Color(194 / 255f, 166 / 255f, 122 / 255f);
    }

    public IBoardElementController BuildBoardElement(int x, int y, Transform parent)
    {
        GameObject o = Object.Instantiate(Prefab);
        o.transform.SetParent(parent, false);
        IBoardElementController bec = o.GetComponent<IBoardElementController>();
        bec.SetCoordinates((x, y));
        BoardElementCell cell = bec.GameObject.GetComponent<BoardElementCell>();
        cell.SetColor(GetCellColor(x, y));
        return bec;
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
