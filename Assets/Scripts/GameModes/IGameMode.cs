using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameMode
{
    void StartGame();
    void StopGame();
    void Manage(IBoardElementController element);
    bool Endgame { get; set; }
    IEnumerator ManageAI(IBoardElementController element, (int x, int y) coords);
}
