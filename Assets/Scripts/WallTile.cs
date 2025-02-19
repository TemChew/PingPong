using TMPro;
using UnityEngine;

public class WallTile : MonoBehaviour
{
    public TextMeshPro wallSign;
    public int wallModifier = 1;
    public int fontSize = 40;
    private int minRandom;
    private int maxRandom;


    private void OnEnable()
    {
        Ball.onWallTouch_ += SetRandomText;

    }
    void Start()
    {
        CreateTextMeshPro();
        GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        minRandom = gc.minRandom;
        maxRandom = gc.maxRandom;
        SetRandomText();
    }

    private void CreateTextMeshPro()
    {
        GameObject textObject = new GameObject("WallTileText");
        textObject.transform.SetParent(transform);
        wallSign = textObject.AddComponent<TextMeshPro>();
       
        wallSign.fontSize = fontSize;
        wallSign.alignment = TextAlignmentOptions.Center;
        wallSign.color = Color.black; 

        textObject.transform.localPosition = new Vector3(0, 0, 0); 
    }

    public void SetText(int newText)
    {
        wallModifier = newText;
        if (wallSign != null)
        {
            wallSign.text = wallModifier.ToString("+0;-0;0");
        }
    }
    public void SetRandomText()
    {
        wallModifier = Random.Range(minRandom, maxRandom + 1);
        SetText(wallModifier);
    }

}

