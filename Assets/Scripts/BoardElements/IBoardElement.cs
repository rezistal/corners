using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardElement
{ 
    void Select();
    void Deselect();
    string Name { get; }
    IRule Rule { get; set; }
}
