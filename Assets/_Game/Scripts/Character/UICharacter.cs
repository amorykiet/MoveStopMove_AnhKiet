using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICharacter : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_Text ScoreText;
    public Color color;
    public CameraFollow cam;

    private void Update()
    {
        if (cam == null) return;
        transform.forward = cam.transform.forward;
    }

    public void OnInit()
    {
        Color randomColor = Color.HSVToRGB(
            Random.Range(0f, 1f),
            Random.Range(.7f, 1f),
            Random.Range(.7f, 1f)
        );

        color = randomColor;
        NameText.color = color;
        ScoreText.color = color;
    }

}
