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

    private GameObject figurePrefab;
    private GameObject boardPrefab;

    void Start()
    {
        boardPrefab = Resources.Load<GameObject>("Prefabs/Board");
        figurePrefab = Resources.Load<GameObject>("Prefabs/Figure");

        IRule rule = pch.Rule;
        gameMode = pch.GameMode;

        List<IPlayer> players = new List<IPlayer>()
        {
            new IPlayer(new BottomRightSC(), Color.black),
            new IPlayer(new TopLeftSC(), Color.red)
        };

        PlayerManager = new PlayerManager(players);
        IBoard board = new ClassicChessBoard();
        BoardManager = new BoardManager(board);


        BoardManager boardManager = new BoardManager(new BoardSC(), new List<Color> { Color.grey, Color.white });

        gm = new Corners(new RuleSteps(), );
        //gm = new Corners(new RuleSteps(), new BoardClassicChess());
    }

    private void Update()
    {

    }

}
