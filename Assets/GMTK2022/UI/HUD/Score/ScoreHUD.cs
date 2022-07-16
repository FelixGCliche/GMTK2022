using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        DiceGameManager.Instance.UpdateScore += UpdateScore;
    }

    private void OnDisable()
    {
        DiceGameManager.Instance.UpdateScore -= UpdateScore;
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("Score : " + newScore); 
    }
}
