using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    
    [SerializeField]
    private PlayerChoises pch;
    [SerializeField]
    private Transform backgroundLayer;
    [SerializeField]
    private Transform cellsLayer;
    [SerializeField]
    private Transform figuresLayer;

    private IGameMode gameMode;
    private Corners gm;
   
    public delegate void Figure(BoardElementController element);

    private void Manage(BoardElementController element)
    {
        gm.Manage(element);
    }

    private void OnEnable()
    {
        BoardElementController.Clicked += Manage;
    }

    private void OnDisable()
    {
        BoardElementController.Clicked -= Manage;
    }

    void Start()
    {
        IRule rule = pch.Rule;
        gameMode = pch.GameMode;

        gm = new Corners(new RuleSteps());
        gm.CreateGameOnCanvas(backgroundLayer, cellsLayer, figuresLayer);

        gm.StartGame();
    }
   
}
