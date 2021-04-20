using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum PlayerTurn
{
    AWAIT_FIGURE,
    AWAIT_CELL
}

public enum CurrentPlayer
{
    PLAYER_ONE,
    PLAYER_TWO
}

public class Corners : IGameMode
{
    
    private GameObject figurePrefab;
    private GameObject cellPrefab;
    private GameObject boardPrefab;

    public IStartCondition firstPlayerSC;
    public IStartCondition seconsdPlayerSC;
    public IStartCondition boardCells;


    public CurrentPlayer currentPlayer;

    public List<BoardElementController> CurrentPlayerFigures()
    {
        if (currentPlayer == CurrentPlayer.PLAYER_ONE)
        {
            return firstPlayerFigures;
        }
        else
        {
            return secondPlayerFigures;
        }
    }

    public List<BoardElementController> OpponentPlayerFigures()
    {
        if (currentPlayer == CurrentPlayer.PLAYER_TWO)
        {
            return secondPlayerFigures;
        }
        else
        {
            return firstPlayerFigures;
        }
    }


    public List<BoardElementController> firstPlayerFigures;
    public List<BoardElementController> secondPlayerFigures;

    public List<BoardElementController> playersFigures;
    public List<BoardElementController> cellsList;
    public List<BoardElementController> allFiguresList;
    
    public void FillFigures(Transform parent)
    {

    }

    private IRule rule;

    public Corners(IRule r)
    {
        rule = r;
        firstPlayerSC = new BottomRightSC();
        seconsdPlayerSC = new TopLeftSC();
        boardCells = new BoardSC();
        currentPlayer = CurrentPlayer.PLAYER_TWO;
    }

    public void CreateGameOnCanvas( Transform backgroundLayer, Transform cellsLayer, Transform figuresLayer)
    {
        firstPlayerFigures = new List<BoardElementController>();
        secondPlayerFigures = new List<BoardElementController>();
        playersFigures = new List<BoardElementController>();
        cellsList = new List<BoardElementController>();
        allFiguresList = new List<BoardElementController>();

        //Sprite boardBackground = Resources.Load<Sprite>("Images/field.png");
        boardPrefab = Resources.Load<GameObject>("Prefabs/Board");
        cellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
        figurePrefab = Resources.Load<GameObject>("Prefabs/Figure");

        //GameObject b = Object.Instantiate(boardPrefab);
        //b.transform.SetParent(backgroundLayer, false);

        foreach (var (x, y) in boardCells.GetConditions())
        {
            GameObject o = Object.Instantiate(cellPrefab);
            o.transform.SetParent(cellsLayer, false);
            BoardElementController bc = o.GetComponent<BoardElementController>();
            bc.SetCoordinates(x, y);
            bc.Rule = rule;
            bc.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));
            if((x + y) % 2 == 0)
            {
                bc.Color = Color.grey;
            }
            else
            {
                bc.Color = Color.white;
            }
            cellsList.Add(bc);
            allFiguresList.Add(bc);
        }
        
        foreach (var (x, y) in firstPlayerSC.GetConditions())
        {
            GameObject o = Object.Instantiate(figurePrefab);
            o.transform.SetParent(figuresLayer, false);
            BoardElementController bc = o.GetComponent<BoardElementController>();
            bc.SetCoordinates(x, y);
            bc.Rule = rule;
            bc.SetTransform(new Vector2((x * 2 + 1)*64, (y * 2 + 1)*64));
            bc.Color = Color.black;
            firstPlayerFigures.Add(bc);
            playersFigures.Add(bc);
            allFiguresList.Add(bc);
        }

        foreach (var (x, y) in seconsdPlayerSC.GetConditions())
        {
            GameObject o = Object.Instantiate(figurePrefab,Vector3.zero,Quaternion.identity);
            o.transform.SetParent(figuresLayer, false);
            BoardElementController bc = o.GetComponent<BoardElementController>();
            bc.SetCoordinates(x, y);
            bc.Rule = rule;
            bc.SetTransform(new Vector2((x * 2 + 1) * 64, (y * 2 + 1) * 64));
            bc.Color =  Color.red;
            secondPlayerFigures.Add(bc);
            playersFigures.Add(bc);
            allFiguresList.Add(bc);
        }
    }

    public void UpdateGame()
    {
        IRule rule = new RuleJumps();
    }

    public void CheckRules(int x, int y)
    {

    }

    public List<(int, int)> PlayersFigures
    {
        get => playersFigures.Select(i => i.GetCoordinates()).ToList();
    }

    public List<(int, int)> PlayerFigures
    {
        get => CurrentPlayerFigures().Select(i => i.GetCoordinates()).ToList();
    }

    public List<(int, int)> OpponentFigures
    {
        get => OpponentPlayerFigures().Select(i => i.GetCoordinates()).ToList();
    }

    public List<(int, int)> AllFigures
    {
        get => allFiguresList.Select(i => i.GetCoordinates()).ToList();
    }

    public List<(int, int)> AllCells
    {
        get => cellsList.Select(i => i.GetCoordinates()).ToList();
    }

    public bool WinCondition()
    {
        List<(int, int)> playerFigures = PlayerFigures;
        List<(int, int)> opponentFigures = OpponentFigures;

        foreach (var v in playerFigures)
        {
            if (!opponentFigures.Contains(v))
            {
                return false;
            }
        }
        return true;
    }

    public void StartGame()
    {

    }

    public void Manage(BoardElementController element)
    {
        foreach(var i in cellsList)
        {
            i.DeSelect();
        }
        foreach (var i in allFiguresList)
        {
            i.DeSelect();
        }
        element.Select();
        var v = element.Rule.GetPositions(element.x, element.y, PlayersFigures);
        foreach (var a in cellsList)
        {
            if (v.Contains(a.GetCoordinates()))
            {
                a.Select();
            }
        }
    }
   
}
