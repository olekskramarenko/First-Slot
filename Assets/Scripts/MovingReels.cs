using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingReels : MonoBehaviour
{
    [SerializeField] private GameObject symbol0;
    [SerializeField] private GameObject symbol3;
    private RectTransform rectTransform;
    [SerializeField] private Ease ease;
    [Range(0,10)][SerializeField] private float animationTime;
    [SerializeField] private int animationLoops;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Tweener tweener = rectTransform.DOAnchorPosY(-2400, animationTime).
            SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.position.y <= 223) {
                child.position += Vector3.up * 800.0f; 
            };
        }
    }
}
