using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCornersHuman : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public string Name { get; }
    public Color Color { get; }
    public List<BoardElementController> FiguresValues { get; }

    public List<(int x, int y)> FiguresKeys
    {
        get
        {
            return FiguresValues.Select(x => x.GetCoordinates()).ToList();
        }
    }

    public PlayerCornersHuman(IStartCondition startCondition, Color color, string name)
    {
        StartCondition = startCondition.GetConditions();
        Color = color;
        FiguresValues = new List<BoardElementController>();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        Name = name;
    }

    public BoardElementController GetFigureByCoords((int x, int y) coords)
    {
        throw new System.NotImplementedException();
    }
    public List<BoardElementController> ActiveFiguresValues => throw new System.NotImplementedException();
}
