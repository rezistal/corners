using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GMDraughts : IGameMode
{
    public bool Endgame { get; set; }

    private IBoardManager boardManager;
    private IPlayerManager playerManager;
    private TurnFlow state;
    private List<((int x, int y) cellToMove, (int x, int y) cellToKill)> killList;
    private IRule queenRule;
    private Sprite queenSprite;
    private List<(int x, int y)> blackQueenSC;
    private List<(int x, int y)> whiteQueenSC;


    private enum TurnFlow
    {
        PICK_FIGURE,  //Можно выбирать фигуры
        MOVE_FIGURE,  //Можно передвинуть фигуру не срубая другую
        KILLING_SPREE //Передвинуть фигуру можно только срубив другую шашку
    }

    public GMDraughts(IBoardManager boardManager, IPlayerManager playerManager)
    {
        this.boardManager = boardManager;
        this.playerManager = playerManager;
        Endgame = false;
        queenRule = new RuleQueenDraughts();
        queenSprite = Resources.Load<Sprite>("Images/Queen");
        blackQueenSC = new SCBlackQueen().GetConditions();
        whiteQueenSC = new SCWhiteQueen().GetConditions();
        state = TurnFlow.PICK_FIGURE;
    }

    private void CheckQueen(BoardElementController figure)
    {
        if (!figure.Rule.Equals(queenRule) && (blackQueenSC.Contains(figure.GetCoordinates()) || whiteQueenSC.Contains(figure.GetCoordinates())))
        {
            figure.Rule = queenRule;
            figure.SetSprite(queenSprite);
        }
    }

    public void Manage(BoardElementController figure)
    {
        List<(int x, int y)> selectCells;
        switch (state)
        {
            case TurnFlow.PICK_FIGURE:
                //Снимаем подсветку с выбранных клеток
                boardManager.ResetSelected();
                //Запоминаем и подсвечиваем выбранную фигуру
                playerManager.Select(figure);
                //Список фигур которые можно срубить
                killList = figure.Rule.GetKillPositions(
                    figure.x, figure.y, playerManager.CurrentPlayer.FiguresKeys, playerManager.NextPlayer.FiguresKeys, boardManager.Board.Size);

                if (killList.Any())
                {
                    //Подсвечиваем только те поля по которым можно срубить
                    selectCells = killList.Select(i => i.cellToMove).ToList();
                    boardManager.Select(selectCells);
                    state = TurnFlow.KILLING_SPREE;
                }
                else
                {
                    //Вычисляем координаты куда согласно правилам может сходить фигура
                    List<(int, int)> emptyCells = figure.Rule.GetPositions(figure.x, figure.y, playerManager.AllFiguresKeys, boardManager.Board.Size);
                    boardManager.Select(emptyCells);
                    state = TurnFlow.MOVE_FIGURE;
                }
                break;
            case TurnFlow.KILLING_SPREE:
                switch (figure.ElementName)
                {
                    //Если можно срубить несколько фигур по разным направлениям то шашку можно перевыбрать
                    case "pawn":
                        state = TurnFlow.PICK_FIGURE;
                        Manage(figure);
                        break;
                    //На какую клетку встали что бы срубить фигуру
                    case "cell":
                        //Снимаем подсветку с ранее выбранных клеток
                        boardManager.ResetSelected();
                        //Из списка фигур которые можно срубить берем координаты фигуры которую шашка рубит переходя на текущую клетку
                        (int x, int y) cellToKill = killList.Where(x => x.cellToMove == (figure.x, figure.y)).ToList().ElementAt(0).cellToKill;
                        //Перемещаем фигуру и рубим фигуру на координате
                        playerManager.MoveToKill(figure.GetCoordinates(), cellToKill);
                        //Стала ли фигура дамкой
                        CheckQueen(playerManager.SelectedFigure);
                        //Перевычисляем список фигур которые еще раз может срубить текущая фигура
                        killList = playerManager.SelectedFigure.Rule.GetKillPositions(
                            figure.x, figure.y, playerManager.CurrentPlayer.FiguresKeys, playerManager.NextPlayer.FiguresKeys, boardManager.Board.Size);

                        //Продолжаем рубить - можно только ранее выбранной фигурой
                        if (killList.Any())
                        {
                            //Делаем все остальные фигуры некликабельными
                            playerManager.DeactivateAll();
                            //Продолжаем рубить выбранной
                            playerManager.Select(playerManager.SelectedFigure);
                            //Подсвечиваем клетки куда можно срубить дальше
                            selectCells = killList.Select(i => i.cellToMove).ToList();
                            boardManager.Select(selectCells);
                            state = TurnFlow.KILLING_SPREE;
                        }
                        else
                        {
                            //Следующий ход когда нечего рубить
                            MakeNextTurn();
                        }

                        break;
                }
                break;
            case TurnFlow.MOVE_FIGURE:
                switch (figure.ElementName)
                {
                    //Фигуру можно перевыбрать
                    case "pawn":
                        state = TurnFlow.PICK_FIGURE;
                        Manage(figure);
                        break;
                    case "cell":
                        //Двигаем фигуру
                        playerManager.MoveSelected(figure.GetCoordinates());
                        //Стала ли фигура дамкой
                        CheckQueen(playerManager.SelectedFigure);
                        //Отменяем выделение фигуры
                        playerManager.Deselect();
                        //Снимаем подсветку с ранее выбранных клеток
                        boardManager.ResetSelected();
                        //Следующий ход
                        MakeNextTurn();
                        break;
                }
                break;
        }
    }

    //Передаем ход следующему игроку
    private void MakeNextTurn()
    {
        //Делаем ВСЕ клетки некликабельными
        boardManager.ResetBoard();
        //Делаем все фигуры некликабельными
        playerManager.DeactivateAll();

        //Проверяем условие победы текущего игрока
        if (CheckWin())
        {
            //Флаг окончания игры
            Endgame = true;
        }
        else
        {
            //Следующий игрок
            playerManager.ChangePlayer();
            //Если ни одна не может рубить - активируем все фигуры
            if (!killList.Any())
            {
                playerManager.ActivateCurrentPlayer();
            }
            state = TurnFlow.PICK_FIGURE;
        }
    }

    private bool CheckWin()
    {
        killList.Clear();
        List<(int x, int y)> availableCells = new List<(int x, int y)>();
        foreach (BoardElementController b in playerManager.NextPlayer.ActiveFiguresValues)
        {
            //Проверяем может ли следующий игрок рубить фигуры
            List<((int x, int y) cellToMove, (int x, int y) cellToKill)> kills = b.Rule.GetKillPositions(
                b.x, b.y, playerManager.NextPlayer.FiguresKeys, playerManager.CurrentPlayer.FiguresKeys, boardManager.Board.Size);
            //Активируем только те фишки которые могут рубить
            if (kills.Any())
            {
                killList.AddRange(kills);
                b.Activate();
            }

            //Проверяем может ли следующий игрок ходить
            availableCells.AddRange(b.Rule.GetPositions(b.x, b.y, playerManager.AllFiguresKeys, boardManager.Board.Size));
        }
        //Нет ходов
        if(!killList.Any() && !availableCells.Any())
        {
            return true;
        }
        return false;
    } 

    public IEnumerator ManageAI(BoardElementController element, (int x, int y) coords)
    {
        yield return new WaitForSeconds(2f);
    }

    public void StartGame()
    {
        foreach (IPlayer player in playerManager.PlayersChain.Params)
        {
            if (player.Color == Color.black)
            {
                player.FiguresValues.ForEach(i => i.Rule = new RuleBlackDraughts());
            }
            if (player.Color == Color.red)
            {
                player.FiguresValues.ForEach(i => i.Rule = new RuleWhiteDraughts());
            }
        }

        //Делаем ВСЕ клетки некликабельными
        boardManager.ResetBoard();
        //Делаем все фигуры некликабельными
        playerManager.DeactivateAll();
        //Делаем фигуры текущего игрока кликабельными
        playerManager.ActivateCurrentPlayer();
    }

    public void StopGame()
    {
        //Делаем ВСЕ клетки некликабельными
        boardManager.ResetBoard();
        //Делаем все фигуры некликабельными
        playerManager.DeactivateAll();
    }
}

