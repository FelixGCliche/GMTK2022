using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private Dice diceToSpawn;

    private List<Dice> dices = new List<Dice>();

    private void OnEnable()
    {
        DiceGameManager.Instance.RestartGame  += ClearDice;
        DiceGameManager.Instance.NextTurn += SpawnDice;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.RestartGame -= ClearDice;
        DiceGameManager.Instance.NextTurn -= SpawnDice;
    }

    private void SpawnDice()
    {
        dices.Add(Instantiate(diceToSpawn, spawner.transform.position, spawner.transform.rotation));
    }

    private void ClearDice()
    {
        foreach(Dice dice in dices)
        {
            Destroy(dice.gameObject);
        }
        dices.Clear();
    }
}
