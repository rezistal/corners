using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardElementCell : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private IBoardElementController controller;
    [SerializeField]
    private GameObject highlight;

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

    public string Name { get => "cell"; }

    public IRule Rule { get; set; }
}
