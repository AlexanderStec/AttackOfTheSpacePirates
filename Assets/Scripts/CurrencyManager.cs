using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CurrencyManager : MonoBehaviour
{
    public int balance;
    public TextMeshProUGUI moneyDisplay;

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
        moneyDisplay.SetText(" " + balance);
    }

}  