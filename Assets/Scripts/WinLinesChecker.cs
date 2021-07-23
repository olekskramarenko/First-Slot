using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    //[SerializeField] private int[] winLine; // always 3 items { LIst, List, List}
    [SerializeField] private List<int[]> winLineList;
    //private List<Symbol> otherSymbolsLine = new List<Symbol>();
    //private List<Symbol> winSymbolsLine = new List<Symbol>();

    private ResultsLists GetWinSymbols(int[] winLine)
    {
        var resultsList = new ResultsLists();
        foreach (Symbol symbol in symbols)
        {
            SeparateAndSetSymbols(symbol, winLine, resultsList);
        }
        return resultsList;
    }

    private ResultsLists SeparateAndSetSymbols(Symbol symbol, int[] winLine, ResultsLists resultsList)
    {
        bool coincidence = false;
        foreach (var value in winLine)
        {
            if (symbol.SymbolFinalId == value)
            {
                resultsList.WinSymbolsLineList.Add(symbol);
                coincidence = true;
                /*return;*/ // Todo: problem with double values of winLines
            }
        }
        if (!coincidence)
        {
            resultsList.OtherSymbolsLineList.Add(symbol);
        }
        return resultsList;
    }
    public void CheckWinLine()
    {
        foreach (var winLine in winLineList)
        {

            var resultsList = GetWinSymbols(winLine);
            if (winSymbolsLine[0].SymbolType == winSymbolsLine[1].SymbolType && winSymbolsLine[1].SymbolType == winSymbolsLine[2].SymbolType)
            {
                print("### WINLINE FOUND " + winSymbolsLine[0] + winSymbolsLine[1] + winSymbolsLine[2]);
                foreach (Symbol winSymbol in winSymbolsLine)
                {
                    winSymbol.SymbolAnimation.Play("pulse");
                }
                foreach (Symbol otherSymbol in otherSymbolsLine)
                {
                    otherSymbol.SymbolAnimation.Play("shadow");
                    //otherSymbol.SymbolAnimation.isPlaying;
                }
            }
            winSymbolsLine.Clear();
            otherSymbolsLine.Clear();
        }

    }
}
