using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    List<(int x, int y)> FiguresKeys { get; }
    List<BoardElementController> FiguresValues { get; }
    List<(int x, int y)> StartCondition { get; }
    GameObject Prefab { get; }
    string Name { get; }
    Color Color { get; }
    BoardElementController GetFigureByCoords((int x, int y) coords);
    List<BoardElementController> ActiveFiguresValues { get; }
}