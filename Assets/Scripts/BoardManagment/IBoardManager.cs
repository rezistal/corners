using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardManager
{
    Dictionary<(int x, int y), IBoardElementController> Figures { get; }
    List<IBoardElementController> ActiveFigures { get; }
    IBoard Board { get; }
    void ResetBoard();
    void ResetSelected();
    void Select(List<(int x, int y)> cells);
    void CreateBoardAt(Transform parent);
}
