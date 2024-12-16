using UnityEngine;
using TMPro;
using ChartAndGraph;

public class CryptoPannel : MonoBehaviour
{
    public CryptoCurrency cryptoCurrency;

    public TMP_Text cryptoCurrencyName;
    public TMP_Text High;
    public TMP_Text Low;
    public TMP_Text CurrencyCode;

    public GraphChart graphChart;

    public Material red_materials_fill;
    public Material green_materials_fill;

    // Start is called before the first frame update
    public void AssignUI()
    {
        cryptoCurrencyName.text = cryptoCurrency.name;
        High.text = cryptoCurrency.High;
        Low.text = cryptoCurrency.Low;
        CurrencyCode.text = cryptoCurrency.FromCurrencyCode;

        graphChart.DataSource.ClearCategory("Amount");

        graphChart.DataSource.StartBatch();

        if(cryptoCurrency.last5Day[4] < cryptoCurrency.last5Day[3] )
            graphChart.DataSource.SetCategoryFill("Amount", red_materials_fill, false);
        else
            graphChart.DataSource.SetCategoryFill("Amount", green_materials_fill, false);

        for (int j = 0; j < 5; j++)
        {
            graphChart.DataSource.AddPointToCategory("Amount", j, cryptoCurrency.last5Day[j]);
        }
        graphChart.DataSource.EndBatch();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
