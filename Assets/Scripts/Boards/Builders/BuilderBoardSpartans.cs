using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBoardSpartans : IBuilderBoardCells
{
    private GameObject Prefab;
    private Sprite whiteCell;
    private Sprite whiteCellSelection;
    private Sprite blackCell;
    private Sprite blackCellSelection;

    public BuilderBoardSpartans()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Spartans/SpartanCell");
        whiteCell = Resources.Load<Sprite>("Images/Spartans/Cells/whiteCell");
        whiteCellSelection = Resources.Load<Sprite>("Images/Spartans/Cells/whiteCellSelection");
        blackCell = Resources.Load<Sprite>("Images/Spartans/Cells/blackCell");
        blackCellSelection = Resources.Load<Sprite>("Images/Spartans/Cells/blackCellSelection");
    }

    public IBoardElementController BuildBoardElement(int x, int y, Transform parent)
    {
        GameObject o = Object.Instantiate(Prefab);
        o.transform.SetParent(parent, false);
        IBoardElementController bec = o.GetComponent<IBoardElementController>();
        bec.SetCoordinates((x, y));
        BESpartanCell cell = bec.GameObject.GetComponent<BESpartanCell>();
        cell.SetImages(GetCellSprites(x, y));
        return bec;
    }

    public (Sprite cell, Sprite cellSelected) GetCellSprites(int x, int y)
    {
        if ((x + y) % 2 == 0)
        {
            return (blackCell, blackCellSelection);
        }
        return (whiteCell, whiteCellSelection);
    }
}
