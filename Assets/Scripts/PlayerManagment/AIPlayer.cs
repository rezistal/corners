using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : IPlayer
{
    public Dictionary<(int x, int y), BoardElementController> Figures { get; }

    public List<(int x, int y)> StartCondition { get; }

    public GameObject Prefab { get; }

    public Color Color { get => Color.magenta; }

    public string Name { get => "AI"; }

    public AIPlayer()
    {
        Figures = new Dictionary<(int x, int y), BoardElementController>();
        StartCondition = new BottomRightSC().GetConditions();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
    }
}