using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleDraughtsReal : IRule
{
    public List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize)
    {
        List<(int x, int y)> steps = new RuleSteps().GetPositions(current_x, current_y, boardState, boardSize);
        List<(int x, int y)> jumps = new RuleDraughts().GetPositions(current_x, current_y, boardState, boardSize);
        steps.AddRange(jumps);
        return steps;
    }

    List<((int x, int y) cellToMove, (int x, int y) cellToKill)> IRule.GetKillPositions(int current_x, int current_y, List<(int x, int y)> friendlyFigures, List<(int x, int y)> enemyFigures, int boardSize)
    {
        throw new System.NotImplementedException();
    }
}
