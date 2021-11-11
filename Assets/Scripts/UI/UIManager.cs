using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Text score_text;
  

    private void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        score_text.text = "Enemies: " + GameStats.enemy_characters_count.ToString();
    }

}
