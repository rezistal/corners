using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BESpartanCell : MonoBehaviour, IBoardElement
{
    public string Name => "cell";

    public IRule Rule { get ; set; }

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
