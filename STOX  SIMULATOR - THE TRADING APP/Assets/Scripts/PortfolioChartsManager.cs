using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using TMPro;

public class PortfolioChartsManager : MonoBehaviour
{
    public GraphChart AmountChart;
    public GraphChart ProfitChart;

    public PlayerAccount playerAccount;

    public TMP_Text AmountText;
    public TMP_Text ProfitText;

    // Start is called before the first frame update
    void Start()
    {
        chartUpdate();
    }

    public void chartUpdate()
    {
        AmountChart.DataSource.ClearCategory("Amount");
        ProfitChart.DataSource.ClearCategory("Profit");
        
        // Amount
        AmountChart.DataSource.StartBatch();
        int notOfAmountPoints = playerAccount.amountTransaction.Count;
        for (int i = 0; i < notOfAmountPoints; i++)
        {
            AmountChart.DataSource.AddPointToCategory("Amount", i, playerAccount.amountTransaction[i]);
        }
        AmountChart.DataSource.EndBatch();

        // Profit
        ProfitChart.DataSource.StartBatch();
        int notOfProfitPoints = playerAccount.profitList.Count;
        for (int i = 0; i < notOfProfitPoints; i++)
        {
            ProfitChart.DataSource.AddPointToCategory("Profit", i, playerAccount.profitList[i]);
        }

        ProfitChart.DataSource.EndBatch();

        AmountText.text = "Amount : " + playerAccount.AmountInHand;
        ProfitText.text = "Profit : " + playerAccount.totalProfit;
    }
}
