using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private float fadeOutValue = 0.01f;

    public void SetScore(int score)
    {
        text.text += score.ToString();
    }

    void Update()
    {
        Color color = text.color;
        if(color.a <= 0)
            Destroy(gameObject);
        color.a -= fadeOutValue;
        text.color = color;
    }
}
