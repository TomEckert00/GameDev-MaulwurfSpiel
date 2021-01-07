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
           
            if(spawnObjectsInGame <9)
            {
                int index = gameManager.GetRandomIndexOfGrid();
                while (gameManager.IsGridOfIndexFilled(index))
                {
                    index = gameManager.GetRandomIndexOfGrid();
                }
                Vector3 positionOfSpawnObject = gameManager.GetButtonPositionWithIndex(index);
                Instantiate(spawnObject, positionOfSpawnObject, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                gameManager.SetGridSpaceContainsSpawnObjectOfIndex(index, true);
                spawnObjectsInGame++;
            }
            else
            {
                gameIsActive = false;
                Debug.Log("Stopped the game due to 9 objects");
            }
            yield return new WaitForSeconds(1);
            Debug.Log("Wait");
        }
    }

    private void GetGameStatusFromGameManager()
    {
        gameIsActive = gameManager.GetGameStatus();
    }
}
