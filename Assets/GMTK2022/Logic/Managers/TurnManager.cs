using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    public delegate void NextTurnDelegate();
    public event NextTurnDelegate NextTurn;

    public void StartNextTurn()
    {
        NextTurn();
    }
}
