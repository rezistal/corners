using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSteps : IRule
{
    public List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState)
    {
        List<(int x, int y)> requirements = new List<(int x, int y)>
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
}
