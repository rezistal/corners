using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager
{
    private Dictionary<(int x, int y), BoardElementController> StartPositions;

    public ChainedParameters<IPlayer> playersChain { get; private set; }
    public static event GameplayManager.AI ActivateAI;
    public BoardElementController selectedFigure { get; private set; }
    public IPlayer CurrentPlayer { get => playersChain.Current; }
    public IPlayer NextPlayer { get => playersChain.GetNext(); }
    public List<(int x, int y)> AllFiguresKeys
    {
        get
        {
            List<(int x, int y)> tl = new List<(int x, int y)>();
            foreach (IPlayer p in playersChain.Params)
            {
                tl.AddRange(p.FiguresKeys);
            }
            return tl;
        }
    }
    public List<BoardElementController> AllFiguresValues
    {
        get
        {
            List<BoardElementController> bl = new List<BoardElementController>();
            foreach (IPlayer p in playersChain.Params)
            {
                bl.AddRange(p.FiguresValues);
            }
            return bl;
        }
    }

    public PlayerManager(List<IPlayer> players)
    {
        playersChain = new ChainedParameters<IPlayer>(players);
        StartPositions = new Dictionary<(int x, int y), BoardElementController>();
    }

    //Выбор следующего игрока
    public void ChangePlayer()
    {
        playersChain.SetNext();
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
        if (playersChain.Current.GetType().ToString() == "AIPlayer")
        {
            ActivateAI();
        }
        else
        {
            foreach (BoardElementController b in playersChain.Current.FiguresValues)
            {
                b.Activate();
            }
        }
    }

    //Запоминаем и подсвечиваем выбранную фигуру
    public void Select(BoardElementController figure)
    {
        //Снимаем старое выделение
        if (selectedFigure != null)
        {
            selectedFigure.Deselect();
        }

        selectedFigure = figure;
        selectedFigure.Select();
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
        selectedFigure.SetCoordinates(coords);
        //Перемещаем фигуру по полю
        selectedFigure.SetTransform(new Vector2((coords.x * 2 + 1) * 64, (coords.y * 2 + 1) * 64));
    }

    public void Deselect()
    {
        //Снимаем сфигуры выделение
        selectedFigure.Deselect();
        //Обнуляем выбранную фигуру 
        selectedFigure = null;
    }

    //Перемещение запомненной фигуры на новые координаты и срубание фигуры на указанной клетке
    public void MoveToKill((int x, int y) cellToMove, (int x, int y) cellToKill)
    {
        //Меняем координаты фигуры
        selectedFigure.SetCoordinates(cellToMove);
        //Перемещаем фигуру по полю
        selectedFigure.SetTransform(new Vector2((cellToMove.x * 2 + 1) * 64, (cellToMove.y * 2 + 1) * 64));
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
        selectedFigure = null;
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
        foreach (IPlayer player in playersChain.Params)
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
