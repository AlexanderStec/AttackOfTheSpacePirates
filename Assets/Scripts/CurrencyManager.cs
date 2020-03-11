using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CurrencyManager : MonoBehaviour
{
    public int balance;
    public TextMeshProUGUI moneyDisplay;
    public TextMeshProUGUI ShopmoneyDisplay;

    private void Start()
    {

        if (!SceneManager.GetActiveScene().name.Equals("Level 1") && tag.Equals("Player")) //if not on first level and player is in scene this runs
        {
            CurrencyManager sk = Data.CurrentCurrency;
            balance = sk.balance;
        }
    }

    public CurrencyManager(int bal = 0)
    {
        if (bal < 0)
            Debug.LogWarning("Initialized currency manager with negative balance!");
        balance = bal;
    }

    public void addMoney(int val)
    {
        if (val < 0)
            Debug.LogWarning("You shouldn't be adding negative money!");
        balance = balance + val;
    }

    public void removeMoney(int val)
    {
        if (val < 0)
            Debug.LogWarning("You shouldn't be removing negative money!");
        balance = balance - val;
    }

    public bool hasEnoughMoneyForThis(int val)
    {
        return balance - val >= 0;
    }

    public void gimmeAllYourGold()
    {
        balance = 0;
    }
    private void FixedUpdate()
    {
        Data.CurrentCurrency = this.GetComponent<CurrencyManager>();
        moneyDisplay.SetText(" " + balance);
        ShopmoneyDisplay.SetText(" " + balance);
    }

}  