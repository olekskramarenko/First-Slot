using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MovingReels : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private RectTransform[] allReels;
    [Range(0,10)][SerializeField] private float animationTime;
    // Start is called before the first frame update
    public void MovingStart()
    {
        foreach (RectTransform reel in allReels)
        {
            Tweener tweener = reel.DOAnchorPosY(-3200, animationTime).SetEase(ease);       
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
