using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager
{
    public BoardElementController selectedFigure;
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
    private Dictionary<(int x, int y), BoardElementController> StartPositions;
    private ChainedParameters<IPlayer> playersChain;

    public IPlayer CurrentPlayer { get => playersChain.Current; }
    public IPlayer NextPlayer { get => playersChain.GetNext(); }

    public void ChangePlayer()
    {
        playersChain.SetNext();
    }

    public void DeactivateAll()
    {
        foreach (BoardElementController b in AllFiguresValues)
        {
            b.Deactivate();
            b.Deselect();
        }
    }

    public void ActivateCurrentPlayer()
    {
        foreach(BoardElementController b in playersChain.Current.FiguresValues)
        {
            b.Activate();
        }
    }

    public void ResetSelected()
    {
        if(selectedFigure != null)
        {
            selectedFigure.Deselect();
        }
    }

    public void MoveFigureTo((int x, int y) coords)
    {
        //Убираем старые координаты из списка фигур активного игрока
        CurrentPlayer.FiguresKeys.Remove(selectedFigure.GetCoordinates());
        //Добавляем новые координаты
        CurrentPlayer.FiguresKeys.Add(coords);
        //Меняем координаты фигуры
        selectedFigure.SetCoordinates(coords);
        //Перемещаем фигуру по полю
        selectedFigure.SetTransform(new Vector2((coords.x * 2 + 1) * 64, (coords.y * 2 + 1) * 64));
        //Снимаем сфигуры выделение
        selectedFigure.Deselect();
        //Обнуляем выбранную фигуру 
        selectedFigure = null;
    }

    public PlayerManager(List<IPlayer> players)
    {
        playersChain = new ChainedParameters<IPlayer>(players);
        StartPositions = new Dictionary<(int x, int y), BoardElementController>();
    }

    public void SoftReset()
    {
        selectedFigure = null;
        foreach (var v in StartPositions)
        {
            v.Value.SetCoordinates(v.Key);
            v.Value.SetTransform(new Vector2((v.Key.x * 2 + 1) * 64, (v.Key.y * 2 + 1) * 64));
        }
        foreach (IPlayer player in playersChain.Params)
        {
            player.FiguresKeys.Clear();
            foreach ((int x, int y) in player.StartCondition)
            {
                player.FiguresKeys.Add((x, y));
            }
        }
    }
    
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

                player.FiguresKeys.Add((x, y));
                player.FiguresValues.Add(bec);
                StartPositions.Add((x, y), bec);
            }
        }
    }
}
