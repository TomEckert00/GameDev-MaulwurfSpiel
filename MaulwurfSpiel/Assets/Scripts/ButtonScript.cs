using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int number;

    public void Start()
    {
        gameObject.GetComponentInChildren<Text>().text = number.ToString();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
