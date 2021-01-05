using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button[] gameFields;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    private int score;
    private int highscore;
    private Time time;

    private bool gameIsActive = true;

    public GameObject molePrefab;

    //public Mole molePrefab = new Mole();

    private float intervalTime = 1.0f;
    void Start()
    {
        StartCoroutine(SpawnMoles());
    }

    IEnumerator SpawnMoles()
    {
        while (gameIsActive)
        {
            SpawnMoleOnRandomPosition();
            yield return new WaitForSeconds(1);
        }
    }

    public void SpawnMoleOnRandomPosition()
    {
        int randomIndex = GenerateRandomNumber();
        Instantiate(molePrefab);
        molePrefab.gameObject.transform.position = new Vector3(0, 0, 0);
        Debug.Log(randomIndex);
    }

    public int GenerateRandomNumber()
    {
        return Random.Range(0, gameFields.Length);
    }

}
