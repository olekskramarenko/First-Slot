using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PrizeAnimator : MonoBehaviour
{
    [SerializeField] private Text prizeText;
    [SerializeField] private PrizeCalculation prizeCalculation;
    [SerializeField] private Symbol[] symbols;
    [SerializeField] private float forwardScale, pulseScale, symbolsFade, animTime;
    private int prevPrize = 0;
    private bool isAnimPlaying;
    private bool isStopPushed;
    


    public bool IsAnimPlaying { get => isAnimPlaying; set => isAnimPlaying = value; }
    public bool IsStopPushed { get => isStopPushed; set => isStopPushed = value; }

    public delegate void PlaySoundEvent(SoundType sound);
    public static event PlaySoundEvent OnSoundPLayed;

    public void UpdatePrizeCounter()
    {
        int duration = 1;
        var prize = prizeCalculation.TotalPrize;
        if (prevPrize != prize) 
        {
            if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prizeCounter);
            prizeText.DOCounter(prevPrize, prize, duration);
            prevPrize = prize;
        }
    }
    public void PlayWinAnimation(ResultsLists resultList)
    {
        if (isStopPushed) return;
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.prize);
        var winSymbolsLineList = resultList.WinSymbolsLineList;
        var otherSymbolsLineList = resultList.OtherSymbolsLineList;
        var putForwardTime = animTime / 3;
        var pulseTime = animTime / 6;
        var stayFadeTime = animTime / 1.6f;
        int loops = 2;
        int fullScale = 1;
        int fullFade = 1;
        foreach (Symbol winSymbol in winSymbolsLineList)
        {
            isAnimPlaying = true;
            winSymbol.ParticleSystem.Play();
            var symbolRT = winSymbol.SymbolRT;
            symbolRT.DOScale(forwardScale, putForwardTime).OnComplete(() =>
              {
                  symbolRT.DOScale(pulseScale, pulseTime).SetLoops(loops, LoopType.Yoyo).OnComplete(() =>
                  {
                      symbolRT.DOScale(fullScale, putForwardTime).OnComplete(() =>
                      {
                          isAnimPlaying = false;
                      });
                  });
              });
        }
        foreach (Symbol otherSymbol in otherSymbolsLineList)
        {
            var image = otherSymbol.SymbolImage;
            image.DOFade(symbolsFade, pulseTime).OnComplete(() =>
            {
                image.DOFade(symbolsFade, stayFadeTime).OnComplete(() =>
                {
                    image.DOFade(fullFade, pulseTime);
                });
            });
        }
    }

    public void PLaySmallAnimation(Symbol symbol)
    {
        if (OnSoundPLayed != null) OnSoundPLayed(SoundType.scatter);
        var putForwardTime = animTime / 3;
        var pulseTime = animTime / 6;
        int fullScale = 1;
        int loops = 2;
        var symbolRT = symbol.SymbolRT;
        symbolRT.DOScale(forwardScale, putForwardTime).OnComplete(() =>
        {
            symbolRT.DOScale(pulseScale, pulseTime).SetLoops(loops, LoopType.Yoyo).OnComplete(() =>
            {
                symbolRT.DOScale(fullScale, putForwardTime);
            });
        });
    }
    public void StopWinAnimation()
    {
        int fullScale = 1;
        int fullFade = 1;
        float time = 0.1f;
        isStopPushed = true;
        foreach (Symbol symbol in symbols)
        {
            DOTween.Kill(symbol.SymbolRT);
            DOTween.Kill(symbol.SymbolImage);
            symbol.SymbolRT.DOScale(fullScale, time);
            symbol.SymbolImage.DOFade(fullFade, time);
            symbol.ParticleSystem.Stop();
        }
        isAnimPlaying = false;
    }
}
