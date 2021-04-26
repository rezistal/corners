using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : IBoardManager
{
    public Dictionary<(int x, int y), IBoardElementController> Figures { get; }
    public List<IBoardElementController> ActiveFigures { get; }
    public IBoard Board { get; }

    private IBuilderBoardCells Builder;

    public BoardManager(IBoard board)
    {
        Figures = new Dictionary<(int x, int y), IBoardElementController>();
        ActiveFigures = new List<IBoardElementController>();
        Board = board;
        Builder = new BuilderBoardSpartans();
    }

    //Все клетки не выделены и не кликабельны
    public void ResetBoard()
    {
        foreach (IBoardElementController b in Figures.Values)
        {
            b.Deactivate();
            b.Deselect();
        }
    }

    //Снимаем выделение и кликабельность с ранее выбранных клеток
    public void ResetSelected()
    {
        foreach (IBoardElementController b in ActiveFigures)
        {
            b.Deactivate();
            b.Deselect();
        }
        ActiveFigures.Clear();
    }

    //Выеделяем и делаем кликабельными клетки по координатам
    public void Select(List<(int x, int y)> cells)
    {
        foreach((int x, int y) c in cells)
        {
            if (Figures.ContainsKey(c))
            {
                Figures[c].Activate();
                Figures[c].Select();
                ActiveFigures.Add(Figures[c]);
            }
        }
    }

    //Размещаем клетки в игре
    public void CreateBoardAt(Transform parent)
    {
        foreach ((int x, int y) in Board.StartCondition)
        {
            IBoardElementController bec = Builder.BuildBoardElement(x, y, parent);
            Figures.Add((x, y), bec);
        }
    }
}
