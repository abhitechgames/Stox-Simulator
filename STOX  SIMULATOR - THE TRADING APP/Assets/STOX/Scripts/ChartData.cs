using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ChartAndGraph;

public class ChartData : MonoBehaviour
{
    public GraphChart graphChart;
    public Stock selectedStockForInvesting;

    public TMP_Text text;
    public Text normalText;

    public Material red_materials_fill;
    public Material green_materials_fill;

    private MaterialTiling mats;

    public TMP_Text MaxPrice;
    public TMP_Text MinPrice;

    // Start is called before the first frame update
    void Start()
    {
        lastWeekChart();
    }

    public void lastWeekChart()
    {
        graphChart.DataSource.ClearCategory("Stock");
        graphChart.DataSource.ClearCategory("Average Stock Price");

        graphChart.DataSource.StartBatch();

        float sum = 0f;

        float max = selectedStockForInvesting.lastWeek[0];
        float min = selectedStockForInvesting.lastWeek[0];

        for (int i = 0; i < selectedStockForInvesting.lastWeek.Length; i++)
        {
            sum += selectedStockForInvesting.lastWeek[i];

            if (selectedStockForInvesting.lastWeek[i] > max)
                max = selectedStockForInvesting.lastWeek[i];
            if (selectedStockForInvesting.lastWeek[i] < min)
                min = selectedStockForInvesting.lastWeek[i];
        }

        float Avg = sum / selectedStockForInvesting.lastWeek.Length;

        if(Avg > selectedStockForInvesting.lastWeek[6])
            graphChart.DataSource.SetCategoryFill("Stock", red_materials_fill, false);
        else
            graphChart.DataSource.SetCategoryFill("Stock", green_materials_fill, false);

        graphChart.DataSource.AddPointToCategory("Average Stock Price", 0, Avg);
        graphChart.DataSource.AddPointToCategory("Average Stock Price", 6, Avg);

        MaxPrice.text = max.ToString();
        MinPrice.text = min.ToString();

        for (int i = 0; i < selectedStockForInvesting.lastWeek.Length; i++)
        {
            graphChart.DataSource.AddPointToCategory("Stock", i, selectedStockForInvesting.lastWeek[i]);
        }
        graphChart.DataSource.EndBatch();
    }

    public void lastMonthChart()
    {
        graphChart.DataSource.ClearCategory("Stock");
        graphChart.DataSource.ClearCategory("Average Stock Price");

        graphChart.DataSource.StartBatch();
        
        float sum = 0f;
        float max = selectedStockForInvesting.lastMonth[0];
        float min = selectedStockForInvesting.lastMonth[0];

        for (int i = 0; i < selectedStockForInvesting.lastMonth.Length; i++)
        {
            sum += selectedStockForInvesting.lastMonth[i];

            if (selectedStockForInvesting.lastMonth[i] > max)
                max = selectedStockForInvesting.lastMonth[i];
            if (selectedStockForInvesting.lastMonth[i] < min)
                min = selectedStockForInvesting.lastMonth[i];
        }

        float Avg = sum / selectedStockForInvesting.lastMonth.Length;

        if(sum / selectedStockForInvesting.lastMonth.Length > selectedStockForInvesting.lastMonth[29])
            graphChart.DataSource.SetCategoryFill("Stock", red_materials_fill, false);
        else
            graphChart.DataSource.SetCategoryFill("Stock", green_materials_fill, false);
            
        graphChart.DataSource.AddPointToCategory("Average Stock Price", 0, Avg);
        graphChart.DataSource.AddPointToCategory("Average Stock Price", 29, Avg);

        MaxPrice.text = max.ToString();
        MinPrice.text = min.ToString();

        for (int i = 0; i < selectedStockForInvesting.lastMonth.Length; i++)
        {
            graphChart.DataSource.AddPointToCategory("Stock", i, selectedStockForInvesting.lastMonth[i]);
        }
        graphChart.DataSource.EndBatch();
    }

    public void lastYearChart()
    {
        graphChart.DataSource.ClearCategory("Stock");
        graphChart.DataSource.ClearCategory("Average Stock Price");

        graphChart.DataSource.StartBatch();

        float sum = 0f;
        float max = selectedStockForInvesting.lastYear[0];
        float min = selectedStockForInvesting.lastYear[0];
        
        for (int i = 0; i < selectedStockForInvesting.lastYear.Length; i++)
        {
            sum += selectedStockForInvesting.lastYear[i];

            if (selectedStockForInvesting.lastYear[i] > max)
                max = selectedStockForInvesting.lastYear[i];
            if (selectedStockForInvesting.lastYear[i] < min)
                min = selectedStockForInvesting.lastYear[i];
        }

        float Avg = sum / selectedStockForInvesting.lastYear.Length;

        if(sum / selectedStockForInvesting.lastYear.Length > selectedStockForInvesting.lastYear[11])
            graphChart.DataSource.SetCategoryFill("Stock", red_materials_fill, false);
        else
            graphChart.DataSource.SetCategoryFill("Stock", green_materials_fill, false);

        graphChart.DataSource.AddPointToCategory("Average Stock Price", 0, Avg);
        graphChart.DataSource.AddPointToCategory("Average Stock Price", 11, Avg);

        MaxPrice.text = max.ToString();
        MinPrice.text = min.ToString();

        for (int i = 0; i < selectedStockForInvesting.lastYear.Length; i++)
        {
            graphChart.DataSource.AddPointToCategory("Stock", i, selectedStockForInvesting.lastYear[i]);
        }
        graphChart.DataSource.EndBatch();
    }

    // IEnumerator disableTextController()
    // {
    //     yield return new WaitForSeconds(.5f);
    //     GameObject.Find("textController").SetActive(false);
    // }
    
    // Update is called once per frame
    void LateUpdate()
    {
        text.text = normalText.text;
    }
}
