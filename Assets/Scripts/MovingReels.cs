using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class MovingReels : MonoBehaviour
{

    [SerializeField] private RectTransform[] allReelsRT;
    [SerializeField] private MovingSymbols[] MovingSymbols;
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private GameObject playButton, stopButton;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    private readonly float distanceStart = 2;
    private readonly float distanceWay = 12;
    private readonly float distanceStop = 1; // Important, value 1 nedded for correct showing final screens
    private Dictionary<RectTransform, MovingSymbols> reelsDictionary;


    private void Start()
    {
        stopButton.SetActive(false);
        reelsDictionary = new Dictionary<RectTransform, MovingSymbols>();
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            reelsDictionary.Add(allReelsRT[i], MovingSymbols[i]);
        }
    }
    public void MovingStart()
    {
        playButton.SetActive(false);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            reelsDictionary[reel].ReelState = ReelState.Spin;
            reelsDictionary[reel].StartReelPos = reel.localPosition.y;
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + (distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel));
        }
    }
    private void MovingWay(RectTransform reel)
    {
        stopButton.SetActive(true);
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, previousDistance));
    }
    private void MovingSlowDown(RectTransform reel, float previousDistance)
    {
        if (!playButton.activeSelf)
        {
            stopButton.SetActive(false);
        }
        reelsDictionary[reel].ReelState = ReelState.SlowDown;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + previousDistance + (distanceStop * symbolHeight * symbolsCount)), timeStop, true)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel));
    }
    private void SetSymbolDefaultPosition(RectTransform reel)
    {
        var finalReelPosition = reel.localPosition.y;
        var lastSpinDistance = -(finalReelPosition - reelsDictionary[reel].StartReelPos);
        reelsDictionary[reel].FullSpinDistance += lastSpinDistance;
        reelsDictionary[reel].ResetSymbolReelsCounter();
        if (reelsDictionary[reel].ReelId == allReelsRT.Length - 1)
        {
            playButton.SetActive(true);
            stopButton.SetActive(false);
            FinalResult.SetNextFinalScreen();
        }
    }

    public void MovingStop()
    {
        stopButton.SetActive(false);
        for (int i = 0; i < allReelsRT.Length; i++)
        {
            var reel = allReelsRT[i];
            var index = i;
            DOTween.Kill(reel);
            var distBeforeStopPressed = -(reel.localPosition.y - reelsDictionary[reel].StartReelPos);
            var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeStopPressed);
            reel.DOAnchorPosY(-(reelsDictionary[reel].FullSpinDistance + correctedSymbolsDist), 0.1f)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, correctedSymbolsDist));
        }
    }

    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed)
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / symbolHeight) * symbolHeight;
        return correctedSymbolsDist;
    }
}
