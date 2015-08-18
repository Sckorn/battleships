using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game {
    private int gameHash;
    private int startTime;
    private bool gamePaused = false;
    private int CurrentTurn = 1;
    public HumanPlayer Human;
    public AIPlayer AI;
    public CustomError LastError;

    private bool gameOver = false;

    public bool GameIsOver
    {
        get { return this.gameOver; }
        set { this.gameOver = value; }
    }

    public Game()
    {
        this.AI = new AIPlayer();
        this.Human = new HumanPlayer();
        EventManager.OnGameOver += this.GameOver;
        EventManager.OnGamePause += this.Pause;
        EventManager.OnGameResume += this.Resume;
    }

    public int WhosTurn
    {
        get { return this.CurrentTurn; }
        set { this.CurrentTurn = value; }
    }

    public bool isGamePaused
    {
        get { return this.gamePaused; }
        set { this.gamePaused = value; }
    }

    private void GameOver()
    {
        this.gameOver = true;
        GameObject.Find("jobster").GetComponent<Jobster>().DestroyCrosshair();
        BaseEventData bed = new BaseEventData(EventSystem.current);
        //EventSystem.current.SetSelectedGameObject(GameObject.Find("CanvasEndGame"), bed);
        GameObject.Find("CanvasEndGame").GetComponent<Canvas>().enabled = true;
        if (this.WhosTurn == 1)
        {
            GameObject.Find("gameWinner").GetComponent<Text>().text = "You Win!";
        }
        else
        {
            GameObject.Find("gameWinner").GetComponent<Text>().text = "You Lose!";
        }
    }

    private void Pause()
    {
        this.gamePaused = true;
        GameObject.Find("CanvasPauseMenu").GetComponent<Canvas>().enabled = true;
    }

    private void Resume()
    {
        this.gamePaused = true;
        GameObject.Find("CanvasPauseMenu").GetComponent<Canvas>().enabled = false;
    }
}
