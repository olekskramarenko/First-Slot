using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnticipationController : MonoBehaviour
{
    [SerializeField] private Image lightBox, leftLight, rightLight;
    [SerializeField] private RectTransform antisipationRT, lightBoxRT, leftLightRT, rightLightRT;
    [SerializeField] private float fadeTime, fadeIntensity;
    [SerializeField] private int loops;
    public void PlayLongSpinAnimation()
    {
        antisipationRT.DOScale(1, 1);
        //lightBox.DOFade(fadeIntensity, fadeTime).SetLoops(loops, LoopType.Yoyo);
    }

    public void StopLonSpinAnimation()
    {
        antisipationRT.DOScale(0, 0);
    }

}
