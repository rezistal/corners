using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    public Dictionary<(int x, int y), BoardElementController> Figures;
    public List<BoardElementController> ActiveFigures;
    private IBoard Board;

    public void ResetBoard()
    {
        foreach (BoardElementController b in Figures.Values)
        {
            b.Deactivate();
            b.Deselect();
        }
    }

    public void ResetSelected()
    {
        foreach (BoardElementController b in ActiveFigures)
        {
            b.Deactivate();
            b.Deselect();
        }
        ActiveFigures.Clear();
    }

    public void Select(List<(int x, int y)> cells)
    {
        foreach((int x, int y) c in cells)
        {
            if (Figures.ContainsKey(c))
            {
                Figures[c].Activate();
                Figures[c].Select();
                ActiveFigures.Add(Figures[c]);
            }
        }
    }

    public BoardManager(IBoard board)
    {
        Board = board;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
        ActiveFigures = new List<BoardElementController>();
    }

    public void CreateBoardAt(Transform parent)
    {
        foreach ((int x, int y) in Board.StartCondition)
        {
            GameObject o = Object.Instantiate(Board.Prefab);
            o.transform.SetParent(parent, false);
            BoardElementController bec = o.GetComponent<BoardElementController>();
            bec.SetCoordinates((x, y));
            bec.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));
            bec.Color = Board.GetCellColor(x,y);
            Figures.Add((x, y), bec);
        }
    }
}
