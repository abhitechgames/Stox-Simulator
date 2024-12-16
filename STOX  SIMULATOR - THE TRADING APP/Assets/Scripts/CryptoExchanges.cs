using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;
using ChartAndGraph;

public class CryptoExchanges : MonoBehaviour
{
    [Header("CryptoCurrency")]
    public CryptoCurrency[] cryptoCurrencies;

    string cryptoURL = "https://finnhub.io/api/v1/crypto/candle?symbol=BINANCE:";
    string apiKey = "&token=c3akj22ad3idt41dmlm0";


    [Header("UI")]
    public TMP_Text[] CurrencyName;

    public TMP_Text[] FromCurrencyCode;

    public TMP_Text[] Lowest;
    public TMP_Text[] Highest;

    public GraphChart[] graphChart;

    public Material red_materials_fill;
    public Material green_materials_fill;

    public PlayerAccount playerAccount;

    public GameObject CryptoCurrencyPannel;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Fetched", 0) == 1)
        {
            AssignUI();
        }
        string currentDate = System.DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        for (int i = 0; i < cryptoCurrencies.Length; i++)
        {
            StartCoroutine(Fetcher(cryptoURL + cryptoCurrencies[i].FromCurrencyCode + "USDT&resolution=D&from=1625122788" + "&to=" + currentDate + apiKey, cryptoCurrencies[i]));
        }
        StartCoroutine(UpdateUI());
        PlayerPrefs.SetInt("Fetched", 1);
    }

    IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(1.5f);
        AssignUI();
    }

    void AssignUI()
    {
        for (int i = 0; i < cryptoCurrencies.Length; i++)
        {
            CurrencyName[i].text = cryptoCurrencies[i].FromCurrencyName;

            FromCurrencyCode[i].text = cryptoCurrencies[i].FromCurrencyCode;

            Lowest[i].text = (float.Parse(cryptoCurrencies[i].Low) * playerAccount.currencyCode).ToString();
            
            Highest[i].text = (float.Parse(cryptoCurrencies[i].High) * playerAccount.currencyCode).ToString();

            graphChart[i].DataSource.ClearCategory("Amount");

            graphChart[i].DataSource.StartBatch();

            if(cryptoCurrencies[i].last5Day[4] < cryptoCurrencies[i].last5Day[3] )
                graphChart[i].DataSource.SetCategoryFill("Amount", red_materials_fill, false);
            else
                graphChart[i].DataSource.SetCategoryFill("Amount", green_materials_fill, false);

            for (int j = 0; j < 5; j++)
            {
                graphChart[i].DataSource.AddPointToCategory("Amount", j, cryptoCurrencies[i].last5Day[j]);
            }
            graphChart[i].DataSource.EndBatch();
        }
    }

    IEnumerator Fetcher(string URL, CryptoCurrency crypto)
    {
        UnityWebRequest res = UnityWebRequest.Get(URL);

        yield return res.SendWebRequest();

        JSONNode cryptoInfo = JSON.Parse(res.downloadHandler.text);

        crypto.SetDirty();

        string lowest = cryptoInfo["l"][cryptoInfo["l"].Count-1];
        string highest = cryptoInfo["h"][cryptoInfo["h"].Count-1];

        if(lowest != null)
            crypto.Low = lowest;    
        if(highest != null)
            crypto.High = highest;   

        for (int i = 0, j = 5; i < 5; i++)
        {
            crypto.last5Day[i] = cryptoInfo["c"][cryptoInfo["c"].Count-j] * playerAccount.currencyCode;
            j--;
        }
            
        yield return new WaitForSeconds(2f);
    }

    public void OpenCryptoCurrencyPannel(CryptoCurrency cryptoCurrency)
    {
        CryptoCurrencyPannel.GetComponent<CryptoPannel>().cryptoCurrency = cryptoCurrency;
        CryptoCurrencyPannel.GetComponent<CryptoPannel>().AssignUI();
        CryptoCurrencyPannel.SetActive(true);
    }
}
