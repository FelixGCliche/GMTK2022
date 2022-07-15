using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    [SerializeField] private Dice diceToSpawn;

    private void OnEnable()
    {
        TurnManager.Instance.NextTurn += SpawnDice;
    }

    private void OnDisable()
    {
        TurnManager.Instance.NextTurn -= SpawnDice;
    }

    public void SpawnDice()
    {
        Instantiate(diceToSpawn, spawner.transform.position, spawner.transform.rotation);
    }
}
