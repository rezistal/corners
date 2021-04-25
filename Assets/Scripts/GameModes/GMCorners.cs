using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GMCorners : IGameMode
{
    private IRule rule;
    private IBoardManager boardManager;
    private IPlayerManager playerManager;

    public bool Endgame { get; set; }

    public GMCorners(IRule r, IBoardManager b, IPlayerManager p)
    {
        rule = r;
        playerManager = p;
        boardManager = b;
        Endgame = false;
    }
    
    public void StartGame()
    {
        //В этой игре все фигуры двигаются по одному выбранному игроками правилу. Применяем его к каждой фигуре
        foreach (BoardElementController bec in playerManager.AllFiguresValues)
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
        List<(int, int)> currentPlayerFiguresPositions = playerManager.CurrentPlayer.FiguresKeys;
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

    //Ход игры для игроков
    public void Manage(BoardElementController figure)
    {
        switch (figure.ElementName)
        {
            case "pawn":
                //Снимаем подсветку с выбранных
                boardManager.ResetSelected();
                //Запоминаем и подсвечиваем выбранную фигуру
                playerManager.Select(figure);
                //Вычисляем координаты куда согласно правилам может сходить фигура
                List<(int,int)> cells = figure.Rule.GetPositions(figure.x, figure.y, playerManager.AllFiguresKeys, boardManager.Board.Size);
                //Подсвечиваем найденные координаты
                boardManager.Select(cells);
                break;
            //Клетки обычно некликабельны. Если клетка кликабельна - значит выбрана фигура и клетка доступна для перемещения на нее
            case "cell":
                //Двигаем фигуру
                playerManager.MoveFigureTo(figure.GetCoordinates());
                //Снимаем подсветку с ранее выбранных клеток
                boardManager.ResetSelected();
                //Проверям условие победы
                CheckWin();
                break;
        }
    }

    //Ход игры для AI
    public IEnumerator ManageAI(BoardElementController figure, (int x, int y) cell)
    {
        yield return new WaitForSeconds(0.8f);
        //Запоминаем и подсвечиваем выбранную фигуру
        playerManager.Select(figure);
        yield return new WaitForSeconds(0.8f);
        //Подсвечиваем выбранную координату
        boardManager.Figures[cell].Select();
        yield return new WaitForSeconds(0.8f);
        //Двигаем фигуру
        playerManager.MoveFigureTo(cell);
        yield return new WaitForSeconds(0.8f);
        //Снимаем подсветку с ранее выбранной клетки
        boardManager.Figures[cell].Deselect();
        //Проверям условие победы
        CheckWin();
    }

    private void CheckWin()
    {
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
    }
}
