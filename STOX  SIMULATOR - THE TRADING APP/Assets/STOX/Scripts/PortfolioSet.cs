using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PortfolioSet : MonoBehaviour
{
    public GameObject infoGatherer;

    public TMP_Text userName;
    public TMP_Text currency;

    public TMP_Text Amount;
    public TMP_Text Profit;
    public TMP_Text Shares;

    public TMP_InputField Name;
    public TMP_Dropdown Currency;
    public TMP_InputField IntialAmount;

    public Button Done;

    public Toggle toggle;

    public PlayerAccount playerAccount;

    public GameObject ResetMenu;

    public bool isProfile = false;

    int isInfoGathered;

    private void Start() {
        isInfoGathered = PlayerPrefs.GetInt("isInfoGathered", 0);

        if(isInfoGathered == 0)
        {
            infoGatherer.SetActive(true);
            playerAccount.CreateAmountTransList();
            playerAccount.CreateProfitTransList();
        }

        PlayerPrefs.SetInt("Fetched", 0);

        UIupdate();
    }

    public void OpenInvestedStocks(GameObject InvestedPannel)
    {
        InvestedPannel.SetActive(true);
    }
    public void CloseInvestedStocks(GameObject InvestedPannel)
    {
        InvestedPannel.GetComponent<Animator>().SetTrigger("Close");
        StartCoroutine(disablenvestedStocks(InvestedPannel));
    }

    IEnumerator disablenvestedStocks(GameObject InvestedPannel)
    {
        yield return new WaitForSeconds(1f);
        InvestedPannel.SetActive(false);
    }

    private void Update() {
        if(!isProfile)
            if(Name.text != "")
            {
                toggle.gameObject.SetActive(true);
            }
            else 
            {
                toggle.gameObject.SetActive(false);
            }

        if (Application. platform == RuntimePlatform. Android)
            if (Input. GetKey(KeyCode. Escape))
            {
                Application.Quit(0);
                return;
            }
    }

    public void DoneButtonClicked()
    {
        PlayerPrefs.SetString("PlayerName", Name.text);
        PlayerPrefs.SetString("PlayerCurrency", Currency.captionText.text);
        PlayerPrefs.SetFloat("PlayerAmount", float.Parse(IntialAmount.text));
        if(Currency.captionText.text == "$USD")
        {
            PlayerPrefs.SetFloat("PlayerCurrencyCode", 1f);
        }
        else if(Currency.captionText.text == "INR")
        {
            PlayerPrefs.SetFloat("PlayerCurrencyCode", 74.22f);
        }
        else if(Currency.captionText.text == "$MXN")
        {
            PlayerPrefs.SetFloat("PlayerCurrencyCode", 19.88f);
        }
        else if(Currency.captionText.text == "£GBP")
        {
            PlayerPrefs.SetFloat("PlayerCurrencyCode", 0.72f);
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerCurrencyCode", 73.31f);
        }

        playerAccount.name = PlayerPrefs.GetString("PlayerName");
        playerAccount.currency = PlayerPrefs.GetString("PlayerCurrency");
        playerAccount.currencyCode = PlayerPrefs.GetFloat("PlayerCurrencyCode");
        playerAccount.amountTransaction.Remove(500f);
        playerAccount.ChangeAmount(PlayerPrefs.GetFloat("PlayerAmount", 500f));
        playerAccount.AmountTransactionList();

        UIupdate();
        FindObjectOfType<PortfolioChartsManager>().chartUpdate();

        PlayerPrefs.SetInt("isInfoGathered", 1);
        infoGatherer.GetComponent<Animator>().SetTrigger("Close");

        StartCoroutine(disableinfoGatherer());
    }

    IEnumerator disableinfoGatherer()
    {
        yield return new WaitForSeconds(1f);
        infoGatherer.SetActive(false);
    }

    public void UIupdate()
    {
        userName.text = PlayerPrefs.GetString("PlayerName");
        currency.text = PlayerPrefs.GetString("PlayerCurrency");

        if(!isProfile)
        {
            Amount.text = playerAccount.AmountInHand.ToString();
            Shares.text = playerAccount.Shares.ToString();
            Profit.text = playerAccount.totalProfit.ToString();
        }
    }

    public void EnableDoneButton()
    {
        Done.gameObject.SetActive(true);
    }

    public void ResetScreen()
    {
        ResetMenu.SetActive(true);
    }
    public void ResetSimulator()
    {
        PlayerPrefs.DeleteAll();
        playerAccount.amountTransaction.Clear();
        SceneManager.LoadScene("PORTFOLIO");
    }
    public void ResetScreenDisable()
    {
        ResetMenu.SetActive(false);
    }

}
