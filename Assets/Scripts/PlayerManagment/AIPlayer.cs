using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : IPlayer
{
    public List<(int x, int y)> StartCondition { get; }
    public GameObject Prefab { get; }
    public Color Color { get => Color.magenta; }
    public string Name { get => "AI"; }
    public List<(int x, int y)> FiguresKeys { get; }
    public List<BoardElementController> FiguresValues { get; }

    public AIPlayer()
    {
        StartCondition = new BottomRightSC().GetConditions();
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        FiguresKeys = new List<(int x, int y)>();
        FiguresValues = new List<BoardElementController>();
    }
}