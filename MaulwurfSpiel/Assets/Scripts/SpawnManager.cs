using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnObject;

    public GameManager gameManager;

    public bool gameIsActive;

    private int spawnObjectsInGame;

    public void StartSpawningLoop()
    {
        spawnObjectsInGame = 0;
        GetGameStatusFromGameManager();
        StartCoroutine(StartGameLoop());
    }

    IEnumerator StartGameLoop()
    {
        
        while (gameIsActive)
        {
            // check if game is still active
            GetGameStatusFromGameManager();
           
            //check if all spaces are filled
            if(spawnObjectsInGame <9)
            {
                int randomIndex = gameManager.GetRandomIndexOfGrid();
                while (gameManager.IsGridOfIndexFilled(randomIndex))
                {
                    randomIndex = gameManager.GetRandomIndexOfGrid();
                }
                SpawnTheObjectWithIndex(randomIndex);
            }
            else
            {
                Debug.Log("board full, nothing spawned");
            }
            yield return new WaitForSeconds(1);
            Debug.Log("Waited one Second");
        }
    }

    private void SpawnTheObjectWithIndex(int randomIndex)
    {
        Vector3 positionOfSpawnObject = gameManager.GetButtonPositionWithIndex(randomIndex);
        Instantiate(spawnObject, positionOfSpawnObject, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        gameManager.SetGridSpaceContainsSpawnObjectOfIndex(randomIndex, true);
        spawnObjectsInGame++;
    }

    private void GetGameStatusFromGameManager()
    {
        gameIsActive = gameManager.IsGameActive;
    }
}
