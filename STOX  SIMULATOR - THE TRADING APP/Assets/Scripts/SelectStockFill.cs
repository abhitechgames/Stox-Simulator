using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class SelectStockFill : MonoBehaviour
{
    public Stock SelectedStock;

    public TMP_Text TradeText;
    public TMP_Text CompanyName;
    public TMP_Text Open;
    public TMP_Text Close;
    public TMP_Text High;
    public TMP_Text Low;
    public TMP_Text Volume;

    public TMP_Text Shares;
    public TMP_Text Profit;
    public TMP_Text ChangePercent;

    public float change;

    public GameObject TradeScreen;
    public GameObject BuyScreen;
    public GameObject SellScreen;

    public PlayerAccount playerAccount;

    public GameObject TradeButton;


    // Start is called before the first frame update
    private void Start() {
        UIupdate();
    }

    public void UIupdate()
    {
        CompanyName.text = SelectedStock.name;
        Volume.text = SelectedStock.current;
        Open.text = SelectedStock.open;
        Close.text =  SelectedStock.close;
        High.text =  SelectedStock.high;
        Low.text = SelectedStock.low;

        Shares.text = SelectedStock.shares.ToString();

        if(Shares.text != "0")
            change = (float.Parse(SelectedStock.current) - SelectedStock.PriceWhenBuying) / 100f;
        else
            change = 0f;

        SelectedStock.changeProfit((SelectedStock.PriceWhenBuying + SelectedStock.PriceWhenBuying * change/100) - SelectedStock.PriceWhenBuying);
        SelectedStock.referencedStock.changeProfit(SelectedStock.Profit); 

        ChangePercent.text = change.ToString() + "%";

        Profit.text = SelectedStock.Profit.ToString();
    }

    public void Trade()
    {
        if(TradeScreen.activeInHierarchy == false)
        {
            TradeScreen.SetActive(true);
            TradeText.text = "X";
        }
        else
        {
            TradeScreen.GetComponent<Animator>().SetTrigger("Close");
            TradeText.text = "Trade";
            StartCoroutine(DisableTradeScreen(TradeScreen));
        }
    }
    public void Buy()
    {
        BuyScreen.SetActive(true);
        TradeButton.SetActive(false);
        TradeText.text = "TRADE";
        TradeScreen.GetComponent<Animator>().SetTrigger("Close");
        StartCoroutine(DisableTradeScreen(TradeScreen));
    }
    public void Sell()
    {
        SellScreen.SetActive(true);
        TradeButton.SetActive(false);
        TradeText.text = "TRADE";
        TradeScreen.GetComponent<Animator>().SetTrigger("Close");
        StartCoroutine(DisableTradeScreen(TradeScreen));
    }

    public IEnumerator DisableTradeScreen(GameObject obj)
    {
        yield return new WaitForSeconds(.5f);
        obj.SetActive(false);
    }

    private void Update() {
        if (Application. platform == RuntimePlatform. Android)
            if (Input. GetKey(KeyCode. Escape))
            {
                SceneManager.LoadScene("INVEST");
                return;
            }
    }
    
}

