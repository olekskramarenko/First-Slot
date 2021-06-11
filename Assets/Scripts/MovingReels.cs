using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MovingReels : MonoBehaviour
{
    [SerializeField] private Ease easeStart, easeWay, easeStop;
    [SerializeField] private RectTransform[] allReels;
    [SerializeField] private float delay;
    [Range(0, 10)] [SerializeField] private float timeStart, timeWay, timeStop;
    [Range(0, 20)] [SerializeField] private float distanceStart, distanceWay, distanceStop;
    [SerializeField] private GameObject playButton, stopButton;

    private void Start()
    {
        stopButton.SetActive(false);
    }
    public void MovingStart()
    {
        playButton.SetActive(false);
        stopButton.SetActive(true);
        foreach (RectTransform reel in allReels)
        {
            Tweener tweener = reel.DOAnchorPosY(-(distanceStart * 800), timeStart)
                .SetDelay(delay)
                .SetEase(easeStart)
                .OnComplete(() => MovingWay(reel));
        }
    }
    public void MovingWay(RectTransform reel)
    {
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-((distanceWay + distanceStart) * 800), timeWay)
            .SetEase(easeWay)
            .OnComplete(() => MovingSlowDown(reel));
    }
    public void MovingSlowDown (RectTransform reel)
    {
        DOTween.Kill(reel);
        reel.DOAnchorPosY(-((distanceStop + distanceWay + distanceStart) * 800), timeStop)
            .SetEase(easeStop);
    }
    public void MovingStop()
    {
        playButton.SetActive(true);
        stopButton.SetActive(false);
        foreach (RectTransform reel in allReels)
        {
            DOTween.Kill(reel);
            MovingSlowDown(reel); 
        }
    }

}
