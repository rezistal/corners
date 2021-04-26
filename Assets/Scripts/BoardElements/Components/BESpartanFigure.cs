using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BESpartanFigure : MonoBehaviour, IBoardElementComponent
{
    [SerializeField]
    private Image unselected;
    [SerializeField]
    private Image selected;

    private Sprite unselectedEvaluated;
    private Sprite selectedEvaluated;
   
    public string Name { get => "pawn"; }

    public void Deselect()
    {
        selected.gameObject.SetActive(false);
        unselected.gameObject.SetActive(true);
    }

    public void Select()
    {
        selected.gameObject.SetActive(true);
        unselected.gameObject.SetActive(false);
    }

    public void SetImages(Sprite unselected, Sprite selected, Sprite unselectedEvaluated, Sprite selectedEvaluated)
    {
        this.unselected.sprite = unselected;
        this.selected.sprite = selected;
        this.unselectedEvaluated = unselectedEvaluated;
        this.selectedEvaluated = selectedEvaluated;
    }

    public void Evolutiate()
    {
        selected.sprite = selectedEvaluated;
        unselected.sprite = unselectedEvaluated;
    }
}
