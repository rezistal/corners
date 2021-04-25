using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMChess : IGameMode
{
    public bool Endgame { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Manage(IBoardElementController element)
    {
        throw new System.NotImplementedException();
    }

    public void StartGame()
    {
        throw new System.NotImplementedException();
    }

    public void StopGame()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ManageAI(IBoardElementController element, (int x, int y) coords)
    {
        throw new System.NotImplementedException();
    }
}
