using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilderOldColored : IPlayerBuilder
{
    private GameObject Prefab;
    private Sprite queen;

    public PlayerBuilderOldColored()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Figure");
        queen = Resources.Load<Sprite>("Images/queen");
    }

    public IBoardElementController BuildPlayerFigure(int x, int y, Transform parent, IPlayer player)
    {

        GameObject o = Object.Instantiate(Prefab);
        o.transform.SetParent(parent, false);
        IBoardElementController bec = o.GetComponent<IBoardElementController>();

        bec.SetCoordinates((x, y));

        BoardElementPawn cell = bec.GameObject.GetComponent<BoardElementPawn>();
        cell.Setup(player.Color, queen);

        return bec;
    }
}
