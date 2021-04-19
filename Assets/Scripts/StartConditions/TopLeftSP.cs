using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftSC : IStartCondition
{
    public List<(int x, int y)> GetConditions()
    {
        return new List<(int x, int y)>
        {
            (6,3),(7,3),(8,3),
            (6,2),(7,2),(8,2),
            (6,1),(7,1),(8,1),
        };
    }
}
