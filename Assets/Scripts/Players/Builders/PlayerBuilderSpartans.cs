using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilderSpartans : IPlayerBuilder
{
    private GameObject Prefab;

    private Sprite whitePawn;
    private Sprite whitePawnSelected;
    private Sprite whiteQueen;
    private Sprite whiteQueenSelected;


    private Sprite blackPawn;
    private Sprite blackPawnSelected;
    private Sprite blackQueen;
    private Sprite blackQueenSelected;

    public PlayerBuilderSpartans()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Spartans/SpartanFigure");

        whitePawn = Resources.Load<Sprite>("Images/Spartans/Figures/whitePawn");
        whitePawnSelected = Resources.Load<Sprite>("Images/Spartans/Figures/whitePawnSelected");
        whiteQueen = Resources.Load<Sprite>("Images/Spartans/Figures/whiteQueen");
        whiteQueenSelected = Resources.Load<Sprite>("Images/Spartans/Figures/whiteQueenSelected");

        blackPawn = Resources.Load<Sprite>("Images/Spartans/Figures/blackPawn");
        blackPawnSelected = Resources.Load<Sprite>("Images/Spartans/Figures/blackPawnSelected");
        blackQueen = Resources.Load<Sprite>("Images/Spartans/Figures/blackQueen");
        blackQueenSelected = Resources.Load<Sprite>("Images/Spartans/Figures/blackQueenSelected");
    }

    Color black = new Color(50 / 255f, 86 / 255f, 117 / 255f);
    Color red = new Color(163 / 255f, 6 / 255f, 25 / 255f);
    public IBoardElementController BuildPlayerFigure(int x, int y, Transform parent, IPlayer player)
    {
        GameObject o = Object.Instantiate(Prefab);
        o.transform.SetParent(parent, false);
        IBoardElementController bec = o.GetComponent<IBoardElementController>();

        bec.SetCoordinates((x, y));

        BESpartanFigure cell = bec.GameObject.GetComponent<BESpartanFigure>();
        if(player.Color == black)
        {
            cell.SetImages(blackPawn, blackPawnSelected, blackQueen, blackQueenSelected);
        }
        if (player.Color == red)
        {
            cell.SetImages(whitePawn, whitePawnSelected, whiteQueen, whiteQueenSelected);
        }

        return bec;
    }
}
