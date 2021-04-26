using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IBoardElementController : IPointerClickHandler
{
    int X { get; }
    int Y { get; }
    
    string ElementName { get; }
    
    bool Alive { get; }
    IRule Rule { get; set; }
    //Color Color { get; set; }
    GameObject GameObject { get; }
    event GameplayManager.Figure Clicked;

    void Die();
    void Resurrect();

    void Activate();
    void Deactivate();

    void Select();
    void Deselect();

    void SetCoordinates((int x, int y) c);
    (int x, int y) GetCoordinates();
    
    void Evolutiate();

}
