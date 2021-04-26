using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardElementPawn : MonoBehaviour, IBoardElementComponent
{
    [SerializeField]
    private IBoardElementController controller;
    [SerializeField]
    private Image background;
    private Color oldColor;
    private Sprite queen;

    public string Name { get => "pawn"; }

    private void Awake()
    {
        controller = GetComponent<IBoardElementController>();
    }

    public void Select()
    {
        oldColor = background.color;
        background.color = Color.yellow;
    }

    public void Deselect()
    {
        background.color = oldColor;
    }

    public void Setup(Color color, Sprite s)
    {
        background.color = color;
        oldColor = color;
        queen = s;
    }

    public void Evolutiate()
    {
        background.sprite = queen;
    }
}
