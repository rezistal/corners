using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : IPlayer
{
    public Dictionary<(int x, int y), BoardElementController> Figures { get; }
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public string Name { get; }
    public Color Color { get; }

    public RealPlayer(IStartCondition startCondition, Color color, string name)
    {
        StartCondition = startCondition.GetConditions();
        Color = color;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        Name = name;
    }
}
