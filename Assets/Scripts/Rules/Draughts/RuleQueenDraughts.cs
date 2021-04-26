using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleQueenDraughts : IRule
{
    public List<((int x, int y) cellToMove, (int x, int y) cellToKill)> 
        GetKillPositions(
            int current_x,
            int current_y,
            List<(int x, int y)> friendlyFigures,
            List<(int x, int y)> enemyFigures,
            int boardSize
        )
    {
        List<((int x, int y) cellToMove, (int x, int y) cellToKill)> result = new List<((int x, int y) cellToMove, (int x, int y) cellToKill)>();
        result.AddRange(KillDirection(1, 1, current_x, current_y, friendlyFigures, enemyFigures, boardSize));
        result.AddRange(KillDirection(1, -1, current_x, current_y, friendlyFigures, enemyFigures, boardSize));
        result.AddRange(KillDirection(-1, 1, current_x, current_y, friendlyFigures, enemyFigures, boardSize));
        result.AddRange(KillDirection(-1, -1, current_x, current_y, friendlyFigures, enemyFigures, boardSize));
        return result;
    }

    public List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize)
    {
        List<(int x, int y)> result = new List<(int x, int y)>();
        result.AddRange(MoveDirection(1, 1, current_x, current_y, boardState, boardSize));
        result.AddRange(MoveDirection(1, -1, current_x, current_y, boardState, boardSize));
        result.AddRange(MoveDirection(-1, 1, current_x, current_y, boardState, boardSize));
        result.AddRange(MoveDirection(-1, -1, current_x, current_y, boardState, boardSize));
        return result;
    }

    private List<(int x, int y)> MoveDirection(int inc_x, int inc_y, int current_x, int current_y, List<(int x, int y)> boardState, int boardSize)
    {
        int next_x = current_x + inc_x;
        int next_y = current_y + inc_y;
        List<(int x, int y)> result = new List<(int x, int y)>();
        if (next_x > 0
            && next_x <= boardSize
            && next_y > 0
            && next_y <= boardSize
            && !boardState.Contains((next_x, next_y)))
        {
            result.Add((next_x, next_y));
            result.AddRange(MoveDirection(inc_x, inc_y, next_x, next_y, boardState, boardSize));
        }
        return result;
    }

    private List<((int x, int y) cellToMove, (int x, int y) cellToKill)> 
        KillDirection(
            int inc_x, 
            int inc_y, 
            int current_x, 
            int current_y, 
            List<(int x, int y)> friendlyFigures, 
            List<(int x, int y)> enemyFigures, 
            int boardSize
        )
    {
        (int x, int y) cellToMove = (current_x + inc_x*2, current_y + inc_y*2);
        (int x, int y) cellToKill = (current_x + inc_x, current_y + inc_y);
        int next_y = current_y + inc_y;
        List<((int x, int y) cellToMove, (int x, int y) cellToKill)> result = new List<((int x, int y) cellToMove, (int x, int y) cellToKill)>();
        if (cellToMove.x > 0
            && cellToMove.x <= boardSize
            && cellToMove.y > 0
            && cellToMove.y <= boardSize
            && cellToKill.x > 0
            && cellToKill.x <= boardSize
            && cellToKill.y > 0
            && cellToKill.y <= boardSize
            )
        {
            if (!friendlyFigures.Contains(cellToMove) && !enemyFigures.Contains(cellToMove) && enemyFigures.Contains(cellToKill))
            {
                result.Add((cellToMove, cellToKill));
            }
            else
            {
                if(!friendlyFigures.Contains(cellToMove) && !enemyFigures.Contains(cellToMove))
                {
                    result.AddRange(KillDirection(inc_x, inc_y, cellToKill.x, cellToKill.y, friendlyFigures, enemyFigures, boardSize));
                }
            }
        }
        return result;
    }
}
