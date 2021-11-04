using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score_text;
    private int _score;

    private void Awake()
    {
        _score = 0;
        ChangeScore();
    }

    public void UpdateScoreText(int i)
    {
        _score += i;
        ChangeScore();
    }

    private void ChangeScore()
    {
        score_text.text = _score.ToString();
    }
}
