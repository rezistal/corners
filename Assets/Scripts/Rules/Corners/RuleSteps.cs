using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RuleSteps : IRule
{
    public List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize)
    {
        List<(int x, int y)> init = new List<(int x, int y)>
        {
            (current_x + 1, current_y + 1),
            (current_x + 1, current_y + 0),
            (current_x + 1, current_y - 1),
            (current_x + 0, current_y - 1),
            (current_x - 1, current_y - 1),
            (current_x - 1, current_y + 0),
            (current_x - 1, current_y + 1),
            (current_x + 0, current_y + 1)
        };

        List<(int x, int y)> requirements =
            init.Where(i =>
                i.x > 0 &&
                i.x <= boardSize &&
                i.y > 0 &&
                i.y <= boardSize
            ).Select(i => i).ToList();

        List<(int x, int y)> result = new List<(int x, int y)>();

        foreach (var requirement in requirements)
        {
            if (!boardState.Contains(requirement))
            {
                result.Add(requirement);
            }
        }

        return result;
    }

    List<((int x, int y) cellToMove, (int x, int y) cellToKill)> IRule.GetKillPositions(int current_x, int current_y, List<(int x, int y)> friendlyFigures, List<(int x, int y)> enemyFigures, int boardSize)
    {
        throw new System.NotImplementedException();
    }
}
