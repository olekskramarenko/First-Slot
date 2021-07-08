using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MovingReels : MonoBehaviour
{
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private RectTransform[] allReels; 
    [SerializeField] private FinalResult FinalResult;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [Range(0, 20)] [SerializeField] private float distanceStart, distanceWay, distanceStop;
    [SerializeField] private GameObject playButton, stopButton;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int symbolsCount;
    [SerializeField] private MovingSymbols[] MovingSymbols;
    private bool[] slowDownIsActive;
    private float[] startReelPosition, fullReelDistance;

    private void Start()
    {
        stopButton.SetActive(false);
        startReelPosition = new float[allReels.Length];
        fullReelDistance = new float[allReels.Length];
        slowDownIsActive = new bool[allReels.Length];
    }
    public void MovingStart()
    {
        playButton.SetActive(false);
        for (int i = 0; i < allReels.Length; i++)
        {
            var reel = allReels[i];
            var index = i; // Для корректной работы счетчика циклов вне цикла
            slowDownIsActive[index] = false;
            startReelPosition[index] = reel.position.y;
            reel.DOAnchorPosY(-(fullReelDistance[index]+(distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel, index));
        }
    }
    private void MovingWay(RectTransform reel, int index)
    {
        stopButton.SetActive(true);
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, previousDistance));
    }
    private void MovingSlowDown(RectTransform reel, int index, float previousDistance)
    {
        if (!playButton.activeSelf)
        {
            stopButton.SetActive(false);
        }
        slowDownIsActive[index] = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance + (distanceStop  * symbolHeight * symbolsCount)), timeStop)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel, index));
    }
    private void SetSymbolDefaultPosition(RectTransform reel, int index)
    {
        var finalReelPosition = reel.position.y;
        var lastSpinDistance = -(finalReelPosition - startReelPosition[index]);
        fullReelDistance[index] += lastSpinDistance;
        MovingSymbols[index].ResetSymbolReelsCounter();
        if (!playButton.activeSelf)
        {
            playButton.SetActive(true);
            stopButton.SetActive(false);
        }
        if (index == allReels.Length-1)
        {
            FinalResult.SetNextFinalScreen();
        }
    }

    public void MovingStop()
    {
        stopButton.SetActive(false);
        for (int i = 0; i < allReels.Length; i++)
        {
            var reel = allReels[i];
            var index = i;
            DOTween.Kill(reel);
            var distBeforeStopPressed = -(reel.position.y - startReelPosition[index]);
            var correctedSymbolsDist = CalculateCorrectSymbolsDist(distBeforeStopPressed, index);
            reel.DOAnchorPosY(-(fullReelDistance[index] + correctedSymbolsDist), 0.1f)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, correctedSymbolsDist));
        } 
    }

    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed, int index)
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / symbolHeight) * symbolHeight;
        return correctedSymbolsDist;
    }
    public bool[] SlowDownIsActive { get => slowDownIsActive; }
}
