using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public TMP_Text scoreText;

    void Update()
    {
        if (GameManager.GM.gameState == GameState.Playing)
        {
            scoreText.text = "Score : " + Mathf.FloorToInt(GameManager.GM.CalculateScore());
        }
        else if (GameManager.GM.gameState == GameState.Dead)
        {
            scoreText.text = "High Score : " + GameManager.GM.GetHighScore();
        }
    }
}
