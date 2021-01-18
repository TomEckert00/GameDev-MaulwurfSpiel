using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;

    private GameManager gameManager;

    public GameObject spawnedObject;

    public GameObject SpawnedObject
    {
        get {return spawnedObject; }
        set { spawnedObject = value; }
    }

    public bool containsSpawnObject;

    public bool ContainsSpawnObject
    {
        get { return containsSpawnObject; }
        set { containsSpawnObject = value; }
    }

    public void SetGameControllerReference(GameManager manager)
    {
        gameManager = manager;
    }

    public void DestroyItem()
    {
        if (spawnedObject != null)
        {
            DestroySpawnObjectAndUpdateEverything(SpawnObjectReference().PointsIfDestroyed);
        }
        else
        {
            gameManager.UpdateScore(-1);
            gameManager.UpdateLives(-1);
        }
    }

    private SpawnObject SpawnObjectReference()
    {
        return spawnedObject.GetComponent<SpawnObject>();
    }

    public void ClearGridSpaceImmediatly()
    {
        DestroySpawnObjectAndUpdateEverything(0);
    }

    public IEnumerator InitiateSelfDestruction() 
    {
        yield return new WaitForSeconds(2);
        if (spawnedObject != null)
        {
            Debug.Log("initiate self destruction");
            DestroySpawnObjectAndUpdateEverything(SpawnObjectReference().PointsIfMissed);
            gameManager.UpdateLives(-1);
        }
        else
        {
            Debug.Log("already gone!");
        }
    }

    private void DestroySpawnObjectAndUpdateEverything(int value)
    {
        Destroy(spawnedObject);
        gameManager.UpdateScore(value);
        gameManager.CountItemCountDownOnBoard();
        containsSpawnObject = false;
        spawnedObject = null;
    }
}
