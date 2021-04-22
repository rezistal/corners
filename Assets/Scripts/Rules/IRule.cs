using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
    List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize);
}
