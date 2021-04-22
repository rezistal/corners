using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceAI
{
    void MakeTurn(List<BoardElementController> figures, List<(int x, int y)> allFigures);
    event GameplayManager.Choice Declare;
}
