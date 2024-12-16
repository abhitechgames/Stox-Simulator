using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class BuyStocks : MonoBehaviour
{
    public PlayerAccount playerAccount;

    public TMP_Text AmountInHand;
    public TMP_Text Shares;

    TMP_InputField amountvalue;

    public float selectedValue;

    public Stock selectedStock; 

    private SelectStockFill selectStockFill;

    public SellStocks sellStocks;

    public GameObject TradeButton;

    // Start is called before the first frame update
    private void Start() 
    {
        selectStockFill = FindObjectOfType<SelectStockFill>();
        UIupdate();
    }

    public void UIupdate()
    {
        AmountInHand.text = playerAccount.AmountInHand.ToString();
        Shares.text = selectedStock.referencedStock.shares.ToString();

    }

    public void BuyInputFieldValueChanged(TMP_InputField value)
    {
        amountvalue = value;
        selectedValue = int.Parse(value.text);
        if(selectedValue > playerAccount.AmountInHand)
        {
            selectedValue = playerAccount.AmountInHand;
            value.text = selectedValue.ToString();
        }
        else if(selectedValue < 0f)
        {
            selectedValue = 0f;
            value.text = selectedValue.ToString();
        }

    }

    public void Buy()
    {
        if(selectedValue == 0f)
            return;


        if(selectedValue <= playerAccount.AmountInHand)
        {
            selectedStock.changeShares(selectedStock.shares + selectedValue);
            selectedStock.referencedStock.changeShares(selectedStock.shares);

            playerAccount.ChangeShares(playerAccount.Shares + selectedValue);
            playerAccount.ChangeAmount(playerAccount.AmountInHand - selectedValue);

            playerAccount.AmountTransactionList();

            selectedStock.setPriceWhenBuying(float.Parse(selectedStock.current));
            selectedStock.referencedStock.setPriceWhenBuying(float.Parse(selectedStock.referencedStock.current));

            UIupdate();
            sellStocks.UIupdate();
            selectStockFill.UIupdate();
            Close();
        }

    }

    private void LateUpdate() {
        if(playerAccount.AmountInHand < 0f)
        {
            playerAccount.ChangeAmount(0f);
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
        TradeButton.SetActive(true);
        StartCoroutine(FindObjectOfType<SelectStockFill>().DisableTradeScreen(gameObject));
    }
}


        //  for saving in text files
        // string path = "Assets/InvestedStocks/InvestedStocks.txt";

        // StreamReader reader = new StreamReader(path); 
        // string line = reader.ReadToEnd();

        // reader.Close();

        // StreamWriter writer = new StreamWriter(path, true);
        // writer.WriteLine(selectedStock.symbol + ":" + selectedStock.close);
        // writer.Close();
        
        // selectedValue = 0f;
        // amountvalue.text = selectedValue.ToString(); 