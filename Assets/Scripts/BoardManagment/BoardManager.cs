using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard
{
    List<(int x, int y)> GetConditions();
    GameObject Prefab { get; }
    Color Next();
}

public class ClassicChessBoard : IBoard
{
    private ChainedParameters<Color> colors;
    private readonly IStartCondition StartCondition;
    public List<(int x, int y)> GetConditions()
    {
        return StartCondition.GetConditions();
    }

    public GameObject Prefab { get; }

    public ClassicChessBoard()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Cell");
        StartCondition = new BoardSC();
        List<Color> colorsList = new List<Color>()
        {
            Color.grey,
            Color.white
        };
        colors = new ChainedParameters<Color>(colorsList);
    }

    public Color Next()
    {
        return colors.Next();
    }
}

public class BoardManager
{
    private Dictionary<(int x, int y), BoardElementController> Figures;
    private IBoard Board;

    public BoardManager(IBoard board)
    {
        Board = board;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
    }

    public void CreateBoardAt(Transform parent)
    {
        foreach ((int x, int y) in Board.GetConditions())
        {
            GameObject o = Object.Instantiate(Board.Prefab);
            o.transform.SetParent(parent, false);
            BoardElementController bec = o.GetComponent<BoardElementController>();
            bec.SetCoordinates((x, y));
            bec.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));
            bec.Color = Board.Next();
            Figures.Add((x, y), bec);
        }
    }
}
