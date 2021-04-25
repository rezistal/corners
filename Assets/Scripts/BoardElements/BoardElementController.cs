using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IBoardElementController : IPointerClickHandler
{
    int X { get; }
    int Y { get; }
    GameObject GameObject { get; }
    IRule Rule { get; set; }
    Color Color { get; set; }
    string ElementName { get; }
    bool Alive { get; set; }

    void SetSprite(Sprite s);
    void SetCoordinates((int x, int y) c);
    (int x, int y) GetCoordinates();
    void SetTransform(Vector2 v);
    void Activate();
    void Deactivate();
    void Select();
    void Deselect();

    event GameplayManager.Figure Clicked;
}

[Serializable]
public class BoardElementController : MonoBehaviour, IBoardElementController
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private IBoardElement element;

    public int X { get; private set; }
    public int Y { get; private set; }
    public GameObject GameObject { get => gameObject; }

    public IRule Rule { get => element.Rule; set => element.Rule = value; }
    public Color Color { get => img.color; set => img.color = value; }

    public string ElementName { get => element.Name; }

    public bool Alive { get; set; }

    public event GameplayManager.Figure Clicked;

    private void Awake()
    {
        Alive = true;
        element = gameObject.GetComponent<IBoardElement>();
    }

    public void SetSprite(Sprite s)
    {
        img.sprite = s;
    }

    public void SetCoordinates((int x, int y) c)
    {
        X = c.x;
        Y = c.y;
    }

    public (int x, int y) GetCoordinates()
    {
        return (X, Y);
    }

    public void SetTransform(Vector2 v)
    {
        rectTransform.localPosition = v;
    }

    public void Activate()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void Deactivate()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(this);
    }

    public void Select()
    {
        element.Select();
    }

    public void Deselect()
    {
        element.Deselect();
    }
}
