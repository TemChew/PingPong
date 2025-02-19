using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public int maxScore = 10;
    public float roundDelay = 3f;
    public Ball ball;
    public int minRandom = -1;
    public int maxRandom = 1;

    public void AddScore(int playerID)
    {

        if (playerID == 1)
        {
            player1Score++;
            UpdateScoreText(player1ScoreText, player1Score);

            if (player1Score >= maxScore)
            {
                EndGame(1);
            }
            else
            {
                StartNextRound();
            }
        }
        else if (playerID == 2)
        {
            player2Score++;
            UpdateScoreText(player2ScoreText, player2Score);

            if (player2Score >= maxScore)
            {
                EndGame(2);
            }
            else
            {
                StartNextRound();
            }
        }
    }

    private void UpdateScoreText(TextMeshProUGUI scoreText, int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            Debug.LogWarning("Score text UI is not assigned!");
        }
    }

    private void EndGame(int winnerID)
    {
        Debug.Log("Player " + winnerID + " wins!");
        // Add logic for showing a winner UI panel
    }

    private void StartNextRound()
    {
        if (ball != null)
        {
            ball.ResetPositionWithDelay(roundDelay);
        }
    }

    public void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreText(player1ScoreText, player1Score);
        UpdateScoreText(player2ScoreText, player2Score);
    }
}
