using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols;
    [SerializeField] private int[] winLine;
    List<Symbol> otherSymbols = new List<Symbol>();
    List<Symbol> winSymbolsLine = new List<Symbol>();

    private void GetWinSymbols()
    {
        foreach (Symbol symbol in symbols)
        {
            SeparateAndSetSymbols(symbol);
        }
    }

    private void SeparateAndSetSymbols ( Symbol symbol)
    {
        bool coincidence = false;
        foreach (var value in winLine)
        {
            if (symbol.SymbolFinalId == value)
            {
                winSymbolsLine.Add(symbol);
                coincidence = true;
                //return; 
            }
        }
        if ( !coincidence)
        {
            otherSymbols.Add(symbol);
        } 
    }
    public void CheckWinLine()
    {
        GetWinSymbols();
        if ( winSymbolsLine[0].SymbolType == winSymbolsLine[1].SymbolType && winSymbolsLine[1].SymbolType == winSymbolsLine[2].SymbolType)
        {
            print("### WINLINE FOUND " + winSymbolsLine[0] + winSymbolsLine[1] + winSymbolsLine[2]);
            foreach (Symbol winSymbol in winSymbolsLine)
            {
                winSymbol.SymbolAnimation.Play("pulse");
            }
            foreach ( Symbol otherSymbol in otherSymbols)
            {
                otherSymbol.SymbolAnimation.Play("shadow");
            }
        }
        winSymbolsLine.Clear();
        otherSymbols.Clear();
    }
}
 