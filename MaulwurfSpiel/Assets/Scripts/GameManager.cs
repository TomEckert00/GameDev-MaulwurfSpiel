using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Text[] lifeSlots = new Text[3];
    public Text[] buttonList;
    public GameObject restartButton;
    public GameObject startInfo;
    public GameObject gameOverPanel;
    public Text gameOverText;

    public SpawnManager spawnManager;

    private int score;
    private int currentLives;
    private bool isGameActive = false;

    public bool IsGameActive
    {
        get { return isGameActive; }
        set { isGameActive = value; }
    }

    void Awake()
    {
        InitializeVariablesWithStartValues();

        PreparePanelsAndTexts();
        
        SetGameControllerReferenceOnButtons();
    }

    private void InitializeVariablesWithStartValues() {
        score = 0;
        currentLives = 3;
    }

    private void PreparePanelsAndTexts()
    {
        foreach (Text slot in lifeSlots){
            slot.text = "";
        }
        scoreText.SetText("Score: " + score);
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
        score+= value;
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

    public void StartGame()
    {
        isGameActive = true;
        spawnManager.StartSpawningLoop();
        SetBoardInteractable(true);
        startInfo.SetActive(false);
    }

    void GameOver()
    {
        StartCoroutine(ClearAllGridSpaces());
        isGameActive = false;
        SetBoardInteractable(false);
        SetGameOverText("Highscore:" + score);
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

    public void RestartGame()
    {
        InitializeVariablesWithStartValues();
        PreparePanelsAndTexts();
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
