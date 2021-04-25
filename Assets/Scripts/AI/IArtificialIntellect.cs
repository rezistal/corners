using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArtificialIntellect
{
    (IBoardElementController element, (int x, int y) coords) Calculations();
}
