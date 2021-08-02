using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCalculation : MonoBehaviour
{
    //[SerializeField] private WinLinesChecker winLinesChecker;
    //[SerializeField] private Text counterText;
    private int totalPrize;

    public int TotalPrize => totalPrize;

    public void CalculatePrize(List<Symbol> winSymbols)
    {
        var prize = winSymbols[0].SymbolCost;
        totalPrize += prize;
        print("### totalPrize = " + totalPrize);
    }
} 

