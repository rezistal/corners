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
    private IPlayerManager playerManager;
    private IBoardManager boardManager;
    private IArtificialIntellect ai;

    public delegate void AI();
    public delegate void Figure(BoardElementController element);

    private void Manage(BoardElementController element)
    {
        gameMode.Manage(element);
    }

    private void ManageAI()
    {
        (BoardElementController element, (int x, int y) coords) = ai.Calculations();
        StartCoroutine(gameMode.ManageAI(element, coords));
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
        playerManager.ActivateAI += ManageAI;
    }

    private void OnDisable()
    {
        BoardElementController.Clicked -= Manage;
        playerManager.ActivateAI -= ManageAI;
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
            if(playerManager.CurrentPlayer.GetType().ToString() == "AIPlayer")
            {
                forfeitButton.SetActive(false);
            }
            else
            {
                forfeitButton.SetActive(true);
            }
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
