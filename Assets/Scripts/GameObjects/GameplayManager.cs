using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private PlayerChoises pch;
    [SerializeField]
    private Transform cellsLayer;
    [SerializeField]
    private Transform figuresLayer;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject forfeitButton;

    private IGameMode gameMode;
    private PlayerManager playerManager;
    private BoardManager boardManager;
    private InterfaceAI ai;

    public delegate void Figure(BoardElementController element);
    public delegate void Choice(BoardElementController element, (int x, int y) coords);

    private void Manage(BoardElementController element)
    {
        gameMode.Manage(element);
    }

    private void AIManage(BoardElementController element, (int x, int y) coords)
    {
        StartCoroutine(AITurnStepOne(element));
        StartCoroutine(AITurnStepTwo(coords));
    }

    private IEnumerator AITurnStepOne(BoardElementController element)
    {
        gameMode.Manage(element);
        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator AITurnStepTwo((int x, int y) coords)
    {
        yield return new WaitForSeconds(0.4f);
        gameMode.Manage(boardManager.Figures[coords]);
        StopAllCoroutines();
    }
    private void Awake()
    {
        playerManager = pch.PlayerManager;
        boardManager = pch.BoardManager;
        gameMode = pch.GameMode;
        ai = pch.ai;
    }
    private void OnEnable()
    {
        BoardElementController.Clicked += Manage;
        ai.Declare += AIManage;

    }

    private void OnDisable()
    {
        BoardElementController.Clicked -= Manage;
        ai.Declare -= AIManage;
    }

    void Start()
    {
        //Заполняем поле фигурами и клетками
        playerManager.CreatePlayerFiguresAt(figuresLayer);
        boardManager.CreateBoardAt(cellsLayer);

        ResetGame();
    }

    private void Update()
    {
        if (gameMode.Endgame)
        {
            title.text = "Победил " + playerManager.CurrentPlayer.Name + "!";
            title.color = playerManager.CurrentPlayer.Color;
            forfeitButton.SetActive(false);
        }
        else
        {
            title.text = "Сейчас ходит " + playerManager.CurrentPlayer.Name;
            title.color = playerManager.CurrentPlayer.Color;
        }
    }

    public void Forfeit()
    {
        gameMode.StopGame();
        gameMode.Endgame = true;
        playerManager.ChangePlayer();
        forfeitButton.SetActive(false);
    }

    //Бросаем кубик кто будет ходить первым
    private IEnumerator RollDice()
    {
        yield return new WaitForSeconds(0.2f);
        playerManager.ChangePlayer();
        StartCoroutine(RollDice());
        yield return new WaitForSeconds(2.5f);
        StopAllCoroutines();
        forfeitButton.SetActive(true);
        gameMode.StartGame();
    }

    public void ResetGame()
    {
        StopAllCoroutines();
        forfeitButton.SetActive(false);
        gameMode.StopGame();
        gameMode.Endgame = false;
        playerManager.SoftReset();
        StartCoroutine(RollDice());
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
