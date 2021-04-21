using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Corners : IGameMode
{
    private IRule rule;
    private BoardManager boardManager;
    private PlayerManager playerManager;

    public bool Endgame { get; set; }

    public Corners(IRule r, BoardManager b, PlayerManager p)
    {
        rule = r;
        playerManager = p;
        boardManager = b;
        Endgame = false;
    }
    
    public void StartGame()
    {
        //В этой игре все фигуры двигаются по одному выбранному игроками правилу. Применяем его к каждой фигуре
        foreach (BoardElementController bec in playerManager.AllFigures.Values.ToList())
        {
            bec.Rule = rule;
        }
        Refresh();
        //Делаем фигуры текущего игрока кликабельными
        playerManager.ActivateCurrentPlayer();
    }

    public void StopGame()
    {   
        Refresh();
    }

    //Условие победы в уголках - все фигуры текущего игрока встали на начальные ккординаты его оппонента
    private bool WinCondition()
    {
        //return true;
        List<(int, int)> currentPlayerFiguresPositions = playerManager.CurrentPlayer.Figures.Keys.ToList();
        List<(int, int)> opponentFiguresStartPositions = playerManager.NextPlayer.StartCondition;

        foreach (var v in currentPlayerFiguresPositions)
        {
            if (!opponentFiguresStartPositions.Contains(v))
            {
                return false;
            }
        }
        return true;
    }

    private void Refresh()
    {
        //Делаем ВСЕ клетки некликабельными
        boardManager.ResetBoard();
        //Делаем все фигуры некликабельными
        playerManager.DeactivateAll();
    }

    public void Manage(BoardElementController figure)
    {
        switch (figure.ElementName)
        {
            case "pawn":
                //Снимаем подсветку с выбранных
                boardManager.ResetSelected();
                //Снимаем подсветку с предыдущей активной фигуры
                playerManager.ResetSelected();
                //Запоминаем выбранную фигуру
                playerManager.selectedFigure = figure;
                //Подсвечиваем выбранную фигуру
                playerManager.selectedFigure.Select();
                //Вычисляем координаты куда согласно правилам может сходить фигура
                List<(int,int)> cells = figure.Rule.GetPositions(figure.x, figure.y, playerManager.AllFigures.Keys.ToList());
                //Подсвечиваем найденный координаты
                boardManager.Select(cells);
                break;
            //Клетки обычно некликабельны. Если клетка кликабельна - значит выбрана фигура и клетка доступна для перемещения на нее
            case "cell":
                //Двигаем фигуру
                playerManager.MoveFigureTo(figure.GetCoordinates());
                //Снимаем подсветку с ранее выбранных клеток
                boardManager.ResetSelected();
                //Проверям условие победы
                if (WinCondition())
                {
                    //Останавливаем игру
                    StopGame();
                    //Флаг окончания игры
                    Endgame = true;
                }
                else
                {
                    //Следующий игрок
                    playerManager.ChangePlayer();
                    //Передаем ход следующему игроку
                    Refresh();
                    //Делаем фигуры текущего игрока кликабельными
                    playerManager.ActivateCurrentPlayer();
                }
                break;
        }
    }
}
