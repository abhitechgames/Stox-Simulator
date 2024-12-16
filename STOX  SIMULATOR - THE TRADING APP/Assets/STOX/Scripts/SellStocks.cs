using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellStocks : MonoBehaviour
{
    public PlayerAccount playerAccount;

    public TMP_Text AmountInHand;
    public TMP_Text Shares;
    public TMP_Text Profit;

    TMP_InputField amountvalue;

    public float selectedValue;

    public Stock selectedStock; 

    public SelectStockFill selectStockFill;

    public BuyStocks buyStocks;

    public GameObject TradeButton;


    // Start is called before the first frame update
    private void Start() {
        UIupdate();
    }

    public void UIupdate()
    {
        AmountInHand.text = playerAccount.AmountInHand.ToString();
        Shares.text = selectedStock.referencedStock.shares.ToString();
        Profit.text = selectedStock.Profit.ToString();
    }

    public void SellInputFieldValueChanged(TMP_InputField value)
    {
        amountvalue = value;
        selectedValue = int.Parse(value.text);
        if(selectedValue > selectedStock.referencedStock.shares)
        {
            selectedValue = selectedStock.referencedStock.shares;
            value.text = selectedValue.ToString();
        }
        else if(selectedValue < 0f)
        {
            selectedValue = 0f;
            value.text = selectedValue.ToString();
        }

    }
    public void Sell()
    {
        if(selectedValue == 0f)
            return;

        if(selectedValue <= selectedStock.referencedStock.shares)
        {
            playerAccount.ChangeAmount(playerAccount.AmountInHand + (selectedValue + selectedValue * selectStockFill.change/100f));
            playerAccount.AmountTransactionList();
            playerAccount.ChangeShares(playerAccount.Shares - selectedValue);
            playerAccount.ChangeProfit(playerAccount.totalProfit + (selectedValue * selectStockFill.change/100f));
            playerAccount.ProfitTransactionList();
            
            selectedStock.changeShares(selectedStock.shares - selectedValue);
            selectedStock.referencedStock.changeShares(selectedStock.shares);

            if(selectedStock.referencedStock.shares == 0f)
            {
                selectedStock.referencedStock.setPriceWhenBuying(0f);
                selectedStock.setPriceWhenBuying(0f);
            }
            UIupdate();
            buyStocks.UIupdate();
            selectStockFill.UIupdate();
            Close();

        }
    }

    private void LateUpdate() {
        if(selectedStock.shares < 0f)
        {
            selectedStock.changeShares(0f);
            selectedStock.referencedStock.changeShares(0f);
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
        TradeButton.SetActive(true);
        StartCoroutine(FindObjectOfType<SelectStockFill>().DisableTradeScreen(gameObject));
    }
}
