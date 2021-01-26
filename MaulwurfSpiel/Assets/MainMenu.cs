using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private int highScore;
    public int HighScore
    {
        set
        {
            highScore = value;
            UpdateHighscoreText();
        }
    }

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScoreStorage");
        UpdateHighscoreText();
    }

    private void UpdateHighscoreText()
    {
        highScoreText.text = "Highscore: " + highScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
