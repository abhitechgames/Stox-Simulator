using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stock")]
public class Stock : ScriptableObject 
{
    public string symbol;

    public new string name;

    public string open;
    public string close;
    public string high;
    public string low;
    public string current;

    public Stock referencedStock;

    public float shares;
    public float PriceWhenBuying;
    public float Profit;

    //For Charts
    public float[] lastWeek = new float[7]; 
    public float[] lastMonth = new float[30]; 
    public float[] lastYear = new float[12]; 

    public void changeShares(float Shares)
    {
        shares = Shares;
        PlayerPrefs.SetFloat(symbol + "shares", shares);
        //AAPLshares
    }
    public void setPriceWhenBuying(float price)
    {
        PriceWhenBuying = price;
        PlayerPrefs.SetFloat(symbol + "pricewhenbuying", PriceWhenBuying);
        //AAPLpricewhenbuying
    }
    public void changeProfit(float profit)
    {
        Profit = profit;
        PlayerPrefs.SetFloat(symbol + "profit", Profit);
        //AAPLprofit
    }
}