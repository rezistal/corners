using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private PlayerChoises pch;
    [SerializeField]
    private Canvas chooseGame;
    [SerializeField]
    private Canvas cornersOptions;
    [SerializeField]
    private Dropdown ruleOptions;
    [SerializeField]
    private Dropdown modeOptions;
    [SerializeField]
    private Canvas cornersOptionsPVP;
    [SerializeField]
    private Canvas cornersOptionsPVE;
    [SerializeField]
    private InputField firstNamePVP;
    [SerializeField]
    private InputField secondNamePVP;
    [SerializeField]
    private InputField firstNamePVE;

    void Start()
    {
        chooseGame.gameObject.SetActive(true);
        cornersOptions.gameObject.SetActive(false);
        cornersOptionsPVP.gameObject.SetActive(false);
        cornersOptionsPVE.gameObject.SetActive(false);
    }

    public void Corners()
    {
        chooseGame.gameObject.SetActive(false);
        cornersOptions.gameObject.SetActive(true);
    }

    public void Chess()
    {

    }

    public void Draughts()
    {

    }

    public void BackToMenu()
    {
        chooseGame.gameObject.SetActive(true);
        cornersOptions.gameObject.SetActive(false);
    }

    public void NextCornersOptions()
    {
        cornersOptions.gameObject.SetActive(false);
        switch (modeOptions.value)
        {
            case 0:
                cornersOptionsPVP.gameObject.SetActive(true);
                break;
            case 1:
                cornersOptionsPVE.gameObject.SetActive(true);
                break;
        }
    }

    public void BackToCornersOptions()
    {
        cornersOptionsPVP.gameObject.SetActive(false);
        cornersOptionsPVE.gameObject.SetActive(false);
        cornersOptions.gameObject.SetActive(true);
    }

    public void StartCorners()
    {
        switch (ruleOptions.value)
        {
            case 0:
                pch.Rule = new RuleDraughts();
                break;
            case 1:
                pch.Rule = new RuleJumps();
                break;
            case 2:
                pch.Rule = new RuleSteps();
                break;
        }

        List<IPlayer> players = new List<IPlayer>();
        switch (modeOptions.value)
        {
            case 0:
                players.Add(new RealPlayer(new BottomRightSC(), Color.black, firstNamePVP.text));
                players.Add(new RealPlayer(new TopLeftSC(), Color.red, secondNamePVP.text));
                break;
            case 1:
                players.Add(new AIPlayer());
                players.Add(new RealPlayer(new TopLeftSC(), Color.red, firstNamePVE.text));
                break;
        }
        pch.PlayerManager = new PlayerManager(players);

        IBoard board = new ClassicChessBoard();
        pch.BoardManager = new BoardManager(board);

        pch.GameMode = new Corners(pch.Rule, pch.BoardManager, pch.PlayerManager);

        SceneManager.LoadScene("GameplayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
