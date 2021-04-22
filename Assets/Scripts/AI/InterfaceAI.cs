using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceAI
{
    (BoardElementController element, (int x, int y) coords) Calculations();
}
