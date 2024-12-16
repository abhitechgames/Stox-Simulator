using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public Animator ChooseLangAnim;
    public GameObject LangScene;

    public GameObject InvestBack;
    public GameObject InvestUI;
    public GameObject StockGrid;

    public GameObject BuyMenu;
    public GameObject SellMenu;
    public Animator BuyMenuAnim;
    public Animator SellMenuAnim;

    public Slider decideAmountSlider;
    public Slider decideSellSlider;

    public TMP_Text selectedValue;
    public TMP_Text sellSelectedValue;
    public TMP_Text maxSelectValue;
    public TMP_Text sellMaxSelectValue;

    public float AmountInHand = 500f;
    public float Positions = 0f;
    public float Profits = 0f;

    public TMP_Text AmountInHandText;
    public TMP_Text PositionsText;
    public TMP_Text ProfitsText;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableLangScene", 5f);
    }

    public void amountChanged()
    {
        float value = decideAmountSlider.value;
        selectedValue.text = "$" + value.ToString();
    }

    public void sellAmountChanged()
    {
        float value = decideSellSlider.value;
        sellSelectedValue.text = "$" + value.ToString();
    }

    void EnableLangScene()
    {
        LangScene.SetActive(true);
    }

    public void Continue()
    {
        ChooseLangAnim.SetTrigger("Trans");
        Invoke("EnableInvestTools", 2f);
    }

    void EnableInvestTools()
    {
        InvestBack.SetActive(true);
        StockGrid.SetActive(true);
        InvestUI.SetActive(true);
    }

    public void BuyMenuOpen()
    {
        BuyMenu.SetActive(true);
        decideAmountSlider.value = AmountInHand / 2;
        decideAmountSlider.maxValue = AmountInHand;
        maxSelectValue.text = "$" + AmountInHand.ToString();
    }
    public void SellMenuOpen()
    {
        decideSellSlider.value = Positions / 2;
        decideSellSlider.maxValue = Positions;
        sellMaxSelectValue.text = "$" + Positions.ToString();
        SellMenu.SetActive(true);
    }
    public void BuyMenuClose ()
    {
        BuyMenuAnim.SetTrigger("Close");
        Invoke("DisableBuyMenu", 1f);
    }
    public void SellMenuClose()
    {
        SellMenuAnim.SetTrigger("Close");
        Invoke("DisableBuyMenu", 1f);
    }

    void DisableBuyMenu()
    {
        BuyMenu.SetActive(false);
        SellMenu.SetActive(false);
    }

    public void BuyStocks()
    {
        if (AmountInHand == 0)
        {
            Debug.Log("You not have enough Money to buy Stocks");
            BuyMenuAnim.SetTrigger("Close");
            Invoke("DisableBuyMenu", 1f);
            return;
        }
        Positions += decideAmountSlider.value;
        AmountInHand -= decideAmountSlider.value;

        PositionsText.text = "$" + Positions.ToString();
        AmountInHandText.text = "$" + AmountInHand.ToString();

        decideAmountSlider.maxValue = AmountInHand;
        maxSelectValue.text = "$" + AmountInHand.ToString();

        BuyMenuAnim.SetTrigger("Close");
        Invoke("DisableBuyMenu", 1f);
    }

    public void SellStocks()
    {
        if (Positions == 0)
        {
            Debug.Log("You not have Stocks to Sell");
            SellMenuAnim.SetTrigger("Close");
            Invoke("DisableBuyMenu", 1f);
            return;
        }

        Positions -= decideSellSlider.value;
        AmountInHand += decideSellSlider.value;

        PositionsText.text = "$" + Positions.ToString();
        AmountInHandText.text = "$" + AmountInHand.ToString();

        SellMenuAnim.SetTrigger("Close");
        Invoke("DisableBuyMenu", 1f);
    }
}
