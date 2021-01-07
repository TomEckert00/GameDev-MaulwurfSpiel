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
    public SpawnManager spawnManager;
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

    public bool IsGridOfIndexFilled(int index)
    {
        return buttonList[index].GetComponentInParent<GridSpace>().GetContainsSpawnObject();
    }

    public void CountUp()
    {
        score++;
        scoreText.SetText("Score: " + score);
    }

    public Vector3 GetButtonPositionWithIndex(int index)
    {
        return buttonList[index].GetComponentInParent<Transform>().position;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }

    public void SetGridSpaceContainsSpawnObjectOfIndex(int index, bool boolean)
    {
        buttonList[index].GetComponentInParent<GridSpace>().SetContainsSpawnObject(boolean);
    }


    public int GetRandomIndexOfGrid()
    {
        return Random.Range(0, buttonList.Length);
    }

    public void StartGame()
    {
        isGameActive = true;
        spawnManager.StartSpawningLoop();
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
