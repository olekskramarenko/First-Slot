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
    private bool[] slowDownIsActive;
    private float[] startReelPosition, fullReelDistance;

    private void Start()
    {
        stopButton.SetActive(false);
        //distBeforeStopPressed = new float[allReels.Length];
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
    public void MovingWay(RectTransform reel, int index)
    {
        stopButton.SetActive(true);
        //print("##### MovingWay");
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, previousDistance));
    }
    public void MovingSlowDown(RectTransform reel, int index, float previousDistance)
    {
        //print("### previousDistance=" + "reel №" + index + " = " + previousDistance);
        if (!playButton.activeSelf)
        {
            stopButton.SetActive(false);
        }
        print("### MovingSlowDown");
        slowDownIsActive[index] = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullReelDistance[index] + previousDistance + (distanceStop  * symbolHeight * symbolsCount)), timeStop)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel, index));
    }
    public void SetSymbolDefaultPosition(RectTransform reel, int index)
    {
        //print("### SetSymbolDefaultPosition");
        var finalReelPosition = reel.position.y;
        var lastSpinDistance = -(finalReelPosition - startReelPosition[index]);
        fullReelDistance[index] += lastSpinDistance;
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
            //print("### correctSymbolsDist=" + "reel №" + index + " = " + correctedSymbolsDist);
            //print("### distBeforeStopPressed[index]=" + "reel №" + index + " = " + distBeforeStopPressed);
            //print("### MovingStop() alldist=" + (-(fullReelDistance[index] + correctedSymbolsDist)));
            reel.DOAnchorPosY(-(fullReelDistance[index] + correctedSymbolsDist), 1)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, correctedSymbolsDist));
        } 
    }

    private float CalculateCorrectSymbolsDist(float distBeforeStopPressed, int index)
    {
        float correctedSymbolsDist;
        correctedSymbolsDist = Mathf.Ceil(distBeforeStopPressed / 200) * 200;
        return correctedSymbolsDist;
    }
    public bool[] SlowDownIsActive { get => slowDownIsActive; }
}
