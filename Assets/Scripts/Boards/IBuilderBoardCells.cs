using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilderBoardCells
{
    IBoardElementController BuildBoardElement(int x, int y, Transform parent);
}
