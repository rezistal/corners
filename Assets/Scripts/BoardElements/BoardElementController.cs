using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoardElementController : MonoBehaviour, IPointerClickHandler
{
    public int x;
    public int y;

    public static event GameplayManager.FigureAction Clicked;

    private Image img;
    private IBoardElement element;
    private RectTransform rt;

    private void Awake()
    {
        img = gameObject.GetComponent<Image>();
        element = gameObject.GetComponent<IBoardElement>();
        rt = gameObject.GetComponent<RectTransform>();
        //rt.anchoredPosition = Vector3.zero;
    }
    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        img.color = color;
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

    public void SetTransform(Vector3 v)
    {
        rt.position = v;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked(x, y);
        //element.Clicked(x, y);
    }
}
