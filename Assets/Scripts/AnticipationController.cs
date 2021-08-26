using UnityEngine;
using DG.Tweening;

public class AnticipationController : MonoBehaviour
{
    [SerializeField] private RectTransform antisipationRT;
    private float animationTime = 0.2f;
    public void PlayLongSpinAnimation()
    {
        antisipationRT.DOScale(1, animationTime);
    }

    public void StopLonSpinAnimation()
    {
        antisipationRT.DOScale(0, animationTime);
    }

}
