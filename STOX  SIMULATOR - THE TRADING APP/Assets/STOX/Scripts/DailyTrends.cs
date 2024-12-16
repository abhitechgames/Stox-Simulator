using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class DailyTrends : MonoBehaviour {

    [Header("STOCKS")]
    public Stock[] stocks;

    [Header("UI")]
    public TMP_Text[] CompanyNames;
    public TMP_Text[] High;
    public TMP_Text[] Low;
    public TMP_Text[] Volume;

    public PlayerAccount playerAccount;

    public Animator notHaveEnoughStockAnimator;

    string currentDate;

    private string searchURL = "https://finnhub.io/api/v1/search?q=";
    
    // private string dailyURL_alphavantage = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=";
    private string dailyURL = "https://finnhub.io/api/v1/quote?symbol=";

    private string lastWeeksDataURL = "https://finnhub.io/api/v1/stock/candle?symbol=";
    
    // private string infoURL = "https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=";
    // private string API_KEY_alphavantage = "&apikey=1C73N1MJFR2IBKWV";
    private string API_KEY = "&token=c3akj22ad3idt41dmlm0";

    bool canDisplayData;

    public GameObject FetchScreen;

    public Stock selectStockForInvesting;
    public Stock[] searchedStock;

    public GameObject NotFound;
    public GameObject[] SearchStockBlock;

    public TMP_Text[] SearchStockName;
    public TMP_Text[] SearchStockSymbol;
    public TMP_Text[] SearchStockType;

    public Animator MenuPannelAnimator;

    // EXTRA KEY - A0O54FQEBDTAK2MT

    private void Start() {
        currentDate = System.DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

        if(PlayerPrefs.GetInt("Fetched", 0) == 1)
        {
            AssignUI();
            return;
        }

        FetchScreen.SetActive(true);
        for (int i = 0; i < stocks.Length; i++)
        {
            CompanyNames[i].text = "";
            High[i].text = "";
            Volume[i].text = "";
            Low[i].text = "";
        }

        StartCoroutine(DisplayUI());

        for (int i = 0; i < stocks.Length; i++)
        {
            StartCoroutine(Fetcher((dailyURL + stocks[i].symbol + API_KEY), stocks[i]));
        }
        //Fetch Completed
        PlayerPrefs.SetInt("Fetched", 1);

        canDisplayData = true;
    }

    IEnumerator DisplayUI()
    {
        yield return new WaitForSeconds(3f);

        if(canDisplayData)
            AssignUI();
        else
            for (int i = 0; i < stocks.Length; i++)
            {
                StartCoroutine(Fetcher((dailyURL + stocks[i].symbol + API_KEY), stocks[i]));
            }
    }

    IEnumerator Fetcher(string URL, Stock stock)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);

        yield return response.SendWebRequest();

        JSONNode stockInfo = JSON.Parse(response.downloadHandler.text);

        //Alpha vantage

            // string open = stockInfo["Time Series (Daily)"][System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-") + (int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")) - 1).ToString()]["1. open"];
            // string close = stockInfo["Time Series (Daily)"][System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-") + (int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")) - 1).ToString()]["4. close"];
            // string high = stockInfo["Time Series (Daily)"][System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-") + (int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")) - 1).ToString()]["2. high"];
            // string low = stockInfo["Time Series (Daily)"][System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-") + (int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")) - 1).ToString()]["3. low"];
            // string volume = stockInfo["Time Series (Daily)"][System.DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-") + (int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("dd")) - 1).ToString()]["6. volume"];
        

        //Finhub

        stock.SetDirty();

        string open = stockInfo["o"];
        string close = stockInfo["pc"];
        string high = stockInfo["h"];
        string low = stockInfo["l"];
        string current = stockInfo["c"];

        if(open != null || close != null ||high != null ||low != null ||current != null )
        {
            stock.open = (float.Parse(open) * playerAccount.currencyCode).ToString();
            stock.close = (float.Parse(close) * playerAccount.currencyCode).ToString();
            stock.high = (float.Parse(high) * playerAccount.currencyCode).ToString();
            stock.low = (float.Parse(low) * playerAccount.currencyCode).ToString();
            stock.current = (float.Parse(current) * playerAccount.currencyCode).ToString();
        }
        
        StartCoroutine(lastWeekDataFetch(lastWeeksDataURL + stock.symbol + "&resolution=D&from=1622516874&to=" + currentDate + API_KEY, stock));
        StartCoroutine(lastMonthDataFetch(lastWeeksDataURL + stock.symbol + "&resolution=D&from=1614600703&to=" + currentDate + API_KEY, stock));
        StartCoroutine(lastYearDataFetch(lastWeeksDataURL + stock.symbol + "&resolution=M&from=1577880703&to=" + currentDate + API_KEY, stock));
    }

    void AssignUI()
    {
        for (int i = 0; i < stocks.Length; i++)
        {
            CompanyNames[i].text = stocks[i].name;
            High[i].text = (float.Parse(stocks[i].high) * playerAccount.currencyCode).ToString();
            Low[i].text = (float.Parse(stocks[i].low) * playerAccount.currencyCode).ToString();
            Volume[i].text = (float.Parse(stocks[i].current) * playerAccount.currencyCode).ToString();
        }
        FetchScreen.SetActive(false);
    }

    public void DataForSearchedStock(Stock stock)
    {
        if(stock.lastWeek[0] != 0f)
        {
            SelectedStockForInvesting(stock);
        }
        else{
            notHaveEnoughStockAnimator.SetTrigger("Pop");
        }
    }
    
    public void SelectedStockForInvesting(Stock stock)
    {
        selectStockForInvesting.name = stock.name;
        selectStockForInvesting.symbol = stock.symbol;
        selectStockForInvesting.current = stock.current;
        selectStockForInvesting.open = stock.open;
        selectStockForInvesting.close = stock.close;
        selectStockForInvesting.high = stock.high;
        selectStockForInvesting.low = stock.low;

        selectStockForInvesting.changeShares(stock.shares);
        selectStockForInvesting.changeProfit(stock.Profit);
        selectStockForInvesting.setPriceWhenBuying(stock.PriceWhenBuying);

        for (int i = 0; i < stock.lastWeek.Length; i++)
        {
            selectStockForInvesting.lastWeek[i] = stock.lastWeek[i];
        }
        for (int i = 0; i < stock.lastMonth.Length; i++)
        {
            selectStockForInvesting.lastMonth[i] = stock.lastMonth[i];
        }
        for (int i = 0; i < stock.lastYear.Length; i++)
        {
            selectStockForInvesting.lastYear[i] = stock.lastYear[i];
        }

        selectStockForInvesting.referencedStock = stock;

        SceneManager.LoadScene("SELECTED STOCK");
    }

    private void Update() {
        if (Application. platform == RuntimePlatform. Android)
            if (Input. GetKey(KeyCode. Escape))
            {
                Application.Quit(0);
                return;
            }
    }

    public void openSearchPannel(GameObject searchPannel)
    {
        searchPannel.SetActive(true);
    }
    public void closeSearchPannel(GameObject searchPannel)
    {
        searchPannel.GetComponent<Animator>().SetTrigger("Close");
        StartCoroutine(disableSearchPannel(searchPannel));
    }

    IEnumerator disableSearchPannel(GameObject searchPannel)
    {
        yield return new WaitForSeconds(1f);
        searchPannel.SetActive(false);
    }

    public void searchForStocks(TMP_InputField searchbar)
    {
        var searchKeyWord = searchbar.text.ToLower();
        foreach (GameObject B in SearchStockBlock)
        {
            B.SetActive(false);
        }
        StartCoroutine(SearchResults(searchURL + searchKeyWord + API_KEY));
    }

    // A0O54FQEBDTAK2MT
    IEnumerator SearchResults(string URL)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);

        yield return response.SendWebRequest();

        JSONNode stockInfo = JSON.Parse(response.downloadHandler.text);

        string count = stockInfo["count"];
        int searchResultsCount = int.Parse(count);
        
        if(searchResultsCount > SearchStockBlock.Length)
            searchResultsCount = SearchStockBlock.Length;

        if(searchResultsCount == 0)
        {
            NotFound.SetActive(true);
        }
        else
        {
            for (int i = 0; i < searchResultsCount; i++)
            {
                string name = stockInfo["result"][i]["description"];
                string symbol = stockInfo["result"][i]["symbol"];
                string type = stockInfo["result"][i]["type"];

                SearchStockName[i].text = name;
                SearchStockSymbol[i].text = symbol;
                SearchStockType[i].text = type;

                searchedStock[i].name = name;
                searchedStock[i].symbol = symbol;

                SearchStockBlock[i].SetActive(true);

                StartCoroutine(Fetcher(dailyURL + symbol + API_KEY, searchedStock[i]));
            }        
            // UI Updates
            // Block.SetActive(true);
            // searchedStock.name = name;
            // searchedStock.symbol = symbol;

            // Symbol.text = symbol;
            // Name.text = name;
            // Type.text = type;
        }
    }

    IEnumerator lastWeekDataFetch(string URL, Stock stock)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);

        yield return response.SendWebRequest();

        JSONNode stockInfo = JSON.Parse(response.downloadHandler.text);

        int len = stockInfo[0].Count;
        
        for (int i = 0, j = len; i < stock.lastWeek.Length; i++)
        {
            j--;
            stock.lastWeek[i] = stockInfo[0][j] * playerAccount.currencyCode;
        }
        Array.Reverse(stock.lastWeek);
    }

    IEnumerator lastMonthDataFetch(string URL, Stock stock)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);

        yield return response.SendWebRequest();

        JSONNode stockInfo = JSON.Parse(response.downloadHandler.text);
        
        int len = stockInfo[0].Count;
        
        for (int i = 0, j = len; i < 30; i++)
        {
            j--;
            stock.lastMonth[i] = stockInfo[0][j] * playerAccount.currencyCode;
        }
        Array.Reverse(stock.lastMonth);
    }

    IEnumerator lastYearDataFetch(string URL, Stock stock)
    {
        UnityWebRequest response = UnityWebRequest.Get(URL);

        yield return response.SendWebRequest();

        JSONNode stockInfo = JSON.Parse(response.downloadHandler.text);

        int len = stockInfo[0].Count;
        
        for (int i = 0, j = len; i < 12; i++)
        {
            j--;
            stock.lastYear[i] = stockInfo[0][j] * playerAccount.currencyCode;
        }
        Array.Reverse(stock.lastYear);
    }
}