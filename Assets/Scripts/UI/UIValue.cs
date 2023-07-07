using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIValue : MonoBehaviour
{
    public int currentValue;
    public int minValue;
    public int maxValue;

    public TMP_Text valueText;

    public void Update()
    {
        valueText.text = currentValue.ToString();
    }

    public void IncreaseValue()
    {
        currentValue++;
        if(currentValue >= maxValue){
            currentValue = minValue;
        }
    }
    public void DecreaseValue()
    {
        currentValue--;
        if(currentValue <= minValue){
            currentValue = maxValue;
        }
    }
}