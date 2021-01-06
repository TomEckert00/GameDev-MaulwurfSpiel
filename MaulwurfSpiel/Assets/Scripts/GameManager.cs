using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text[] buttonList;
    public GameObject spawnManager;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public GameObject startInfo;

    private int score;
    public bool isGameActive = false;

    void Awake()
    {
        score = 0;
        scoreText.SetText("Score: " + score);
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    public void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void CountUp()
    {
        score++;
        scoreText.SetText("Score: " + score);
    }

    public void StartGame()
    {
        isGameActive = true;
        SetBoardInteractable(true);
        startInfo.SetActive(false);
    }

    void GameOver()
    {
        isGameActive = false;
        SetBoardInteractable(false);
        SetGameOverText("Highscore:" + score);
    }


    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        score = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public bool GetGameStatus()
    {
        return isGameActive;
    }

    public void SetGameStatus(bool status)
    {
        isGameActive = status;
    }

}
