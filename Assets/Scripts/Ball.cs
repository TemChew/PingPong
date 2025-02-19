using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;
public class Ball : MonoBehaviour
{
    public float speed = 120f; 
    private Vector3 _initialPosition;
    public static Action<WallTile> onWallTouch;
    public static Action onWallTouch_;
    int ballModifier = 0;
    private TextMeshPro ballSign;
    public int fontSize = 40;

    private void OnEnable()
    {
        onWallTouch += SetModifier;
    }
    void Start()
    {
        _initialPosition = transform.position;
        ResetPosition();
        CreateTextMeshPro();
        SetText(ballModifier);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.name == "LeftRacket")
        {
            Debug.Log("ball/racket " + ballModifier + "/" + other.GetComponent<MoveRacket>().GetRacketModifier());
            if (ballModifier != other.GetComponent<MoveRacket>().GetRacketModifier())
            {
                Debug.Log("goal");
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddScore(1);
            }
            else
            {
                float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
                Vector2 direction = new Vector2(1, Mathf.Clamp(y, -0.75f, 0.75f)).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }
        else if (other.name == "RightRacket")
        {
            Debug.Log("ball/racket " + ballModifier + "/" + other.GetComponent<MoveRacket>().GetRacketModifier());
            if (ballModifier != other.GetComponent<MoveRacket>().GetRacketModifier())
            {
                Debug.Log("goal");
                GameObject.FindWithTag("GameController").GetComponent<GameController>().AddScore(2);
            }
            else
            {
                float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
                Vector2 direction = new Vector2(-1, Mathf.Clamp(y, -0.75f, 0.75f)).normalized;
                GetComponent<Rigidbody2D>().velocity = direction * speed;
            }

        }
        else if (other.tag == "Wall")
        {
            WallTile wall = collision.gameObject.GetComponent<WallTile>();
            onWallTouch.Invoke(wall);
            onWallTouch_.Invoke();
        }
    }

    private float hitFactor(Vector3 ballPosition, Vector3 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }

    public void ResetPositionWithDelay(float delay)
    {
        StartCoroutine(ResetAfterDelay(delay));
    }

    private System.Collections.IEnumerator ResetAfterDelay(float delay)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
        yield return new WaitForSeconds(delay);
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = _initialPosition;
        float randomX = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float randomY = UnityEngine.Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        GetComponent<Rigidbody2D>().velocity = randomDirection * speed;
    }
    private void CreateTextMeshPro()
    {
        GameObject textObject = new GameObject("WallTileText");
        textObject.transform.SetParent(transform);
        ballSign = textObject.AddComponent<TextMeshPro>();

        ballSign.fontSize = fontSize;
        ballSign.alignment = TextAlignmentOptions.Center;
        ballSign.color = Color.black;

        textObject.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void SetText(int newText)
    {
        ballModifier = newText;
        if (ballSign != null)
        {
            ballSign.text = ballModifier.ToString();
        }
    }
    public void SetModifier(WallTile wall)
    {
        int newModifier = ballModifier + wall.wallModifier;
        SetText(newModifier);
    }
}