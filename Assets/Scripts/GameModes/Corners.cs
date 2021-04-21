using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Corners : IGameMode
{
    private readonly IRule rule;
    private readonly BoardManager boardManager;
    private readonly PlayerManager playerManager;

    public BoardElementController selectedFigure;

    public Corners(IRule r, BoardManager b, PlayerManager p)
    {
        rule = r;
        playerManager = p;
        boardManager = b;
    }

    /*
    public bool WinCondition()
    {
        List<(int, int)> playerFigures = PlayerFigures;
        List<(int, int)> opponentFigures = OpponentFigures;

        foreach (var v in playerFigures)
        {
            if (!opponentFigures.Contains(v))
            {
                return false;
            }
        }
        return true;
    }
    */
    public void Manage(BoardElementController element)
    {
        /*
        string t = element.element.GetType().FullName;

        switch (t)
        {
            case "BoardElementPawn":

                selectedFigure = element;

                foreach (var i in cellsList)
                {
                    i.DeSelect();
                    i.Deactivate();
                }
                foreach (var i in allFiguresList)
                {
                    i.DeSelect();
                }
                element.Select();
                var v = element.Rule.GetPositions(element.x, element.y, PlayersFigures);
                foreach (var a in cellsList)
                {
                    if (v.Contains(a.GetCoordinates()))
                    {
                        a.Select();
                        a.Activate();
                    }
                }
                break;
            case "BoardElementCell":
                foreach (var i in cellsList)
                {
                    i.DeSelect();
                    i.Deactivate();
                }
                foreach (var i in allFiguresList)
                {
                    i.DeSelect();
                }
                (int x, int y) c = element.GetCoordinates();
                selectedFigure.SetCoordinates(c);
                selectedFigure.SetTransform(new Vector2((c.x * 2 + 1) * 64, (c.y * 2 + 1) * 64));
                SwitchPlayer();
                break;
        }
        */
    }
}
