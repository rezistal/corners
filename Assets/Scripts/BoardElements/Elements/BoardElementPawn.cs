using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardElementPawn : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private IBoardElementController controller;

    private Color oldColor;
    public string Name { get => "pawn"; }
    public IRule Rule { get; set; }

    private void Awake()
    {
        controller = GetComponent<IBoardElementController>();
    }

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
