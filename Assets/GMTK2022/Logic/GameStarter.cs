using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartAfterLoad());
    }

    private IEnumerator StartAfterLoad()
    {
        yield return null;
        DiceGameManager.Instance.GameStarted();
    }
}
