using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerManager
{
    Dictionary<(int x, int y), IBoardElementController> StartPositions { get; }
    List<IBoardElementController> AllFiguresValues { get; }
    List<(int x, int y)> AllFiguresKeys { get; }

    ChainedParameters<IPlayer> PlayersChain { get; }
    IBoardElementController SelectedFigure { get; }
    IPlayer CurrentPlayer { get; }
    IPlayer NextPlayer { get; }

    event GameplayManager.AI ActivateAI;

    void ChangePlayer();
    void DeactivateAll();
    void ActivateCurrentPlayer();
    void Select(IBoardElementController figure);
    void MoveFigureTo((int x, int y) coords);
    void MoveSelected((int x, int y) coords);
    void Deselect();
    void MoveToKill((int x, int y) cellToMove, (int x, int y) cellToKill);
    void SoftReset();
    void CreatePlayerFiguresAt(Transform parent);

}