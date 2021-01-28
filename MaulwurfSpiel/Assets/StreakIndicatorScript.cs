using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakIndicatorScript : MonoBehaviour
{
    public GameObject indicator;
    private int indicatorWidth;

    public void UpdateStreakPositive(bool update)
    {
        if (update && indicator.GetComponent<RectTransform>().sizeDelta.x<270)
        {
            indicatorWidth += 30;
            UpdateIndicatorLengthInUI();
        }
        else
        {
            indicatorWidth = 0;
            UpdateIndicatorLengthInUI();
        }
    }

    private void UpdateIndicatorLengthInUI()
    {
        indicator.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, indicatorWidth);
    }

}
