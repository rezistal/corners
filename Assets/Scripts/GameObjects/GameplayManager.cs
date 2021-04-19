using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private PlayerChoises pch;
    [SerializeField]
    private Canvas canvas;

    private IRule rule;
    private IGameMode gameMode;
    private Corners gm;


    public delegate void FigureAction(int x, int y);

    private void OnEnable()
    {
        BoardElementController.Clicked += ApplyRules;
    }

    private void OnDisable()
    {
        BoardElementController.Clicked -= ApplyRules;
    }


    public void ApplyRules(int x, int y)
    {
        gm.CheckRules(x,y);
    }

    // Start is called before the first frame update
    void Start()
    {
        rule = pch.Rule;
        gameMode = pch.GameMode;

        gm = new Corners();
        gm.CreateGameOnCanvas(canvas.transform);

    }

    // Update is called once per frame
    void Update()
    {
        gm.UpdateGame();
    }
}
