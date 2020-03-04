using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data: MonoBehaviour
{
    private static StatManager currentStat;
    private static CurrencyManager currentmon;
    private static AbilityManager currentAbility;

    public static StatManager CurrentStats
    {
        get
        {
            return currentStat;
        }
        set
        {
            currentStat = value;
        }
    }

    public static CurrencyManager CurrentCurrency
    {
        get
        {
            return currentmon;
        }
        set
        {
            currentmon = value;
        }
    }

    public static AbilityManager CurrentAbilities
    {
        get
        {
            return currentAbility;
        }
        set
        {
            currentAbility = value;
        }
    }
}
