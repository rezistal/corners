using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardElementCell : MonoBehaviour, IBoardElementComponent
{
    [SerializeField]
    private IBoardElementController controller;
    [SerializeField]
    private GameObject highlight;
    [SerializeField]
    private Image background;

    public string Name { get => "cell"; }

    private void Awake()
    {
        controller = GetComponent<IBoardElementController>();
    }

    private void Start()
    {
        highlight.SetActive(false);
    }

    public void Select()
    {
        highlight.SetActive(true);
    }

    public void Deselect()
    {
        highlight.SetActive(false);
    }

    public void SetColor(Color color)
    {
        background.color = color;
    }

    public void Evolutiate()
    {
        throw new System.NotImplementedException();
    }
}
