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
            gameManager.CountScoreOneUp();
            gameManager.CountItemCountDownOnBoard();
            Debug.Log("kill item "+ spawnedObject.name);
            Destroy(spawnedObject);
            ContainsSpawnObject = false;
            spawnedObject = null;
        }
    }
}
