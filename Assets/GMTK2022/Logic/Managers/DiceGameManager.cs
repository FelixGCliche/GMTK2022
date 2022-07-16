using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameManager : Singleton<DiceGameManager>
{
    public delegate void StartGameDelegate();
    public event StartGameDelegate StartGame;

    public delegate void NextTurnDelegate();
    public event NextTurnDelegate NextTurn;

    public delegate void EndGameDelegate();
    public event EndGameDelegate EndGame;

    public delegate void GameLostDelegate();
    public event GameLostDelegate GameLost;

    public delegate void RestartGameDelegate();
    public event RestartGameDelegate RestartGame;

    public delegate void UpdateScoreDelegate(int newScore);
    public event UpdateScoreDelegate UpdateScore;

    private int score = 0;
    private bool gameIsActive = false;

    public void GameStarted()
    {
        if(!gameIsActive)
        {
            ResetScore();
            gameIsActive = true;
            if(StartGame != null)
                StartGame();
            StartNextTurn();
        }
    }

    public void StartNextTurn()
    {
        if(gameIsActive)
        {
            if(NextTurn != null)
                NextTurn();
        }
    }

    public void LostGame()
    {
        if(gameIsActive)
        {
            if(GameLost != null)
                GameLost();
            GameEnded();
        }
    }

    public void GameEnded()
    {
        if(gameIsActive)
        {
            gameIsActive = false;
            if(EndGame != null)
                EndGame();
        }
    }

    public void GameRestarted()
    {
        ResetScore();
        gameIsActive = false;
        if(RestartGame != null)
            RestartGame();
    }

    public void AddScore(int scoreToAdd)
    {
        if(gameIsActive)
        {
            score += scoreToAdd;
            if(UpdateScore != null)
                UpdateScore(score);
        }
    }

    private void ResetScore()
    {
        if(!gameIsActive)
        {
            score = 0;
            if(UpdateScore != null)
                UpdateScore(score);
        }
    }
}
