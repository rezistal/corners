using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardElementController : MonoBehaviour, IPointerClickHandler
{
    public int x;
    public int y;

    public static event GameplayManager.Figure Clicked;

    [SerializeField]
    private Image img;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private CanvasGroup canvasGroup;

    public Color Color { get => img.color; set => img.color = value; }

    public IBoardElement element;
    public IRule Rule;

    private void Awake()
    {
        element = gameObject.GetComponent<IBoardElement>();

    }

    public void SetSprite(Sprite s)
    {
        img.sprite = s;
    }

    public void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public (int x, int y) GetCoordinates()
    {
        return (x, y);
    }

    public void SetTransform(Vector2 v)
    {
        rectTransform.localPosition = v;
    }

    public void SetState(bool state)
    {
        canvasGroup.blocksRaycasts = state;
        //element.SetActive();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(this);
    }

    public void Select()
    {
        element.Select();
    }

    public void DeSelect()
    {
        element.DeSelect();
    }
}
