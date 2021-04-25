using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AICorners : IArtificialIntellect
{
    private Dictionary<(int x, int y), int> DecisionMatrix;
    private IPlayerManager playerManager;
    private IBoardManager boardManager;

    public AICorners(IPlayerManager playerManager, IBoardManager boardManager)
    {
        this.playerManager = playerManager;
        this.boardManager = boardManager;
        List<(int x, int y)> keys = boardManager.Board.StartCondition;
        List<int> values = new List<int>()
        {
            1008, 962, 912, 800,   720, 613, 492, 357,
             962, 912, 858, 780,   693, 592, 477, 348,
             912, 858, 825, 733,   652, 557, 448, 325,
             800, 780, 733, 672,   597, 508, 405, 288,
             720, 693, 652, 597,   528, 445, 348, 237,
             613, 592, 557, 508,   445, 368, 277, 172,
             492, 477, 448, 405,   348, 277, 192,  93,
             357, 348, 325, 288,   237, 172,  93,   0
        };
        DecisionMatrix = keys.Select((k, i) => new { k, v = values[i] }).ToDictionary(x => x.k, x => x.v);
    }

    public (BoardElementController element, (int x, int y) coords) Calculations()
    {
        Dictionary<BoardElementController, (int val, (int x, int y) coords)> figuresMax =
            new Dictionary<BoardElementController, (int, (int x, int y))>();

        int currentSum = 0;
        foreach (BoardElementController figure in playerManager.CurrentPlayer.FiguresValues)
        {
            //Текущая сумма по всем клеткам
            currentSum += DecisionMatrix[figure.GetCoordinates()];
            //Доступные фигуре для перемещения клетки
            List<(int x, int y)> cells = figure.Rule.GetPositions(figure.x, figure.y, playerManager.AllFiguresKeys, boardManager.Board.Size);

            figuresMax.Add(figure, (0, (0, 0)));
            foreach ((int x, int y) cell in cells)
            {
                if (DecisionMatrix.ContainsKey(cell) && DecisionMatrix[cell] > figuresMax[figure].val)
                {
                    //Для фигуры запоминаем клетку с максимальным приоритетом
                    figuresMax[figure] = (DecisionMatrix[cell], cell);
                }
            }
        }
        BoardElementController selectedFigure = figuresMax.First().Key;
        (int x, int y) selectedCell = figuresMax.First().Value.coords;

        int max = 0;
        foreach (BoardElementController figure in playerManager.CurrentPlayer.FiguresValues)
        {
            //Перевычисляем сумму для всех фигур с учетом того куда они бы сходили
            int calculated_sum = currentSum - DecisionMatrix[figure.GetCoordinates()] + figuresMax[figure].val;
            if (calculated_sum > max)
            {
                //Выбираем ту фигуру, влияние которой на итоговую сумму всех фигур будет максимальным
                max = calculated_sum;
                selectedFigure = figure;
                selectedCell = figuresMax[figure].coords;
            }
        }

        //Выбранная AI фигура и клетка
        return (selectedFigure, selectedCell);
    }
}

