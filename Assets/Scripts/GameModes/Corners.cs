using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : IGameMode
{
    private GameObject figurePrefab;
    private GameObject cellPrefab;


    public enum States
    {
        START,
        PLAYER_ONE_TURN,
        PLAYER_TWO_TURN,
        END
    }

    public List<(int x, int y)> board;

    public IStartCondition firstPlayerSC;
    public IStartCondition seconsdPlayerSC;

    public Corners()
    {
        firstPlayerSC = new BottomRightSC();
        seconsdPlayerSC = new TopLeftSC();
    }

    public void CreateGameOnCanvas(Transform canvas)
    {
        //cellPrefab = Resources.LoadAll<Sprite>("Sprites/Logos");
        //cellPrefab = Resources.Load<GameObject>("Prefabs/Cell");
        figurePrefab = Resources.Load<GameObject>("Prefabs/Figure");
        
        foreach (var (x, y) in firstPlayerSC.GetConditions())
        {
            GameObject o = GameObject.Instantiate(figurePrefab, canvas);
            //o.transform.SetParent(canvas, false);
            BoardElementController bc = o.GetComponent<BoardElementController>();
            bc.SetCoordinates(x, y);
            bc.SetTransform(new Vector3((x * 2 + 1)*64, (y * 2 + 1)*64, 0));
            bc.SetColor(Color.black);
        }

        foreach (var (x, y) in seconsdPlayerSC.GetConditions())
        {
            GameObject o = GameObject.Instantiate(figurePrefab, canvas);
            //o.transform.SetParent(canvas, false);
            BoardElementController bc = o.GetComponent<BoardElementController>();
            bc.SetCoordinates(x, y);
            bc.SetTransform(new Vector3((x * 2 + 1) * 64, (y * 2 + 1) * 64, 0));
            bc.SetColor(Color.white);
        }
        
    }

    public void UpdateGame()
    {

    }

    public void CheckRules(int x, int y)
    {

    }
}
