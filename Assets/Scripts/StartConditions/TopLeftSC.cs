using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftSC : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
            (1,8),(2,8),(3,8),
            (1,7),(2,7),(3,7),
            (1,6),(2,6),(3,6),
        };
    }
}
