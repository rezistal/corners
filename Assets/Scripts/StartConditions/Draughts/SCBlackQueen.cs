using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCBlackQueen : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
            (1,1),      (3,1),      (5,1),      (7,1),
        };
    }
}
