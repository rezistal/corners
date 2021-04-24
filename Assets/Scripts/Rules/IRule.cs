using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRule
{
    List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize);
    List<((int x, int y) cellToMove, (int x, int y) cellToKill)>
        GetKillPositions(
            int current_x,
            int current_y,
            List<(int x, int y)> friendlyFigures,
            List<(int x, int y)> enemyFigures,
            int boardSize
        );
}
