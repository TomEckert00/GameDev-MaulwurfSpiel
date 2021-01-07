﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;

    private GameManager gameManager;

    private bool containsSpawnObject;

    public bool GetContainsSpawnObject()
    {
        return containsSpawnObject;
    }

    public void SetContainsSpawnObject(bool boolean)
    {
        containsSpawnObject = boolean;
    }

    public void SetGameControllerReference(GameManager manager)
    {
        gameManager = manager;
    }

    public void CountUp()
    {
        gameManager.CountUp();
    }
}
