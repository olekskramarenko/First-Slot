using System.Collections.Generic;
using UnityEngine;

public class PrizeCalculation : MonoBehaviour
{
    private int totalPrize;
    private int freeSpinsPrize;
    [SerializeField] private ReelsStateController reelsStateController;

    public int TotalPrize => totalPrize;

    public int FreeSpinsPrize => freeSpinsPrize;

    public void CalculatePrize(List<Symbol> winSymbols)
    {
        var prize = winSymbols[0].SymbolCost;
        totalPrize += prize;
        if (reelsStateController.FreeSpinsGame)
        {
            freeSpinsPrize += prize;
        }
    }
    public void ResetFreeSpinsPrize()
    {
        freeSpinsPrize = 0;
    }
}

