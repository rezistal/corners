using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardElementCell : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private BoardElementController controller;
    [SerializeField]
    private GameObject highlight;

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

    public string Name { get => "cell"; }

    public IRule Rule { get; set; }
}
