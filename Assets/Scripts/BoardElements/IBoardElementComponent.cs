using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardElementComponent
{ 
    void Select();
    void Deselect();
    string Name { get; }
    void Evolutiate();
}
