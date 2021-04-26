using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BESpartanCell : MonoBehaviour, IBoardElementComponent
{
    [SerializeField]
    private Image unselectedImage;
    [SerializeField]
    private Image selectedImage;

    public string Name => "cell";

    public void Deselect()
    {
        selectedImage.gameObject.SetActive(false);
        unselectedImage.gameObject.SetActive(true);
    }

    public void Select()
    {
        selectedImage.gameObject.SetActive(true);
        unselectedImage.gameObject.SetActive(false);
    }

    public void SetImages((Sprite unselected, Sprite selected) s)
    {
        unselectedImage.sprite = s.unselected;
        selectedImage.sprite = s.selected;
    }

    public void Evolutiate()
    {
        throw new System.NotImplementedException();
    }
}
