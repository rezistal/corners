using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCWhiteQueen : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
                  (2,8),      (4,8),      (6,8),      (8,8),
        };
    }
}
