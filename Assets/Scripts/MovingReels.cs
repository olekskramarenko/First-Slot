using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MovingReels : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Ease ease;
    [Range(0,10)][SerializeField] private float animationTime;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Tweener tweener = rectTransform.DOAnchorPosY(-3200, animationTime).
            SetEase(ease);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (RectTransform child in rectTransform)
        {
            if (child.position.y <= 223) {
                child.position += Vector3.up * 800.0f;
                child.GetComponent<Image>().sprite = Resources.Load<Sprite>("symbols/icon_7");
            };
        }
    }
}
