using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gates : MonoBehaviour
{

    public TextMeshPro wallSign;
    public int racketModifier = 0;
    public KeyCode increaseKey = KeyCode.D;
    public KeyCode decreaseKey = KeyCode.A;
    public int fontSize = 50;

    void Update()
    {

        if (Input.GetKeyDown(increaseKey))
        {
            IncreaseModifier(true);
        }
        // ѕровер€ем нажатие клавиш дл€ уменьшени€
        else if (Input.GetKeyDown(decreaseKey))
        {
            IncreaseModifier(false);
        }
    }


    void Start()
    {
        CreateTextMeshPro();

        SetText(racketModifier);
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
        racketModifier = newText;
        if (wallSign != null)
        {
            wallSign.text = racketModifier.ToString();
        }
    }
    private void IncreaseModifier(bool up) {
        racketModifier += up ? 1 : -1;
        SetText(racketModifier);
    }
}
