using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerChoises : ScriptableObject
{
    public IRule Rule { get; set; }
    public IGameMode GameMode { get; set; }
    public IPlayerManager PlayerManager { get; set; }
    public IBoardManager BoardManager { get; set; }
    public IArtificialIntellect ai { get; set; }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }
}
