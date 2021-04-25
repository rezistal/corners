using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : IPlayerManager
{
    public Dictionary<(int x, int y), BoardElementController> StartPositions { get; }
    public List<BoardElementController> AllFiguresValues
    {
        get
        {
            List<BoardElementController> bl = new List<BoardElementController>();
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
    public BoardElementController SelectedFigure { get; private set; }
    public IPlayer CurrentPlayer { get => PlayersChain.Current; }
    public IPlayer NextPlayer { get => PlayersChain.GetNext(); }

    public event GameplayManager.AI ActivateAI;

    public PlayerManager(List<IPlayer> players)
    {
        PlayersChain = new ChainedParameters<IPlayer>(players);
        StartPositions = new Dictionary<(int x, int y), BoardElementController>();
    }

    //Выбор следующего игрока
    public void ChangePlayer()
    {
        PlayersChain.SetNext();
    }

    //Блокировка всех фигур и снятие с них выделения
    public void DeactivateAll()
    {
        foreach (BoardElementController b in AllFiguresValues)
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
            foreach (BoardElementController b in PlayersChain.Current.FiguresValues)
            {
                b.Activate();
            }
        }
    }

    //Запоминаем и подсвечиваем выбранную фигуру
    public void Select(BoardElementController figure)
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
        //Перемещаем фигуру по полю
        SelectedFigure.SetTransform(new Vector2((coords.x * 2 + 1) * 64, (coords.y * 2 + 1) * 64));
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
        //Перемещаем фигуру по полю
        SelectedFigure.SetTransform(new Vector2((cellToMove.x * 2 + 1) * 64, (cellToMove.y * 2 + 1) * 64));
        //Получаем фигуру которую нужно срубить по переданным координатам
        BoardElementController b = NextPlayer.GetFigureByCoords(cellToKill);
        //Рубим фигуру
        b.Alive = false;
        //Отключаем ее видимость на поле
        b.gameObject.SetActive(false);
    }

    //Возвращаем все фигуры на их изначальные позиции
    public void SoftReset()
    {
        SelectedFigure = null;
        foreach (var v in StartPositions)
        {
            v.Value.Alive = true;
            v.Value.gameObject.SetActive(true);
            v.Value.SetCoordinates(v.Key);
            v.Value.SetTransform(new Vector2((v.Key.x * 2 + 1) * 64, (v.Key.y * 2 + 1) * 64));
        }
    }
    
    //Размещаем фигуры на поле
    public void CreatePlayerFiguresAt(Transform parent)
    {
        foreach (IPlayer player in PlayersChain.Params)
        {
            foreach ((int x, int y) in player.StartCondition)
            {
                GameObject o = Object.Instantiate(player.Prefab);
                o.transform.SetParent(parent, false);
                BoardElementController bec = o.GetComponent<BoardElementController>();
                bec.SetCoordinates((x, y));
                bec.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));
                
                bec.Color = player.Color;
                bec.Select();
                bec.Deselect();

                player.FiguresValues.Add(bec);
                StartPositions.Add((x, y), bec);
            }
        }
    }
}
