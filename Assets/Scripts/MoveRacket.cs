using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 30f;
    public string verticalAxis = "Vertical";
    public GameObject gatesHost;
    private Gates gates;

    private void Start()
    {

        gates = gatesHost.GetComponent<Gates>();
    }
    public int GetRacketModifier()
    {
        return gates.racketModifier;
    }

    public void Disable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Enable()
    {
        GetComponent<Collider>().enabled = true;
    }

    void FixedUpdate()
    {
        float velocityY = Input.GetAxisRaw(verticalAxis);
        Vector2 newVelocity = new Vector2(0, Mathf.Clamp(velocityY, -1f, 1f)) * speed; 
        GetComponent<Rigidbody2D>().velocity = newVelocity;
    }
}
