using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    List<(int x, int y)> StartCondition { get; }
    GameObject Prefab { get; }
    Color GetCellColor(int x, int y);
}

public class ClassicChessBoard : IBoard
{
    //private ChainedParameters<Color> colors;
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }

    public ClassicChessBoard()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Cell");
        StartCondition = new BoardSC().GetConditions();
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

public class BoardManager
{
    public Dictionary<(int x, int y), BoardElementController> Figures;
    public Dictionary<(int x, int y), BoardElementController> ActiveFigures;
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
        foreach (BoardElementController b in ActiveFigures.Values)
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
                ActiveFigures.Add(c, Figures[c]);
            }
        }
    }

    public BoardManager(IBoard board)
    {
        Board = board;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
        ActiveFigures = new Dictionary<(int x, int y), BoardElementController>();
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
