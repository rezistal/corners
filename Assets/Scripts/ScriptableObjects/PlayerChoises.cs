using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerChoises : ScriptableObject
{
    public IRule Rule { get; set; }
    public IGameMode GameMode { get; set; }

    private void OnEnable()
    {
        Rule = new RuleJumps();
        GameMode = new Corners(Rule);

    }

    private void OnDisable()
    {
        
    }
}
