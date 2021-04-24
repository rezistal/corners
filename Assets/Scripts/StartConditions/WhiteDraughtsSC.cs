using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteDraughtsSC : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
            (1,3),      (3,3),      (5,3),      (7,3),
                  (2,2),      (4,2),      (6,2),      (8,2),
            (1,1),      (3,1),      (5,1),      (7,1),
        };
    }
}