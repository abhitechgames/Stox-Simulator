using UnityEngine;

[CreateAssetMenu(fileName = "CryptoCurrency", menuName = "CryptoCurrency", order = 0)]
public class CryptoCurrency : ScriptableObject
{
    public string FromCurrencyName;
    public string FromCurrencyCode;


    public string High;
    public string Low;

    public float[] last5Day = new float[5];
}