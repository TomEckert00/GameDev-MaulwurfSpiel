using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnObject;

    public GameManager gameManager;

    public bool gameIsActive;

    public void StartSpawningLoop()
    {
        gameIsActive = gameManager.GetGameStatus();
        StartCoroutine(StartGameLoop());
    }

    IEnumerator StartGameLoop()
    {
        
        while (gameIsActive)
        {
            gameIsActive = gameManager.GetGameStatus();
            Instantiate(spawnObject,gameManager.GetButtonTransformWithRandomIndex().position,Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
            yield return new WaitForSeconds(1);
        }
    }
}
