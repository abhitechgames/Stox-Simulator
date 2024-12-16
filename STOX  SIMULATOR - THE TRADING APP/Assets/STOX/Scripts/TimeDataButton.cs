using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDataButton : MonoBehaviour
{
    public Button[] timeBut;

    private Button selectedButton;

    public Sprite fullSprite;
    public Sprite hollowSprite;

    public Color black;
    public Color green;

    ChartData chartData;

    private void Start() {
        chartData = FindObjectOfType<ChartData>();
    }

    public void ChangeTime(Button b)
    {
        selectedButton = b;
        changeUI();
    }

    void changeUI()
    {
        for (int i = 0; i < timeBut.Length; i++)
        {
            if(timeBut[i] == selectedButton)
            {
                timeBut[i].GetComponent<Image>().sprite = fullSprite;
                timeBut[i].GetComponentInChildren<TMP_Text>().color = black;
                
                if(i == 0)
                    chartData.lastWeekChart();
                else if(i == 1)
                    chartData.lastMonthChart();
                else
                    chartData.lastYearChart();
                // make changes in chart
            }
            else
            {
                timeBut[i].GetComponent<Image>().sprite = hollowSprite;
                timeBut[i].GetComponentInChildren<TMP_Text>().color = green;
            }
        }
    }
}
