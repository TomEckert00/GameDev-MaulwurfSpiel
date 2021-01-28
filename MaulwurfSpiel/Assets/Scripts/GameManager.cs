using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public Text[] lifeSlots = new Text[3];
    public Text[] buttonList;
    public GameObject restartButton;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject readyPanel;
    public Text readyText;
    public GameObject streakIndicator;
    private StreakIndicatorScript streakUI;

    public SpawnManager spawnManager;

    private int score;
    private int currentLives;
    private int currentMultiplier;
    private bool isGameActive = false;
    private int streak;

    public int CurrentMultiplier
    {
        get { return currentMultiplier; }
        set {; }
    }

    public bool IsGameActive
    {
        get { return isGameActive; }
        set { isGameActive = value; }
    }

    void Awake()
    {
        streakUI = streakIndicator.GetComponent<StreakIndicatorScript>();

        InitializeVariablesWithStartValues();

        PreparePanelsAndTexts();
        
        SetGameControllerReferenceOnButtons();

        StartCoroutine(StartGame());
    }

    private void InitializeVariablesWithStartValues() {
        score = 0;
        currentLives = 3;
        currentMultiplier = 1;
        streak = 0;
    }

    private void PreparePanelsAndTexts()
    {
        foreach (Text slot in lifeSlots){
            slot.text = "";
        }
        scoreText.SetText(score.ToString());
        multiplierText.SetText("x" + currentMultiplier);
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);

        ClearLifeSlots();
    }

    public void SetGameControllerReferenceOnButtons()
    {
        foreach ( Text button in buttonList) { 
            button.GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void UpdateScore(int value)
    {
        if(value > 0) {
            streak += 1;
            UpdateMultiplier();
            multiplierText.SetText("x" + currentMultiplier);
            score += value * currentMultiplier;
        }
        else if (value < 0)
        {
            score += value;
            if(score <= 0) { score = 0; }
            streak = 0;
            streakUI.UpdateStreakPositive(false);
            currentMultiplier = 1;
            multiplierText.SetText("x" + currentMultiplier);
        }
        scoreText.SetText(score.ToString());
    }

    private void UpdateMultiplier()
    {
        Debug.Log("CurrentMultiplier: " + currentMultiplier);
        if(streak % 10 == 0 && streak != 0)
        {
            currentMultiplier = (streak / 10)+1;
        }
        if(currentMultiplier > 8)
        {
            currentMultiplier = 8;
        }
        else
        {
            streakUI.UpdateStreakPositive(true);
        }
        Debug.Log("Updated Multiplier: " + currentMultiplier);
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

    public void InitiateSelfDestruction(int index)
    {
        StartCoroutine(GetGridSpaceOfButtonWithIndex(index).InitiateSelfDestruction());
    }

    public Vector3 GetButtonPositionWithIndex(int index)
    {
        return buttonList[index].GetComponentInParent<Transform>().position;
    }

    public int GetRandomIndexOfGrid()
    {
        return Random.Range(0, buttonList.Length);
    }

    private IEnumerator StartGame()
    {
        readyPanel.SetActive(true);
        readyText.text = "Ready?";
        yield return new WaitForSeconds(2);
        readyText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        readyPanel.SetActive(false);
        if (!isGameActive)
        {
            isGameActive = true;
            spawnManager.StartSpawningLoop();
            SetBoardInteractable(true);
        }
    }

    void GameOver()
    {
        StartCoroutine(ClearAllGridSpaces());
        isGameActive = false;
        SetBoardInteractable(false);
        SetGameOverText(score.ToString());
        UpdateHighScore();
        restartButton.SetActive(true);
    }

    public IEnumerator ClearAllGridSpaces()
    {
        yield return new WaitForSeconds(0.001f);
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<GridSpace>().ClearGridSpaceImmediatly();
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScoreStorage"))
        {
            PlayerPrefs.SetInt("HighScoreStorage", score);
        }
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void UpdateLives(int i)
    {
        currentLives += i;
        if (currentLives > 3)
        {
            currentLives = 3;
            ClearLifeSlots();
        }
        else if(currentLives == 3)
        {
            ClearLifeSlots();
        }
        else if(currentLives == 2)
        {
            lifeSlots[0].text = "X";
            lifeSlots[1].text = "";
        }
        else if(currentLives == 1)
        {
            lifeSlots[1].text = "X";
        }
        else
        {
            lifeSlots[2].text = "X";
            GameOver();
        }
    }
    private void ClearLifeSlots()
    {
        foreach (Text text in lifeSlots)
        {
            text.text = "";
        }
    }
}
