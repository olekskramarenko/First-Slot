using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrizeAnimator : MonoBehaviour
{
    [SerializeField] private Text prizeText;
    [SerializeField] private PrizeCalculation prizeCalculation;
    private int prevPrize = 0;

    public void UpdatePrizeCounter()
    {
        var prize = prizeCalculation.TotalPrize;
        if (prevPrize != prize) 
        {
            prizeText.DOCounter(prevPrize, prize, 1);
            prevPrize = prize;
        } 
    }
}
