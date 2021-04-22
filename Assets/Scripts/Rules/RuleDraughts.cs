using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RuleDraughts : IRule
{
    public List<(int x, int y)> GetPositions(int current_x, int current_y, List<(int x, int y)> boardState, int boardSize)
    {
        List<((int x, int y) emptySlots, (int x, int y) filledSlots)> init = new List<((int x, int y), (int x, int y))>
        {
            ((current_x + 2, current_y + 2),(current_x + 1, current_y + 1)),
            ((current_x - 2, current_y + 2),(current_x - 1, current_y + 1)),
            ((current_x + 2, current_y - 2),(current_x + 1, current_y - 1)),
            ((current_x - 2, current_y - 2),(current_x - 1, current_y - 1)),
        };

        List<((int x, int y) emptySlots, (int x, int y) filledSlots)> requirements =
            init.Where(i =>
                i.emptySlots.x > 0 &&
                i.emptySlots.x <= boardSize &&
                i.emptySlots.y > 0 &&
                i.emptySlots.y <= boardSize &&
                i.filledSlots.x > 0 &&
                i.filledSlots.x <= boardSize &&
                i.filledSlots.y > 0 &&
                i.filledSlots.y <= boardSize
            ).Select(i => i).ToList();

        List<(int x, int y)> result = new List<(int x, int y)>();
        foreach (var (emptySlots, filledSlots) in requirements)
        {
            if (boardState.Contains(filledSlots) && !boardState.Contains(emptySlots))
            {
                result.Add(emptySlots);
            }
        }
        if (result.Any())
        {
            for (int i = result.Count - 1; i >= 0; i--)
            {
                int tx = result.ElementAt(i).x;
                int ty = result.ElementAt(i).y;
                boardState.Add((tx, ty));
                result.AddRange(GetPositions(tx, ty, boardState, boardSize));
            }
            return result;
        }
        else
        {
            return new List<(int x, int y)>();
        }
    }
}
