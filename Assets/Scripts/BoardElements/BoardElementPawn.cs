using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardElementPawn : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private BoardElementController controller;
    [SerializeField]
    private Color oldColor;

    private void Start()
    {
        oldColor = controller.Color;
    }

    public void Select()
    {
        controller.Color = Color.yellow;
    }

    public void DeSelect()
    {
        controller.Color = oldColor;
    }
}
