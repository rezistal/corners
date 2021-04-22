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
    private Canvas cornersRulesOptions;
    [SerializeField]
    private Canvas cornersModesOptions;

    [SerializeField]
    private Canvas cornersOptionsPVP;
    [SerializeField]
    private Canvas cornersOptionsPVE;

    [SerializeField]
    private Dropdown ruleOptions;
    [SerializeField]
    private Dropdown modeOptions;

    [SerializeField]
    private InputField firstNamePVP;
    [SerializeField]
    private InputField secondNamePVP;
    [SerializeField]
    private InputField firstNamePVE;

    void Start()
    {
        chooseGame.gameObject.SetActive(true);
        cornersRulesOptions.gameObject.SetActive(false);
        cornersModesOptions.gameObject.SetActive(false);
        cornersOptionsPVP.gameObject.SetActive(false);
        cornersOptionsPVE.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    #region Mainmenu
    public void Chess()
    {

    }

    public void Draughts()
    {

    }

    public void Corners()
    {
        chooseGame.gameObject.SetActive(false);
        cornersRulesOptions.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion Mainmenu

    #region RulesOption
    public void CornersRulesOptions()
    {
        cornersRulesOptions.gameObject.SetActive(false);
        switch (ruleOptions.value)
        {
            case 0:
            case 1:
                cornersOptionsPVP.gameObject.SetActive(true);
                break;
            case 2:
                cornersModesOptions.gameObject.SetActive(true);
                break;
        }
    }

    public void BackToMenu()
    {
        chooseGame.gameObject.SetActive(true);
        cornersRulesOptions.gameObject.SetActive(false);
    }
    #endregion RulesOption

    public void CornersModesOptions()
    {
        cornersModesOptions.gameObject.SetActive(false);
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

    public void BackToCornersRulesOptions()
    {
        cornersRulesOptions.gameObject.SetActive(true);
        cornersOptionsPVP.gameObject.SetActive(false);
        cornersModesOptions.gameObject.SetActive(false);
    }

    public void BackToCornersModesOptions()
    {
        cornersModesOptions.gameObject.SetActive(true);
        cornersOptionsPVE.gameObject.SetActive(false);
    }

    public void StartCorners()
    {
        int mode = modeOptions.value;
        switch (ruleOptions.value)
        {
            case 0:
                mode = 0;
                pch.Rule = new RuleDraughts();
                break;
            case 1:
                mode = 0;
                pch.Rule = new RuleJumps();
                break;
            case 2:
                pch.Rule = new RuleSteps();
                break;
        }

        List<IPlayer> players = new List<IPlayer>();
        switch (mode)
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
        pch.ai = new AICorners();
        pch.PlayerManager = new PlayerManager(players, pch.ai);

        IBoard board = new ClassicChessBoard();
        pch.BoardManager = new BoardManager(board);

        pch.GameMode = new Corners(pch.Rule, pch.BoardManager, pch.PlayerManager);

        SceneManager.LoadScene("GameplayScene");
    }


}
