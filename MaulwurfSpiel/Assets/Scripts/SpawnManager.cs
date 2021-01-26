using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnDelay = 1.0f;
    public GameObject[] spawnObjects;
    private List<GameObject> spawnPoolList = new List<GameObject>();
    private GameObject[] spawnPool;

    public GameManager gameManager;

    public bool gameIsActive;

    public int SpawnObjectsInGame { get; set; }

    public void StartSpawningLoop()
    {
        SpawnObjectsInGame = 0;
        GetGameStatusFromGameManager();
        StartCoroutine(StartGameLoop());
    }

    void Start()
    {
        Debug.Log("Awake");
        StartCoroutine(FillSpawnPool());
    }

    private IEnumerator FillSpawnPool()
    {
        yield return new WaitForEndOfFrame();
        foreach (GameObject spawnObject in spawnObjects)
        {
            Debug.Log("SpawnObject: " + spawnObject.name);
            int spawnRate = spawnObject.GetComponent<SpawnObject>().SpawnRatePercent;
            Debug.Log("SpawnRate: " + spawnRate);
            for (int j = 0; j < spawnRate; j++)
            {
                spawnPoolList.Add(spawnObject);
            }
        }
        Debug.Log("Liste: " + spawnPoolList.Count);
        spawnPool = spawnPoolList.ToArray();
        Debug.Log("Länge" + spawnPool.Length);
    }

    IEnumerator StartGameLoop()
    {
        while (gameIsActive)
        {
            // check if game is still active
            GetGameStatusFromGameManager();
           
            //check if all spaces are filled
            if(SpawnObjectsInGame <9 && gameIsActive)
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
            UpdateSpawnDelay();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void UpdateSpawnDelay()
    {
        spawnDelay = 1.0f;
        int multi = gameManager.CurrentMultiplier;
        if (multi > 1)
        {
            for(int i = 0; i < multi; i++)
            {
                spawnDelay -= 0.05f;
            }
        }
    }
    private void SpawnTheObjectWithIndex(int randomIndex)
    {
        Vector3 positionOfSpawnObject = gameManager.GetButtonPositionWithIndex(randomIndex);
        GameObject so = Instantiate(GetRandomSpawnObject(), positionOfSpawnObject, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        gameManager.SetGridSpaceContainsSpawnObjectOfIndex(randomIndex, true);
        gameManager.SetSpawnedObjectReferenceInGridSpace(randomIndex, so);
        gameManager.InitiateSelfDestruction(randomIndex);
        SpawnObjectsInGame++;
    }

    private GameObject GetRandomSpawnObject()
    {
        int randomIndex = UnityEngine.Random.Range(0,spawnPool.Length);
        return spawnPool[randomIndex];
    }

    private void GetGameStatusFromGameManager()
    {
        gameIsActive = gameManager.IsGameActive;
    }
}
