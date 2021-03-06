using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum Players
    {
        player1 = 0,
        player2 = 1
    }
    public static int DEFAULT_NUMBER_OF_PLAYERS = 2;
    public static string SHEEP_TAG      = "Sheep";
    public static string GUARD_TAG      = "Guard";
    public static string GHOST_TAG      = "Ghost";
    public static string PLAYER_TAG     = "Player";
    public static string SCORE_TAG      = "Score";
    public static string AUDIO_TAG      = "Audio";
    public static string RING_TAG       = "Ring";
    public static string CONTROLLER_TAG = "GameController";
    public static string GEM_TAG        = "Gem";
    public static string END_TAG = "EndPoint";


    private GameObject[] playerList;
    private int[] scoreList;
    private bool isGameOn = false;
    private Timer timer;
    private Canvas[] CanvasPlayerGUIs;
    private Audio gameAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = CONTROLLER_TAG;
        isGameOn = false;
        timer = this.GetComponentInChildren<Timer>();
        CanvasPlayerGUIs = FindObjectsOfType<Canvas>();
        gameAudio = GameObject.FindWithTag(AUDIO_TAG).GetComponent<Audio>();
        DisplayStartUI();
        GameObject.Find("Pause Button").GetComponent<Button>().interactable = false;
    }

    void Update()
    {
        if (HasGameEnded())
        {
            EndGame();
            DisplayEndScreen();
        }
    }

    // To start the game
    public void StartGame()
    {
        timer.SetTimer(GameParameter.gameTimer);
        timer.Awake();
        GameObject.Find("Pause Button").GetComponent<Button>().interactable = true;
        playerList = GameObject.FindGameObjectsWithTag(PLAYER_TAG);
        for (int i = 0; i < playerList.Length; ++i)
        {
            playerList[i].GetComponent<MoveWithKeyboardBehavior>()
                .SetInputKeyboard(GameParameter.inputs[i]);

            playerList[i].GetComponent<CelluloAgent>()
                .SetVisualEffect(VisualEffect.VisualEffectConstAll, GameParameter.colors[i], 255);
        }
        scoreList = new int[playerList.Length];
        DisplayGameUI();
        isGameOn = true;
    }

    // To pause the game (will be usefull later since different behavior than EndGame)
    public void PauseGame()
    {
        timer.PauseTimer();
        isGameOn = false;
        DisplayPauseScreen();
    }

    // To end the game
    private void EndGame()
    {
        isGameOn = false;
        GameObject.Find("Pause Button").GetComponent<Button>().interactable = false;
    }

    // To go back to the main menu
    public void QuitGame()
    {
        EndGame();
        timer.resetTimer();
        GameParameter.ResetGameParameter();
    }

    // To resume the game
    public void ResumeGame()
    {
        timer.ResumeTimer();
        isGameOn = true;
        DisplayGameUI();
    }

    public bool HasGameEnded()
    {
        return GetGameRunningStatus()
            ? timer.verifyIfTimeIsUp()
            : false;
    }

    // To display the game UI (the scores, the back button)
    private void DisplayGameUI()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas UI":
                    CanvasPlayerGUI.enabled = true;
                    break;
                case "MenuCanvasRight":
                    CanvasPlayerGUI.enabled = true;
                break;
            default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To display the game UI (the scores, the back button)
    private void DisplayStartUI()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas Start":
                    CanvasPlayerGUI.enabled = true;
                    break;
                case "MenuCanvasRight":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To display the end screen with the winner(s) name
    private void DisplayEndScreen()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas EndGame":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To display the game over screen because the player got catched
    private void DisplayGameOver1Screen()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas GameOver1":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To display the game over screen because the player got no more time
    private void DisplayGameOver2Screen()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas GameOver2":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To display the end screen with the winner(s) name
    private void DisplayPauseScreen()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas Pause":
                    CanvasPlayerGUI.enabled = true;
                    break;
                case "Canvas UI":
                    CanvasPlayerGUI.enabled = true;
                    break;
                case "MenuCanvasRight":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default:
                    CanvasPlayerGUI.enabled = false;
                    break;
            }
        }
    }

    // To either turn off or on the audio from the in game menu 
    public void SwitchAudio()
    {
        gameAudio.SwitchAudioMode();
    }

    // To get if the game is running of not
    public bool GetGameRunningStatus()
    {
        return isGameOn;
    }

    public void addScoreToPlayer(GameObject player, int points)
    {
        int playerNumber = player.GetComponent<PlayerBehavior>().GetPlayerNumber();
        scoreList[playerNumber] += points;
    }

    public void StageClearedAction()
    {
        GameParameter.SetStageClearingTime(timer.GetElapsedTime());
        timer.resetTimer();
        EndGame();
        DisplayEndScreen();
    }

    public void GuardCaughtPlayer()
    {
        GameParameter.SetStageClearingTime(-1);
        timer.resetTimer();
        EndGame();
        DisplayGameOver1Screen();
    }

    public void subScoreToPlayer(GameObject player, int points)
    {
        int playerNumber = player.GetComponent<PlayerBehavior>().GetPlayerNumber();
        scoreList[playerNumber] -= points;
    }

    public int getScorePlayer(Players p)
    {
        return scoreList[(int)p];
    }

    public List<Players> getWinner()
    {
        List<Players> list = new List<Players>();
        if (scoreList[0] < scoreList[1])
        {
            list.Add(Players.player2);
        }
        else if (scoreList[0] > scoreList[1])
        {
            list.Add(Players.player1);
        }
        else
        {
            list.Add(Players.player1);
            list.Add(Players.player2);
        }

        return list;
    }

}
