using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardElementController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private IBoardElement element;

    public int x { get; private set; }
    public int y { get; private set; }

    public IRule Rule { get => element.Rule; set => element.Rule = value; }
    public Color Color { get => img.color; set => img.color = value; }
    public string ElementName { get => element.Name; }

    public bool Alive { get; set; }

    public static event GameplayManager.Figure Clicked;

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
        x = c.x;
        y = c.y;
    }

    public (int x, int y) GetCoordinates()
    {
        return (x, y);
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
