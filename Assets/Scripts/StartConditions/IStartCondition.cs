using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStartCondition
{
    List<(int x, int y)> GetConditions();
}
