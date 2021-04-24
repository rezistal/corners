using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArtificialIntellect
{
    (BoardElementController element, (int x, int y) coords) Calculations();
}
