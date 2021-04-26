using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDraughtsHuman : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }

    public string Name { get; }
    public Color Color { get; }

    public List<IBoardElementController> FiguresValues { get; }

    public List<(int x, int y)> FiguresKeys
    {
        get
        {
            List<(int x, int y)> result = new List<(int x, int y)>();
            foreach(var f in FiguresValues)
            {
                if (f.Alive)
                {
                    result.Add(f.GetCoordinates());
                }
            }
            return result;
        }
    }

    public List<IBoardElementController> ActiveFiguresValues
    {
        get
        {
            List<IBoardElementController> result = new List<IBoardElementController>();
            foreach (var f in FiguresValues)
            {
                if (f.Alive)
                {
                    result.Add(f);
                }
            }
            return result;
        }
    }

    public PlayerDraughtsHuman(IStartCondition startCondition, Color color, string name)
    {
        StartCondition = startCondition.GetConditions();
        Color = color;
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
