using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public interface IPlayer {}
public class IPlayer
{
    public Dictionary<(int x, int y), BoardElementController> Figures { get; }
    public Color Color { get; }
    public IStartCondition StartCondition { get; }

    public List<(int x, int y)> GetConditions()
    {
        return StartCondition.GetConditions();
    }

    public IPlayer(IStartCondition startCondition, Color color)
    {
        StartCondition = startCondition;
        Color = color;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
    }
}

//public class RealPlayer : IPlayer {}
//public class AIPlayer : IPlayer {}

public class PlayerManager
{
    public Dictionary<(int x, int y), BoardElementController> Figures { get; }
    public List<IPlayer> Players { get; }

    public PlayerManager(List<IPlayer> players)
    {
        Players = players;
        Figures = new Dictionary<(int x, int y), BoardElementController>();
    }
    
    public IPlayer Next()
    {
        throw new System.NotImplementedException();
    }

    public void CreatePlayerFiguresAt(Transform parent)
    {
        foreach (IPlayer player in playerManager.Players)
        {
            foreach ((int x, int y) in player.GetConditions())
            {
                GameObject o = Object.Instantiate(figurePrefab);
                o.transform.SetParent(parent, false);
                BoardElementController bec = o.GetComponent<BoardElementController>();
                bec.SetCoordinates((x, y));
                bec.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));

                bec.Rule = rule;
                bec.Color = player.Color;

                player.Figures.Add((x, y), bec);
                playerManager.Figures.Add((x, y), bec);
            }
        }

    }
}
