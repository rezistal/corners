using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardElementPawn : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private BoardElementController controller;

    private Color oldColor;
    public string Name { get => "pawn"; }
    public IRule Rule { get; set; }

    public void Select()
    {
        oldColor = controller.Color;
        controller.Color = Color.yellow;
    }

    public void Deselect()
    {
        controller.Color = oldColor;
    }


}
