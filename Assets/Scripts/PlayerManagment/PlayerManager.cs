using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public BoardElementController selectedFigure;
    public Dictionary<(int x, int y), BoardElementController> AllFigures { get; set; }
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
        foreach (BoardElementController b in AllFigures.Values)
        {
            b.Deactivate();
            b.Deselect();
        }
    }

    public void ActivateCurrentPlayer()
    {
        foreach(BoardElementController b in playersChain.Current.Figures.Values)
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
        //Убираем старые координаты из списка всех фигур
        AllFigures.Remove(selectedFigure.GetCoordinates());
        //Добавляем новые координаты
        AllFigures.Add(coords, selectedFigure);
        //Убираем старые координаты из списка фигур активного игрока
        CurrentPlayer.Figures.Remove(selectedFigure.GetCoordinates());
        //Добавляем новые координаты
        CurrentPlayer.Figures.Add(coords, selectedFigure);
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
        AllFigures = new Dictionary<(int x, int y), BoardElementController>();
        playersChain = new ChainedParameters<IPlayer>(players);
    }

    public void SoftReset()
    {
        selectedFigure = null;
        foreach (var v in StartPositions)
        {
            v.Value.SetCoordinates(v.Key);
            v.Value.SetTransform(new Vector2((v.Key.x * 2 + 1) * 64, (v.Key.y * 2 + 1) * 64));
        }
        AllFigures = new Dictionary<(int x, int y), BoardElementController>(StartPositions);

        foreach (IPlayer player in playersChain.Params())
        {
            player.Figures.Clear();
            foreach ((int x, int y) in player.StartCondition)
            {
                player.Figures.Add((x, y), AllFigures[(x,y)]);
            }
        }
    }
    
    public void CreatePlayerFiguresAt(Transform parent)
    {
        foreach (IPlayer player in playersChain.Params())
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

                player.Figures.Add((x, y), bec);
                AllFigures.Add((x, y), bec);
            }
        }
        StartPositions = new Dictionary<(int x, int y), BoardElementController>(AllFigures);
    }
}
