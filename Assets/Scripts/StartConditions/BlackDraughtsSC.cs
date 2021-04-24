using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDraughtsSC : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
                  (2,8),      (4,8),      (6,8),      (8,8),
            (1,7),      (3,7),      (5,7),      (7,7),
                  (2,6),      (4,6),      (6,6),      (8,6),
        };
    }
}