using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDraughtsHuman : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public string Name { get; }
    public Color Color { get; }

    public List<IBoardElementController> FiguresValues { get; }

    public List<(int x, int y)> FiguresKeys
    {
        get
        {
            return FiguresValues.Where(x => x.Alive).Select(x => x.GetCoordinates()).ToList();
        }
    }

    public List<IBoardElementController> ActiveFiguresValues
    {
        get
        {
            return FiguresValues.Where(x => x.Alive).ToList();
        }
    }

    public PlayerDraughtsHuman(IStartCondition startCondition, Color color, string name)
    {
        StartCondition = startCondition.GetConditions();
        Color = color;
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        Name = name;
        FiguresValues = new List<IBoardElementController>();
    }

    public IBoardElementController GetFigureByCoords((int x, int y) coords)
    {
        foreach(IBoardElementController b in ActiveFiguresValues)
        {
            if(b.GetCoordinates() == coords)
            {
                return b;
            }
        }
        return null;
    }
}
