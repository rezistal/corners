using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerChoises : ScriptableObject
{
    public IRule Rule { get; set; }
    public IGameMode GameMode { get; set; }
    public PlayerManager PlayerManager { get; set; }
    public BoardManager BoardManager { get; set; }
    public IArtificialIntellect ai { get; set; }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }
}
