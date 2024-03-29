﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : IPlayerManager
{
    public Dictionary<(int x, int y), IBoardElementController> StartPositions { get; }
    public List<IBoardElementController> AllFiguresValues
    {
        get
        {
            List<IBoardElementController> bl = new List<IBoardElementController>();
            foreach (IPlayer p in PlayersChain.Params)
            {
                bl.AddRange(p.FiguresValues);
            }
            return bl;
        }
    }
    public List<(int x, int y)> AllFiguresKeys
    {
        get
        {
            List<(int x, int y)> tl = new List<(int x, int y)>();
            foreach (IPlayer p in PlayersChain.Params)
            {
                tl.AddRange(p.FiguresKeys);
            }
            return tl;
        }
    }

    public ChainedParameters<IPlayer> PlayersChain { get; private set; }
    public IBoardElementController SelectedFigure { get; private set; }
    public IPlayer CurrentPlayer { get => PlayersChain.Current; }
    public IPlayer NextPlayer { get => PlayersChain.GetNext(); }

    public event GameplayManager.AI ActivateAI;

    private IPlayerBuilder Builder;

    public PlayerManager(List<IPlayer> players)
    {
        PlayersChain = new ChainedParameters<IPlayer>(players);
        StartPositions = new Dictionary<(int x, int y), IBoardElementController>();
        Builder = new PlayerBuilderSpartans();
    }

    //Выбор следующего игрока
    public void ChangePlayer()
    {
        PlayersChain.SetNext();
    }

    //Блокировка всех фигур и снятие с них выделения
    public void DeactivateAll()
    {
        foreach (IBoardElementController b in AllFiguresValues)
        {
            b.Deactivate();
            b.Deselect();
        }
    }

    //Передаем управление AIPlayer или делаем фигуры текущего игрока кликабельными
    public void ActivateCurrentPlayer()
    {
        if (PlayersChain.Current.GetType().ToString() == "AIPlayer")
        {
            ActivateAI();
        }
        else
        {
            foreach (IBoardElementController b in PlayersChain.Current.FiguresValues)
            {
                b.Activate();
            }
        }
    }

    //Запоминаем и подсвечиваем выбранную фигуру
    public void Select(IBoardElementController figure)
    {
        //Снимаем старое выделение
        if (SelectedFigure != null)
        {
            SelectedFigure.Deselect();
        }

        SelectedFigure = figure;
        SelectedFigure.Select();
    }

    //Перемещение запомненной фигуры на новые координаты
    public void MoveFigureTo((int x, int y) coords)
    {
        MoveSelected(coords);
        Deselect();
    }

    public void MoveSelected((int x, int y) coords)
    {
        //Меняем координаты фигуры
        SelectedFigure.SetCoordinates(coords);
    }

    public void Deselect()
    {
        //Снимаем сфигуры выделение
        SelectedFigure.Deselect();
        //Обнуляем выбранную фигуру 
        SelectedFigure = null;
    }

    //Перемещение запомненной фигуры на новые координаты и срубание фигуры на указанной клетке
    public void MoveToKill((int x, int y) cellToMove, (int x, int y) cellToKill)
    {
        //Меняем координаты фигуры
        SelectedFigure.SetCoordinates(cellToMove);
        //Получаем фигуру которую нужно срубить по переданным координатам
        IBoardElementController b = NextPlayer.GetFigureByCoords(cellToKill);
        //Рубим фигуру
        b.Die();
    }

    //Возвращаем все фигуры на их изначальные позиции
    public void SoftReset()
    {
        SelectedFigure = null;
        foreach (var v in StartPositions)
        {
            v.Value.Resurrect();
            v.Value.SetCoordinates(v.Key);
        }
    }
    
    //Размещаем фигуры на поле
    public void CreatePlayerFiguresAt(Transform parent)
    {
        foreach (IPlayer player in PlayersChain.Params)
        {
            foreach ((int x, int y) in player.StartCondition)
            {
                IBoardElementController bec = Builder.BuildPlayerFigure(x, y, parent, player);

                player.FiguresValues.Add(bec);
                StartPositions.Add((x, y), bec);
            }
        }
    }
}
