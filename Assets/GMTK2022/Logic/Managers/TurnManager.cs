using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    public delegate void NextTurnDelegate();
    public event NextTurnDelegate NextTurn;

    public delegate void EndGameDelegate();
    public event NextTurnDelegate EndGame;

    public void StartNextTurn()
    {
        if(NextTurn != null)
            NextTurn();
    }

    public void GameEnded()
    {
        if(EndGame != null)
            EndGame();
    }
}
