using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class BoardElementController : MonoBehaviour, IBoardElementController
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private IBoardElementComponent component;

    public int X { get; private set; }
    public int Y { get; private set; }
    public string ElementName { get => component.Name; }
    public IRule Rule { get; set; }
    public GameObject GameObject { get => gameObject; }
    public bool Alive { get; private set; }
    public event GameplayManager.Figure Clicked;

    private void Awake()
    {
        Alive = true;
        component = gameObject.GetComponent<IBoardElementComponent>();
    }

    public void Die()
    {
        Alive = false;
        gameObject.SetActive(false);
    }

    public void Resurrect()
    {
        Alive = true;
        gameObject.SetActive(true);
    }

    public void Activate()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void Deactivate()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void Select()
    {
        component.Select();
    }

    public void Deselect()
    {
        component.Deselect();
    }

    public void SetCoordinates((int x, int y) c)
    {
        X = c.x;
        Y = c.y;
        rectTransform.localPosition = new Vector2((X * 2 + 1) * 64, (Y * 2 + 1) * 64);
    }

    public (int x, int y) GetCoordinates()
    {
        return (X, Y);
    }

    public void Evolutiate()
    {
        component.Evolutiate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(this);
    }

}