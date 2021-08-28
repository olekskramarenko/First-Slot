using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private Symbol[] symbols; // 12 symbols
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private ReelsStateController reelsStateController;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private PrizeAnimator prizeAnimator;
    [SerializeField] private FreeSpinsController freeSpinsController;
    [SerializeField] private MovingReels movingReels;
    private int numberOfReels = 3;
    private Symbol[] symbolsReelOne = new Symbol[4];
    private Symbol[] symbolsReelTwo = new Symbol[4];
    private bool[] scattersOnTwoReels = new bool[2];
    private bool twoScattersFound;
    private List<Symbol> twoScattersList = new List<Symbol>();
    private List<Symbol> threeScattersList = new List<Symbol>();

    public bool TwoScattersFound => twoScattersFound;

    public void CheckAnticipation(int reelId)
    {
        SeparateSymbolsOnReels();
        scattersOnTwoReels[reelId] = CheckScattersOnTwoReels(reelId);
        twoScattersFound = Array.TrueForAll(scattersOnTwoReels, value => value == true);
        if (twoScattersFound)
        {
            movingReels.DoLongSpin();

            twoScattersList.Clear();
        }
    }
    private bool CheckScattersOnTwoReels(int reelId)
    {
        if (reelId == 0)
        {
            foreach (Symbol symbol in symbolsReelOne)
            {
                if (symbol.SymbolFinalId == 0 | symbol.SymbolFinalId == 1 | symbol.SymbolFinalId == 2)
                {
                    if (symbol.SymbolType == SymbolType.scatter)
                    {
                        twoScattersList.Add(symbol);
                        prizeAnimator.PLaySmallAnimation(symbol);
                        return true;
                    }
                }
            }
        }
        else if (reelId == 1)
        {
            foreach (Symbol symbol in symbolsReelTwo)
            {
                if (symbol.SymbolFinalId == 4 | symbol.SymbolFinalId == 5 | symbol.SymbolFinalId == 6)
                {
                    if (symbol.SymbolType == SymbolType.scatter)
                    {
                        twoScattersList.Add(symbol);
                        prizeAnimator.PLaySmallAnimation(symbol);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void SeparateSymbolsOnReels()
    {
        Array.Copy(symbols, 0, symbolsReelOne, 0, 4);
        Array.Copy(symbols, 4, symbolsReelTwo, 0, 4);
    }

    public delegate void ChangeStateEvent(ReelStates reelState);
    public static event ChangeStateEvent OnStateChanged;

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
            }
        }
        if (!coincidence)
        {
            resultsList.OtherSymbolsLineList.Add(symbol);
        }
        return resultsList;
    }

    public void StopAnimation()
    {
        prizeAnimator.StopWinAnimation();
    }

    public void ShowResult()
    {
        var winLines = gameConfig.WinLines;
        StartCoroutine(WaitAndCheckLines(winLines));
    }

    private void WinLineCheck(WinLinesData winLine)
    {
        var resultsList = GetWinSymbols(winLine.WinLine);
        var winSymbol1 = resultsList.WinSymbolsLineList[0];
        var winSymbol2 = resultsList.WinSymbolsLineList[1];
        var winSymbol3 = resultsList.WinSymbolsLineList[2];
        if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
        {
            prizeAnimator.PlayWinAnimation(resultsList);
            prizeCalculation.CalculatePrize(resultsList.WinSymbolsLineList);
        }
    }

    IEnumerator WaitAndCheckLines(WinLinesData[] winLines)
    {
        foreach (var winLine in winLines)
        {
            yield return new WaitUntil(() => !prizeAnimator.IsAnimPlaying | prizeAnimator.IsStopPushed);
            WinLineCheck(winLine);
        }
        yield return new WaitUntil(() => !prizeAnimator.IsAnimPlaying);
        prizeAnimator.UpdatePrizeCounter();
        CheckScatters();
        if (OnStateChanged != null) OnStateChanged(ReelStates.ReadyForSpin);
        prizeAnimator.UpdatePrizeCounter();
        prizeAnimator.IsStopPushed = false;
    }

    private void CheckScatters()
    {
        bool[] scatterOnReels = CheckScattersOnEachReel();
        bool threeScattersFound = Array.TrueForAll(scatterOnReels, value => value == true);
        if (threeScattersFound)
        {
            freeSpinsController.StartFreeSpins();
            threeScattersList.Clear();
        };
        threeScattersList.Clear();

    }

    private bool[] CheckScattersOnEachReel()
    {
        bool[] scatterOnReels = new bool[numberOfReels];
        foreach (Symbol symbol in symbols)
        {
            if (symbol.SymbolFinalId == 0 | symbol.SymbolFinalId == 1 | symbol.SymbolFinalId == 2)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[0] = true;
                    threeScattersList.Add(symbol);
                }
            }
            else if (symbol.SymbolFinalId == 4 | symbol.SymbolFinalId == 5 | symbol.SymbolFinalId == 6)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[1] = true;
                    threeScattersList.Add(symbol);
                }
            }
            else if (symbol.SymbolFinalId == 8 | symbol.SymbolFinalId == 9 | symbol.SymbolFinalId == 10)
            {
                if (symbol.SymbolType == SymbolType.scatter)
                {
                    scatterOnReels[2] = true;
                    threeScattersList.Add(symbol);
                    prizeAnimator.PLaySmallAnimation(symbol);
                }
            }
        }
        return scatterOnReels;
    }
}