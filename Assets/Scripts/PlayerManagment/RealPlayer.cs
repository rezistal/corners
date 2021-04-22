using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public string Name { get; }
    public Color Color { get; }
    public List<(int x, int y)> FiguresKeys { get; }
    public List<BoardElementController> FiguresValues { get; }

    public RealPlayer(IStartCondition startCondition, Color color, string name)
    {
        StartCondition = startCondition.GetConditions();
        Color = color;
        FiguresKeys = new List<(int x, int y)>();
        FiguresValues = new List<BoardElementController>();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        Name = name;
    }
}
