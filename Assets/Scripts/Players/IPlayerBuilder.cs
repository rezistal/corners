using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBuilder
{
    IBoardElementController BuildPlayerFigure(int x, int y, Transform parent, IPlayer player);
}

