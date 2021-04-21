using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameMode
{
    void StartGame();
    void StopGame();
    void Manage(BoardElementController element);
    bool Endgame { get; set; }
}
