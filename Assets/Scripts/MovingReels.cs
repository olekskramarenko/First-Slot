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
    private bool slowDownIsActive;
    private float fullDistance;
    private float lastSpinDistance;
    private float startReelPosition, finalReelPosition;
    private float[] distBeforeStopPressed;

    private void Start()
    {
        stopButton.SetActive(false);
        distBeforeStopPressed = new float[allReels.Length];
    }
public void MovingStart()
    {
        slowDownIsActive = false;
        playButton.SetActive(false);
        stopButton.SetActive(true);
        startReelPosition = allReels[0].position.y;
        print("##### startRellPos=" + startReelPosition);
        for (int i = 0; i < allReels.Length; i++)
        {
            var reel = allReels[i];
            var index = i; // ƒл€ определени€ последнего оборота рилов, перед SetNextFinalScreen() Tweener tweener = 
            reel.DOAnchorPosY(-(fullDistance+(distanceStart * symbolHeight * symbolsCount)), timeStart)
                .SetDelay(i * delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel, index));
        }
    }
    public void MovingWay(RectTransform reel, int index)
    {
        print("##### MovingWay");
        float previousDistance = (distanceWay + distanceStart) * symbolHeight * symbolsCount;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullDistance + previousDistance), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, previousDistance));
    }
    public void MovingSlowDown(RectTransform reel, int index, float previousDistance)
    {
        print("### MovingSlowDown");
        slowDownIsActive = true;
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-(fullDistance + previousDistance + (distanceStop  * symbolHeight * symbolsCount)), timeStop)
            .SetEase(easeStop)
            .OnComplete(() => SetSymbolDefaultPosition(reel, index));
    }
    public void SetSymbolDefaultPosition(RectTransform reel, int index)
    {
        print("### SetSymbolDefaultPosition");
        if (!playButton.activeSelf)
        {
            playButton.SetActive(true);
            stopButton.SetActive(false);
        }
        if (index == allReels.Length-1)
        {
            FinalResult.SetNextFinalScreen();
            finalReelPosition = reel.position.y;
            lastSpinDistance = -(finalReelPosition - startReelPosition);
            fullDistance += lastSpinDistance;
            print("##### finalRellPos=" + finalReelPosition);
            print("##### lastSpinDistance=" + lastSpinDistance);
            print("##### fullDistance=" + fullDistance);
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
            distBeforeStopPressed[i] = -(reel.position.y - startReelPosition);
            print("### distBeforeStopPressed[i]=" + distBeforeStopPressed[i]);
            print("### MovingStop() alldist=" + (-(fullDistance + distBeforeStopPressed[i] + 400)));
            reel.DOAnchorPosY(-(fullDistance + distBeforeStopPressed[i] + 400), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel, index, distBeforeStopPressed[i]));
        } 
    }

    public bool SlowDownIsActive
    {
        get
        {
            return slowDownIsActive;
        }
    }


}
