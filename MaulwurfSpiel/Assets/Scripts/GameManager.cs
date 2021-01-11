using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text[] buttonList;
    public GameObject restartButton;
    public GameObject startInfo;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public SpawnManager spawnManager;

    private int score;
    private bool isGameActive = false;

    public bool IsGameActive
    {
        get { return isGameActive; }
        set { isGameActive = value; }
    }

    void Awake()
    {
        InitializeVariablesWithDefaultValues();

        PreparePanelsAndTexts();
        
        SetGameControllerReferenceOnButtons();
    }

    private void InitializeVariablesWithDefaultValues() {
        score = 0;
    }

    private void PreparePanelsAndTexts()
    {
        scoreText.SetText("Score: " + score);
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

    public void CountScoreOneUp()
    {
        score++;
        scoreText.SetText("Score: " + score);
    }

    public void CountItemCountDownOnBoard()
    {
        spawnManager.SpawnObjectsInGame -= 1;
    }

    public bool IsGridOfIndexFilled(int index)
    {
        return GetGridSpaceOfButtonWithIndex(index).ContainsSpawnObject;
    }
    private GridSpace GetGridSpaceOfButtonWithIndex(int index)
    {
        return buttonList[index].GetComponentInParent<GridSpace>();
    }

    public void SetGridSpaceContainsSpawnObjectOfIndex(int index, bool boolean)
    {
        GetGridSpaceOfButtonWithIndex(index).ContainsSpawnObject = boolean;
    }

    public void SetSpawnedObjectReferenceInGridSpace(int index, GameObject spawnObjectReference)
    {
        GetGridSpaceOfButtonWithIndex(index).SpawnedObject = spawnObjectReference;
    }

    public Vector3 GetButtonPositionWithIndex(int index)
    {
        return buttonList[index].GetComponentInParent<Transform>().position;
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
}
