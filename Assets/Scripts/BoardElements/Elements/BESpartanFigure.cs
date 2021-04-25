using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BESpartanFigure : MonoBehaviour, IBoardElement
{
    public string Name => throw new System.NotImplementedException();

    public IRule Rule { get; set; }

    public void Deselect()
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
