using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "PlayerAccount")]
public class PlayerAccount : ScriptableObject {

    public new string name = "Unknown";
    public string age = "28";
    public string state = "New York";
    public string currency = "USA";
    public string Email = "person@gmail.com";

    public float AmountInHand = 500f;
    public float Shares = 0f;
    public float totalProfit = 0f;

    public List<float> amountTransaction = new List<float>();
    public List<float> profitList = new List<float>();

    public float currencyCode;

    public void ChangeAmount(float Amount)
    {
        AmountInHand = Amount;

        if(AmountInHand < 0f)
        {
            AmountInHand = 0f;
        }

        PlayerPrefs.SetFloat("PlayerAmount", AmountInHand);
        // AmountTransactionList();
    }

    public void CreateAmountTransList()
    {
        amountTransaction = new List<float>();
        amountTransaction.Add(0f);
    }

    public void AmountTransactionList()
    {
        amountTransaction.Add(AmountInHand);
    }

    public void CreateProfitTransList()
    {
        profitList = new List<float>();
        profitList.Add(0f);
    }

    public void ProfitTransactionList()
    {
        profitList.Add(totalProfit);
    }
    public void ChangeShares(float shares)
    {
        Shares = shares;

        if(Shares < 0f)
        {
            Shares = 0f;
        }

        PlayerPrefs.SetFloat("PlayerShares", Shares);
    }
    public void ChangeProfit(float Profit)
    {
        totalProfit = Profit;

        PlayerPrefs.SetFloat("PlayerProfit", totalProfit);
    }

}