using UnityEngine;

public class Goal : MonoBehaviour
{
    public string ballTag = "Ball";
    public GameController gameController;
    public int playerID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ballTag))
        {
            if (playerID != 0)
            {
                gameController.AddScore(playerID);
            }
        }
    }
}