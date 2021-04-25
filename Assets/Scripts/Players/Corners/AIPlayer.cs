﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIPlayer : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public Color Color { get => Color.blue; }
    public string Name { get => "AI"; }
    public List<IBoardElementController> FiguresValues { get; }
    public List<(int x, int y)> FiguresKeys
    {
        get
        {
            return FiguresValues.Select(x => x.GetCoordinates()).ToList();
        }
    }

    public AIPlayer()
    {
        StartCondition = new SCBottomRight().GetConditions();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        FiguresValues = new List<IBoardElementController>();
    }

    public IBoardElementController GetFigureByCoords((int x, int y) coords)
    {
        throw new System.NotImplementedException();
    }
    public List<IBoardElementController> ActiveFiguresValues => throw new System.NotImplementedException();
}