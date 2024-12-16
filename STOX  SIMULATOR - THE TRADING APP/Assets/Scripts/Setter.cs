using UnityEngine;

public class Setter : MonoBehaviour
{
    public Stock[] stocks;
    public PlayerAccount playerAccount;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Stock s in stocks)
        {
            s.changeShares(PlayerPrefs.GetFloat(s.symbol + "shares", 0f));
            s.setPriceWhenBuying(PlayerPrefs.GetFloat(s.symbol + "pricewhenbuying", 0f));
            s.changeProfit(PlayerPrefs.GetFloat(s.symbol + "profit", 0f));
        }

        playerAccount.ChangeAmount(PlayerPrefs.GetFloat("PlayerAmount", 500f));
        playerAccount.ChangeShares(PlayerPrefs.GetFloat("PlayerShares", 0f));
        playerAccount.ChangeProfit(PlayerPrefs.GetFloat("PlayerProfit", 0f));
    }
}
