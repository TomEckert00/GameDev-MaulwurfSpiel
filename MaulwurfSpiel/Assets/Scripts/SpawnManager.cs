using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject SpawnObject;

    public GameManager gameManager;

    public bool gameIsActive;

    public void StartSpawningLoop()
    {
        StartCoroutine(StartGameLoop());
    }

    IEnumerator StartGameLoop()
    {
        while (gameIsActive)
        {
            gameIsActive = gameManager.GetGameStatus();
            yield return new WaitForSeconds(1);
        }
    }
}
