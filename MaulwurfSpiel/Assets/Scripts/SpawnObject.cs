using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int pointsIfMissed;
    public int pointsIfDestroyed;

    public int PointsIfMissed
    {
        get{ return pointsIfMissed; }
        set { pointsIfMissed = value; }
    }

    public int PointsIfDestroyed
    {
        get { return pointsIfDestroyed; }
        set { pointsIfDestroyed = value; }
    }
}
