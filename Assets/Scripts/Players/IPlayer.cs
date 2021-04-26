using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    List<(int x, int y)> FiguresKeys { get; }
    List<IBoardElementController> FiguresValues { get; }
    List<(int x, int y)> StartCondition { get; }
    string Name { get; }
    Color Color { get; }
    IBoardElementController GetFigureByCoords((int x, int y) coords);
    List<IBoardElementController> ActiveFiguresValues { get; }
}